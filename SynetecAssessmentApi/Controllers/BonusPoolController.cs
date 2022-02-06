using Microsoft.AspNetCore.Mvc;
using SynetecAssessmentApi.Domain.Interfaces;
using SynetecAssessmentApi.Dtos;
using System.Threading.Tasks;
using System.Linq;
using SynetecAssessmentApi.Domain.Model;

namespace SynetecAssessmentApi.Controllers
{
    [Route("api/[controller]")]
    public class BonusPoolController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IBonusPoolService _bonusPoolService;

        public BonusPoolController(IEmployeeService employeeService, IBonusPoolService bonusPoolService)
        {
            _employeeService = employeeService;
            _bonusPoolService = bonusPoolService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok((await _employeeService.GetEmployeesAsync())
                .Select(e => EmployeeMapper.Map(e)));
        }

        [HttpPost()]
        public async Task<IActionResult> CalculateBonusAsync([FromBody] CalculateBonusDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState
                    .Where(x => x.Value.Errors.Any())
                    .SelectMany(x => x.Value.Errors)
                    .Select(e => e.ErrorMessage));
            }

            Employee employee = await _employeeService.GetEmployeeAsync(request.SelectedEmployeeId);

            if (null == employee)
            {
                return NoContent();
            }

            decimal amount = _bonusPoolService.CalculateBonus(await _employeeService.GetTotalSalaries(), employee.Salary, request.TotalBonusPoolAmount);

            return Ok(new BonusPoolCalculatorResultDto
            {
                Employee = EmployeeMapper.Map(employee),
                Amount = (int)amount
            });
        }
    }
}
