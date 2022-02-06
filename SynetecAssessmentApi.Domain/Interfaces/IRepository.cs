using SynetecAssessmentApi.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> GetByIdAsync(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();

    }
}
