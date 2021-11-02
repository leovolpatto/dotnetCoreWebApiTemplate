using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eccosys.Workflow.Domain
{
    public interface IEntityService<TEntity> where TEntity : BaseModel
    {
        Task<TEntity> Add(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task Remove(TEntity entity);

        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetById(Guid id);
    }
}
