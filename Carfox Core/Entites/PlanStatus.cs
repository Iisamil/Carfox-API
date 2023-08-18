using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Carfox_Core.Entites
{
       public enum PlanStatus
       {
            [EnumMember(Value = "Pending")]
            Pending,
            [EnumMember(Value = "PaymentRecived")]
            PaymentRecived,
            [EnumMember(Value = "PaymentFailed")]
            PaymentFailed
       }
}

