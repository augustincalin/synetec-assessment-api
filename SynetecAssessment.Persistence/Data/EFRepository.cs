using Microsoft.EntityFrameworkCore;
using SynetecAssessmentApi.Domain.Common;
using SynetecAssessmentApi.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Persistence.Data
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _entities;

        public EFRepository(DbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _entities.SingleOrDefaultAsync(e => e.Id == id);
        }
    }
}
