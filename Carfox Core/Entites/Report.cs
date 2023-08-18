using Carfox.Core.Entities;
using Carfox_Core.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfox_Core.Entites
{
    public class Report : BaseEntity
    {
        public string MessageDescription { get; set; }
        public string UserEmail { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
