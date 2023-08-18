using Carfox.Dtos;
using Carfox_Core.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Carfox.Controllers
{
    public class AdminController : BaseAPIsController
    {
        private readonly UserManager<Admin> _userManager;

        public AdminController(UserManager<Admin> userManager)
        {
            _userManager = userManager;
        }

        //[HttpGet]
        //public async Task<ActionResult<AdminDataDto>> GetAdminData()
        //{

        //}
    }
}
