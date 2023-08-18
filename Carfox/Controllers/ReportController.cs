using Carfox.Core.Repositories;
using Carfox.Extensions;
using Carfox_Core.Entites;
using Carfox_Core.Entites.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Carfox.Controllers
{
    public class ReportController : BaseAPIsController
    {
        private readonly IGenericRepository<Report> _genericRepository;
        private readonly UserManager<AppUser> _userManager;

        public ReportController(IGenericRepository<Report> genericRepository , UserManager<AppUser> userManager)
        {
            _genericRepository = genericRepository;
            _userManager = userManager;
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> AddReport(string message)
        {
            var user = await _userManager.FindUserAddressByEmailAsync(User);

            var report = new Report()
            {
                MessageDescription = message,
                UserEmail = user.Email,
                AppUserId = user.Id
            };

            await _genericRepository.AddProductAsync(report);

            await _genericRepository.UpdateDatabase();

            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllReports()
        {

            var report =  await _genericRepository.GetAllAsync();

            return Ok(report);
        }

    }
}
