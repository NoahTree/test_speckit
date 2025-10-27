# Research: Create Taskify

**Feature**: Create Taskify | **Date**: 2025-10-23
**Purpose**: Document technical decisions, alternatives considered, and rationale for implementation approach

## Technology Stack Decisions

### Decision 1: .NET Aspire for Application Orchestration

**Decision**: Use .NET Aspire 9.2.1 as the application orchestration framework

**Version Specifications**:
- **.NET Aspire SDK**: 9.2.1 (latest stable as of December 2024)
- **Target Framework**: .NET 8.0 (LTS) - Aspire 9.x is backward compatible with .NET 8.0
- **NuGet Packages**:
  - `Aspire.Hosting.PostgreSQL`: 9.2.1 (AppHost project)
  - `Aspire.Npgsql.EntityFrameworkCore.PostgreSQL`: 9.2.1 (ApiService project)
  - `Aspire.Hosting`: 9.2.1 (AppHost project)
- **Container Image**: docker.io/library/postgres:16-alpine

**Rationale**:
- **Service Discovery**: Built-in service discovery simplifies communication between Blazor frontend and REST API
- **Local Development**: Aspire dashboard provides unified view of all services, logs, and metrics
- **Configuration Management**: Centralized configuration with environment-specific overrides
- **Observability**: Integrated OpenTelemetry support for distributed tracing
- **Production Ready**: Easy transition from local development to cloud deployment (Azure Container Apps, Kubernetes)
- **Dashboard Features**: Real-time monitoring, console logs, distributed tracing, metrics, GitHub Copilot integration
- **Backward Compatibility**: Aspire 9.x works with .NET 8.0 LTS while providing latest features

**Alternatives Considered**:
1. **Docker Compose**:
   - Rejected: Less integrated with .NET ecosystem, manual service discovery, limited observability
2. **Standalone Services**:
   - Rejected: Requires manual configuration management, no built-in observability
3. **Tye (predecessor to Aspire)**:
   - Rejected: Deprecated in favor of .NET Aspire
4. **.NET Aspire 8.2** (previous stable):
   - Rejected: 9.2.1 provides enhanced AWS/Azure integration, improved testing, and "Components renamed to Integrations"

**Implementation Impact**:
- Requires .NET 8.0 SDK (version 8.0.100 or later)
- Adds AppHost project (orchestration entry point)
- Adds ServiceDefaults project (shared configuration for OpenTelemetry, health checks, service discovery, HTTP resilience)
- Docker Desktop required for PostgreSQL container
- Aspire dashboard runs on https://localhost:17275 during development

---

### Decision 2: Blazor Server vs Blazor WebAssembly

**Decision**: Use Blazor Server for the frontend

**Rationale**:
- **Real-time Updates**: SignalR built-in, perfect for live task updates across users
- **Simpler State Management**: Server-side state is easier to manage than client-side
- **No API Authentication Needed**: Direct server access eliminates auth complexity for initial phase
- **Faster Initial Load**: No large WebAssembly download required
- **Full .NET API Access**: Direct access to all .NET libraries without browser limitations

**Alternatives Considered**:
1. **Blazor WebAssembly**:
   - Rejected: Requires separate API authentication, larger initial download, more complex state sync
2. **React/Vue with .NET API**:
   - Rejected: Adds JavaScript ecosystem complexity, team needs to know two stacks
3. **ASP.NET MVC with JavaScript**:
   - Rejected: Less interactive, requires more client-side JavaScript for drag-and-drop

**Implementation Impact**: SignalR circuits require server resources (500MB memory budget per instance), reconnection handling needed

---

### Decision 3: PostgreSQL Database

**Decision**: Use PostgreSQL 16 as the primary database

**Rationale**:
- **Relational Model**: Task management naturally fits relational schema (projects → tasks → comments)
- **ACID Guarantees**: Comments and task updates need transaction support
- **JSON Support**: Flexible for future extensibility without schema changes
- **Performance**: Excellent query performance with proper indexing
- **EF Core Support**: First-class support in Entity Framework Core
- **Open Source**: No licensing costs, strong community

**Alternatives Considered**:
1. **SQL Server**:
   - Rejected: Licensing costs, Windows bias (though cross-platform available)
2. **SQLite**:
   - Rejected: Not suitable for concurrent writes from multiple Blazor sessions
3. **MongoDB (NoSQL)**:
   - Rejected: Relational model is natural fit, ACID transactions needed
4. **Cosmos DB**:
   - Rejected: Overkill for 5 users, cost prohibitive for testing phase

**Implementation Impact**: Requires Docker container for local dev, migrations via EF Core

