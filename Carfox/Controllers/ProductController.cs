using AutoMapper;
using Carfox.Api.Dtos;
using Carfox.Controllers;
using Carfox.Core.Entities;
using Carfox.Core.Repositories;
using Carfox.Core.Specifications;
using Carfox.Dtos;
using Carfox.Errors;
using Carfox.Extensions;
using Carfox.Repository;
using Carfox_Core.Entites;
using Carfox_Core.Entites.Identity;
using Carfox_Core.Specification;
using Carfox_Repository.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Talabat.Core.Specifications;

namespace Carfox.Api.Controllers
{
    public class ProductController : BaseAPIsController
    {
        private readonly AppIdentityDbContext _dbContext;
        private readonly IGenericRepository<Core.Entities.Product> _genericRepository;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IGenericRepository<ProductType> _typeRepo;
        private readonly IGenericRepository<Image> _imgRepo;

        public ProductController(AppIdentityDbContext dbContext , 
                                IGenericRepository<Core.Entities.Product> genericRepository,
                                IMapper mapper,
                                IGenericRepository<ProductBrand> brandRepo,
                                UserManager<AppUser> userManager ,
                                IGenericRepository<ProductType> typeRepo,
                                IGenericRepository<Image> imgRepo
                                )
        {
            _dbContext = dbContext;
            _genericRepository = genericRepository;
            _mapper = mapper;
            _brandRepo = brandRepo;
            _userManager = userManager;
            _typeRepo = typeRepo;
            _imgRepo = imgRepo;
        }

        // Add Product [Authorized]

        [Authorize]
        [HttpPost] // POST : ../api/Product
        public async Task<ActionResult<ProductDto>> CreateProductAsync(ProductDto Pdto)
        {
            var user = await _userManager.FindUserAddressByEmailAsync(User);

            var product = _mapper.Map<ProductDto, Core.Entities.Product>(Pdto);

            product.AppUserId = user.Id;

            if (user.CarThatUserCanAdd > 0)
            {
				await _genericRepository.AddProductAsync(product);

				user.CarThatUserCanAdd--;

				user.CarThatUserAdded++;

				await _userManager.UpdateAsync(user);

				await _genericRepository.UpdateDatabase();

				return Ok(Pdto);
			}
           
            else return BadRequest(new ApiResponse(400,"Sorry U Cant Add New Car Without Change Ur Plan"));
        }

        //[Authorize]
        [HttpPost("images")]
        public async Task<IActionResult> AddImagesForProduct(List<IFormFile> files, int productID)
        {
            if (files == null && files.Count==0) return null;

            for (int i = 0; i < files.Count(); i++)
            {
                var images = UploadImages.UploadFile(files[i], "images");

                var newImage = new Image()
                {
                    Picture = images,
                    ProductId = productID,
                };

                await _imgRepo.AddProductAsync(newImage);
            }

            await _imgRepo.UpdateDatabase();

            return Ok();
        }
        // Get All Products

        [HttpGet] // GET : .../api/Product
        public async Task<ActionResult<IReadOnlyList<GetProductDto>>> GetAllAsync([FromQuery]ProductSpecParams specParams)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(specParams);

            var products = await _genericRepository.GetAllWithSpecAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Core.Entities.Product> , IReadOnlyList<GetProductDto>>(products);


            List<Image> ProductImages = new List<Image>();
            for (int i = 0; i < data.Count; i++)
            {
                var spec2 = new GetImageWithProductId(data[i].Id);

                ProductImages.AddRange(await _imgRepo.GetAllWithSpecAsync(spec2));

                if (ProductImages.Count > 0 && ProductImages is not null)
                {
                    for (int j = 0; j < ProductImages.Count; j++)
                    {
                        data[i].Images.Add(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", ProductImages[j].Picture));

                    }
                }
                ProductImages.Clear();
            }
            //foreach (var product in products)
            //{
            //    var spec2 = new GetImageWithProductId(product.Id);

            //    ProductImages.AddRange(await _imgRepo.GetAllWithSpecAsync(spec2));

            //    data

            //}


            return Ok(data);

        }

        // Get Products By Id

        [HttpGet("{id}")] // GET : .../api/Product
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProductById(int id)
        {
            var spec = new ProductWithId(id);

            var products = await _genericRepository.GetByIdWithSpecAsync(spec);

            var data = _mapper.Map<Core.Entities.Product, ProductDto>(products);

            return Ok(data);

        }


        // Get Products Brand

        [HttpGet("brands")] // GET : ../api/product/brands
        public async Task<ActionResult<IEnumerable<BrandsDto>>> GetProductBrands()
        {
            var brands = await _brandRepo.GetAllAsync();

            var mappedBrand =  _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandsDto>>(brands);


            return Ok(mappedBrand);
        }

        // Get Products Brand

        [HttpGet("types")] // GET : ../api/product/types
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var types = await _typeRepo.GetAllAsync();

            return Ok(types);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var getProduct = await _genericRepository.GetByIdAsync(id);
            _genericRepository.DeleteProduct(getProduct);
            await _genericRepository.UpdateDatabase();
            return Ok("Product Deleted !");
        }



    }
}
