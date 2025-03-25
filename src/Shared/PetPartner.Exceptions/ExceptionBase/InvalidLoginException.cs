using System.Net;

namespace PetPartner.Exceptions.ExceptionBase;

public class InvalidLoginException : PetPartnerException
{
    public InvalidLoginException() : base(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID)
    {
    }
}
