# SchoolERP Enterprise Architecture

## 1. Complete Solution Architecture

SchoolERP is designed as a cloud-ready, modular, enterprise-grade school management platform targeting large-scale multi-tenant deployment across thousands of schools and millions of students.

### Architecture Principles
- Separation of concerns through Clean Architecture
- Domain-driven modeling with rich business rules
- CQRS for scalable read/write operations
- Repository + Unit of Work for persistence abstraction
- Dependency Injection for loose coupling
- Strategy and Factory patterns for extensibility
- Specification pattern for query composition
- JWT + role/permission-based security
- Async-by-default processing
- Observability via Serilog and health checks

### Architectural Layers
1. Domain Layer
   - Contains enterprise entities, value objects, enums, domain rules, interfaces, exceptions, and constants.
   - Why: It is the core business model and must remain independent from infrastructure and delivery concerns.

2. Application Layer
   - Contains use cases implemented using CQRS, validators, DTOs, mappings, and interfaces.
   - Why: It orchestrates business workflows while staying decoupled from frameworks and databases.

3. Infrastructure Layer
   - Implements authentication, logging, email, SMS, caching, storage, background jobs, and external integrations.
   - Why: It isolates implementation details and allows swapping providers without impacting core logic.

4. Persistence Layer
   - Implements EF Core DbContext, repositories, unit of work, configurations, migrations, seed data, and SQL artifacts.
   - Why: It centralizes data access and ensures portability and consistency.

5. API Layer
   - Exposes REST endpoints, middleware, filters, health checks, and Swagger.
   - Why: It serves clients, manages HTTP concerns, and protects the core application behind a stable interface.

6. Shared and Contracts Layer
   - Provides common types, DTOs, and cross-cutting contracts.
   - Why: It reduces duplication and improves maintainability.

## 2. Folder Structure (Tree)

```text
SchoolERP.sln
src/
  SchoolERP.Api/
    Controllers/
    Middlewares/
    Filters/
    Extensions/
    Configurations/
    Swagger/
    HealthChecks/
    Program.cs
  SchoolERP.Application/
    Features/
      Students/
        Commands/
        Queries/
        Handlers/
        Validators/
        DTOs/
        Mappings/
        Interfaces/
        Responses/
      Teachers/
      Attendance/
      Fees/
      Library/
      Transport/
      HR/
      Examination/
      Authentication/
      Authorization/
  SchoolERP.Domain/
    Entities/
    ValueObjects/
    Enums/
    Interfaces/
    Events/
    Exceptions/
    Specifications/
    Constants/
    Common/
  SchoolERP.Infrastructure/
    Authentication/
    Logging/
    Email/
    SMS/
    FileStorage/
    Caching/
    BackgroundJobs/
    Repositories/
    Services/
    Configurations/
  SchoolERP.Persistence/
    DbContext/
    EntityConfigurations/
    Migrations/
    SeedData/
    StoredProcedures/
    Views/
    Functions/
    Repositories/
    UnitOfWork/
  SchoolERP.Shared/
  SchoolERP.Contracts/

tests/
  SchoolERP.UnitTests/
  SchoolERP.IntegrationTests/

frontend/
  angular/
    src/app/
      core/
      shared/
      features/
```

## 3. Project References

- SchoolERP.Api -> SchoolERP.Application, SchoolERP.Infrastructure, SchoolERP.Persistence, SchoolERP.Contracts, SchoolERP.Shared
- SchoolERP.Application -> SchoolERP.Domain, SchoolERP.Contracts
- SchoolERP.Infrastructure -> SchoolERP.Domain, SchoolERP.Application
- SchoolERP.Persistence -> SchoolERP.Domain, SchoolERP.Application, SchoolERP.Shared
- SchoolERP.Shared -> none
- SchoolERP.Contracts -> none
- Unit Tests -> SchoolERP.Application, SchoolERP.Domain, SchoolERP.Infrastructure
- Integration Tests -> SchoolERP.Api, SchoolERP.Persistence

## 4. Dependency Flow Diagram

```text
Client -> API Controllers -> Application Handlers -> Domain Services/Entities
                          |                  |
                          v                  v
                   Infrastructure      Persistence
```

## 5. High-Level Architecture Diagram

```text
[Angular SPA/Web App]
        |
        v
[API Gateway / Reverse Proxy]
        |
        v
[ASP.NET Core Web API]
   |        |        |
   |        |        +--> [Authentication/Authorization]
   |        |        +--> [CQRS/Validators/MediatR]
   |        |        +--> [Domain Logic]
   |        |
   +--> [Application Layer]
   |
   +--> [Infrastructure Layer]
   |
   +--> [Persistence Layer]

[SQL Server]
[Redis Cache]
[Blob/File Storage]
[Email/SMS Providers]
```

## 6. Low-Level Architecture Diagram

