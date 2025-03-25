using PetPartner.Application.Services.Cryptography;
using PetPartner.Communication.Requests;
using PetPartner.Communication.Responses;
using PetPartner.Domain.Repositories.User;
using PetPartner.Exceptions.ExceptionBase;

namespace PetPartner.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _readOnlyRepository;
    private readonly PasswordEncrypter _passwordEncrypter;

    public DoLoginUseCase(IUserReadOnlyRepository readOnlyRepository, PasswordEncrypter passwordEncrypter)
    {
        _readOnlyRepository = readOnlyRepository;
        _passwordEncrypter = passwordEncrypter;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var passwordEncrypted = _passwordEncrypter.Encrypt(request.Password);

        var user = await _readOnlyRepository.GetByEmailAndPassword(request.Email, passwordEncrypted) ?? throw new InvalidLoginException(); // ternário "??" = o que está a direita é o que será retornado caso a expressão a esquerda seja nula.

        return new ResponseRegisteredUserJson
        {
            Name = user.Name
        };
    }
}
