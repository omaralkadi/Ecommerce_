using AmazonCore;
using AmazonCore.Interfaces.Repository;
using AmazonRepository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Entities;

namespace AmazonRepository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly Hashtable _Repository;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            _Repository = new Hashtable();
        }
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }
        public IGenericRepository<TEntity, int> Repository<TEntity>() where TEntity : BaseEntity<int>
        {
            var Key = typeof(TEntity).Name;
            if (!_Repository.ContainsKey(Key))
            {
                var Repository = new GenericRepository<TEntity, int>(_context);
                _Repository.Add(Key, Repository);

            }
            return _Repository[Key] as IGenericRepository<TEntity, int>;

        }
    }       
}
