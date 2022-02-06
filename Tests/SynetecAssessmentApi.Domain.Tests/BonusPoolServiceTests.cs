using SynetecAssessmentApi.Domain.Services;
using System;
using Xunit;

namespace SynetecAssessmentApi.Domain.Tests
{
    public class BonusPoolServiceTests
    {
        readonly BonusPoolService _sut;

        public BonusPoolServiceTests()
        {
            _sut = new();
        }

        [Fact]
        public void CalculateBonus_ShouldCalculateAmount()
        {
            var result = _sut.CalculateBonus(100, 15, 123456);
            Assert.Equal(18518.4m, result, 4);
        }

        [Fact]
        public void CalculateBonus_ShouldThrowArgumentException_WhenTotalSalariesIsZero()
        {
            void act() => _sut.CalculateBonus(0, 10, 20);

            var ex = Assert.Throws<ArgumentException>(() => act());

            Assert.IsType<ArgumentException>(ex);
        }

        [Theory]
        [InlineData(-100, 10, 20)]
        [InlineData(100, -10, 20)]
        [InlineData(100, 10, -20)]
        public void CalculateBonus_ShouldThrowArgumentException_WhenCalculationsMakeNoSense(decimal totalSalaries, decimal employeeSalary, decimal totalBonus)
        {
            void act() => _sut.CalculateBonus(totalSalaries, employeeSalary, totalBonus);

            var ex = Assert.Throws<ArgumentException>(() => act());

            Assert.IsType<ArgumentException>(ex);
        }

        [Fact]
        public void CalculateBonus_ShouldThrowArgumentException_WhenTotalEmployeeSalaryIsBiggerThanTheTotal()
        {
            void act() => _sut.CalculateBonus(10, 100, 20);

            var ex = Assert.Throws<ArgumentException>(() => act());

            Assert.IsType<ArgumentException>(ex);
        }
    }
}
