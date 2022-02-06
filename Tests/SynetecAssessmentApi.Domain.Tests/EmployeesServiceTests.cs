using Moq;
using SynetecAssessmentApi.Domain.Interfaces;
using SynetecAssessmentApi.Domain.Model;
using SynetecAssessmentApi.Domain.Services;
using System.Threading.Tasks;
using Xunit;

namespace SynetecAssessmentApi.Domain.Tests
{
    public class EmployeesServiceTests
    {
        private readonly EmployeeService _sut;

        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock = new();

        public EmployeesServiceTests()
        {
            _sut = new EmployeeService(_employeeRepositoryMock.Object);
        }

        [Fact]
        public async Task GetEmployeeAsync_ShouldReturnSingleEmployee_WhenCustomerExists()
        {
            var employeeId = 123;
            _employeeRepositoryMock.Setup(m => m.GetByIdAsync(123)).ReturnsAsync( new Employee(employeeId, "Me", "human", 100, 1));

            var employee = await _sut.GetEmployeeAsync(123);

            Assert.Equal(employeeId, employee.Id);
        }

        [Fact]
        public async Task GetEmployeeAsync_ShouldReturnNull_WhenCustomerDoesntExists()
        {
            _employeeRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(()=>null);

            var employee = await _sut.GetEmployeeAsync(123);

            Assert.Null(employee);
        }
    }
}
