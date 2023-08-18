using Carfox.Core.Entities;
using Carfox_Core.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfox_Core.Entites
{
    public class Favorite : BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; } // Nav Prop

        public int ProductId { get; set; }
        public Product Product { get; set; } // Nav Prop


    }
}
