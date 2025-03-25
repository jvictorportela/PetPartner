using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetPartner.Domain.Repositories;
using PetPartner.Domain.Repositories.User;
using PetPartner.Domain.Security.Criptography;
using PetPartner.Domain.Security.Tokens;
using PetPartner.Domain.Services.LoggedUser;
using PetPartner.Infrastructure.DataAccess;
using PetPartner.Infrastructure.DataAccess.Repositories;
using PetPartner.Infrastructure.Extensions;
using PetPartner.Infrastructure.Security.Criptography;
using PetPartner.Infrastructure.Security.Tokens.Access.Generator;
using PetPartner.Infrastructure.Security.Tokens.Access.Validator;
using PetPartner.Infrastructure.Services.LoggedUser;
using System.Reflection;

namespace PetPartner.Infrastructure;

public static class DependencyInjectionExtensionInfra
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
        AddFluentMigrator(services, configuration);
        AddTokens(services, configuration);
        AddLoggedUser(services);
        AddPasswordEncrypter(services, configuration);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();

        services.AddDbContext<PetPartnerDbContext>(dbContextOptions =>
        {
            dbContextOptions.UseSqlServer(connectionString);
        });
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();

        services.AddFluentMigratorCore().ConfigureRunner(options =>
        {
            options
            .AddSqlServer()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(Assembly.Load("PetPartner.Infrastructure")).For.All();
        });
    }

    private static void AddTokens(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(option => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
        services.AddScoped<IAccessTokenValidator>(option => new JwtTokenValidator(signingKey!));
    }

    private static void AddLoggedUser(IServiceCollection services) => services.AddScoped<ILoggedUser, LoggedUser>();

    private static void AddPasswordEncrypter(IServiceCollection services, IConfiguration configuration)
    {
        var additionalKey = configuration.GetValue<string>("Settings:Passwords:AdditionalKey"); //Com o Binder, consigo passar a tipagem no valor do json vindo do appsettings

        services.AddScoped<IPasswordEncrypter>(option => new Shar512Encrypter(additionalKey!));
    }
}
