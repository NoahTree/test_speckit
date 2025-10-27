# Data Model: Create Taskify

**Feature**: Create Taskify | **Date**: 2025-10-23
**Database**: PostgreSQL 16 | **ORM**: Entity Framework Core 8.0

## Entity Relationship Diagram

```
┌──────────┐         ┌─────────────┐         ┌──────────┐
│   User   │         │   Project   │         │   Task   │
├──────────┤         ├─────────────┤         ├──────────┤
│ Id (PK)  │         │ Id (PK)     │         │ Id (PK)  │
│ Name     │◄───────►│ Name        │◄───────►│ ProjectId│
│ Role     │  M:N    │ Description │   1:N   │ Title    │
│ Email    │         │ CreatedAt   │         │ Descr... │
│ CreatedAt│         │ UpdatedAt   │         │ Status   │
└──────────┘         └─────────────┘         │ AssigneeId│
                                             │ CreatedAt│
                                             │ UpdatedAt│
                                             └────┬─────┘
                                                  │ 1:N
                                                  ▼
                                             ┌──────────┐
                                             │ Comment  │
                                             ├──────────┤
                                             │ Id (PK)  │
                                             │ TaskId   │
                                             │ AuthorId │
                                             │ Text     │
                                             │ CreatedAt│
                                             │ UpdatedAt│
                                             └──────────┘
```

## Entities

### User

Represents a team member (Product Manager or Engineer).

**Table**: `users`

| Column    | Type          | Constraints                   | Description                          |
|-----------|---------------|-------------------------------|--------------------------------------|
| Id        | UUID          | PRIMARY KEY                   | Unique user identifier               |
| Name      | VARCHAR(100)  | NOT NULL                      | User's full name                     |
| Role      | VARCHAR(50)   | NOT NULL                      | "Product Manager" or "Engineer"      |
| Email     | VARCHAR(255)  | NOT NULL, UNIQUE              | User email (unique identifier)       |
| CreatedAt | TIMESTAMP     | NOT NULL, DEFAULT NOW()       | Record creation timestamp            |

**Indexes**:
- `idx_users_email` (UNIQUE) on `Email`

**Validation Rules**:
- Name: 1-100 characters, required
- Role: Must be exactly "Product Manager" or "Engineer"
- Email: Valid email format, required, unique

**Seed Data** (5 predefined users):
1. Sarah Chen (PM) - sarah.chen@taskify.local
2. Alex Rodriguez (Engineer) - alex.rodriguez@taskify.local
3. Jamie Patel (Engineer) - jamie.patel@taskify.local
4. Morgan Kim (Engineer) - morgan.kim@taskify.local
5. Taylor Johnson (Engineer) - taylor.johnson@taskify.local

---

### Project

Represents a work initiative containing multiple tasks.

**Table**: `projects`

| Column      | Type          | Constraints             | Description                    |
|-------------|---------------|-------------------------|--------------------------------|
| Id          | UUID          | PRIMARY KEY             | Unique project identifier      |
| Name        | VARCHAR(200)  | NOT NULL                | Project name                   |
| Description | TEXT          | NULL                    | Project description (optional) |
| CreatedAt   | TIMESTAMP     | NOT NULL, DEFAULT NOW() | Record creation timestamp      |
| UpdatedAt   | TIMESTAMP     | NOT NULL, DEFAULT NOW() | Last update timestamp          |

**Indexes**:
- `idx_projects_created_at` on `CreatedAt` (for sorting)

**Validation Rules**:
- Name: 1-200 characters, required
- Description: 0-2000 characters, optional

**Seed Data** (3 sample projects):
1. "Website Redesign" - "Modernize company website with new design system"
2. "Mobile App Launch" - "Develop and release iOS and Android applications"
3. "Backend Migration" - "Migrate legacy services to microservices architecture"

---

### Task

Represents a work item with title, description, status, and assignee.

**Table**: `tasks`

| Column      | Type          | Constraints                          | Description                           |
|-------------|---------------|--------------------------------------|---------------------------------------|
| Id          | UUID          | PRIMARY KEY                          | Unique task identifier                |
| ProjectId   | UUID          | NOT NULL, FOREIGN KEY → projects(Id) | Parent project                        |
| Title       | VARCHAR(500)  | NOT NULL                             | Task title                            |
| Description | TEXT          | NULL                                 | Detailed description (optional)       |
| Status      | VARCHAR(50)   | NOT NULL                             | "To Do", "In Progress", "In Review", "Done" |
| AssignedTo  | UUID          | NULL, FOREIGN KEY → users(Id)        | Assigned user (optional)              |
| CreatedAt   | TIMESTAMP     | NOT NULL, DEFAULT NOW()              | Record creation timestamp             |
| UpdatedAt   | TIMESTAMP     | NOT NULL, DEFAULT NOW()              | Last update timestamp                 |

