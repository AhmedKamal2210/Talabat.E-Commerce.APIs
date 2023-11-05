using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Talabat.Core.Entities.OrderEntities;
using Talabat.E_Commerce1.HandleRespones;
using Talabat.Sevices.Dtos.BasketDto;
using Talabat.Sevices.Dtos.OrderDto;
using Talabat.Sevices.IServices.IPaymentServices;

namespace Talabat.E_Commerce1.Controllers
{

    public class PaymentController : BaseApiController
    {
        private readonly IPaymentServices _paymentServices;
        private readonly ILogger _logger;
        private const string WhSecret = "whsec_87853b2426476d5eede7864ca0532331e11f6d3a1bb9dc6b28a31851d718039d";

        public PaymentController(IPaymentServices paymentServices , ILogger logger)
        {
            _paymentServices = paymentServices;
            _logger = logger;
        }

        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentServices.CreateOrUpdatePaymentIntent(basketId);

            if (basket == null)
                return BadRequest(new ApiResponse(400, "Problem with your Basket !!"));

            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], WhSecret);

                PaymentIntent paymentIntent;

                OrderResultDto orders;

                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    _logger.LogInformation("Payment Failed : ", paymentIntent.Id);
                    orders = await _paymentServices.UpdateOrderPaymentSucceded(paymentIntent.Id);
                    _logger.LogInformation("Order Updated To Paymnet Failed: : ", orders.Id);

                }
                else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    _logger.LogInformation("Payment Succeed : ", paymentIntent.Id);
                    orders = await _paymentServices.UpdateOrderPaymentFailed(paymentIntent.Id);
                    _logger.LogInformation("Order Updated To Paymnet Succeed: : ", orders.Id);
                }
                // ... handle other event types
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }
    }
}
