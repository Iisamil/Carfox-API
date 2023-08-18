using AutoMapper;
using Carfox.Api.Dtos;
using Carfox.Core.Entities;
using Carfox.Core.Repositories;
using Carfox.Core.Specifications;
using Carfox.Extensions;
using Carfox_Core.Entites;
using Carfox_Core.Entites.Identity;
using Carfox_Core.Repositories;
using Carfox_Core.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Carfox.Controllers
{
    public class FavoriteController : BaseAPIsController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductRepo _productRepo;
        private readonly IFavoriteRepo _favoriteRepo;
        private readonly IMapper _mapper;

        public FavoriteController(UserManager<AppUser> userManager ,
                                  IFavoriteRepo favoriteRepo ,
                                  IProductRepo productRepo ,
                                  IMapper mapper)
        {
            _userManager = userManager;
            _productRepo = productRepo;
            _favoriteRepo = favoriteRepo;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet] // ../api/favorite
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetNumbersOfFavorites ()
        {
            var user = await _userManager.FindUserAddressByEmailAsync(User);

            var spec = new FavoritesProduct(user.Id);

            var favorites = await _favoriteRepo.GetAllByIdWithSpecAsync(spec);

            var FavProductList=new List<Product>();

            foreach (var fav in favorites)
            {
                var Spec = new ProductWithBrandAndTypeSpecifications(fav.ProductId);
                var products = _productRepo.GetByIdWithSpecAsync(Spec);
                FavProductList.Add(await products);
            }
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(FavProductList);

            return Ok(data);
        }

        [Authorize]
        [HttpPost("{id}")] // ../api/favorite/3
        public async Task AddToFavorite(int id)
        {
            var user = await _userManager.FindUserAddressByEmailAsync(User);

            var favorite = new Favorite()
            {
                Id = 1,
                AppUserId = user.Id,
                ProductId = id
            };

            await _favoriteRepo.AddProductAsync(favorite);

            await _favoriteRepo.UpdateDatabase();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task DeleteFromFavorite(int id)
        {
            var user = await _userManager.FindUserAddressByEmailAsync(User);

            var spec = new FavoritesProduct(user.Id);

            var favorites = await _favoriteRepo.GetAllByIdWithSpecAsync(spec);

            Favorite target = null;

            for (int i = 0; i < favorites.Count(); i++)
            {
                if (favorites[i].ProductId == id)
                     target = favorites[i];
            } 
            if (target != null)

             _favoriteRepo.DeleteProduct(target);

            await _favoriteRepo.UpdateDatabase();

        }

    }
}
