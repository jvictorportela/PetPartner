namespace PetPartner.Exceptions.ExceptionBase;

public class ErrorOnValidationException : PetPartnerException
{
    public IList<string> ErrorMessages { get; set; }

    public ErrorOnValidationException(IList<string> errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }
}