---

### Decision 4: REST API Architecture

**Decision**: Implement three separate REST API controllers (Projects, Tasks, Notifications)

**Rationale**:
- **Separation of Concerns**: Each controller handles one domain aggregate
- **Independent Scalability**: Can scale each API independently in future
- **Clear Contracts**: OpenAPI spec per controller simplifies client generation
- **Testing**: Easier to test and mock individual controllers
- **Spec Alignment**: Directly maps to requirement FR-005 through FR-027

**Alternatives Considered**:
1. **Single Unified API Controller**:
   - Rejected: Violates single responsibility, harder to test
2. **GraphQL**:
   - Rejected: Unnecessary complexity for simple CRUD, over-fetching not a concern with 45 tasks
3. **gRPC**:
   - Rejected: Binary protocol overkill, REST more widely understood

**Implementation Impact**: Three OpenAPI specs, three controller classes, shared repository pattern

---

### Decision 5: Drag-and-Drop Implementation

**Decision**: HTML5 Drag and Drop API with JavaScript interop

**Rationale**:
- **Browser Native**: No third-party library dependencies
- **Accessibility**: Can be enhanced with keyboard navigation
- **Blazor Interop**: Clean integration via IJSRuntime
- **Performance**: Native browser API is fastest option
- **Touch Support**: Works on mobile devices with polyfill

**Alternatives Considered**:
1. **Third-party Blazor Component (e.g., Radzen, MudBlazor)**:
   - Rejected: Adds dependency, reduces control over behavior
2. **SortableJS with Blazor wrapper**:
   - Rejected: Extra JS library, more complex than needed
3. **Pure C# Implementation (no drag-drop, click to move)**:
   - Rejected: Poor UX compared to drag-and-drop

**Implementation Impact**: Requires small JS file (~50 lines), DragContainer and DropZone Blazor components

---

## Architectural Patterns

### Decision 6: Repository Pattern

**Decision**: Implement Repository pattern for data access

**Rationale**:
- **Testability**: Easy to mock repositories for unit tests
- **Separation of Concerns**: Business logic separated from data access
- **Query Encapsulation**: Complex queries hidden behind simple methods
- **EF Core Abstraction**: Can swap ORM in future if needed

**Alternatives Considered**:
1. **Direct DbContext Usage in Controllers**:
   - Rejected: Tight coupling, hard to test, violates SRP
2. **Specification Pattern**:
   - Rejected: Overkill for simple CRUD operations
3. **CQRS**:
   - Rejected: Unnecessary complexity for read-write parity use case

**Implementation Impact**: Repository interfaces and implementations for each entity

---

### Decision 7: Service Layer

**Decision**: Implement Service layer between Controllers and Repositories

**Rationale**:
- **Business Logic**: Task assignment validation, comment ownership checks
- **Transaction Management**: Multi-repository operations (e.g., task + notification)
- **API/UI Reusability**: Same services used by both REST API and Blazor components
- **Testing**: Services testable independently of HTTP/SignalR concerns

**Alternatives Considered**:
1. **Fat Controllers (no service layer)**:
   - Rejected: Business logic in controllers is hard to reuse and test
2. **Domain-Driven Design (DDD) with Aggregates**:
   - Rejected: Over-engineered for simple CRUD application

**Implementation Impact**: Service classes for Project, Task, Notification domains

---

## Testing Strategy

### Decision 8: xUnit + bUnit + Testcontainers

**Decision**:
- xUnit for unit and integration tests
- bUnit for Blazor component tests
- Testcontainers for database integration tests

**Rationale**:
- **xUnit**: Most popular .NET test framework, clean syntax, parallel execution
- **bUnit**: Purpose-built for Blazor, renders components in test context
- **Testcontainers**: Real PostgreSQL in Docker for integration tests, no mocking DB

**Alternatives Considered**:
1. **NUnit**:
   - Rejected: Less popular in modern .NET, fewer ecosystem tools
2. **MSTest**:
   - Rejected: Less feature-rich than xUnit
3. **In-Memory Database (EF Core)**:
   - Rejected: Not accurate representation of PostgreSQL behavior
4. **Manual DB Setup**:
   - Rejected: Slow, not isolated between test runs

**Implementation Impact**: NuGet packages for xUnit, bUnit, Testcontainers.PostgreSQL

---

## Performance Optimization Decisions

### Decision 9: Entity Framework Core with Compiled Queries

**Decision**: Use EF Core with compiled queries for hot paths

