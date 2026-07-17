using SchoolERP.Application;
using SchoolERP.Infrastructure;
using SchoolERP.Persistence;

namespace SchoolERP.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSchoolErpServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication();
        services.AddPersistence(configuration);
        services.AddInfrastructure(configuration);
        return services;
    }
}
