# Tasks: Create Taskify

**Input**: Design documents from `/specs/001-create-taskify/`
**Prerequisites**: plan.md, spec.md, data-model.md, research.md, contracts/, quickstart.md

**Tests**: Following TDD approach per constitution - all tests written FIRST before implementation

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (e.g., US1, US2, US3, US4)
- Include exact file paths in descriptions

## Path Conventions

Based on plan.md structure:
- **AppHost**: `Taskify.AppHost/` - .NET Aspire orchestration
- **ServiceDefaults**: `Taskify.ServiceDefaults/` - Shared Aspire configuration
- **API Service**: `Taskify.ApiService/` - REST API backend
- **Blazor Web**: `Taskify.Web/` - Blazor Server frontend
- **Tests**: `tests/Taskify.ApiService.Tests/` and `tests/Taskify.Web.Tests/`

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and .NET Aspire solution structure

- [ ] T001 Create solution file Taskify.sln with .NET 8.0 SDK
- [ ] T002 Create Taskify.AppHost project with .NET Aspire 9.2.1 reference
- [ ] T003 [P] Create Taskify.ServiceDefaults project for shared Aspire configuration
- [ ] T004 [P] Create Taskify.ApiService project with ASP.NET Core 8.0 Web API template
- [ ] T005 [P] Create Taskify.Web project with Blazor Server template
- [ ] T006 [P] Create tests/Taskify.ApiService.Tests project with xUnit 2.8.1
- [ ] T007 [P] Create tests/Taskify.Web.Tests project with bUnit 1.40.0
- [ ] T008 Configure .gitignore for .NET projects (bin/, obj/, .vs/, .idea/)
- [ ] T009 Add NuGet package references per research.md version matrix to all projects
- [ ] T010 [P] Configure AppHost Program.cs with PostgreSQL resource and service references
- [ ] T011 [P] Implement ServiceDefaults/Extensions.cs with OpenTelemetry, health checks, service discovery
- [ ] T012 [P] Configure launchSettings.json for all projects with correct ports (API: 7001, Web: 7124, Dashboard: 17275)
- [ ] T013 Start PostgreSQL container using docker run per quickstart.md
- [ ] T014 Configure User Secrets for connection string in Taskify.ApiService

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

### Database Foundation

- [ ] T015 Create User entity in Taskify.ApiService/Models/Entities/User.cs (Id, Name, Role, Email, CreatedAt)
- [ ] T016 [P] Create Project entity in Taskify.ApiService/Models/Entities/Project.cs (Id, Name, Description, CreatedAt, UpdatedAt)
- [ ] T017 [P] Create Task entity in Taskify.ApiService/Models/Entities/Task.cs (Id, ProjectId, Title, Description, Status, AssignedTo, CreatedAt, UpdatedAt)
- [ ] T018 [P] Create Comment entity in Taskify.ApiService/Models/Entities/Comment.cs (Id, TaskId, AuthorId, Text, CreatedAt, UpdatedAt)
- [ ] T019 Create TaskifyDbContext in Taskify.ApiService/Data/TaskifyDbContext.cs with all entities configured
- [ ] T020 Configure entity relationships and indexes per data-model.md in TaskifyDbContext
- [ ] T021 Create initial migration InitialCreate using dotnet ef migrations add
- [ ] T022 Create DbContextSeed.cs with seed data: 5 users (1 PM, 4 Engineers), 3 projects, 27-45 tasks (5-15 per project, min 1 per column)
- [ ] T023 Apply migration using dotnet ef database update and verify seed data

### API Infrastructure

