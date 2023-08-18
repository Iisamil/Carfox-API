using Carfox.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfox_Core.Entites
{
    public class Image : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } // Nav prop
        public string Picture { get; set; }
    }
}