**Rationale**:
- **Performance**: Compiled queries avoid repeated LINQ expression parsing
- **Type Safety**: Maintains C# type checking vs raw SQL
- **Maintainability**: Easier to refactor than string-based SQL

**Alternatives Considered**:
1. **Dapper (micro-ORM)**:
   - Rejected: Loses type safety, more boilerplate for simple CRUD
2. **Raw ADO.NET**:
   - Rejected: Too much boilerplate, no migrations support
3. **EF Core without optimization**:
   - Rejected: May not meet <100ms p95 DB query requirement

**Implementation Impact**: Compiled query definitions in repository base class

---

### Decision 10: Real-time Update Strategy

**Decision**: SignalR broadcast for task updates, no database polling

**Rationale**:
- **Low Latency**: Sub-500ms update propagation vs 5s polling interval
- **Server Resources**: Blazor Server already has SignalR circuits open
- **Simplicity**: No polling infrastructure or state reconciliation needed
- **User Experience**: Immediate feedback on drag-and-drop actions

**Alternatives Considered**:
1. **Database Polling (every 5 seconds)**:
   - Rejected: High latency, unnecessary DB load
2. **WebSockets (separate from SignalR)**:
   - Rejected: Blazor Server already uses SignalR (which uses WebSockets)
3. **Server-Sent Events**:
   - Rejected: One-way only, Blazor Server needs bidirectional

**Implementation Impact**: Notification service broadcasts task update events to all connected clients

---

## Security Decisions

### Decision 11: Input Validation with FluentValidation

**Decision**: Use FluentValidation for all input validation

**Rationale**:
- **Expressive**: Readable validation rules separate from domain models
- **Reusable**: Same validators for API and Blazor forms
- **Comprehensive**: Built-in validators for common patterns
- **Error Messages**: Customizable, user-friendly error messages

**Alternatives Considered**:
1. **Data Annotations Only**:
   - Rejected: Less expressive, tightly couples validation to models
2. **Manual Validation**:
   - Rejected: Boilerplate, error-prone, inconsistent

**Implementation Impact**: Validator classes for each DTO, validation pipeline in API

---

## Development Workflow Decisions

### Decision 12: Database Migrations

**Decision**: EF Core Migrations with seed data for initial users/projects

**Rationale**:
- **Version Control**: Migration files track schema changes over time
- **Reproducibility**: Same schema setup in dev, test, prod
- **Data Seeding**: Initial 5 users and 3 projects created automatically
- **Rollback**: Can undo migrations if needed

**Alternatives Considered**:
1. **SQL Scripts**:
   - Rejected: No automatic generation from C# models
2. **Database-First (scaffold from DB)**:
   - Rejected: Code-first is better for greenfield projects

**Implementation Impact**: Migration commands in development workflow, seed data class

---

### Decision 13: .NET Aspire ServiceDefaults Configuration

**Decision**: Implement ServiceDefaults project with Aspire 9.x patterns for shared configuration

**Version-Specific Considerations**:
- **.NET Aspire 9.x Changes**:
  - Components renamed to "Integrations" (terminology change from 8.x)
  - Enhanced testing support with `IDistributedApplicationTestingBuilder`
  - Improved OpenTelemetry configuration options
  - Service discovery enhancements

**ServiceDefaults Responsibilities**:
1. **OpenTelemetry Configuration**:
   - Logging: Structured logs with correlation IDs
   - Metrics: Custom meters for business metrics
   - Tracing: Distributed tracing across Blazor Server SignalR circuits and REST API calls

2. **Health Checks**:
   - Database connectivity checks (PostgreSQL via `Npgsql.HealthChecks`)
   - SignalR hub health monitoring
   - Custom health checks for task service availability

3. **Service Discovery**:
   - AppHost configures service endpoints
   - ServiceDefaults provides discovery client configuration
   - Blazor Web project uses discovery to locate ApiService

4. **HTTP Client Resilience**:
   - Retry policies with exponential backoff
   - Circuit breaker for API failures
   - Timeout configurations (<200ms target)

**Implementation Pattern** (Aspire 9.x):
```csharp
// ServiceDefaults/Extensions.cs
public static IHostApplicationBuilder AddServiceDefaults(this IHostApplicationBuilder builder)
{
    builder.AddOpenTelemetry();
    builder.AddDefaultHealthChecks();
    builder.Services.AddServiceDiscovery();
    builder.Services.ConfigureHttpClientDefaults(http =>
    {
        http.AddStandardResilienceHandler(); // Aspire 9.x pattern
        http.AddServiceDiscovery();
    });
    return builder;
}
```