```text
Controller -> Command/Query -> Handler -> Validator -> Repository -> DbContext -> SQL Server
                                  |                      |
                                  |                      +--> UnitOfWork
                                  +--> Mapper/DTO
                                  +--> Domain Rules
```

## 7. Database ER Diagram

The database is modeled around normalized entities and relationships:
- Users -> Roles/Permissions
- Users -> Parents/Teachers/Students/Employees
- Students -> StudentClasses -> Classes/Sections/AcademicYears
- Teachers -> TeacherSubjects/TeacherClasses -> Subjects/Classes
- Students -> Attendance, Fees, BookIssues, Marks, ReportCards
- Classes -> ExamSchedules, Timetables, FeeStructures
- Buildings -> Rooms -> Timetables
- Buses -> Drivers -> Routes/Stops -> StudentTransport

## 8. Complete SQL Scripts

The schema is available in [SchoolERP.Database/00_DatabaseSchema.sql](SchoolERP.Database/00_DatabaseSchema.sql).

## 9. Entity Classes

Core entity examples:
- Student
- Teacher
- Class
- Subject
- Attendance
- FeeStructure
- Book
- Exam
- Employee
- Notification

These should be implemented in the Domain layer with business methods and invariants.

## 10. DbContext

The persistence project contains a DbContext that maps all entities to SQL Server tables and configures relationships and indexes.

## 11. Repository Interfaces

Repository abstractions should be defined in the Domain layer to keep persistence concerns out of core business logic.

## 12. Repository Implementations

Concrete EF Core repository implementations should live in the Persistence layer and implement the repository interfaces.

## 13. CQRS Structure

The application layer should organize code as:
- Commands
- Queries
- Handlers
- Validators
- DTOs
- Mappings
- Responses

Example:
- Students/CreateStudentCommand
- Students/GetStudentByIdQuery

## 14. API Folder Structure

The API layer should follow:
- Controllers
- Middlewares
- Filters
- Extensions
- Configurations
- Swagger
- HealthChecks

## 15. Angular Folder Structure

```text
frontend/angular/src/app/
  core/
    services/
    guards/
    interceptors/
    models/
    auth/
  shared/
    components/
    pipes/
    directives/
  features/
    auth/
    dashboard/
    students/
    teachers/
    attendance/
    fees/
    library/
    exams/
    transport/
    hr/
```

## 16. Naming Conventions

- Classes: PascalCase
- Methods: PascalCase
- Variables: camelCase
- Constants: PascalCase with UPPER_CASE for static constants where appropriate
- Files: PascalCase for classes, lowercase-dash for Angular modules where appropriate
- DTOs: suffix with Dto
- Commands: suffix with Command
- Queries: suffix with Query
- Handlers: suffix with Handler
- Validators: suffix with Validator

## 17. Coding Standards

- Use nullable reference types
- Favor explicit types over var where readability benefits
- Use async/await throughout I/O-bound work
- Guard clauses for input validation
- Avoid overengineering; prefer clear and maintainable code
- Use interfaces for external dependencies
- Keep controllers thin and handlers rich in behavior
- Use FluentValidation for input validation

## 18. Best Practices

- Enforce authentication and authorization globally
- Use centralized exception handling
- Use database transactions for multi-step operations
- Prefer bulk operations for high-volume writes
- Cache frequently requested reference data
- Use pagination and filtering for list endpoints
- Add health checks and metrics for production observability
- Use background jobs for email, reports, notifications

## 19. Scalability Recommendations

- Use SQL Server partitioning for large transaction tables
- Use Redis for distributed caching and session state
- Use background queues for long-running work
- Use read replicas for reporting workloads
- Introduce message brokers for asynchronous integration
- Consider multi-tenant architecture with schema or database per tenant
- Design APIs for statelessness and horizontal scale

## 20. Future Migration to Microservices

A good migration path is to split by bounded contexts:
- Identity and Access
- Student Management
- Academic Management
- Finance
- Library
- Transport
- HR and Payroll
- Communication and Notifications

Each microservice can own its database, domain model, and APIs while using event-driven integration.

## Why These Decisions Matter

- Clean Architecture ensures long-term maintainability and independent evolution of layers.
- CQRS makes command and query paths scalable and easier to optimize.
- Repository + Unit of Work reduce persistence coupling and improve testability.
- Strategy and Factory patterns support interchangeable implementations such as payment providers or notification channels.
- Specification pattern helps compose complex filtering logic cleanly.
- JWT + permission-based authorization is essential for enterprise-grade security.
- Serilog and health checks provide observability required for production systems.
- Redis, pagination, filtering, and async processing directly improve performance at scale.

## Recommended Implementation Order

1. Domain entities and value objects
2. Application commands/queries/handlers/validators
3. Persistence and EF Core mappings
4. API controllers and DTOs
5. Authentication and authorization
6. Logging, exception handling, and rate limiting
7. Caching and background jobs
8. Angular frontend modules
9. Automated testing and deployment hardening
