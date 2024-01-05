using FluentValidation;
using VbApi.VbSchema;

namespace VbApi.VbBusiness.Validator;

public class CreateCustomerValidator : AbstractValidator<CustomerRequest>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.DateOfBirth).NotEmpty();

    }
}