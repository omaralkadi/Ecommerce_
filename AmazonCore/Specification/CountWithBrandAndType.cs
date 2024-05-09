using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Entities;

namespace AmazonCore.Specification
{
    public class CountWithBrandAndType : BaseSpecification<Product, int>
    {
        public CountWithBrandAndType(ProductSpecParam param) : base
            (
             p => (!param.TypeId.HasValue || p.ProductTypeId == param.TypeId) &&
                (!param.BrandId.HasValue || p.ProductBrandId == param.BrandId)
            )
        {

        }
    }
}