**Breaking Changes from Aspire 8.x to 9.x**:
- Namespace changes for integration packages
- Health check registration API changes
- Updated service discovery configuration syntax
- Migration guide available: https://learn.microsoft.com/en-us/dotnet/aspire/get-started/upgrade-to-aspire-9

**Implementation Impact**:
- All projects (AppHost, ApiService, Web) reference ServiceDefaults project
- Unified configuration reduces duplication across services
- Observability enabled by default for all HTTP calls and database queries

---

### Decision 14: .NET Aspire Dashboard Monitoring Strategy

**Decision**: Leverage Aspire dashboard for development monitoring, prepare for Application Insights in production

**Dashboard Capabilities** (.NET Aspire 9.x):
- **Console Logs**: Real-time log streaming from all resources (ApiService, Web, PostgreSQL)
- **Distributed Tracing**: Trace requests across Blazor Server circuits, SignalR hubs, REST API, and database
- **Metrics**: Real-time performance metrics (request duration, error rates, database query times)
- **Resource Management**: Start, stop, restart resources from dashboard UI
- **Environment Variables**: View and modify configuration at runtime
- **GitHub Copilot Integration**: AI-powered assistance for log analysis and troubleshooting (Aspire 9.x feature)

**Development Workflow**:
1. Launch AppHost (`dotnet run` in Taskify.AppHost)
2. Dashboard opens automatically at https://localhost:17275
3. View all services, their endpoints, and health status
4. Monitor logs and traces in real-time during drag-and-drop testing
5. Analyze performance metrics to ensure <200ms API response times

**Production Migration Path** (Phase 2):
- Aspire dashboard is development-only
- Production monitoring via Azure Application Insights or similar APM tool
- OpenTelemetry exports configured in ServiceDefaults support both dashboard (dev) and Application Insights (prod)
- Same telemetry code works in both environments

**Performance Monitoring Targets**:
- API response time: <200ms p95 (visible in dashboard metrics)
- Drag-and-drop operations: <100ms perceived latency (trace view)
- Real-time updates: <500ms propagation (distributed trace across SignalR)
- Database queries: <100ms p95 (EF Core instrumentation)

**Implementation Impact**:
- No additional monitoring infrastructure needed for development
- Production observability requires Azure Application Insights configuration
- Dashboard URL documented in quickstart.md for developer onboarding

---

---

## Version Matrix and Package Specifications

Comprehensive list of all package versions for .NET 8.0 LTS target framework with .NET Aspire 9.x compatibility.

### Core Framework Versions

| Package | Version | Project(s) | Purpose |
|---------|---------|------------|---------|
| .NET SDK | 8.0.100+ | All | Target framework (LTS) |
| .NET Aspire SDK | 9.2.1 | All | Distributed app orchestration |

### Aspire Integration Packages

| Package | Version | Project | Purpose |
|---------|---------|---------|---------|
| Aspire.Hosting | 9.2.1 | AppHost | Core hosting framework |
| Aspire.Hosting.PostgreSQL | 9.2.1 | AppHost | PostgreSQL resource definition |
| Aspire.Npgsql.EntityFrameworkCore.PostgreSQL | 9.2.1 | ApiService | EF Core + PostgreSQL integration |

### Database and ORM Packages

| Package | Version | Project | Purpose |
|---------|---------|---------|---------|
| Microsoft.EntityFrameworkCore | 8.0.x | ApiService | EF Core ORM (LTS) |
| Microsoft.EntityFrameworkCore.Design | 8.0.x | ApiService | Migrations tooling |
| Npgsql.EntityFrameworkCore.PostgreSQL | 8.0.x | ApiService | PostgreSQL provider |
| Npgsql.HealthChecks | 8.0.x | ApiService | Health check integration |

**Version Rationale**: Using EF Core 8.0.x (LTS) instead of 9.0.x (STS) for long-term stability. Aspire 9.x integration packages are compatible with EF Core 8.0.x.

### Validation and API Packages

| Package | Version | Project | Purpose |
|---------|---------|---------|---------|
| FluentValidation | 12.0.0+ | ApiService | Input validation |
| FluentValidation.DependencyInjectionExtensions | 12.0.0+ | ApiService | DI integration |
| Swashbuckle.AspNetCore | 6.8.0+ | ApiService | OpenAPI/Swagger |

**Breaking Change Note**: FluentValidation 12.0+ requires .NET 8.0 minimum. FluentValidation.AspNetCore is deprecated; use manual validation approach.

### Blazor and Frontend Packages

| Package | Version | Project | Purpose |
|---------|---------|---------|---------|
| Microsoft.AspNetCore.Components.Web | 8.0.x | Web | Blazor Server components |
| Microsoft.AspNetCore.SignalR.Client | 8.0.x | Web | SignalR client (built into Blazor Server) |

