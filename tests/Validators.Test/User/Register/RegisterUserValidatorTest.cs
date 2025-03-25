using CommonTestUtilities.Requests;
using PetPartner.Application.UseCases.User.Register;

namespace Validators.Test.User.Register;


public class RegisterUserValidatorTest
{
    [Fact] //Anotation para tipar função teste
    public void Success()
    {
        //Instânciar
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();

        //Executar
        var result = validator.Validate(request);

        //Assert = verificar se o resultado foi o esperado.
        //Verificar
        Assert.NotNull(result);
        //Assert.Equivalent(request.Name, result.Name); Gerar UseCase
        Assert.True(result.IsValid);
    }
}
