using Carfox_Core.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Carfox.Extensions
{
    public static class UpdateDataForUser
    {
        public static async Task<AppUser> FindUserAddressByEmailAsync(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var Email = user.FindFirstValue(ClaimTypes.Email);

            var User = await userManager.Users.FirstOrDefaultAsync(U => U.Email == Email);

            return User;
        }
    } }
