using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carfox.Core.Entities;
using Talabat.Core.Specifications;

namespace Carfox.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product>
    {
        // This CTOR Used For Get All Products

        public ProductWithBrandAndTypeSpecifications(ProductSpecParams specParams)
            :base(P =>
                                (string.IsNullOrEmpty(specParams.Search) || P.ProductBrand.name.ToLower().Contains(specParams.Search)) &&
                                (!specParams.BrandId.HasValue || P.ProductBrandId == specParams.BrandId.Value)
                 )
        {
            Includes.Add(P => P.ProductBrand);

            // Sorting
            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "priceAsc": AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc": AddOrderByDecsending(P => P.Price); 
                        break;

                    default:
                        AddOrderBy(P => P.Price);
                        break;
                }
            }
        }

        // This CTOR Used For Get Specific Products With Id

        public ProductWithBrandAndTypeSpecifications(int id):base(P => P.Id == id)
        {
            Includes.Add(P => P.ProductBrand);
        }

    }
}
