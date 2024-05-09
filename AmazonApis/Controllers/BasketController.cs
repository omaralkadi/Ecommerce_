using AmazonApis.Dtos;
using AmazonApis.Errors;
using AmazonCore.Entities;
using AmazonCore.Interfaces.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazonApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public IMapper Mapper { get; }

        public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
            _basketRepository = basketRepository;
            Mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdateBasket(CustomerBasketDto basket)
        {
            var MappedCustomerBasket = Mapper.Map<CustomerBasket>(basket);
            var CreatedBasket = await _basketRepository.UpdateOrCreateBasket(MappedCustomerBasket);
            if(CreatedBasket == null) { return BadRequest(new ApiResponse(400)); }
            return Ok(CreatedBasket);
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string BasketId)
        {
            return Ok(await _basketRepository.GetBasketAsync(BasketId));
        }

        [HttpDelete]
        public async Task<bool> DeleteBasket(string BasketId)
        {
            return await _basketRepository.DeleteBasketAsync(BasketId);
        }
    }
}
