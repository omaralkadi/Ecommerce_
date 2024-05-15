using AmazonApis.Dtos;
using AmazonApis.Errors;
using AmazonCore.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace AmazonApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IConfiguration _configuration;
        const string endpointSecret = "whsec_a7dbc540ff987361f7206ab24d73d89fcf962ac7d41da5920898a81a99df4eda";
        public IMapper _Mapper { get; }

        public PaymentsController(IPaymentService paymentService, IMapper mapper,IConfiguration configuration)
        {
            _paymentService = paymentService;
            _Mapper = mapper;
            _configuration = configuration;
        }

        [ProducesResponseType(typeof(CustomerBasketDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status400BadRequest)]
        [HttpPost("{BasketId}")]
        [Authorize]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string BasketId)
        {
            var customerBasket = await _paymentService.CreateOrUpdatePaymentIntent(BasketId);
            if (customerBasket == null) return BadRequest(new ApiResponse(400, "There is a problem with your basket"));
            var MappedBasket = _Mapper.Map<CustomerBasketDto>(customerBasket);
            return Ok(MappedBasket);

        }

        [HttpPost("webhook")]
        public async Task<IActionResult> StripWebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);

                // Handle the event
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    await _paymentService.UpdatePaymentIntentSucceedOrFaild(paymentIntent.Id,false);
                }
                else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    await _paymentService.UpdatePaymentIntentSucceedOrFaild(paymentIntent.Id, true);

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