- [ ] T024 Create base Repository interface in Taskify.ApiService/Repositories/IRepository.cs
- [ ] T025 [P] Create ProjectRepository in Taskify.ApiService/Repositories/ProjectRepository.cs
- [ ] T026 [P] Create TaskRepository in Taskify.ApiService/Repositories/TaskRepository.cs
- [ ] T027 [P] Create CommentRepository in Taskify.ApiService/Repositories/CommentRepository.cs
- [ ] T028 [P] Create UserRepository in Taskify.ApiService/Repositories/UserRepository.cs
- [ ] T029 Configure dependency injection for repositories in Program.cs
- [ ] T030 [P] Create DTOs in Taskify.ApiService/Models/DTOs/: UserDto.cs, ProjectDto.cs, TaskDto.cs, CommentDto.cs per data-model.md
- [ ] T031 [P] Create request DTOs: UpdateTaskRequest.cs, CreateCommentRequest.cs, UpdateCommentRequest.cs
- [ ] T032 [P] Setup FluentValidation 12.0+ validators in Taskify.ApiService/Validation/ for all request DTOs
- [ ] T033 Configure Swagger/OpenAPI in Program.cs with API documentation
- [ ] T034 [P] Setup global exception handling middleware in Taskify.ApiService
- [ ] T035 [P] Configure CORS policy for Blazor Web frontend in Program.cs

### Blazor Web Infrastructure

- [ ] T036 Create Taskify.Web/Services/ApiClient.cs with HttpClient for API communication using service discovery
- [ ] T037 [P] Create Taskify.Web/Services/StateService.cs for managing current user state
- [ ] T038 [P] Create Taskify.Web/Components/Layout/MainLayout.razor with basic structure
- [ ] T039 [P] Create Taskify.Web/Components/Shared/LoadingSpinner.razor component
- [ ] T040 Configure Taskify.Web Program.cs with ApiClient and StateService DI registration
- [ ] T041 Add Bootstrap 5 CSS to Taskify.Web/wwwroot/css/app.css for responsive design

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel

---

## Phase 3: User Story 1 - User Selection and Project Navigation (Priority: P1) 🎯 MVP

**Goal**: Enable users to select their identity from 5 predefined team members and navigate to view all 3 available projects without authentication

**Independent Test**: Launch application → see 5 users → select user → see 3 projects → click project → reach Kanban board (verified via navigation)

### Tests for User Story 1 (TDD - Write tests FIRST)

> **NOTE: Write these tests FIRST, ensure they FAIL before implementation**

- [ ] T042 [P] [US1] Unit test for UserRepository.GetAllUsers() in tests/Taskify.ApiService.Tests/Unit/Repositories/UserRepositoryTests.cs
- [ ] T043 [P] [US1] Unit test for ProjectRepository.GetAllProjects() in tests/Taskify.ApiService.Tests/Unit/Repositories/ProjectRepositoryTests.cs
- [ ] T044 [P] [US1] Contract test for GET /api/projects in tests/Taskify.ApiService.Tests/Contract/ProjectsApiContractTests.cs (verify matches projects-api.yaml)
- [ ] T045 [P] [US1] Contract test for GET /api/projects/{projectId} in tests/Taskify.ApiService.Tests/Contract/ProjectsApiContractTests.cs
- [ ] T046 [P] [US1] Integration test for user selection workflow in tests/Taskify.Web.Tests/Integration/UserSelectionTests.cs using bUnit
- [ ] T047 [P] [US1] Integration test for project list display in tests/Taskify.Web.Tests/Integration/ProjectListTests.cs using bUnit
- [ ] T048 [P] [US1] E2E test for complete navigation flow (user select → projects → board) in tests/Taskify.Web.Tests/Integration/E2E/NavigationWorkflowTests.cs

### Implementation for User Story 1

#### API Layer (Projects API)

- [ ] T049 [P] [US1] Create ProjectService in Taskify.ApiService/Services/ProjectService.cs with GetAllProjects() and GetProjectById() methods
- [ ] T050 [US1] Create ProjectsController in Taskify.ApiService/Controllers/ProjectsController.cs implementing GET /api/projects and GET /api/projects/{projectId} per contracts/projects-api.yaml
- [ ] T051 [US1] Add validation and error handling to ProjectsController (404 for not found)
- [ ] T052 [US1] Add structured logging for project operations in ProjectService

#### Blazor UI Components

