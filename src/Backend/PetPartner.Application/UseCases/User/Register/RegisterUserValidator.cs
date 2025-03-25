using FluentValidation;
using PetPartner.Communication.Requests;
using PetPartner.Domain.Extensions;
using PetPartner.Exceptions;

namespace PetPartner.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty()
            .WithMessage(ResourceMessagesException.NAME_EMPY);
        RuleFor(user => user.Email).NotEmpty()
            .WithMessage(ResourceMessagesException.EMAIL_EMPTY);
        RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessagesException.PASSWORD_GREATHER_OR_EQUAL_6);
        When(user => string.IsNullOrEmpty(user.Email).IsFalse(), () =>
        {
            RuleFor(user => user.Email).EmailAddress()
                 .WithMessage(ResourceMessagesException.EMAIL_INVALID);
        });
    }
}