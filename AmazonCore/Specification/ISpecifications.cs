using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Entities;

namespace AmazonCore.Specification
{
    public interface ISpecifications<T,TKey> where T : BaseEntity<TKey>
    {

        Expression<Func<T, bool>> Criteria { get; set; }
        List<Expression<Func<T,object>>> Includes { get; set; }
        Expression<Func<T,object>> OrderBy { get; set; }
        Expression<Func<T,object>> OrderByDesc { get; set; }

        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }



    }
}
