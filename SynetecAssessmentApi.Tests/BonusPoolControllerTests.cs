using Microsoft.AspNetCore.Mvc;
using Moq;
using SynetecAssessmentApi.Controllers;
using SynetecAssessmentApi.Domain.Interfaces;
using SynetecAssessmentApi.Domain.Model;
using SynetecAssessmentApi.Dtos;
using System.Threading.Tasks;
using Xunit;

namespace SynetecAssessmentApi.Tests
{
    public class BonusPoolControllerTests
    {
        private readonly BonusPoolController _sut;
        private readonly Mock<IEmployeeService> _mockEmployeeService = new();
        private readonly Mock<IBonusPoolService> _mockBonusService = new();

        public BonusPoolControllerTests()
        {
            _sut = new BonusPoolController(_mockEmployeeService.Object, _mockBonusService.Object);
        }

        [Fact]
        public async Task CalculateBonus_ShouldReturnDto_WhenCustomerExists()
        {
            var employeeId = 123;
            var totalSalaries = 100;
            var totalBonus = 100;

            var request = new CalculateBonusDto
            {
                SelectedEmployeeId = employeeId,
                TotalBonusPoolAmount = 100
            };

            var employee = GetTestEmployee(employeeId);

            _mockEmployeeService.Setup(mock => mock.GetEmployeeAsync(employeeId)).ReturnsAsync(employee);
            _mockEmployeeService.Setup(mock => mock.GetTotalSalaries()).ReturnsAsync(totalSalaries);
            _mockBonusService.Setup(mock => mock.CalculateBonus(totalSalaries, employee.Salary, totalBonus)).Returns(10);

            var result = await _sut.CalculateBonusAsync(request);

            Assert.IsType<OkObjectResult>(result);
            var responseValue = ((OkObjectResult)result).Value;
            Assert.IsType<BonusPoolCalculatorResultDto>(responseValue);
            Assert.Equal(10, (responseValue as BonusPoolCalculatorResultDto).Amount);
            Assert.Equal(employee.Fullname, (responseValue as BonusPoolCalculatorResultDto).Employee.Fullname);
        }

        [Fact]
        public async Task CalculateBonus_ShouldReturnNoContent_WhenCustomerDoesntExist()
        {
            var request = new CalculateBonusDto
            {
                SelectedEmployeeId = 123,
                TotalBonusPoolAmount = 100
            };

            _mockEmployeeService.Setup(mock => mock.GetEmployeeAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            var result = await _sut.CalculateBonusAsync(request);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task CalculateBonus_ShouldReturnBadRequest_WhenModelStateHasErrors()
        {
            var employeeId = 123;
            CalculateBonusDto request = new CalculateBonusDto
            {
                SelectedEmployeeId = employeeId,
                TotalBonusPoolAmount = 0
            };

            var employee = GetTestEmployee(employeeId);

            _mockEmployeeService.Setup(mock => mock.GetEmployeeAsync(It.IsAny<int>())).ReturnsAsync(employee);
            _sut.ModelState.AddModelError("error", "some error");

            var result = await _sut.CalculateBonusAsync(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CalculateBonus_ShouldCallBonusService_CalculateBonus()
        {
            var employeeId = 123;
            var totalSalaries = 100;
            var totalBonus = 100;

            CalculateBonusDto request = new CalculateBonusDto
            {
                SelectedEmployeeId = employeeId,
                TotalBonusPoolAmount = 100
            };

            var employee = GetTestEmployee(employeeId);

            _mockEmployeeService.Setup(mock => mock.GetEmployeeAsync(employeeId)).ReturnsAsync(employee);
            _mockEmployeeService.Setup(mock => mock.GetTotalSalaries()).ReturnsAsync(totalSalaries);
            _mockBonusService.Setup(mock => mock.CalculateBonus(totalSalaries, employee.Salary, totalBonus)).Returns(10).Verifiable();

            await _sut.CalculateBonusAsync(request);

            _mockBonusService.Verify();
        }

        private Employee GetTestEmployee(int employeeId)
        {
            return new Employee(employeeId, "Me", "human", 10, 1);
        }
    }
}
