using SynetecAssessmentApi.Domain.Interfaces;
using SynetecAssessmentApi.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Domain.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> GetEmployeeAsync(int id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Task<decimal> GetTotalSalaries()
        {
            return await _employeeRepository.GetTotalSalaries();
        }
    }
}
