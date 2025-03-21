using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetPartner.Domain.Repositories;
using PetPartner.Domain.Repositories.User;
using PetPartner.Infrastructure.DataAccess;
using PetPartner.Infrastructure.DataAccess.Repositories;
using PetPartner.Infrastructure.Extensions;
using System.Reflection;

namespace PetPartner.Infrastructure;

public static class DependencyInjectionExtensionInfra
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
        AddFluentMigrator(services, configuration);
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
}
