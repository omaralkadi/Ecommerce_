using AmazonApis.Dtos;
using AmazonApis.Errors;
using AmazonCore.Entities.Order;
using AmazonCore.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AmazonApis.Controllers
{
    public class OrdersController : ApiController
    {
        private readonly IOrderService _orderService;
        public IMapper Mapper { get; }

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            Mapper = mapper;
        }

        [HttpPost("CreateOrder")]
        [Authorize]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderdto)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var MappedAddress = Mapper.Map<Address>(orderdto.shipToAddress);
            var Result = await _orderService.CreateOrderAsync(BuyerEmail, orderdto.BasketId, orderdto.DelivetMethodId, MappedAddress);
            if (Result is null) return BadRequest(new ApiResponse(400));
            return Ok(Result);
        }

        [ProducesResponseType(typeof(IEnumerable<OrderToReturnDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [Authorize]
        [HttpGet("GetAllOrdersForSpecUser")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetOrderForSpecificUserAsync(email);
            var MappedOrders = Mapper.Map<IEnumerable<OrderToReturnDto>>(orders);
            if (MappedOrders is null) return NotFound(new ApiResponse(404, "There is not Orders Found"));
            return Ok(MappedOrders);
        }


        [ProducesResponseType(typeof(IEnumerable<OrderToReturnDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrdersById(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.CreateOrderByIdForSpecificUserAsync(email, id);
            var MappedOrders = Mapper.Map<OrderToReturnDto>(orders);
            if (MappedOrders is null) return NotFound(new ApiResponse(404, "There is not Orders Found"));
            return Ok(MappedOrders);
        }



        [ProducesResponseType(typeof(IEnumerable<DeliveryMethod>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<Order>> GetAllOrderMethods()
        {
            var methods = await _orderService.GetDeliveryMethod();
            if (methods is null) return NotFound(new ApiResponse(404, "There is no OrderMethods"));
            return Ok(methods);
        }
    }
}