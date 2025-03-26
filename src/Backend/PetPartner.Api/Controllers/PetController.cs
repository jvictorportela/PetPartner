using Microsoft.AspNetCore.Mvc;
using PetPartner.Api.Attributes;
using PetPartner.Application.UseCases.Pet;
using PetPartner.Communication.Requests.Pet;
using PetPartner.Communication.Responses;
using PetPartner.Communication.Responses.Pet;

namespace PetPartner.Api.Controllers;

[AuthenticatedUser]
public class PetController : PetPartnerBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredPetJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterPet([FromServices] IRegisterPetUseCase useCase, [FromBody] RequestPetJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
