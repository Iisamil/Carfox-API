using Carfox.Core.Specifications;
using Carfox_Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfox_Core.Specification
{
    public class PaymentSpec : BaseSpecifications<Payment>
    {
        public PaymentSpec(string paymentIntentId):base(P => P.PaymentIntentId == paymentIntentId)
        {
            
        }
    }
}
