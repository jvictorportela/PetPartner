using PetPartner.Application.Services.Cryptography;
using PetPartner.Communication.Requests;
using PetPartner.Communication.Responses;
using PetPartner.Domain.Repositories.User;
using PetPartner.Domain.Security.Criptography;
using PetPartner.Domain.Security.Tokens;
using PetPartner.Exceptions.ExceptionBase;

namespace PetPartner.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _readOnlyRepository;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public DoLoginUseCase(IUserReadOnlyRepository readOnlyRepository, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator accessTokenGenerator)
    {
        _readOnlyRepository = readOnlyRepository;
        _passwordEncrypter = passwordEncrypter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var passwordEncrypted = _passwordEncrypter.Encrypt(request.Password);

        var user = await _readOnlyRepository.GetByEmailAndPassword(request.Email, passwordEncrypted) ?? throw new InvalidLoginException(); // ternário "??" = o que está a direita é o que será retornado caso a expressão a esquerda seja nula.

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Tokens = new ResponseTokensJson
            {
                AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }
}
