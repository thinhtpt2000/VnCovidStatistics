using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VnCovidStatistics.Core.Entities;
using VnCovidStatistics.Core.Interfaces;
using VnCovidStatistics.Infrastructure.Data;

namespace VnCovidStatistics.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        
        private readonly VietnamCovidStatisticsContext _context;
        protected readonly DbSet<T> _entities;
        
        public BaseRepository(VietnamCovidStatisticsContext context)
        {
            this._context = context;
            _entities = _context.Set<T>();
        }
        
        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public async Task Delete(Guid id)
        {
            var entity = await GetById(id); 
            _entities.Remove(entity);
        }
    }
}