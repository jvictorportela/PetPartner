using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetPartner.Application.Services.AutoMapper;
using PetPartner.Application.UseCases.User.Register;

namespace PetPartner.Application;

public static class DependencyInjectionExtensionApplication
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddUseCases(services);
        AddAutoMapper(services, configuration);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }

    private static void AddAutoMapper(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(option => new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping());
        }).CreateMapper());
    }
}
