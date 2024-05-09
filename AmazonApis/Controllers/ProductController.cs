using AmazonApis.Dtos;
using AmazonApis.Errors;
using AmazonApis.Helper;
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
    public class ProductController : ApiController
    {
        private readonly IGenericRepository<Product, int> _repository;
        private readonly IGenericRepository<ProductBrand, int> _brandRepo;
        private readonly IGenericRepository<ProductType, int> _typesRepo;
        public IMapper _Mapper { get; }

        public ProductController(IGenericRepository<Product, int> Repository, IGenericRepository<ProductBrand, int> BrandRepo, IGenericRepository<ProductType, int> TypeRepo, IMapper Mapper)
        {
            _repository = Repository;
            _brandRepo = BrandRepo;
            _Mapper = Mapper;
            _typesRepo = TypeRepo;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Pagination<ProductDto>>>> GetAllProducts([FromQuery] ProductSpecParam param)
        {
            var spec = new productWithBrandAndTypeSpecification(param);
            var product = await _repository.GetAllWithSpec(spec);
            var mappedProduct = _Mapper.Map<IEnumerable<ProductDto>>(product);
            var CountSpec = new CountWithBrandAndType(param);
            var Count = await _repository.GetCountWithSpec(CountSpec);
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
            var product = await _repository.GetByIdWithSpec(spec);
            if (product is null) return NotFound(new ApiResponse(404));
            return Ok(_Mapper.Map<ProductDto>(product));
        }
        [HttpGet("GetAllBrands")]
        public async Task<ActionResult<Pagination<ProductBrand>>> GetAllBrands()
        {
            var Brands = await _brandRepo.GetAll();
            return Ok(Brands);

        }

        [HttpGet("GetAllTypes")]
        public async Task<ActionResult<ProductBrand>> GetAllTypes()
        {
            var Types = await _typesRepo.GetAll();

            return Ok(Types);

        }

    }
}
