using SynetecAssessmentApi.Domain.Model;

namespace SynetecAssessmentApi.Dtos
{
    public static class EmployeeMapper
    {
        public static EmployeeDto Map(Employee employee)
        {
            return new EmployeeDto
            {
                Fullname = employee.Fullname,
                JobTitle = employee.JobTitle,
                Salary = employee.Salary,
                Department = new DepartmentDto
                {
                    Title = employee.Department?.Title,
                    Description = employee.Department?.Description
                }
            };
        }
    }
}