- [ ] T053 [P] [US1] Create UserSelection.razor page in Taskify.Web/Components/Pages/ displaying 5 users with role badges
- [ ] T054 [P] [US1] Create ProjectList.razor page in Taskify.Web/Components/Pages/ displaying 3 projects as cards
- [ ] T055 [US1] Implement user selection logic in UserSelection.razor to set StateService.CurrentUser
- [ ] T056 [US1] Implement project navigation logic in ProjectList.razor to route to KanbanBoard page with projectId parameter
- [ ] T057 [US1] Create Index.razor redirecting to UserSelection.razor as app entry point
- [ ] T058 [US1] Style user cards with distinct visual design for PM vs Engineer roles in app.css
- [ ] T059 [US1] Style project cards with hover effects and click affordance in app.css

**Checkpoint**: At this point, User Story 1 should be fully functional - users can select identity and navigate to projects

---

## Phase 4: User Story 2 - Kanban Board Task Management (Priority: P2)

**Goal**: Display all tasks organized in 4 Kanban columns (To Do, In Progress, In Review, Done) and enable drag-and-drop movement between columns with visual distinction for assigned tasks

**Independent Test**: Select user → open project → see 4 columns with tasks → drag task between columns → verify status change persists → see own tasks highlighted

### Tests for User Story 2 (TDD - Write tests FIRST)

> **NOTE: Write these tests FIRST, ensure they FAIL before implementation**

- [ ] T060 [P] [US2] Unit test for TaskRepository.GetTasksByProjectId() in tests/Taskify.ApiService.Tests/Unit/Repositories/TaskRepositoryTests.cs
- [ ] T061 [P] [US2] Unit test for TaskService.UpdateTaskStatus() in tests/Taskify.ApiService.Tests/Unit/Services/TaskServiceTests.cs
- [ ] T062 [P] [US2] Contract test for GET /api/projects/{projectId}/with-tasks in tests/Taskify.ApiService.Tests/Contract/ProjectsApiContractTests.cs (verify matches projects-api.yaml)
- [ ] T063 [P] [US2] Contract test for PATCH /api/tasks/{taskId} in tests/Taskify.ApiService.Tests/Contract/TasksApiContractTests.cs (verify matches tasks-api.yaml)
- [ ] T064 [P] [US2] Integration test for Kanban board rendering in tests/Taskify.Web.Tests/Integration/KanbanBoardTests.cs using bUnit
- [ ] T065 [P] [US2] Integration test for drag-and-drop task movement in tests/Taskify.Web.Tests/Integration/DragDropTests.cs using bUnit + JSInterop mocking
- [ ] T066 [P] [US2] Integration test for task highlighting based on current user in tests/Taskify.Web.Tests/Integration/TaskHighlightTests.cs
- [ ] T067 [P] [US2] Integration test with Testcontainers for task status update persistence in tests/Taskify.ApiService.Tests/Integration/Database/TaskStatusPersistenceTests.cs

### Implementation for User Story 2

#### API Layer (Tasks API - Read & Update)

- [ ] T068 [P] [US2] Create TaskService in Taskify.ApiService/Services/TaskService.cs with GetTasksByProject() and UpdateTask() methods
- [ ] T069 [US2] Implement GET /api/projects/{projectId}/with-tasks in ProjectsController returning ProjectWithTasksDto per contracts/projects-api.yaml
- [ ] T070 [P] [US2] Create TasksController in Taskify.ApiService/Controllers/TasksController.cs implementing PATCH /api/tasks/{taskId} per contracts/tasks-api.yaml
- [ ] T071 [US2] Add FluentValidation for UpdateTaskRequest (status must be one of 4 valid values)
- [ ] T072 [US2] Add error handling for task not found (404) and invalid status (400) in TasksController
- [ ] T073 [US2] Add structured logging for task status changes in TaskService

#### Drag-and-Drop Infrastructure

- [ ] T074 [P] [US2] Create dragdrop.js in Taskify.Web/wwwroot/js/ implementing HTML5 Drag and Drop API (dragstart, dragover, drop events)
- [ ] T075 [P] [US2] Create DragContainer.razor in Taskify.Web/Components/DragDrop/ as wrapper for draggable items
- [ ] T076 [P] [US2] Create DropZone.razor in Taskify.Web/Components/DragDrop/ for column drop targets
- [ ] T077 [US2] Implement JavaScript interop in DragContainer and DropZone for drag-and-drop event handling