### Testing Packages

| Package | Version | Project | Purpose |
|---------|---------|---------|---------|
| xunit | 2.8.1 | Tests | Test framework |
| xunit.runner.visualstudio | 2.5.4 | Tests | Visual Studio test runner |
| Microsoft.NET.Test.Sdk | 17.8.0+ | Tests | Test SDK |
| bunit | 1.40.0 | Web.Tests | Blazor component testing |
| bunit.web | 1.40.0 | Web.Tests | Web-specific bUnit extensions |
| FluentAssertions | 6.12.0+ | Tests | Assertion library |
| Moq | 4.20.0+ | Tests | Mocking framework |
| Testcontainers.PostgreSQL | 3.8.0+ | ApiService.Tests | Integration test containers |

**Version Rationale**: bUnit 1.40.0 supports .NET 8.0 well. bUnit 2.x drops support for .NET versions prior to .NET 8.

### OpenTelemetry Packages (via Aspire ServiceDefaults)

| Package | Version | Project | Purpose |
|---------|---------|---------|---------|
| OpenTelemetry.Exporter.OpenTelemetryProtocol | (via Aspire) | All | OTLP exporter for dashboard |
| OpenTelemetry.Extensions.Hosting | (via Aspire) | All | Hosting integration |
| OpenTelemetry.Instrumentation.AspNetCore | (via Aspire) | ApiService, Web | ASP.NET Core instrumentation |
| OpenTelemetry.Instrumentation.Http | (via Aspire) | All | HTTP client instrumentation |
| OpenTelemetry.Instrumentation.Runtime | (via Aspire) | All | .NET runtime metrics |

**Note**: ServiceDefaults project manages OpenTelemetry packages. No direct references needed in application projects.

### Container Images

| Image | Version | Usage | Purpose |
|-------|---------|-------|---------|
| postgres | 16-alpine | Development | PostgreSQL database |
| mcr.microsoft.com/dotnet/aspnet | 8.0-alpine | Production | ASP.NET Core runtime |
| mcr.microsoft.com/dotnet/sdk | 8.0-alpine | Build | .NET SDK for builds |

### Version Compatibility Matrix

| .NET Version | Aspire Version | EF Core Version | Compatibility |
|--------------|----------------|-----------------|---------------|
| .NET 8.0 LTS | 9.2.1 | 8.0.x | ✅ Recommended |
| .NET 8.0 LTS | 9.2.1 | 9.0.x | ⚠️ Works but STS |
| .NET 8.0 LTS | 8.2.x | 8.0.x | ✅ Stable but older |
| .NET 9.0 STS | 9.2.1 | 9.0.x | ✅ Latest features |

**Recommendation**: Use .NET 8.0 LTS + Aspire 9.2.1 + EF Core 8.0.x for long-term stability.

### Version Update Strategy

1. **Security Updates**: Apply patch versions immediately (8.0.x → 8.0.y)
2. **Aspire Updates**: Review release notes, test in dev before upgrading
3. **Breaking Changes**: Follow migration guides (linked in Decision 13)
4. **Dependency Scanning**: Run `dotnet list package --vulnerable` weekly
5. **LTS Migration**: Plan migration to .NET 10 LTS (November 2025) in Phase 3

---

## Summary

All technical decisions align with Constitution principles:
- **TDD**: Testing strategy supports 80% unit, 70% integration coverage (xUnit 2.8.1, bUnit 1.40.0)
- **Performance**: Compiled queries + SignalR meet <200ms API, <500ms real-time targets
- **Security**: FluentValidation 12.0+ defensive coding prepares for future auth
- **Code Quality**: Repository/Service patterns keep code modular and testable
- **UX Consistency**: Blazor component library ensures consistent UI patterns
- **Observability**: .NET Aspire 9.2.1 dashboard + OpenTelemetry for comprehensive monitoring
- **Version Stability**: .NET 8.0 LTS + Aspire 9.2.1 + EF Core 8.0.x for long-term support

**Version-Specific Documentation**:
- .NET Aspire 9.x migration guide: https://learn.microsoft.com/en-us/dotnet/aspire/get-started/upgrade-to-aspire-9
- EF Core 8.0 documentation: https://learn.microsoft.com/en-us/ef/core/
- FluentValidation 12.0 upgrade guide: https://docs.fluentvalidation.net/en/latest/upgrading-to-12.html

**Next Phase**: Generate data models, API contracts, and quickstart documentation using version-specific patterns
