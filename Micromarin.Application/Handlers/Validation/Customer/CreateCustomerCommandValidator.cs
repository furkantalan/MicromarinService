
using FluentValidation;
using Micromarin.Application.Commands.Customers;


namespace Micromarin.Application.Handlers.Validation.Customer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand.Request>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First Name cannot be empty")
            .MaximumLength(50).WithMessage("First Name cannot exceed 50 characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last Name cannot be empty")
            .MaximumLength(50).WithMessage("Last Name cannot exceed 50 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number cannot be empty")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address cannot be empty")
            .MaximumLength(200).WithMessage("Address cannot exceed 200 characters");
    }
}