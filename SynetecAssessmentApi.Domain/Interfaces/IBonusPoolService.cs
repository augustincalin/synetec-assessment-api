namespace SynetecAssessmentApi.Domain.Interfaces
{
    public interface IBonusPoolService
    {
        public decimal CalculateBonus(decimal totalSalaries, decimal employeeSalary, decimal totalBonus);
    }
}