#### Blazor Kanban Board

- [ ] T078 [P] [US2] Create TaskCard.razor in Taskify.Web/Components/Shared/ displaying task title, assignee, and comment count
- [ ] T079 [P] [US2] Create KanbanBoard.razor page in Taskify.Web/Components/Pages/ with 4 column layout
- [ ] T080 [US2] Implement OnInitializedAsync in KanbanBoard to fetch ProjectWithTasksDto from ApiClient
- [ ] T081 [US2] Implement task grouping by status column in KanbanBoard (To Do, In Progress, In Review, Done)
- [ ] T082 [US2] Implement OnTaskDropped callback in KanbanBoard to call PATCH /api/tasks/{taskId} via ApiClient
- [ ] T083 [US2] Implement task highlighting logic in TaskCard based on StateService.CurrentUser match with AssignedTo
- [ ] T084 [US2] Style Kanban columns with CSS Grid in app.css (4 equal columns, scrollable)
- [ ] T085 [US2] Style TaskCard with distinct background colors for assigned vs unassigned tasks in app.css
- [ ] T086 [US2] Add drag visual feedback (opacity, cursor) during drag operations in app.css

**Checkpoint**: At this point, User Stories 1 AND 2 should both work independently - full Kanban board with drag-and-drop

---

## Phase 5: User Story 3 - Task Assignment (Priority: P3)

**Goal**: Enable assignment of tasks to any of the 5 predefined users from the task card UI, with assignments persisted and visible

**Independent Test**: Open task card → see assignment dropdown with 5 users → select user → save → verify assignment shown and task highlighted for assigned user

### Tests for User Story 3 (TDD - Write tests FIRST)

> **NOTE: Write these tests FIRST, ensure they FAIL before implementation**

- [ ] T087 [P] [US3] Unit test for TaskService.AssignTask() in tests/Taskify.ApiService.Tests/Unit/Services/TaskServiceTests.cs
- [ ] T088 [P] [US3] Contract test for PATCH /api/tasks/{taskId} with assignedTo field in tests/Taskify.ApiService.Tests/Contract/TasksApiContractTests.cs
- [ ] T089 [P] [US3] Integration test for task assignment UI in tests/Taskify.Web.Tests/Integration/TaskAssignmentTests.cs using bUnit
- [ ] T090 [P] [US3] Integration test with Testcontainers for assignment persistence in tests/Taskify.ApiService.Tests/Integration/Database/TaskAssignmentPersistenceTests.cs

### Implementation for User Story 3

#### API Layer (Task Assignment)

- [ ] T091 [US3] Extend TaskService.UpdateTask() to handle assignedTo changes in Taskify.ApiService/Services/TaskService.cs
- [ ] T092 [US3] Update PATCH /api/tasks/{taskId} in TasksController to accept assignedTo in UpdateTaskRequest
- [ ] T093 [US3] Add validation for assignedTo (must be valid user ID or null) in UpdateTaskRequest validator
- [ ] T094 [US3] Add logging for task assignment changes in TaskService

#### Blazor Task Assignment UI

- [ ] T095 [P] [US3] Create TaskDetailsModal.razor in Taskify.Web/Components/Shared/ as modal dialog for task details
- [ ] T096 [US3] Implement user dropdown in TaskDetailsModal displaying 5 users fetched from GET /api/users (if not cached in StateService)
- [ ] T097 [US3] Implement OnAssignTask callback in TaskDetailsModal to call PATCH /api/tasks/{taskId} with assignedTo
- [ ] T098 [US3] Add "View Details" button to TaskCard that opens TaskDetailsModal
- [ ] T099 [US3] Display current assignee name in TaskCard (if assigned)
- [ ] T100 [US3] Style TaskDetailsModal with backdrop, close button, and form layout in app.css
- [ ] T101 [US3] Add loading spinner during assignment API call in TaskDetailsModal

**Checkpoint**: At this point, User Stories 1, 2, AND 3 should all work independently - full task management with assignment

---

## Phase 6: User Story 4 - Task Commenting and Discussion (Priority: P4)

