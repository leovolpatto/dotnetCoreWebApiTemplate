using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eccosys.Workflow.Domain
{
    public interface IEntityRepository<TEntity> where TEntity : BaseModel
    {
        Task<TEntity> Add(TEntity entity);

        Task<List<TEntity>> AddMany(IEnumerable<TEntity> entities);

        Task<TEntity> Update(TEntity entity);

        Task<TEntity> Remove(TEntity entity);

        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetById(int id);
    }
}
