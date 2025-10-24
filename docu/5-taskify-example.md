# Taskify 프로젝트 사례 연구

Spec-Kit을 활용한 실제 프로젝트 구현 사례를 단계별로 분석합니다.

---

## 프로젝트 개요

**프로젝트명**: Taskify - 팀 생산성 플랫폼
**기간**: 약 2시간 (문서화 + 구현 일부)
**진행률**: 23/170 tasks (13.5%)
**기술 스택**: .NET 8.0 + Aspire + Blazor Server + PostgreSQL

---

## 🎬 프로젝트 시작

### Step 1: 사용자 요구사항 (자연어)

**원본 요청**:
> "Taskify라는 팀 생산성 플랫폼을 만들어줘. 5명의 사용자(PM 1명, 엔지니어 4명)가 3개 프로젝트의 작업을 Kanban 보드로 관리할 수 있어야 해. 드래그 앤 드롭으로 작업을 이동하고, 코멘트를 추가/수정/삭제할 수 있어야 해. 자신의 작업은 다른 색으로 표시되어야 하고, 로그인은 필요 없어 (테스트용)."

**길이**: 약 150 단어
**복잡도**: 중간 (5개 User Story로 분해됨)

### Step 2: Specification 생성

**명령어**:
```bash
/speckit.specify "Taskify 팀 생산성 플랫폼..."
```

**생성 시간**: ~20-30분

**생성 파일**: `specs/001-create-taskify/spec.md` (164 lines)

**주요 내용**:
- ✅ 5개 User Stories (P1-P5 우선순위)
- ✅ 17개 Functional Requirements
- ✅ 5개 Key Entities
- ✅ 8개 Success Criteria
- ✅ 8개 Edge Cases

**핵심 User Stories**:
```markdown
1. User Story 1 (P1): User Selection and Project Navigation
   - 사용자 선택 → 프로젝트 목록 → Kanban 보드

2. User Story 2 (P2): View and Navigate Kanban Board
   - 4개 컬럼 표시 (To Do, In Progress, In Review, Done)
   - 자신의 작업 색상 구분

3. User Story 3 (P3): Move Tasks Between Columns
   - 드래그 앤 드롭 기능
   - 상태 자동 업데이트

4. User Story 4 (P4): Assign Tasks to Team Members
   - 5명 사용자 목록에서 선택
   - 자신에게 할당 시 색상 변경

5. User Story 5 (P5): Add and Manage Comments
   - 무제한 코멘트
   - 자신의 코멘트만 수정/삭제 가능
```

---

## 📋 Planning 단계

### Step 3: 기술 계획 수립

**명령어**:
```bash
/speckit.plan
```

**생성 시간**: ~40-60분

**생성 파일** (5개):
1. `plan.md` (850 lines) - 메인 구현 계획
2. `research.md` (600 lines) - 기술 조사
3. `data-model.md` (300 lines) - DB 설계
4. `quickstart.md` (350 lines) - 빠른 시작 가이드
5. `contracts/` (3 files) - OpenAPI 스펙

### 주요 기술 결정

#### 1. .NET Aspire 버전 선택
**조사 내용** (research.md):
```markdown
### .NET Aspire Version Selection
**Question**: 9.2.1 (latest) vs 8.2.2 (LTS)?
**Investigation**:
  - 9.2.1: .NET 9.0 필요, 최신 기능
  - 8.2.2: .NET 8.0 LTS 호환, 안정성

**Decision**: 8.2.2
**Rationale**:
  1. .NET 8.0 LTS 사용 (장기 지원)
  2. 프로덕션 안정성 우선
  3. EF Core 8.0.x와 호환성
```

#### 2. Frontend Framework 선택
**조사 내용** (research.md):
```markdown
### Blazor Server vs WASM
**Options**:
  A. Blazor Server
     장점: SignalR 내장, 작은 번들, 실시간 업데이트
     단점: 서버 의존, 연결 필요

  B. Blazor WebAssembly
     장점: 오프라인, 클라이언트 성능
     단점: 큰 번들, SignalR 별도 구성

**Decision**: Blazor Server
**Rationale**:
  1. Real-time collaboration (드래그 앤 드롭 상태 공유)
  2. 초기 로딩 속도 (<1초 목표)
  3. 오프라인 기능 불필요 (초기 버전)
```

### Constitution Check 결과

**plan.md** 자동 검증:
```markdown
## Constitution Check

### I. Code Quality First
- [x] 가독성 기준 정의 (명확한 네이밍, 주석)
- [x] 함수 50줄 이하 설계
- [x] 파일 500줄 이하 구조
- [x] OpenAPI/Swagger 문서화 계획

### II. Test-Driven Development
- [x] TDD 워크플로우 확정: 테스트 먼저 작성
- [x] 80% unit / 70% integration 목표
- [x] Unit/Integration/Contract/E2E 테스트 정의

**Gate Result**: ✅ PASS
```

