using Carfox_Core.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfox.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AppUserId { get; set; } // Foreign Key
        public AppUser AppUser { get; set; } // Nav Property [One]
        public string Governorate { get; set; }
        public string Area { get; set; }
        public bool IsNew { get; set; }
        public int ProductBrandId { get; set; } // Foreign Key
        public ProductBrand ProductBrand { get; set; } // Nav Property [One]
        public string Model { get; set; }
        public int Year { get; set; }
        public int zipCode { get; set; }
        public string FuelType { get; set; }
        public bool IsAutomatic { get; set; }
        public string BodyShape { get; set; }
        public int EngineCapacity { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public string? Description { get; set; }
    }
}
