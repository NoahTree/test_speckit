# Quickstart Guide: Create Taskify

**Feature**: Create Taskify | **Date**: 2025-10-23
**Purpose**: Get Taskify running locally in under 10 minutes

## Prerequisites

Ensure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (version 8.0.100 or later)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (for PostgreSQL)
- [Git](https://git-scm.com/downloads)
- IDE: [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/) with [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)

**Verify installations**:
```bash
dotnet --version  # Should be 8.0.x or higher
docker --version  # Should be 20.x or higher
git --version     # Any recent version
```

---

## Quick Start (5 minutes)

### 1. Clone the Repository

```bash
git clone <repository-url>
cd taskify
git checkout 001-create-taskify
```

### 2. Start PostgreSQL

```bash
docker run --name taskify-postgres \
  -e POSTGRES_PASSWORD=taskify-dev-password \
  -e POSTGRES_DB=taskify \
  -p 5432:5432 \
  -d postgres:16-alpine
```

**Verify PostgreSQL is running**:
```bash
docker ps | grep taskify-postgres
```

### 3. Set User Secrets (Development)

```bash
cd Taskify.ApiService
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=taskify;Username=postgres;Password=taskify-dev-password"
cd ..
```

### 4. Run Database Migrations

```bash
cd Taskify.ApiService
dotnet ef database update
cd ..
```

This creates tables and seeds:
- ✅ 5 users (1 PM, 4 Engineers)
- ✅ 3 projects
- ✅ 27-45 tasks (5-15 per project)
- ✅ At least 1 task per status column per project

### 5. Run the Application

**Option A: Visual Studio 2022**
1. Open `Taskify.sln`
2. Set `Taskify.AppHost` as startup project
3. Press F5 (Debug) or Ctrl+F5 (Run without debugging)

**Option B: Command Line**
```bash
cd Taskify.AppHost
dotnet run
```

**Option C: Visual Studio Code**
1. Open workspace root
2. Press F5 (will use `.vscode/launch.json` configuration)

### 6. Open the Application

The .NET Aspire dashboard will open automatically at: **https://localhost:17275**

From the dashboard:
- **Taskify.Web** (Blazor UI): Click "View" button → opens `https://localhost:7124`
- **Taskify.ApiService** (REST API): Click "View" button → opens `https://localhost:7001/swagger`

### 7. Test the Application

1. **User Selection**: Click on a user (e.g., "Sarah Chen")
2. **Project List**: See 3 projects, click on "Website Redesign"
3. **Kanban Board**: See 4 columns with tasks
4. **Drag and Drop**: Drag a task from "To Do" to "In Progress"
5. **Task Details**: Click a task card to view details
6. **Comments**: Add a comment, edit it, delete it

---

## Project Structure

```
taskify/
├── Taskify.sln                       # Solution file
├── Taskify.AppHost/                  # .NET Aspire orchestration
│   └── Program.cs                    # Aspire configuration
├── Taskify.ServiceDefaults/          # Shared Aspire defaults
│   └── Extensions.cs                 # Common service extensions
├── Taskify.ApiService/               # REST API
│   ├── Controllers/                  # API endpoints
│   ├── Services/                     # Business logic
│   ├── Repositories/                 # Data access
│   ├── Models/                       # Entities + DTOs
│   ├── Data/                         # DbContext + Migrations
│   └── Program.cs                    # API startup
├── Taskify.Web/                      # Blazor Server UI
│   ├── Components/                   # Razor components
│   │   ├── Pages/                    # Page components
│   │   ├── Shared/                   # Reusable components
│   │   └── DragDrop/                 # Drag-drop components
│   ├── Services/                     # API client + state
│   └── Program.cs                    # Blazor startup
└── tests/                            # Test projects
    ├── Taskify.ApiService.Tests/     # API tests
    └── Taskify.Web.Tests/            # UI tests
```

---

## Development Workflow

### Running Tests

**Unit Tests**:
```bash
dotnet test --filter Category=Unit
```

**Integration Tests** (requires PostgreSQL):
```bash
dotnet test --filter Category=Integration
```

**All Tests**:
```bash
dotnet test
```

### Database Commands

**Create Migration**:
```bash
cd Taskify.ApiService
dotnet ef migrations add MigrationName
```

**Apply Migrations**:
```bash
dotnet ef database update
```

**Rollback Migration**:
```bash
dotnet ef database update PreviousMigrationName
```

**Drop Database** (⚠️ destructive):
```bash
dotnet ef database drop --force
docker stop taskify-postgres && docker rm taskify-postgres
```

### Code Quality Checks

**Linting**:
```bash
dotnet format --verify-no-changes
```

**Security Scan**:
```bash
dotnet list package --vulnerable
```

**Build**:
```bash
dotnet build --configuration Release
```

---

## Configuration

### Environment Variables

**Development** (via User Secrets):
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=taskify;Username=postgres;Password=taskify-dev-password"
dotnet user-secrets set "Aspire:Dashboard:Enabled" "true"
```

**Production** (via environment variables or Azure Key Vault):
```bash
export ConnectionStrings__DefaultConnection="Host=prod-db;Port=5432;Database=taskify;Username=taskify_user;Password=<secure-password>"
export ASPNETCORE_ENVIRONMENT=Production
```

### Ports

| Service             | HTTP Port | HTTPS Port |
|---------------------|-----------|------------|
| Taskify.Web         | 5124      | 7124       |
| Taskify.ApiService  | 5001      | 7001       |
| Aspire Dashboard    | 17274     | 17275      |
| PostgreSQL          | 5432      | N/A        |

**Changing Ports**: Edit `Taskify.AppHost/Program.cs` or `launchSettings.json` files.

---

## Troubleshooting

### Issue: PostgreSQL connection failed

**Symptom**: `Npgsql.NpgsqlException: Failed to connect to [::1]:5432`

**Solution**:
1. Verify Docker container is running: `docker ps | grep postgres`
2. Check connection string in user secrets
3. Ensure PostgreSQL is listening on port 5432: `docker logs taskify-postgres`

### Issue: Migrations not applied

**Symptom**: `Npgsql.PostgresException: 42P01: relation "tasks" does not exist`

**Solution**:
```bash
cd Taskify.ApiService
dotnet ef database update --verbose
```

### Issue: Blazor Server circuit disconnected

**Symptom**: "Reconnecting..." message in browser, UI frozen

**Solution**:
1. Check browser console for WebSocket errors
2. Restart the application (Ctrl+C, then `dotnet run`)
3. Clear browser cache and reload

### Issue: Aspire dashboard not opening

**Symptom**: Dashboard URL times out

**Solution**:
1. Check if AppHost is running: `dotnet run` in `Taskify.AppHost/`
2. Try accessing dashboard directly: `https://localhost:17275`
3. Disable HTTPS redirect temporarily in `Program.cs`

---

## Next Steps

### Phase 1: Exploration (5 minutes)
- ✅ Browse all 3 projects
- ✅ Drag tasks between columns
- ✅ Add comments to tasks
- ✅ Edit/delete your own comments
- ✅ Observe real-time updates (open 2 browser windows)

### Phase 2: Code Exploration (15 minutes)
- 📖 Read `data-model.md` to understand entities
- 📖 Browse `contracts/*.yaml` for API specifications
- 🔍 Explore `Taskify.ApiService/Controllers/` for REST endpoints
- 🔍 Explore `Taskify.Web/Components/Pages/` for Blazor pages

### Phase 3: Make Changes (30 minutes)
- 🛠️ Add a new project in seed data (`Data/DbContextSeed.cs`)
- 🛠️ Change Kanban column colors (`wwwroot/css/app.css`)
- 🛠️ Add a "Priority" field to tasks (migration + UI)

### Phase 4: Testing (20 minutes)
- ✅ Write a unit test for `TaskService`
- ✅ Write an integration test for Tasks API
- ✅ Write a Blazor component test with bUnit

---

## Resources

### Documentation
- [.NET Aspire Documentation](https://learn.microsoft.com/en-us/dotnet/aspire/)
- [Blazor Server Documentation](https://learn.microsoft.com/en-us/aspnet/core/blazor/)
- [Entity Framework Core Documentation](https://learn.microsoft.com/en-us/ef/core/)
- [PostgreSQL Documentation](https://www.postgresql.org/docs/16/)

### Project Artifacts
- [Feature Specification](./spec.md)
- [Implementation Plan](./plan.md)
- [Research Decisions](./research.md)
- [Data Model](./data-model.md)
- [API Contracts](./contracts/)

### Tools
- [Aspire Dashboard](https://localhost:17275) - Service orchestration
- [Swagger UI](https://localhost:7001/swagger) - API testing
- [pgAdmin](https://www.pgadmin.org/) - PostgreSQL GUI (optional)

---

## Getting Help

### Common Commands Reference

```bash
# Start fresh
docker stop taskify-postgres && docker rm taskify-postgres
docker run --name taskify-postgres -e POSTGRES_PASSWORD=taskify-dev-password -e POSTGRES_DB=taskify -p 5432:5432 -d postgres:16-alpine
cd Taskify.ApiService && dotnet ef database update && cd ..
cd Taskify.AppHost && dotnet run

# Run tests
dotnet test

# Format code
dotnet format

# Check security
dotnet list package --vulnerable

# View logs
docker logs taskify-postgres
cd Taskify.AppHost && dotnet run --verbose
```

### Performance Monitoring

.NET Aspire dashboard provides:
- ✅ Service health checks
- ✅ HTTP request tracing
- ✅ Database query performance
- ✅ Memory/CPU usage
- ✅ Distributed tracing (OpenTelemetry)

Access at: **https://localhost:17275**

---

**Estimated Setup Time**: 5-10 minutes
**Estimated Learning Time**: 1-2 hours to understand full architecture

Happy coding! 🚀
