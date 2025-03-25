using FluentValidation;
using FluentValidation.Validators;

namespace PetPartner.Application.SharedValidators;

public class PasswordValidator<T> : PropertyValidator<T, string>
{
    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", "Password cannot be empty.");
            return false;
        }

        if (password.Length < 6)
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", "Invalid password.");

            return false;
        }

        return true;
    }

    public override string Name => "PasswordValidator";

    protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";
}