**Goal**: Enable users to add, edit, and delete their own comments on tasks, with restrictions preventing modification of other users' comments

**Independent Test**: Open task → add comment → see comment appear → edit own comment → delete own comment → verify cannot edit/delete others' comments

### Tests for User Story 4 (TDD - Write tests FIRST)

> **NOTE: Write these tests FIRST, ensure they FAIL before implementation**

- [ ] T102 [P] [US4] Unit test for CommentRepository.GetCommentsByTaskId() in tests/Taskify.ApiService.Tests/Unit/Repositories/CommentRepositoryTests.cs
- [ ] T103 [P] [US4] Unit test for CommentService.CreateComment() in tests/Taskify.ApiService.Tests/Unit/Services/CommentServiceTests.cs
- [ ] T104 [P] [US4] Unit test for CommentService.UpdateComment() with ownership check in tests/Taskify.ApiService.Tests/Unit/Services/CommentServiceTests.cs
- [ ] T105 [P] [US4] Unit test for CommentService.DeleteComment() with ownership check in tests/Taskify.ApiService.Tests/Unit/Services/CommentServiceTests.cs
- [ ] T106 [P] [US4] Contract test for GET /api/tasks/{taskId}/comments in tests/Taskify.ApiService.Tests/Contract/TasksApiContractTests.cs (verify matches tasks-api.yaml)
- [ ] T107 [P] [US4] Contract test for POST /api/tasks/{taskId}/comments in tests/Taskify.ApiService.Tests/Contract/TasksApiContractTests.cs
- [ ] T108 [P] [US4] Contract test for PATCH /api/comments/{commentId} in tests/Taskify.ApiService.Tests/Contract/TasksApiContractTests.cs
- [ ] T109 [P] [US4] Contract test for DELETE /api/comments/{commentId} in tests/Taskify.ApiService.Tests/Contract/TasksApiContractTests.cs
- [ ] T110 [P] [US4] Integration test for comment CRUD operations in tests/Taskify.Web.Tests/Integration/CommentTests.cs using bUnit
- [ ] T111 [P] [US4] Integration test for comment ownership validation (403 error) in tests/Taskify.ApiService.Tests/Integration/Controllers/CommentOwnershipTests.cs

### Implementation for User Story 4

#### API Layer (Comments API)

- [ ] T112 [P] [US4] Create CommentService in Taskify.ApiService/Services/CommentService.cs with CreateComment(), UpdateComment(), DeleteComment(), GetCommentsByTask() methods
- [ ] T113 [US4] Implement comment ownership validation in CommentService (check authorId matches X-Current-User-Id header)
- [ ] T114 [US4] Implement GET /api/tasks/{taskId}/comments in TasksController per contracts/tasks-api.yaml
- [ ] T115 [US4] Implement POST /api/tasks/{taskId}/comments in TasksController per contracts/tasks-api.yaml
- [ ] T116 [P] [US4] Implement PATCH /api/comments/{commentId} in TasksController with 403 Forbidden for non-owners
- [ ] T117 [P] [US4] Implement DELETE /api/comments/{commentId} in TasksController with 403 Forbidden for non-owners
- [ ] T118 [US4] Add FluentValidation for CreateCommentRequest and UpdateCommentRequest (text 1-2000 chars)
- [ ] T119 [US4] Add error handling for comment not found (404), forbidden (403), invalid input (400)
- [ ] T120 [US4] Add structured logging for all comment operations in CommentService
- [ ] T121 [US4] Update GET /api/tasks/{taskId} to include TaskDetailsDto with comments array

#### Blazor Comment UI

