using AutoMapper;
using Carfox.Api.Dtos;
using Carfox.Core.Entities;
using Carfox.Core.Repositories;
using Carfox.Core.Specifications;
using Carfox.Errors;
using Carfox.Extensions;
using Carfox_Core.Entites;
using Carfox_Core.Entites.Identity;
using Carfox_Core.Repositories;
using Carfox_Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Carfox.Controllers
{
    public class TopCarController : BaseAPIsController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICarTopRepo _carTopRepo;
        private readonly IGenericRepository<Product> _genericRepository;
        private readonly IMapper _mapper;

        public TopCarController(UserManager<AppUser> userManager ,
            ICarTopRepo carTopRepo,
            IGenericRepository<Product> genericRepository,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _carTopRepo = carTopRepo;
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTopCar(int id)
        {
            var product = await _genericRepository.GetByIdAsync( id );

            if ( product == null ) return BadRequest(new ApiResponse(400,"Cannot Find Car With This ID"));
            var IsTop = await _carTopRepo.GetTopCarById($"{id} +redis");
            if ( IsTop != null ) return Ok();
            var user =await _userManager.FindUserAddressByEmailAsync(User);
            if (user.TopCarThatUserCanAdd > 0)
            {
                var TopCarId = $"{id} +redis";
                var Topcar = new TopCars()
                {
                    TopCarId = TopCarId,
                    ProductId = id,
                    UserId = user.Id

                };

                await _carTopRepo.AddTopCar(Topcar, 20);
                user.TopCarThatUserCanAdd--;
                return Ok("Top Car Added Successfully");
            }
            else return Unauthorized( new ApiResponse(401, "You Cant Add Top Car, Get New Plan First"));
            
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetAllTopCars()
        {

            var topCar= await _carTopRepo.GetAllTopCars();
            var TopProduct = new List<Product>();
            foreach (var item in topCar)
            {
                var Spec = new ProductWithBrandAndTypeSpecifications(item.ProductId);

                TopProduct.Add(await _genericRepository.GetByIdWithSpecAsync(Spec));
            }

            var mappedProduct = _mapper.Map<IEnumerable<Product>,IEnumerable<ProductDto>>(TopProduct);


            return mappedProduct;
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteTopCar(int id)
        {
            await _carTopRepo.RemoveTopCar($"{id} +redis");
            return Ok("Top Car Removed Successfully !");
        }
    }
}
