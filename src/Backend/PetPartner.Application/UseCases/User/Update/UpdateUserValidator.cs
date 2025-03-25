using FluentValidation;
using PetPartner.Communication.Requests;
using PetPartner.Domain.Extensions;

namespace PetPartner.Application.UseCases.User.Update;

public class UpdateUserValidator : AbstractValidator<RequestUpdateUserJson>
{
    public UpdateUserValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage("Name cannot be empy.");
        RuleFor(request => request.Email).NotEmpty().WithMessage("Email cannot be empty.");

        When(request => string.IsNullOrWhiteSpace(request.Email).IsFalse(), () =>
        {
            RuleFor(request => request.Email).EmailAddress().WithMessage("Invalid email.");
        });
    }
}