**Indexes**:
- `idx_tasks_project_id` on `ProjectId` (for filtering by project)
- `idx_tasks_assigned_to` on `AssignedTo` (for filtering by user)
- `idx_tasks_status` on `Status` (for Kanban column queries)
- Composite: `idx_tasks_project_status` on `(ProjectId, Status)` (for Kanban board query)

**Validation Rules**:
- Title: 1-500 characters, required
- Description: 0-5000 characters, optional
- Status: Must be one of: "To Do", "In Progress", "In Review", "Done"
- ProjectId: Must reference existing project
- AssignedTo: Must reference existing user if provided

**Business Rules**:
- Tasks can exist without assignee (AssignedTo NULL)
- Status transitions are unrestricted (any status → any status)
- Deleting a project cascades to delete all tasks

**Seed Data Distribution** (per requirement FR-018, FR-019):
- Each project: 5-15 tasks randomly distributed
- Each status column: minimum 1 task per project
- Example distribution for "Website Redesign":
  - To Do: 3 tasks
  - In Progress: 2 tasks
  - In Review: 2 tasks
  - Done: 4 tasks
  - Total: 11 tasks

---

### Comment

Represents a discussion entry on a task.

**Table**: `comments`

| Column     | Type         | Constraints                        | Description                          |
|------------|--------------|-----------------------------------|--------------------------------------|
| Id         | UUID         | PRIMARY KEY                        | Unique comment identifier            |
| TaskId     | UUID         | NOT NULL, FOREIGN KEY → tasks(Id)  | Parent task                          |
| AuthorId   | UUID         | NOT NULL, FOREIGN KEY → users(Id)  | Comment author                       |
| Text       | TEXT         | NOT NULL                           | Comment text                         |
| CreatedAt  | TIMESTAMP    | NOT NULL, DEFAULT NOW()            | Original creation timestamp          |
| UpdatedAt  | TIMESTAMP    | NOT NULL, DEFAULT NOW()            | Last edit timestamp                  |

**Indexes**:
- `idx_comments_task_id` on `TaskId` (for fetching task comments)
- `idx_comments_created_at` on `CreatedAt` (for chronological ordering)

**Validation Rules**:
- Text: 1-2000 characters, required
- TaskId: Must reference existing task
- AuthorId: Must reference existing user

**Business Rules**:
- Comments cannot be edited by users other than the author (enforced in service layer)
- Comments cannot be deleted by users other than the author (enforced in service layer)
- CreatedAt never changes; UpdatedAt changes on edit
- Deleting a task cascades to delete all comments

---

## State Transitions

### Task Status Workflow

```
┌─────────┐
│ To Do   │
└────┬────┘
     │
     ▼
┌─────────────┐
│ In Progress │
└────┬────────┘
     │
     ▼
┌───────────┐
│ In Review │
└────┬──────┘
     │
     ▼
┌──────┐
│ Done │
└──────┘
```

**Allowed Transitions**: Any status → Any status (unrestricted for flexibility)

**Rationale**: Initial phase focuses on basic Kanban functionality. Future phases may add workflow validation (e.g., "To Do" → "Done" requires approval).

---

## Database Schema Creation

### EF Core Migrations

**Migration Name**: `InitialCreate`

**Up Script** (generated by EF Core):
```sql
CREATE TABLE users (
    id UUID PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    role VARCHAR(50) NOT NULL,
    email VARCHAR(255) NOT NULL UNIQUE,
    created_at TIMESTAMP NOT NULL DEFAULT NOW()
);

CREATE TABLE projects (
    id UUID PRIMARY KEY,
    name VARCHAR(200) NOT NULL,
    description TEXT,
    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMP NOT NULL DEFAULT NOW()
);

CREATE TABLE tasks (
    id UUID PRIMARY KEY,
    project_id UUID NOT NULL REFERENCES projects(id) ON DELETE CASCADE,
    title VARCHAR(500) NOT NULL,
    description TEXT,
    status VARCHAR(50) NOT NULL,
    assigned_to UUID REFERENCES users(id) ON DELETE SET NULL,
    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMP NOT NULL DEFAULT NOW()
);

CREATE TABLE comments (
    id UUID PRIMARY KEY,
    task_id UUID NOT NULL REFERENCES tasks(id) ON DELETE CASCADE,
    author_id UUID NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    text TEXT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMP NOT NULL DEFAULT NOW()
);

-- Indexes
CREATE UNIQUE INDEX idx_users_email ON users(email);
CREATE INDEX idx_projects_created_at ON projects(created_at);
CREATE INDEX idx_tasks_project_id ON tasks(project_id);
CREATE INDEX idx_tasks_assigned_to ON tasks(assigned_to);
CREATE INDEX idx_tasks_status ON tasks(status);
CREATE INDEX idx_tasks_project_status ON tasks(project_id, status);
CREATE INDEX idx_comments_task_id ON comments(task_id);
CREATE INDEX idx_comments_created_at ON comments(created_at);
```

