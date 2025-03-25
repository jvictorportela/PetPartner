using FluentValidation;
using PetPartner.Application.SharedValidators;
using PetPartner.Communication.Requests;

namespace PetPartner.Application.UseCases.User.ChangePassword;

public class ChangePasswordValidator : AbstractValidator<RequestChangePasswordJson>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator<RequestChangePasswordJson>());
    }
}
