using AmazonCore.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Entities;

namespace AmazonRepository.Products
{
    public class productWithBrandAndTypeSpecification<T, TKey> : BaseSpecification<T, TKey> where T : BaseEntity<TKey>
    {
        public productWithBrandAndTypeSpecification()
        {

        }
        public productWithBrandAndTypeSpecification(Expression<Func<T, bool>> criteria) : base(criteria)
        {

        }
    }
}
