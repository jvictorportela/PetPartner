﻿using PetPartner.Communication.Requests;
using PetPartner.Domain.Extensions;
using PetPartner.Domain.Repositories;
using PetPartner.Domain.Repositories.User;
using PetPartner.Domain.Security.Criptography;
using PetPartner.Domain.Services.LoggedUser;
using PetPartner.Exceptions.ExceptionBase;

namespace PetPartner.Application.UseCases.User.ChangePassword;

public class ChangePasswordUseCase : IChangePasswordUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserUpdateOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordEncrypter _passwordEncripter;

    public ChangePasswordUseCase(
        ILoggedUser loggedUser,
        IPasswordEncrypter passwordEncripter,
        IUserUpdateOnlyRepository repository,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _passwordEncripter = passwordEncripter;
    }

    public async Task Execute(RequestChangePasswordJson request)
    {
        var loggedUser = await _loggedUser.User();

        Validate(request, loggedUser);

        var user = await _repository.GetById(loggedUser.Id);

        user.Password = _passwordEncripter.Encrypt(request.NewPassword);

        _repository.Update(user);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestChangePasswordJson request, Domain.Entities.User loggedUser)
    {
        var result = new ChangePasswordValidator().Validate(request);

        var currentPasswordEncripted = _passwordEncripter.Encrypt(request.Password);

        if (currentPasswordEncripted.Equals(loggedUser.Password).IsFalse())
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty,"Invalid password."));

        if (result.IsValid.IsFalse())
            throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
    }
}
