using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfox.Core.Entities
{
    public class ProductBrand : BaseEntity
    {
        public string name { get; set; }

        public string slug { get; set; }
        public string logo { get; set; }


    }
}
