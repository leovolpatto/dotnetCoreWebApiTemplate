using Eccosys.Workflow.Domain;
using Eccosys.Workflow.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Eccosys.Workflow.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity> : IEntityRepository<TEntity> where TEntity : BaseModel
    {
        protected readonly EntityContext _entityContext;
        
        public BaseRepository(EntityContext context)
        {
            _entityContext = context;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            var result = _entityContext.Set<TEntity>().Add(entity).Entity;
            await _entityContext.SaveChangesAsync();

            return result;
        }

        public async Task<List<TEntity>> AddMany(IEnumerable<TEntity> entities)
        {
            _entityContext.Set<TEntity>().AddRange(entities);
            await _entityContext.SaveChangesAsync();

            return new List<TEntity>(entities);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _entityContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            var result = await _entityContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);

            return result;
        }

        public async Task<TEntity> Remove(TEntity entity)
        {
            var result = _entityContext.Set<TEntity>().Remove(entity).Entity;
            await _entityContext.SaveChangesAsync();

            return result;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            var result = _entityContext.Set<TEntity>().Update(entity).Entity;
            await _entityContext.SaveChangesAsync();
            return result;
        }
    }
}