### 프로젝트 구조

**plan.md** 정의:
```
Taskify.sln
├── Taskify.AppHost/              # .NET Aspire orchestration
├── Taskify.ServiceDefaults/      # Shared Aspire config
├── Taskify.ApiService/           # REST API
│   ├── Controllers/
│   ├── Services/
│   ├── Repositories/
│   ├── Models/Entities/
│   └── Data/
├── Taskify.Web/                  # Blazor Server
│   ├── Components/Pages/
│   ├── Components/Shared/
│   └── Services/
└── tests/
    ├── Taskify.ApiService.Tests/
    └── Taskify.Web.Tests/
```

### 데이터 모델 설계

**data-model.md**:
```markdown
## Entity Relationship Diagram
User (1) ──< (N) Task
User (1) ──< (N) Comment
Project (1) ──< (N) Task
Task (1) ──< (N) Comment

## Tables (4개)
- users: id, name, email, role, created_at
- projects: id, name, description, created_at
- tasks: id, project_id, title, description, status, assigned_to_id, created_at, updated_at
- comments: id, task_id, user_id, content, created_at, updated_at
```

---

## ✅ Tasks 분해

### Step 4: 작업 목록 생성

**명령어**:
```bash
/speckit.tasks
```

**생성 시간**: ~45-60분

**생성 파일**: `tasks.md` (800 lines, 170 tasks)

### Phase 구조

```markdown
Phase 1: Setup (14 tasks)
├── T001-T009: 프로젝트 구조 생성
└── T010-T014: 환경 설정

Phase 2: Foundational (27 tasks)
├── T015-T023: Database Foundation
├── T024-T035: API Infrastructure
└── T036-T041: Blazor Infrastructure

Phase 3: User Story 1 (18 tasks)
├── T042-T048: Tests FIRST
└── T049-T059: Implementation

Phase 4: User Story 2 (28 tasks)
Phase 5: User Story 3 (22 tasks)
Phase 6: User Story 4 (20 tasks)
Phase 7: User Story 5 (25 tasks)
Phase 8: Real-time & Polish (16 tasks)
```

### TDD 구조 예제

**Phase 3: User Story 1**:
```markdown
### Tests (TDD - Write FIRST)
- [ ] T042 [P] [US1] Unit test: UserRepository.GetAllUsers()
      Location: tests/Taskify.ApiService.Tests/Unit/Repositories/UserRepositoryTests.cs
      Verify: Returns all 5 seeded users

- [ ] T043 [P] [US1] Unit test: ProjectRepository.GetAllProjects()
      Location: tests/Taskify.ApiService.Tests/Unit/Repositories/ProjectRepositoryTests.cs
      Verify: Returns all 3 seeded projects

- [ ] T044 [P] [US1] Contract test: GET /api/projects
      Location: tests/Taskify.ApiService.Tests/Contract/ProjectsApiContractTests.cs
      Verify: Response matches contracts/projects-api.yaml schema

### Implementation (After tests FAIL)
- [ ] T049 [P] [US1] Create ProjectService.cs
      Methods: GetAllProjects(), GetProjectById()

- [ ] T050 [US1] Create ProjectsController.cs
      Endpoints: GET /api/projects, GET /api/projects/{id}

- [ ] T053 [P] [US1] Create UserSelection.razor
      Display: 5 users with role badges (PM vs Engineer)
```

### 병렬 실행 최적화

**[P] 태그 활용**:
```markdown
## Phase 2: Foundational

### Database Foundation
- [ ] T015 Create User entity
- [ ] T016 [P] Create Project entity     ← 병렬 가능
- [ ] T017 [P] Create Task entity        ← 병렬 가능
- [ ] T018 [P] Create Comment entity     ← 병렬 가능
- [ ] T019 Create TaskifyDbContext       ← T015-T018 완료 후

이유: 4개 엔티티는 서로 독립적으로 작성 가능
의존성: DbContext는 모든 엔티티 필요
```

---

## 💻 Implementation 단계

### Step 5: 구현 실행

**명령어**:
```bash
/speckit.implement
```

### Prerequisites Check

```bash
✅ Checking prerequisites...
├── tasks.md exists
├── plan.md exists
├── spec.md exists
└── .NET SDK 8.0.404 installed

⚠️ Blocker: .NET SDK not found
└── Action: Install .NET 8.0 SDK to /Users/devpeng/.dotnet
```

