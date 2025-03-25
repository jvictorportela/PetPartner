using Microsoft.AspNetCore.Mvc;
using PetPartner.Application.UseCases.Login.DoLogin;
using PetPartner.Communication.Requests;
using PetPartner.Communication.Responses;

namespace PetPartner.Api.Controllers;

public class LoginController : PetPartnerBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase, [FromBody] RequestLoginJson request)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}