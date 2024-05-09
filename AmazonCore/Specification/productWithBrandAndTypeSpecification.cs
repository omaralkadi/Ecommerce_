using Talabat_Core.Entities;

namespace AmazonCore.Specification
{
    public class productWithBrandAndTypeSpecification : BaseSpecification<Product, int>
    {
        public productWithBrandAndTypeSpecification(ProductSpecParam param) : base
            (
              p => (!param.BrandId.HasValue || p.ProductBrandId == param.BrandId) &&
                 (!param.TypeId.HasValue || p.ProductTypeId == param.TypeId) &&
                 (string.IsNullOrWhiteSpace(param.Search) || p.Name.ToLower().Contains(param.Search))
            )
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);

            if (param.Sort.HasValue)
            {
                switch (param.Sort)
                {
                    case SortingTypes.PriceAsc:
                        OrderBy = p => p.Price;
                        break;
                    case SortingTypes.PriceDesc:
                        OrderByDesc = p => p.Price;
                        break;
                    case SortingTypes.NameDesc:
                        OrderByDesc = p => p.Name;
                        break;
                }
            }
            else
            {
                OrderBy = p => p.Name;
            }
            ApplyPagination((param.PageIndex-1)*param.PageSize,param.PageSize);
        }
        public productWithBrandAndTypeSpecification(int id) : base(p => p.Id == id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }
       
    }
}