using Microsoft.EntityFrameworkCore;
using SynetecAssessmentApi.Domain.Interfaces;
using SynetecAssessmentApi.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Persistence.Data
{
    public class EmployeeRepository : EFRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DbContext context) : base(context) { }

        public override async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _entities.Include(employee => employee.Department).ToListAsync();
        }

        public override async Task<Employee> GetByIdAsync(int id)
        {
            return await _entities.Include(employee => employee.Department).SingleOrDefaultAsync(employee => employee.Id == id);
        }

        public async Task<decimal> GetTotalSalaries()
        {
            return await _entities.SumAsync(employee => employee.Salary);
        }
    }
}
