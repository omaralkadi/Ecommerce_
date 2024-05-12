using AmazonCore.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Entities;

namespace AmazonCore
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public Task<int> Complete();
        IGenericRepository<TEntity, int> Repository<TEntity>() where TEntity:BaseEntity<int>;

    }
}