**문제 해결**:
```bash
# .NET SDK 설치 (사용자 디렉토리)
curl -sSL https://dot.net/v1/dotnet-install.sh | bash -s -- --version 8.0.404 --install-dir /Users/devpeng/.dotnet

# Aspire workload 설치
export PATH="/Users/devpeng/.dotnet:$PATH"
dotnet workload install aspire
```

### Checklist Validation

```bash
✅ Validating checklists/requirements.md...
├── [15/15] All requirements checked
│   ├── .NET 8.0 SDK: ✅
│   ├── Docker Desktop: ✅
│   ├── PostgreSQL 16: ✅
│   └── Git: ✅
└── ✓ PASS - Implementation approved
```

### Phase 1: Setup (T001-T014)

**실행 결과**:
```bash
Executing Phase 1: Setup

T001 ✅ Created Taskify.sln
     Command: dotnet new sln -n Taskify

T002 ✅ Created Taskify.AppHost
     Command: dotnet new aspire-apphost -n Taskify.AppHost
     Added: Program.cs, appsettings.json

T003 ✅ Created Taskify.ServiceDefaults
     Command: dotnet new aspire-servicedefaults -n Taskify.ServiceDefaults
     Added: Extensions.cs (OpenTelemetry, Health checks)

T004 ✅ Created Taskify.ApiService
     Command: dotnet new webapi -n Taskify.ApiService
     Added: Program.cs, Controllers/, Models/

T005 ✅ Created Taskify.Web
     Command: dotnet new blazor -n Taskify.Web --interactivity Server
     Added: Program.cs, Components/

T006-T007 ✅ Created test projects
     Command: dotnet new xunit (x2)

T008 ✅ Configured .gitignore
     Added: bin/, obj/, .vs/, .idea/, etc.

T009 ✅ Added NuGet packages
     ├── AppHost: Aspire.Hosting.PostgreSQL 8.2.2
     ├── ApiService: Aspire.Npgsql.EntityFrameworkCore.PostgreSQL 8.2.2
     │              FluentValidation 11.9.2
     │              Swashbuckle.AspNetCore 6.8.1
     └── ServiceDefaults: (template packages)

T010 ✅ Configured AppHost Program.cs
     Added:
       - PostgreSQL resource definition
       - taskifydb database
       - apiservice + webfrontend references

T011 ✅ ServiceDefaults already complete (from template)

T012 ✅ Configured launchSettings.json
     Ports:
       - AppHost: 17275 (HTTPS)
       - ApiService: 7001 (HTTPS), 5001 (HTTP)
       - Web: 7124 (HTTPS), 5124 (HTTP)

T013 ✅ PostgreSQL configuration (Aspire manages this)

T014 ✅ User Secrets configured
     Command: dotnet user-secrets set "ConnectionStrings:taskifydb" "..."

Phase 1 Complete: 14/14 tasks ✅
Build Status: dotnet build Taskify.sln - SUCCESS
```

### Phase 2: Foundational (T015-T023)

**실행 결과**:
```bash
Executing Phase 2: Foundational (Database)

T015 ✅ Created User.cs entity
     Location: Taskify.ApiService/Data/Entities/User.cs
     Properties:
       - Id (int, PK)
       - Name (string, required, max 100)
       - Email (string, required, unique, max 200)
       - Role (string, required, max 50)
       - CreatedAt (DateTime)
     Navigation:
       - AssignedTasks (ICollection<TaskItem>)
       - Comments (ICollection<Comment>)

T016 ✅ Created Project.cs entity (parallel with T017-T018)
T017 ✅ Created TaskItem.cs entity
T018 ✅ Created Comment.cs entity

T019 ✅ Created TaskifyDbContext.cs
     Configured:
       - DbSet<User> Users
       - DbSet<Project> Projects
       - DbSet<TaskItem> Tasks
       - DbSet<Comment> Comments

T020 ✅ Configured entity relationships
     - User.Email: UNIQUE index
     - Task.ProjectId: FK CASCADE DELETE
     - Task.AssignedToId: FK SET NULL
       - Comment.TaskId: FK CASCADE DELETE

T021 ✅ Created InitialCreate migration
     Command: dotnet ef migrations add InitialCreate
     Files:
       - Migrations/20251024_InitialCreate.cs
       - Migrations/TaskifyDbContextModelSnapshot.cs

T022 ✅ Created DbContextSeed.cs
     Seed data:
       - 5 users (Alice=PM, Bob/Charlie/Diana/Ethan=Engineers)
       - 3 projects (E-commerce, Mobile App, Analytics)
       - 45 tasks (15 per project, distributed across 4 statuses)
     Distribution:
       - To Do: 15 tasks
       - In Progress: 12 tasks
       - In Review: 9 tasks
       - Done: 9 tasks

T023 ✅ Migration configured to run on startup
     Updated: Program.cs
     Added:
       - EnsureCreatedAsync()
       - DbContextSeed.SeedAsync()

Phase 2 Complete: 9/27 tasks (33%)
Build Status: dotnet build Taskify.sln - SUCCESS
```

