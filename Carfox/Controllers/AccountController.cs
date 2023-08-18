using Carfox.Api.Dtos;
using Carfox.Dtos;
using Carfox.Errors;
using Carfox.Extensions;
using Carfox_Core.Entites;
using Carfox_Core.Entites.Identity;
using Carfox_Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Carfox.Controllers
{
    public class AccountController : BaseAPIsController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController
            (UserManager<AppUser> userManager,
             SignInManager<AppUser> signInManager,
             ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]    // .../api/account/login
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            //if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return Ok(new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user, _userManager)
            });
        }

        [HttpPost("register")] // .../api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            //if (CheckEmailExist(registerDto.Email).Result.Value)
            //    return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "Email Already In Use !" } });
            var user = new AppUser()
            {
                FName = registerDto.FirstName,
                LName = registerDto.LastName,
                Email = registerDto.Email,                // hossammostafa@gmail.com
                PasswordHash = registerDto.Password,
                Gender = registerDto.Gender,
                UserName = registerDto.Email.Split("@")[0]  // hossammostafa
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return Ok(new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user, _userManager)
            });
        }

        [Authorize]
        [HttpPut]  // .../api/account/
        public async Task<ActionResult<UpdateUserDataDtos>> UpdateUserData(UpdateUserDataDtos updateUser)
        {

            var user = await _userManager.FindUserAddressByEmailAsync(User);

            // Manual Mapping
            #region Mapping
            user.FName = updateUser.FName;
            user.LName = updateUser.LName;
            user.Image = updateUser.Image;
            user.City = updateUser.City;
            user.Country = updateUser.Country;
            user.PhoneNumber = updateUser.PhoneNumber;

            #endregion

            // Update User Data in Database
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return Ok(updateUser);
        }

        [Authorize]
        [HttpGet("getuserdata")] // .../api/account/
        public async Task<ActionResult<UpdateUserDataDtos>> GetUserData()
        {

            var user = await _userManager.FindUserAddressByEmailAsync(User);

            var getUser = new UpdateUserDataDtos()
            {
                City = user.City,
                Country = user.Country,
                PhoneNumber = user.PhoneNumber,
                FName = user.FName,
                LName = user.LName,
                Image = user.Image,
            };

            return Ok(getUser);
        }

        //[Authorize]
        //[HttpGet("signout")]
        //public async Task<IActionResult> LogOut()
        //{
        //    var user = _userManager.FindUserAddressByEmailAsync(User);

        //    var logout = _signInManager.SignOutAsync(user);
        //    return Ok(user);
            
        //}
    }
}
