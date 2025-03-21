using AutoMapper;
using PetPartner.Communication.Requests;
using PetPartner.Communication.Responses;
using PetPartner.Domain.Repositories;
using PetPartner.Domain.Repositories.User;
using PetPartner.Exceptions.ExceptionBase;

namespace PetPartner.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _writeOnlyRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserReadOnlyRepository _readOnlyRepository;
    //AccessToken
    //PasswordEncrypter

    public RegisterUserUseCase(IUserWriteOnlyRepository writeOnlyRepository, IMapper mapper, IUnitOfWork unitOfWork, IUserReadOnlyRepository readOnlyRepository)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _readOnlyRepository = readOnlyRepository;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);

        await _writeOnlyRepository.Add(user);
        await _unitOfWork.Commit();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        var emailExist = await _readOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExist)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "Email all ready registered."));

        if (!result.IsValid)
        {
            var errorsMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages);
        }
    }
}
