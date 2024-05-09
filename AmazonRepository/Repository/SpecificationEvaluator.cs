using AmazonCore.Specification;
using Microsoft.EntityFrameworkCore;
using Talabat_Core.Entities;

namespace AmazonRepository.Repository
{
    public static class SpecificationEvaluator<T, TKey> where T : BaseEntity<TKey>
    {
        public static IQueryable<T> GetQuerey(IQueryable<T> input, ISpecifications<T, TKey> spec)
        {
            var querey = input;

            if (spec.Criteria is not null)
            {
                querey = querey.Where(spec.Criteria);
            }
            if(spec.OrderBy is not null)
            {
                querey=querey.OrderBy(spec.OrderBy);
            }
            if(spec.OrderByDesc is not null)
            {
                querey = querey.OrderByDescending(spec.OrderByDesc);
            }
            if (spec.IsPaginationEnabled)
            {
                querey = querey.Skip(spec.Skip).Take(spec.Take);
            }
            if (spec.Includes is not null)
            {
                querey = spec.Includes.Aggregate(querey, (currentQuery, IncludeExpression) => currentQuery.Include(IncludeExpression));
            }
            return querey;
        }

    }
}