using Ardalis.GuardClauses;
using SynetecAssessmentApi.Domain.Interfaces;

namespace SynetecAssessmentApi.Domain.Services
{
    public class BonusPoolService : IBonusPoolService
    {
        public decimal CalculateBonus(decimal totalSalaries, decimal employeeSalary, decimal totalBonus)
        {
            Guard.Against.Zero(totalSalaries, nameof(totalSalaries), "Can't calculate percentage from 0!");
            Guard.Against.Negative(totalSalaries, nameof(totalSalaries), "Negative totalSalaries makes no sense");
            Guard.Against.Negative(employeeSalary, nameof(employeeSalary), "Ah, poor guy, he must bring money from home!");
            Guard.Against.Negative(totalBonus, nameof(totalBonus), "Negative totalBonus makes no sense.");
            Guard.Against.AgainstExpression((totalSalaries) => totalSalaries >= employeeSalary, totalSalaries, "The salary of one employee can't be bigger than the total of salaries.");

            return (employeeSalary / totalSalaries) * totalBonus;
        }

    }
}
