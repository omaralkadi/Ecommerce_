using AmazonApis.Dtos;
using AmazonApis.Errors;
using AmazonApis.Helper;
using AmazonCore;
using AmazonCore.Interfaces.Repository;
using AmazonCore.Specification;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat_Core.Entities;

namespace AmazonApis.Controllers
{
    public class ProductsController : ApiController
    {

        private readonly IUnitOfWork _unitOfWork;
        public IMapper _Mapper { get; }

        public ProductsController(IUnitOfWork unitOfWork, IMapper Mapper)
        {
            _unitOfWork = unitOfWork;
            _Mapper = Mapper;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Pagination<ProductDto>>>> GetAllProducts([FromQuery] ProductSpecParam param)
        {
            var spec = new productWithBrandAndTypeSpecification(param);
            var product = await _unitOfWork.Repository<Product,int>().GetAllWithSpec(spec);
            var mappedProduct = _Mapper.Map<IEnumerable<ProductDto>>(product);
            var CountSpec = new CountWithBrandAndType(param);
            var Count = await _unitOfWork.Repository<Product,int>().GetCountWithSpec(CountSpec);
            var PaginatedData = new Pagination<ProductDto>()
            {
                Index = param.PageIndex,
                PageSize = param.PageSize,
                Count = Count,
                Data = mappedProduct
            };

            return Ok(PaginatedData);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var spec = new productWithBrandAndTypeSpecification(id);
            var product = await _unitOfWork.Repository<Product,int>().GetByIdWithSpec(spec);
            if (product is null) return NotFound(new ApiResponse(404));
            return Ok(_Mapper.Map<ProductDto>(product));
        }
        [HttpGet("brands")]
        public async Task<ActionResult<Pagination<ProductBrand>>> GetAllBrands()
        {
            var Brands = await _unitOfWork.Repository<ProductBrand,int>().GetAll();
            return Ok(Brands);

        }

        [HttpGet("types")]
        public async Task<ActionResult<ProductBrand>> GetAllTypes()
        {
            var Types = await _unitOfWork.Repository<ProductBrand,int>().GetAll();

            return Ok(Types);

        }

    }
}
