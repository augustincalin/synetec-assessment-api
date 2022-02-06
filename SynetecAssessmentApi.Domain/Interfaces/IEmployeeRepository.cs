using SynetecAssessmentApi.Domain.Model;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Domain.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<decimal> GetTotalSalaries();
    }
}