- [ ] T122 [P] [US4] Create CommentList.razor in Taskify.Web/Components/Shared/ displaying comments in chronological order
- [ ] T123 [P] [US4] Create CommentForm.razor in Taskify.Web/Components/Shared/ for adding new comments
- [ ] T124 [US4] Implement OnAddComment callback in CommentForm to call POST /api/tasks/{taskId}/comments
- [ ] T125 [US4] Implement edit mode in CommentList for own comments (inline editing with save/cancel)
- [ ] T126 [US4] Implement OnEditComment callback to call PATCH /api/comments/{commentId}
- [ ] T127 [US4] Implement OnDeleteComment callback to call DELETE /api/comments/{commentId} with confirmation dialog
- [ ] T128 [US4] Show edit/delete buttons only for comments where authorId matches StateService.CurrentUser
- [ ] T129 [US4] Integrate CommentList and CommentForm into TaskDetailsModal
- [ ] T130 [US4] Update TaskCard to display comment count badge
- [ ] T131 [US4] Fetch comments when TaskDetailsModal opens via GET /api/tasks/{taskId}
- [ ] T132 [US4] Style comments with author name, timestamp, and action buttons in app.css
- [ ] T133 [US4] Add scrollable container for comment list in TaskDetailsModal in app.css
- [ ] T134 [US4] Add validation feedback for comment text length (1-2000 chars) in CommentForm

**Checkpoint**: All user stories (1-4) should now be independently functional - complete Taskify feature set

---

## Phase 7: Real-time Updates (Cross-Cutting Enhancement)

**Goal**: Enable real-time updates across users using SignalR for task changes and new comments

**Note**: This phase enhances all user stories but can be implemented after core functionality is complete

### Tests for Real-time Updates (TDD)

- [ ] T135 [P] Unit test for NotificationService.BroadcastTaskUpdated() in tests/Taskify.ApiService.Tests/Unit/Services/NotificationServiceTests.cs
- [ ] T136 [P] Integration test for SignalR hub connection in tests/Taskify.Web.Tests/Integration/SignalRTests.cs

### Implementation for Real-time Updates

#### API Layer (SignalR Hub)

- [ ] T137 [P] Create NotificationService in Taskify.ApiService/Services/NotificationService.cs for broadcasting events
- [ ] T138 [P] Create NotificationsHub in Taskify.ApiService/Hubs/NotificationsHub.cs implementing SignalR Hub
- [ ] T139 Configure SignalR in Taskify.ApiService Program.cs with hub mapping
- [ ] T140 Integrate NotificationService.BroadcastTaskUpdated() into TaskService after status/assignment changes
- [ ] T141 Integrate NotificationService.BroadcastCommentAdded() into CommentService after comment creation
- [ ] T142 [P] Integrate NotificationService.BroadcastCommentUpdated() into CommentService after comment edit
- [ ] T143 [P] Integrate NotificationService.BroadcastCommentDeleted() into CommentService after comment deletion

#### Blazor SignalR Client

- [ ] T144 Create NotificationService.cs in Taskify.Web/Services/ with HubConnection for /hubs/notifications
- [ ] T145 Implement OnTaskUpdated handler in KanbanBoard to refresh task data on SignalR event
- [ ] T146 Implement OnCommentAdded handler in TaskDetailsModal to append new comment to list
- [ ] T147 [P] Implement OnCommentUpdated handler in TaskDetailsModal to update comment text
- [ ] T148 [P] Implement OnCommentDeleted handler in TaskDetailsModal to remove comment from list
- [ ] T149 Configure automatic reconnection with exponential backoff in NotificationService
- [ ] T150 Add connection status indicator in MainLayout showing "Connected" / "Reconnecting..."

---

## Phase 8: Polish & Cross-Cutting Concerns

**Purpose**: Quality improvements, documentation, and final validations

