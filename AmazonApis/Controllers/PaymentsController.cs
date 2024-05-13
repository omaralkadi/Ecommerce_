using AmazonApis.Dtos;
using AmazonApis.Errors;
using AmazonCore.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazonApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public IMapper _Mapper { get; }

        public PaymentsController(IPaymentService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _Mapper = mapper;
        }

        [ProducesResponseType(typeof(CustomerBasketDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string BasketId)
        {
            var customerBasket = await _paymentService.CreateOrUpdatePaymentIntent(BasketId);
            if (customerBasket == null) return BadRequest(new ApiResponse(400, "There is a problem with your basket"));
            var MappedBasket = _Mapper.Map<CustomerBasketDto>(customerBasket);
            return Ok(MappedBasket);

        }
    }
}
