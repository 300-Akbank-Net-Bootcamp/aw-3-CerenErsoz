using FluentValidation;
using VbApi.Controllers;

namespace VbApi.ValidationRules
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(employee => employee.Name)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(250)
                .WithMessage("Name is not valid.");

            RuleFor(employee => employee.DateOfBirth)
                .NotEmpty()
                .WithMessage("Date is not valid.");

            RuleFor(employee => employee.Email)
                .NotEmpty()
                .WithMessage("Email address is not valid.");

            RuleFor(employee => employee.Phone)
                .NotEmpty()
                .WithMessage("Phone is not valid.");

            RuleFor(employee => employee.HourlySalary)
                .NotEmpty()
                .InclusiveBetween(30, 400)
                .WithMessage("Hourly salary does not fall within allowed range. (Range 30-400)");
        }
    }
}
