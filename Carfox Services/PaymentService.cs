using Carfox.Core.Repositories;
using Carfox_Core.Entites;
using Carfox_Core.Entites.Identity;
using Carfox_Core.Specification;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Services;

namespace Talabat.Services
{
    public class PaymentService : IPaymentServices
    {
        private readonly IConfiguration _configuration;
        private readonly IGenericRepository<Payment> _genericRepository;

        public PaymentService(IConfiguration configuration , IGenericRepository<Payment> genericRepository )
        {
            _configuration     = configuration;
            _genericRepository = genericRepository;
        }
        public async Task CreatePaymentIntent(decimal Budget, AppUser user )
        {
            StripeConfiguration.ApiKey = _configuration["StripeSetting:SecretKey"];

            Payment payment = new Payment()
            {
                Budget = Budget,
                AppUserId = user.Id
            };

            // Payment Intent
            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(payment.PaymentIntentId)) // Create Payment
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)Budget*100,
                    Currency = "usd", // Dolar Currency
                    PaymentMethodTypes = new List<string>() { "card"}
                };

                paymentIntent = await service.CreateAsync(options); // => Create Payment Service

                payment.PaymentIntentId = paymentIntent.Id;

                payment.ClientSecret = paymentIntent.ClientSecret;
            }
        }

        public async Task<Payment> UpdatePaymentIntentToSucceededOrFailed(string paymentIntentId, bool isSucceeded)
        {
            var spec = new PaymentSpec(paymentIntentId);

            var payment = await _genericRepository.GetByIdWithSpecAsync(spec);

            if (isSucceeded)
                payment.PlanStatus = PlanStatus.PaymentRecived;
            else
                payment.PlanStatus = PlanStatus.PaymentFailed;

            _genericRepository.UpdateProduct(payment);

            await _genericRepository.UpdateDatabase();

            return payment;
        }
    }
}
