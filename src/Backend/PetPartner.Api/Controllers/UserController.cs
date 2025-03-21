using Microsoft.AspNetCore.Mvc;
using PetPartner.Application.UseCases.User.Register;
using PetPartner.Communication.Requests;
using PetPartner.Communication.Responses;

namespace PetPartner.Api.Controllers;

public class UserController : PetPartnerBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register([FromServices] IRegisterUserUseCase useCase, [FromBody] RequestRegisterUserJson request)
    {
        //Validate retornando de forma desorganizada.
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }
}
