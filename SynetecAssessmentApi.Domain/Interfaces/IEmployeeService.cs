using SynetecAssessmentApi.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Domain.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeAsync(int id);
        Task<decimal> GetTotalSalaries();
    }
}
