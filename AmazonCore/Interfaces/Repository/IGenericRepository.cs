using AmazonCore.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Entities;

namespace AmazonCore.Interfaces.Repository
{
    public interface IGenericRepository<T, TKey> where T : BaseEntity<TKey>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(TKey? id);

        #region WithSpecification
        Task<IEnumerable<T>> GetAllWithSpec(ISpecifications<T,TKey> spec);
        Task<T?> GetByIdWithSpec(ISpecifications<T,TKey> spec);

        Task<int> GetCountWithSpec(ISpecifications<T, TKey> spec);

        #endregion
        Task AddAsync(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
