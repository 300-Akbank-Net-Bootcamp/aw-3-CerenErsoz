using FluentValidation;
using VbApi.Controllers;

namespace VbApi.ValidationRules
{
    public class StaffValidator : AbstractValidator<Staff>
    {
        public StaffValidator()
        {
            RuleFor(staff => staff.Name)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(250)
                .WithMessage("Name is not valid.");

            RuleFor(staff => staff.Email)
                .NotEmpty()
                .WithMessage("Email address is not valid.");


            RuleFor(staff => staff.Phone)
                .NotEmpty()
                .WithMessage("Phone is not valid.");

            RuleFor(staff => staff.HourlySalary)
                .NotEmpty()
                .InclusiveBetween(30, 400)
                .WithMessage("Hourly salary does not fall within allowed range.");
        }
    }
}