---

## Data Transfer Objects (DTOs)

### UserDto
```csharp
public record UserDto(
    Guid Id,
    string Name,
    string Role,
    string Email
);
```

### ProjectDto
```csharp
public record ProjectDto(
    Guid Id,
    string Name,
    string? Description,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
```

### ProjectWithTasksDto
```csharp
public record ProjectWithTasksDto(
    Guid Id,
    string Name,
    string? Description,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    IEnumerable<TaskSummaryDto> Tasks
);
```

### TaskDto
```csharp
public record TaskDto(
    Guid Id,
    Guid ProjectId,
    string Title,
    string? Description,
    string Status,
    Guid? AssignedTo,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
```

### TaskSummaryDto (for Kanban board)
```csharp
public record TaskSummaryDto(
    Guid Id,
    string Title,
    string Status,
    Guid? AssignedTo,
    string? AssigneeName,
    int CommentCount
);
```

### TaskDetailsDto (for task card)
```csharp
public record TaskDetailsDto(
    Guid Id,
    Guid ProjectId,
    string ProjectName,
    string Title,
    string? Description,
    string Status,
    Guid? AssignedTo,
    string? AssigneeName,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    IEnumerable<CommentDto> Comments
);
```

### CommentDto
```csharp
public record CommentDto(
    Guid Id,
    Guid TaskId,
    Guid AuthorId,
    string AuthorName,
    string Text,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    bool CanEdit,  // true if current user is author
    bool CanDelete // true if current user is author
);
```

### CreateTaskRequest
```csharp
public record CreateTaskRequest(
    Guid ProjectId,
    string Title,
    string? Description,
    string Status,
    Guid? AssignedTo
);
```

### UpdateTaskRequest
```csharp
public record UpdateTaskRequest(
    string Title,
    string? Description,
    string Status,
    Guid? AssignedTo
);
```

### CreateCommentRequest
```csharp
public record CreateCommentRequest(
    Guid TaskId,
    string Text
);
```

### UpdateCommentRequest
```csharp
public record UpdateCommentRequest(
    string Text
);
```

---

## Performance Considerations

### Query Optimization

**Kanban Board Query** (hot path):
```csharp
// Compiled query for Kanban board
var tasks = await _context.Tasks
    .Where(t => t.ProjectId == projectId)
    .Include(t => t.AssignedUser)
    .Select(t => new TaskSummaryDto(
        t.Id,
        t.Title,
        t.Status,
        t.AssignedTo,
        t.AssignedUser != null ? t.AssignedUser.Name : null,
        t.Comments.Count()
    ))
    .AsNoTracking()  // Read-only, no change tracking overhead
    .ToListAsync();
```

**Performance Targets**:
- Kanban board load: <100ms p95 (25-50 tasks with joins)
- Task details with comments: <150ms p95 (includes 1:N comment relation)
- Task update: <50ms p95 (single row update)
- Comment creation: <50ms p95 (single row insert)

### Caching Strategy

**Not Implemented in Phase 1** - Database query performance sufficient for 5 users and 45 tasks.

**Future Consideration** (Phase 2+): Redis cache for Kanban board queries when concurrent users exceed 50.

---

## Seed Data Summary

**Users**: 5 (1 PM, 4 Engineers) - hardcoded in migration
**Projects**: 3 - hardcoded in migration
**Tasks**: 27-45 total (5-15 per project) - randomized seed with constraints:
- Each project must have at least 1 task in each status column
- Remaining tasks distributed randomly
- Some tasks assigned to users, some unassigned
**Comments**: 0 initially (created by users during testing)

---

## Database Backups & Recovery

**Local Development**: No backups (Docker volume, recreate via migrations)
**Production** (Phase 2+): PostgreSQL automated backups (pg_dump daily, point-in-time recovery)

---

This data model satisfies all functional requirements (FR-001 through FR-027) and supports all success criteria (SC-001 through SC-007).
