using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfox_Core.Entites.Identity
{
    public enum PlanType
    {
        Free = 0 , Premium = 1 , Pro = 2
    }
    public class AppUser : IdentityUser
    {
        public string Image { get; set; } = string.Empty;
        public string FName { get; set; }
        public string LName { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public bool? IsCenter { get; set; } = true;
        public bool Gender { get; set; }
        public PlanType Plan { get; set; }
        public int CarThatUserCanAdd { get; set; } = 1;
        public int TopCarThatUserCanAdd { get; set; } = 1;
        public int CarThatUserAdded { get; set; } = 0;

    }
}
