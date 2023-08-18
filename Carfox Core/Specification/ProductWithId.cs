using Carfox.Core.Entities;
using Carfox.Core.Specifications;
using Carfox_Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfox_Core.Specification
{
    public class ProductWithId : BaseSpecifications<Product>
    {
        public ProductWithId(int id) : base(i => i.Id == id)
        { 
        
        }

    }
  public class GetImageWithProductId : BaseSpecifications<Image>
    {
        public GetImageWithProductId(int id) : base(i => i.ProductId == id)
        { 
        
        }

    }
}