- [ ] T151 [P] Add comprehensive XML documentation comments to all public APIs
- [ ] T152 [P] Add README.md in repository root with project overview and quickstart link
- [ ] T153 [P] Run dotnet format to ensure consistent code style across solution
- [ ] T154 Run all unit tests and verify ≥80% code coverage (per constitution)
- [ ] T155 Run all integration tests with Testcontainers and verify ≥70% coverage
- [ ] T156 [P] Run all contract tests and verify 100% OpenAPI spec compliance
- [ ] T157 [P] Run all E2E tests and verify complete user workflows
- [ ] T158 Verify all 27 functional requirements (FR-001 to FR-027) are met via manual testing
- [ ] T159 Verify all 7 success criteria (SC-001 to SC-007) are met via measurement
- [ ] T160 Execute complete quickstart.md workflow from scratch to validate developer experience
- [ ] T161 [P] Run dotnet list package --vulnerable and update any packages with vulnerabilities
- [ ] T162 [P] Performance testing: Verify API response times <200ms p95 using dotnet-trace
- [ ] T163 [P] Performance testing: Verify drag-and-drop <100ms perceived latency via browser DevTools
- [ ] T164 [P] Performance testing: Verify real-time updates <500ms propagation via multiple browser windows
- [ ] T165 [P] Accessibility audit: Verify WCAG 2.1 AA compliance for all Blazor components using browser tools
- [ ] T166 Code review: Check all repository/service patterns follow separation of concerns
- [ ] T167 Code review: Verify all error handling provides meaningful messages
- [ ] T168 Code review: Verify all logging uses structured logging with correlation IDs
- [ ] T169 [P] Create deployment guide for Azure Container Apps in docs/deployment.md (Phase 2 prep)
- [ ] T170 Final validation: Deploy to local Aspire dashboard and demonstrate all 4 user stories end-to-end

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all user stories
- **User Story 1 (Phase 3)**: Depends on Foundational phase completion
- **User Story 2 (Phase 4)**: Depends on Foundational phase completion - Can start in parallel with US1
- **User Story 3 (Phase 5)**: Depends on Foundational phase completion - Can start in parallel with US1/US2
- **User Story 4 (Phase 6)**: Depends on Foundational phase completion - Can start in parallel with US1/US2/US3
- **Real-time Updates (Phase 7)**: Depends on all user stories being complete (enhances US2, US4)
- **Polish (Phase 8)**: Depends on all desired user stories being complete

### User Story Dependencies

- **User Story 1 (P1)**: No dependencies on other stories - Fully independent (user selection + project list)
- **User Story 2 (P2)**: No dependencies on other stories - Fully independent (Kanban board works without assignment/comments)
- **User Story 3 (P3)**: No dependencies on other stories - Fully independent (assignment works via API, not UI dependent)
- **User Story 4 (P4)**: No dependencies on other stories - Fully independent (comments work via modal, not board dependent)

**All user stories are independently testable after Foundational phase completion**

### Within Each User Story

- Tests MUST be written and FAIL before implementation begins (TDD approach)
- API Layer (repositories, services, controllers) before Blazor UI
- Blazor components can be built in parallel if they work on different files
- Integration before cross-story features

### Parallel Opportunities

- **Setup Phase**: Tasks T003-T007 (project creation) and T010-T012 (configuration) can run in parallel
- **Foundational Phase**:
  - Entities T015-T018 can run in parallel
  - Repositories T025-T028 can run in parallel
  - DTOs T030-T031 can run in parallel
  - Infrastructure tasks T034-T035, T037-T039, T041 can run in parallel
- **Once Foundational completes**: All 4 user stories can start in parallel (if team capacity allows)
- **Within each user story**: All test tasks marked [P] can run in parallel
- **Within each user story**: Model/DTO creation tasks can run in parallel
- **Real-time Updates Phase**: Tasks T137-T138, T142-T143, T147-T148 can run in parallel

---

## Parallel Example: User Story 2

