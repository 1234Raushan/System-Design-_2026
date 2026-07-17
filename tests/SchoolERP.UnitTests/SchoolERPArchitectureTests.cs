using Xunit;
using SchoolERP.Infrastructure.Services;

namespace SchoolERP.UnitTests;

public class SchoolERPArchitectureTests
{
    //[Fact]
    //public void DomainLayer_ShouldNotDependOnApplicationOrInfrastructure()
    //{
    //    var domainAssembly = typeof(SchoolERP.Domain.Entities.Student).Assembly;
    //    //var applicationAssembly = typeof(SchoolERP.Application.Queries.GetStudentsQuery).Assembly;
    //    var infrastructureAssembly = typeof(SchoolERP.Infrastructure.Services.CacheService).Assembly;

    //    Assert.DoesNotContain(domainAssembly.GetReferencedAssemblies(), a => a.Name == "SchoolERP.Application");
    //    Assert.DoesNotContain(domainAssembly.GetReferencedAssemblies(), a => a.Name == "SchoolERP.Infrastructure");
    //    Assert.DoesNotContain(domainAssembly.GetReferencedAssemblies(), a => a.Name == "SchoolERP.Persistence");
    //}
}
