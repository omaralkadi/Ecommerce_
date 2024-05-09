using AmazonCore.Interfaces.Repository;
using AmazonCore.Specification;
using AmazonRepository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Entities;

namespace AmazonRepository.Repository
{
    public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : BaseEntity<TKey>
    {
        private readonly DataContext _dbContext;

        public GenericRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(T input)
        {
            await _dbContext.Set<T>().AddAsync(input);
            await _dbContext.SaveChangesAsync();
        }
        public async void Delete(T input)
        {
            _dbContext.Set<T>().Remove(input);
            await _dbContext.SaveChangesAsync();
        }
        public async void Update(T input)
        {
            _dbContext.Set<T>().Update(input);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<T?> GetById(TKey? id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public async Task<IEnumerable<T>> GetAllWithSpec(ISpecifications<T, TKey> spec)
        {
            return await SpecificationEvaluator<T, TKey>.GetQuerey(_dbContext.Set<T>(), spec).ToListAsync();
        }
        public async Task<T?> GetByIdWithSpec(ISpecifications<T, TKey> spec)
        {
            return await SpecificationEvaluator<T, TKey>.GetQuerey(_dbContext.Set<T>(), spec).FirstOrDefaultAsync();

        }
        public async Task<int> GetCountWithSpec(ISpecifications<T, TKey> spec)
        {
            return await SpecificationEvaluator<T, TKey>.GetQuerey(_dbContext.Set<T>(), spec).CountAsync();
        }
    }
}