```bash
# Launch all tests for User Story 2 together (TDD - write these FIRST):
Task: "[US2] Unit test for TaskRepository.GetTasksByProjectId() in tests/Taskify.ApiService.Tests/Unit/Repositories/TaskRepositoryTests.cs"
Task: "[US2] Unit test for TaskService.UpdateTaskStatus() in tests/Taskify.ApiService.Tests/Unit/Services/TaskServiceTests.cs"
Task: "[US2] Contract test for GET /api/projects/{projectId}/with-tasks in tests/Taskify.ApiService.Tests/Contract/ProjectsApiContractTests.cs"
Task: "[US2] Contract test for PATCH /api/tasks/{taskId} in tests/Taskify.ApiService.Tests/Contract/TasksApiContractTests.cs"
Task: "[US2] Integration test for Kanban board rendering in tests/Taskify.Web.Tests/Integration/KanbanBoardTests.cs"
Task: "[US2] Integration test for drag-and-drop task movement in tests/Taskify.Web.Tests/Integration/DragDropTests.cs"
Task: "[US2] Integration test for task highlighting in tests/Taskify.Web.Tests/Integration/TaskHighlightTests.cs"
Task: "[US2] Integration test for task status persistence in tests/Taskify.ApiService.Tests/Integration/Database/TaskStatusPersistenceTests.cs"

# After tests are written and FAILING, launch implementation tasks in parallel:
Task: "[US2] Create TaskService in Taskify.ApiService/Services/TaskService.cs"
Task: "[US2] Create dragdrop.js in Taskify.Web/wwwroot/js/"
Task: "[US2] Create DragContainer.razor in Taskify.Web/Components/DragDrop/"
Task: "[US2] Create DropZone.razor in Taskify.Web/Components/DragDrop/"
Task: "[US2] Create TaskCard.razor in Taskify.Web/Components/Shared/"
Task: "[US2] Create KanbanBoard.razor in Taskify.Web/Components/Pages/"
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup (T001-T014)
2. Complete Phase 2: Foundational (T015-T041) - CRITICAL, blocks all stories
3. Complete Phase 3: User Story 1 (T042-T059)
4. **STOP and VALIDATE**: Test User Story 1 independently using quickstart.md
5. Deploy to Aspire dashboard and demo user selection + project navigation

**MVP Delivers**: Basic navigation flow - users can select identity and view projects (SC-001: ≤3 clicks to board)

### Incremental Delivery

1. Complete Setup + Foundational (T001-T041) → Foundation ready
2. Add User Story 1 (T042-T059) → Test independently → Deploy/Demo (MVP!)
3. Add User Story 2 (T060-T086) → Test independently → Deploy/Demo (Kanban + drag-drop!)
4. Add User Story 3 (T087-T101) → Test independently → Deploy/Demo (Task assignment!)
5. Add User Story 4 (T102-T134) → Test independently → Deploy/Demo (Full collaboration!)
6. Add Real-time Updates (T135-T150) → Test independently → Deploy/Demo (Live updates!)
7. Polish & Final Validation (T151-T170) → Production-ready release

Each story adds value without breaking previous stories.

### Parallel Team Strategy

With multiple developers after Foundational phase completes:

1. Team completes Setup + Foundational together (T001-T041)
2. Once Foundational is done:
   - **Developer A**: User Story 1 (T042-T059) - Entry point + navigation
   - **Developer B**: User Story 2 (T060-T086) - Kanban board + drag-drop
   - **Developer C**: User Story 3 (T087-T101) - Task assignment
   - **Developer D**: User Story 4 (T102-T134) - Comments
3. Stories complete and integrate independently
4. Team collaborates on Real-time Updates (T135-T150) and Polish (T151-T170)

---

## Test Coverage Targets (Per Constitution)

- **Unit Tests**: ≥80% code coverage for Services, Repositories, Validators
- **Integration Tests**: ≥70% coverage for Controllers, Database operations, Blazor components
- **Contract Tests**: 100% OpenAPI specification compliance (all endpoints in contracts/)
- **E2E Tests**: All 4 user stories validated end-to-end with happy path + edge cases

---

## Notes

- **[P] tasks**: Different files, no dependencies - can run in parallel
- **[Story] label**: Maps task to specific user story for traceability (US1, US2, US3, US4)
- **TDD Approach**: ALL tests written FIRST before implementation (per constitution NON-NEGOTIABLE)
- **Independent Stories**: Each user story completable and testable without others
- **Verify tests FAIL**: Before implementing, ensure all tests fail as expected (red-green-refactor)
- **Commit frequently**: After each task or logical group
- **Checkpoints**: Stop at any checkpoint to validate story independently
- **Version Pinning**: Use exact versions from research.md (Aspire 9.2.1, EF Core 8.0.x, xUnit 2.8.1, bUnit 1.40.0, FluentValidation 12.0+)
- **Aspire Dashboard**: Monitor all services, logs, traces at https://localhost:17275 during development
- **Performance**: Measure against targets using Aspire dashboard metrics (API <200ms, drag-drop <100ms, real-time <500ms)

**Avoid**: Vague tasks, same file conflicts, cross-story dependencies that break independence, skipping tests
