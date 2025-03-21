using FluentValidation;
using PetPartner.Communication.Requests;
using PetPartner.Domain.Extensions;

namespace PetPartner.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty()
            .WithMessage("Name is required");
        RuleFor(user => user.Email).NotEmpty()
            .WithMessage("Email is required");
        When(user => string.IsNullOrEmpty(user.Email).IsFalse(), () =>
        {
            RuleFor(user => user.Email).EmailAddress()
                 .WithMessage("Email is invalid");
        });
    }
}