using Carfox.Core.Entities;
using Carfox_Core.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfox_Core.Entites
{
    public class Payment : BaseEntity
    {
        public string PaymentIntentId { get; set; }

        public string AppUserId { get; set; }

        public string ClientSecret { get; set; }

        public decimal Budget { get; set; }

        public AppUser AppUser { get; set; } // Nav Prop

        public PlanStatus PlanStatus { get; set; } = PlanStatus.Pending;
    }
}
