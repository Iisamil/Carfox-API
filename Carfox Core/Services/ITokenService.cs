using Carfox_Core.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfox_Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user , UserManager<AppUser> userManager);
    }
}
