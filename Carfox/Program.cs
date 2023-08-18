using Carfox.APIs.Helpers;
using Carfox.Core.Repositories;
using Carfox.Extensions;
using Carfox.Repository;
using Carfox_Core.Entites.Identity;
using Carfox_Core.Repositories;
using Carfox_Repository;
using Carfox_Repository.Identity;
using Carfox_Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
using Talabat.Core.Services;
using Talabat.Services;

namespace Carfox
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Allow DependancyInjection for Fav , Product Repos

            //builder.Services.AddScoped(typeof(IProductRepo), typeof(ProductRepo));
            builder.Services.AddScoped<IProductRepo, ProductRepo>();
            builder.Services.AddScoped<ICarTopRepo, CarTopRepo>();
            builder.Services.AddScoped(typeof(IFavoriteRepo), typeof(FavoriteRepo));


            // Allow DependancyInjection for SystemDbContext
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

			// Allow Dependacny Injection For Redis
			builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
			{
				var connection = builder.Configuration.GetConnectionString("RedisConnection");
				return ConnectionMultiplexer.Connect(connection);
			});
			// Allow DependancyInjection for JWT
			builder.Services.AddScoped<ITokenService, TokenService>();

            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            builder.Services.AddScoped(typeof(IPaymentServices), typeof(PaymentService));

            builder.Services.AddAutoMapper(typeof(MappingProfiles));

            builder.Services.AddSignalR();







            #endregion

            var app = builder.Build();

            #region Update Database

            // Using => to Make Dispose()
            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var loggerFactory = services.GetRequiredService<ILoggerFactory>(); // make error shown in Kestrel Screen

            try
            {

                // Update DataBase for IdentityUser
                var identityContext = services.GetRequiredService<AppIdentityDbContext>();
                await identityContext.Database.MigrateAsync();

                // Seeding
                await DataSeedContext.SeedAsync(identityContext);

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, " An Error Occured During Applying Migrations !");
            }

            #endregion


            #region Configure Middlewares
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();


            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

            #endregion  
        }
    }
}