# SchoolERP

A production-ready enterprise School ERP solution designed with Clean Architecture, CQRS, EF Core, SQL Server, Redis, Swagger, Serilog, and Docker-ready structure.

## Solution Structure

- SchoolERP.Api
- SchoolERP.Application
- SchoolERP.Domain
- SchoolERP.Infrastructure
- SchoolERP.Persistence
- SchoolERP.Shared
- SchoolERP.Contracts
- SchoolERP.UnitTests
- SchoolERP.IntegrationTests

## Build

```bash
dotnet build SchoolERP.sln
```

## Run

```bash
dotnet run --project src/SchoolERP.Api
```

## Notes

- SQL Server connection is configured through appsettings.json.
- Redis is expected at localhost:6379 for caching.
- The initial implementation includes student query support via MediatR and a sample API endpoint.
