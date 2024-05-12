using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Entities;

namespace AmazonCore.Specification
{
    public class BaseSpecification<T, TKey> : ISpecifications<T, TKey> where T : BaseEntity<TKey>
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Skip { get ; set ; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }

        public BaseSpecification()
        {

        }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public void ApplyPagination(int skip,int take)
        {
            Skip= skip;
            Take= take;
            IsPaginationEnabled = true;
        }

        public void OrderDesc(Expression<Func<T, object>> orderBySpec)
        {
            OrderByDesc = orderBySpec;
        }
        public void Order(Expression<Func<T, object>> order)
        {
            OrderBy = order;
        }


    }
}
