using FluentValidation;
using PetPartner.Application.SharedValidators;
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
        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());
        When(user => string.IsNullOrEmpty(user.Email).IsFalse(), () =>
        {
            RuleFor(user => user.Email).EmailAddress()
                 .WithMessage(ResourceMessagesException.EMAIL_INVALID);
        });
    }
}