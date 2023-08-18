using Carfox_Core.Entites;
using Carfox_Core.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Services
{
    public interface IPaymentServices
    {
        Task CreatePaymentIntent(decimal Budget , AppUser user);

        Task<Payment> UpdatePaymentIntentToSucceededOrFailed(string paymentIntentId, bool isSucceeded);
    }
}