---

## 📊 진행 현황 (2시간 후)

### 완료된 작업
```
총 진행률: 23/170 tasks (13.5%)

✅ Phase 1: Setup (14/14 tasks, 100%)
⏳ Phase 2: Foundational (9/27 tasks, 33%)
⏸️ Phase 3: User Story 1 (0/18 tasks, 0%)
⏸️ Phase 4-8: (0/111 tasks, 0%)
```

### 생성된 파일
```
문서 (6개 + 3 contracts):
✅ constitution.md (200 lines)
✅ spec.md (164 lines)
✅ plan.md (850 lines)
✅ research.md (600 lines)
✅ data-model.md (300 lines)
✅ quickstart.md (350 lines)
✅ tasks.md (800 lines)
✅ contracts/*.yaml (3 files)
✅ checklists/requirements.md

코드 (12개 주요 파일):
✅ Taskify.sln
✅ Taskify.AppHost/Program.cs
✅ Taskify.ServiceDefaults/Extensions.cs
✅ Taskify.ApiService/Data/Entities/*.cs (4 entities)
✅ Taskify.ApiService/Data/TaskifyDbContext.cs
✅ Taskify.ApiService/Data/DbContextSeed.cs
✅ Taskify.ApiService/Data/Migrations/InitialCreate.cs
✅ Taskify.ApiService/Program.cs
✅ Taskify.Web/Program.cs
✅ tests/ (2 projects)
```

### 빌드 상태
```bash
$ dotnet build Taskify.sln

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:02.65
```

---

## 💡 핵심 인사이트

### 1. 시간 절감
```
수동 작업 예상 시간:
- 문서 작성: 8-12시간
- 프로젝트 구조: 2-3시간
- DB 설계 및 마이그레이션: 2-4시간
총: 12-19시간

Spec-Kit 활용 실제 시간:
- 문서 작성: 2시간 (AI 자동 생성)
- 프로젝트 구조: 30분
- DB 설계 및 마이그레이션: 30분
총: 3시간

시간 절감: 75-84%
```

### 2. 품질 향상
```
✅ Constitution 기반 자동 검증
✅ TDD 구조 강제 (테스트 먼저)
✅ OpenAPI 계약 사전 정의
✅ 체크리스트 기반 품질 게이트
```

### 3. 일관성
```
✅ 모든 User Story에 동일한 구조
✅ 네이밍 컨벤션 자동 적용
✅ 프로젝트 구조 표준화
```

---

## 🚀 다음 단계 (MVP 완성)

### Option A: MVP First (추천)
```
남은 작업: 36 tasks
예상 시간: 2-3시간

Phase 2 완료 (T024-T041): 18 tasks
├── T024-T028: Repository 구현
├── T029-T035: API Infrastructure
└── T036-T041: Blazor Infrastructure

Phase 3 완료 (T042-T059): 18 tasks
├── T042-T048: Tests FIRST
└── T049-T059: User Story 1 구현

결과: 기능하는 MVP
- 사용자 선택
- 프로젝트 목록
- Kanban 보드 표시
```

### 검증 가능한 결과물
```bash
# MVP 완성 후 테스트
1. dotnet run (Taskify.AppHost)
2. Open https://localhost:7124
3. Select user "Alice Johnson"
4. See 3 projects
5. Click "E-commerce Platform"
6. See Kanban board with 4 columns
7. See 15 tasks distributed
8. ✅ MVP 검증 완료
```

---

## 📈 학습 포인트

### Spec-Kit 활용의 핵심
1. **Constitution부터 시작**: 품질 기준을 먼저 정의
2. **상세한 Spec 작성**: AI가 정확히 이해하도록
3. **Plan 단계 활용**: 기술 결정을 문서화
4. **TDD 엄격히 준수**: tasks.md 구조 활용
5. **MVP 전략**: 전체가 아닌 핵심 기능 먼저

### 피해야 할 함정
1. ❌ Constitution 건너뛰기
2. ❌ Spec을 너무 모호하게 작성
3. ❌ Plan 없이 바로 Tasks로
4. ❌ Tasks를 너무 크게 분해
5. ❌ TDD 무시하고 구현부터

---

## 📞 참고 자료

- [workflow.md](./workflow.md) - 전체 워크플로우
- [commands.md](./commands.md) - 명령어 상세
- [files.md](./files.md) - 파일 구조
- [best-practices.md](./best-practices.md) - 베스트 프랙티스

---

**작성일**: 2025-10-24
**프로젝트**: Taskify
**진행 상태**: Phase 2 진행 중 (13.5%)
