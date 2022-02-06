using FluentValidation;
using SynetecAssessmentApi.Dtos;

namespace SynetecAssessmentApi.Validators
{
    public class CalculateBonusDtoValidator : AbstractValidator<CalculateBonusDto>
    {
        public CalculateBonusDtoValidator()
        {
            RuleFor(x => x.SelectedEmployeeId).NotEmpty();
            RuleFor(x=>x.TotalBonusPoolAmount).NotEmpty().GreaterThan(0);
        }
    }
}
