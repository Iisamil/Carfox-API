using Carfox.Controllers;
using Carfox.Errors;
using Carfox.Extensions;
using Carfox_Core.Entites;
using Carfox_Core.Entites.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Numerics;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{
    public class PaymentsController : BaseAPIsController
    {
        private readonly IPaymentServices _paymentServices;
        private readonly ILogger<PaymentsController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private const string whSecret = "whsec_0413890e0fb1009ec413771d7abf58fc3c09f8107a78f48d6359f612d8ee1d8b";


        public PaymentsController(IPaymentServices paymentServices , ILogger<PaymentsController> logger, UserManager<AppUser> userManager)
        {
            _paymentServices = paymentServices;
            _logger = logger;
            _userManager = userManager;
        }

        // Eslam Call This End Point When User Choose His Own Plan
        [HttpPost("paymentintent")] // .../api/payments/paymentintent 
        public async Task<ActionResult> CreateOrUpdatePaymentIntent(PlanType plan)
        {

            var user = await _userManager.FindUserAddressByEmailAsync(User);
            user.Plan = plan;

            if (user.CarThatUserCanAdd == 0 && user.CarThatUserAdded == 0)
            {

                switch (plan)
                {
                    case PlanType.Free:
                    default:
                        user.CarThatUserCanAdd = 1;
                        break;
                    case PlanType.Premium:

                        await _paymentServices.CreatePaymentIntent(200m, user);
                       

                        break;
                    case PlanType.Pro:
                        await _paymentServices.CreatePaymentIntent(400m, user);
                        
                        break;
                }
            }
            else if (user.CarThatUserCanAdd == 0 && user.CarThatUserAdded > 0)
            {

                switch (plan)
                {
                    case PlanType.Premium:
                        await _paymentServices.CreatePaymentIntent(200m, user);


                        break;
                    case PlanType.Pro:
                         await _paymentServices.CreatePaymentIntent(400m, user);

                        break;

                    default:
                        return BadRequest(new ApiResponse(400, "you cant add new car without new plan"));
                       
                }
            }

           

            return Ok();
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var user = await _userManager.FindUserAddressByEmailAsync(User);

            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json,
            Request.Headers["Stripe-Signature"], whSecret);

            var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
            Payment payment;

            switch (stripeEvent.Type)
            {
                case Events.PaymentIntentSucceeded:
                    payment = await _paymentServices.UpdatePaymentIntentToSucceededOrFailed(paymentIntent.Id, true);
                    _logger.LogInformation(" Payment Succeeded , Thank You " , paymentIntent.Id);
                    switch (user.Plan)
                    {
                        case PlanType.Premium:
                            user.CarThatUserCanAdd = 5;
                            user.TopCarThatUserCanAdd = 2;

                            break;
                        case PlanType.Pro:
                            user.CarThatUserCanAdd = 30;
                            user.TopCarThatUserCanAdd = 10;
                            break;

                        default:
                            return BadRequest(new ApiResponse(400, "you cant add new car without new plan"));

                    }
                    break;

                case Events.PaymentIntentPaymentFailed:
                    payment = await _paymentServices.UpdatePaymentIntentToSucceededOrFailed(paymentIntent.Id , false);
                    _logger.LogInformation(" Payment Failed ! Try Again" , paymentIntent.Id);
                    break;
            }
            return Ok();
        }
    }
}
