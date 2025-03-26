using AutoMapper;
using PetPartner.Communication.Requests.Pet;
using PetPartner.Communication.Responses.Pet;
using PetPartner.Domain.Extensions;
using PetPartner.Domain.Repositories;
using PetPartner.Domain.Repositories.Pet;
using PetPartner.Domain.Services.LoggedUser;
using PetPartner.Exceptions.ExceptionBase;

namespace PetPartner.Application.UseCases.Pet;

public class RegisterPetUseCase : IRegisterPetUseCase
{
    private readonly IPetWriteOnlyRepositoy _writeOnlyRepositoy;
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterPetUseCase(IPetWriteOnlyRepositoy writeOnlyRepositoy, ILoggedUser loggedUser, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _writeOnlyRepositoy = writeOnlyRepositoy;
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseRegisteredPetJson> Execute(RequestPetJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.User();

        var pet = _mapper.Map<Domain.Entities.Pet>(request);
        pet.UserId = loggedUser.Id;

        pet.Pictures = _mapper.Map<List<Domain.Entities.Picture>>(request.Pictures);

        await _writeOnlyRepositoy.Add(pet);
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisteredPetJson>(pet);
    }

    private static void Validate(RequestPetJson request)
    {
        var result = new PetValidator().Validate(request);

        if (result.IsValid.IsFalse())
            throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
    }
}
