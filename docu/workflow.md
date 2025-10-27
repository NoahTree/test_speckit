# Spec-Kit 워크플로우 완벽 가이드

**목표**: 6단계 Spec-Driven Development 워크플로우 마스터하기

**소요 시간**: 1시간 (읽기), 2-3시간 (실습)

Spec-Kit의 6단계 워크플로우를 실제 Taskify 프로젝트 예제와 함께 상세히 설명합니다.

**📖 관련 문서**:
- [quickstart.md](./quickstart.md) - 5분 빠른 시작
- [getting-started.md](./getting-started.md) - 첫 프로젝트 적용
- [commands.md](./commands.md) - 명령어 레퍼런스
- [examples.md](./examples.md) - Taskify 실제 사례

---

## 📊 전체 워크플로우 개요

```
1. Constitution    ─┐
   (프로젝트 헌법)   │
                    ├─→ 2. Specify ─→ 3. Plan ─→ 4. Tasks ─→ 5. Implement
                    │    (기능 명세)   (기술 계획)  (작업 분해)   (구현 실행)
6. Validate  ───────┘
   (품질 검증)
```

**핵심 원칙**:
- **순차적 실행**: 각 단계는 이전 단계의 출력을 입력으로 사용
- **품질 게이트**: 각 단계마다 검증 체크포인트
- **반복 가능**: 필요시 이전 단계로 돌아가 수정 가능

---

## 1️⃣ Constitution (헌법 작성)

### 목적
프로젝트의 **불변 원칙**과 품질 기준을 정의합니다.

### 실행 방법
```bash
/speckit.constitution
```

### 대화형 프로세스
```
Q: 코드 품질에서 가장 중요한 것은?
A: 가독성과 유지보수성

Q: 테스트 전략은?
A: TDD 의무화, 80% 유닛 테스트 커버리지

Q: 성능 목표는?
A: API 응답 시간 < 200ms, UI 인터랙션 < 100ms

Q: 보안 요구사항은?
A: Input validation, SQL injection 방지, HTTPS only
```

### 생성 파일
```
.specify/memory/constitution.md
```

### Constitution 구조
```markdown
# Project Constitution: Taskify

## I. Code Quality First
- 모든 함수는 50줄 이하
- 모든 파일은 500줄 이하
- Public API는 반드시 문서화
- TypeScript/C# 강타입 사용

## II. Test-Driven Development (NON-NEGOTIABLE)
- ✅ 모든 코드는 테스트 먼저 작성 (TDD)
- ✅ Unit test coverage ≥ 80%
- ✅ Integration test coverage ≥ 70%
- ✅ E2E tests for critical user flows

## III. User Experience Consistency
- WCAG 2.1 AA 준수
- 3G 네트워크에서 <3초 로딩
- 모든 인터랙션 <100ms 반응

## IV. Performance Requirements
- API 응답: p95 < 200ms
- DB 쿼리: p95 < 100ms
- 메모리 사용: <500MB per instance

## V. Security by Default
- 모든 입력 검증 (FluentValidation)
- SQL Injection 방지 (Parameterized queries)
- XSS 방지 (Content Security Policy)
- 민감정보는 User Secrets/Key Vault
```

### 핵심 개념: **Non-Negotiable Principles**
헌법에서 정의한 원칙은 **모든 후속 단계에서 자동 검증**됩니다:
- `/speckit.plan`의 "Constitution Check" 섹션
- `/speckit.implement`의 품질 게이트

---

## 2️⃣ Specify (기능 명세)

### 목적
**무엇을 만들 것인가**를 상세하게 정의합니다.

### 실행 방법
```bash
/speckit.specify "Taskify 팀 생산성 플랫폼 만들기. 5명 사용자(PM 1명, 엔지니어 4명)가
3개 프로젝트의 작업을 Kanban 보드로 관리. 드래그 앤 드롭, 코멘트 기능 포함..."
```

### 입력 형식
**자연어 요구사항** (제약 없음):
- 짧은 설명 (1-2문장) ✅
- 긴 설명 (여러 단락) ✅
- 기술적 세부사항 포함 ✅
- 비즈니스 목표 중심 ✅

### 생성 파일
```
specs/001-create-taskify/spec.md
```

### Spec.md 구조

#### 1. User Scenarios & Testing (필수)
```markdown
### User Story 1 - User Selection and Project Navigation (Priority: P1)

As a team member, I need to select myself from the user list and access
the project dashboard so I can start working on my tasks.

**Why this priority**: 앱의 진입점이므로 최우선

**Independent Test**: 앱 실행 → 5명 사용자 표시 → 선택 → 3개 프로젝트 표시

**Acceptance Scenarios**:
1. **Given** 앱이 실행되면, **When** 초기 화면을 보면,
   **Then** 5명의 사용자가 표시된다 (PM 1명, 엔지니어 4명)
2. **Given** 사용자 선택 화면에서, **When** 사용자를 클릭하면,
   **Then** 프로젝트 목록이 표시된다
```

#### 2. Requirements (필수)
```markdown
### Functional Requirements
- **FR-001**: 시스템은 5명의 사전정의된 사용자를 표시해야 함
- **FR-002**: 비밀번호 없이 사용자 선택 가능해야 함
- **FR-003**: 사용자 선택 후 3개 프로젝트 표시
...

### Key Entities
- **User**: 팀 멤버 (Name, Role, Email)
- **Project**: 작업 프로젝트 (Name, Description)
- **Task**: 작업 항목 (Title, Status, Assignee)
- **Comment**: 작업 코멘트 (Content, Author, Timestamp)
```

#### 3. Success Criteria (필수)
```markdown
### Measurable Outcomes
- **SC-001**: 사용자 선택 후 프로젝트 목록 표시까지 <5초
- **SC-002**: 자신의 작업을 3초 이내에 식별 가능 (색상 구분)
- **SC-003**: 드래그 앤 드롭으로 작업 이동 <5초
- **SC-004**: 댓글 추가 <10초
- **SC-005**: 100% 댓글 소유권 규칙 준수
```

### Taskify Spec.md 실제 예시
```
📄 spec.md (164 lines)
├── 5 User Stories (P1-P5 우선순위)
├── 17 Functional Requirements (FR-001 ~ FR-017)
├── 5 Key Entities (User, Project, Task, Comment, Kanban Column)
├── 8 Success Criteria (SC-001 ~ SC-008)
└── 8 Edge Cases
```

---

## 3️⃣ Plan (기술 계획)

### 목적
**어떻게 만들 것인가**에 대한 기술적 청사진을 작성합니다.

### 실행 방법
```bash
/speckit.plan
```

**입력**: `specs/001-create-taskify/spec.md`
**출력**:
- `specs/001-create-taskify/plan.md`
- `specs/001-create-taskify/research.md`
- `specs/001-create-taskify/data-model.md`
- `specs/001-create-taskify/quickstart.md`
- `specs/001-create-taskify/contracts/` (OpenAPI 스펙)

### Plan.md 구조

#### 1. Summary
```markdown
## Summary
Taskify는 .NET Aspire로 오케스트레이션되는 팀 생산성 플랫폼입니다.
Blazor Server를 사용한 실시간 UI 렌더링, PostgreSQL 데이터 저장,
REST API 기반 Projects, Tasks, Notifications 엔드포인트를 제공합니다.
```

#### 2. Technical Context
```markdown
**Language/Version**: C# / .NET 8.0 LTS
**Primary Framework**: .NET Aspire 9.2.1
**Frontend**: Blazor Server (SignalR 내장)
**Backend**: ASP.NET Core 8.0 Web API
**Storage**: PostgreSQL 16
**ORM**: Entity Framework Core 8.0.x
**Real-time**: SignalR
**Testing**: xUnit 2.8.1, bUnit 1.40.0
**Validation**: FluentValidation 12.0+
```

#### 3. Constitution Check ⭐
```markdown
## Constitution Check

### I. Code Quality First
- [x] 가독성 기준 정의 (명확한 네이밍)
- [x] 함수 50줄 이하 설계
- [x] 파일 500줄 이하 구조
- [x] OpenAPI/Swagger 문서화 계획

### II. Test-Driven Development
- [x] TDD 워크플로우 확정
- [x] 80% unit / 70% integration coverage 목표
- [x] Unit/Integration/Contract/E2E 테스트 정의

**Gate Result**: [x] PASS
```

#### 4. Project Structure
```markdown
Taskify.sln
├── Taskify.AppHost/              # .NET Aspire orchestration
├── Taskify.ServiceDefaults/      # Shared configuration
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

#### 5. Architecture Diagrams
```markdown
### System Architecture
┌─────────────┐
│ Blazor Web  │ ─── SignalR ──→ Real-time updates
└──────┬──────┘
       │ HTTPS
       ↓
┌─────────────┐
│  API Service│ ─── REST API
└──────┬──────┘
       │ EF Core
       ↓
┌─────────────┐
│ PostgreSQL  │
└─────────────┘
```

#### 6. Data Model
별도 파일 `data-model.md`로 분리:
```markdown
# Data Model: Create Taskify

## Entity Relationship Diagram (ERD)
User (1) ──< (N) Task
User (1) ──< (N) Comment
Project (1) ──< (N) Task
Task (1) ──< (N) Comment

## Tables

### users
| Column    | Type         | Constraints      |
|-----------|--------------|------------------|
| id        | INT          | PK, AUTO_INCREMENT |
| name      | VARCHAR(100) | NOT NULL         |
| email     | VARCHAR(200) | NOT NULL, UNIQUE |
| role      | VARCHAR(50)  | NOT NULL         |
| created_at| TIMESTAMP    | DEFAULT NOW()    |
```

#### 7. API Contracts
별도 디렉토리 `contracts/`에 OpenAPI 스펙:

**projects-api.yaml**:
```yaml
openapi: 3.0.0
paths:
  /api/projects:
    get:
      summary: Get all projects
      responses:
        '200':
          content:
            application/json:
              schema:
                type: array
                items:
                  $ref: '#/components/schemas/ProjectDto'
```

#### 8. Quickstart Guide
별도 파일 `quickstart.md`:
```markdown
# Quickstart Guide: Create Taskify

## Quick Start (5 minutes)

### 1. Clone Repository
git clone <repo>
cd taskify

### 2. Start PostgreSQL
docker run --name taskify-postgres -e POSTGRES_PASSWORD=dev -p 5432:5432 -d postgres:16-alpine

### 3. Run Application
cd Taskify.AppHost
dotnet run

### 4. Open Browser
https://localhost:17275 (Aspire Dashboard)
https://localhost:7124 (Blazor Web)
https://localhost:7001/swagger (API)
```

### Taskify Plan.md 실제 결과
```
📄 plan.md (850 lines)
├── Technical Context (기술 스택, 버전, 성능 목표)
├── Constitution Check (5개 섹션 검증)
├── Project Structure (폴더 구조)
├── Architecture Diagrams (시스템 아키텍처)
├── Implementation Phases (4개 Phase)
└── Testing Strategy (TDD 접근법)

📄 research.md (600 lines)
├── .NET Aspire 9.2.1 조사
├── Blazor Server vs WASM 비교
├── PostgreSQL vs MySQL 선택 근거
└── Version Matrix (모든 NuGet 패키지)

📄 data-model.md (300 lines)
├── ERD Diagram
├── 4개 테이블 정의
├── Indexes 전략
└── Migration 스크립트

📄 quickstart.md (350 lines)
├── Prerequisites
├── 5분 Quick Start
├── Troubleshooting
└── Next Steps

📁 contracts/ (3 files)
├── projects-api.yaml (OpenAPI 3.0)
├── tasks-api.yaml
└── notifications-api.yaml
```

---

## 4️⃣ Tasks (작업 분해)

### 목적
구현 계획을 **실행 가능한 작업**으로 분해합니다.

### 실행 방법
```bash
/speckit.tasks
```

**입력**: plan.md, spec.md, data-model.md, contracts/
**출력**: `specs/001-create-taskify/tasks.md`

### Tasks.md 구조

#### Format: `[ID] [P?] [Story] Description`
```
- **[P]**: 병렬 실행 가능 (다른 파일, 의존성 없음)
- **[Story]**: User Story ID (US1, US2, ...)
- ID: T001, T002, ...
```

#### Phase 1: Setup (공통 인프라)
```markdown
## Phase 1: Setup (Shared Infrastructure)

**Purpose**: .NET Aspire 솔루션 구조 초기화

- [ ] T001 Create Taskify.sln with .NET 8.0 SDK
- [ ] T002 Create Taskify.AppHost with .NET Aspire 9.2.1
- [ ] T003 [P] Create Taskify.ServiceDefaults
- [ ] T004 [P] Create Taskify.ApiService (Web API)
- [ ] T005 [P] Create Taskify.Web (Blazor Server)
- [ ] T006 [P] Create tests/Taskify.ApiService.Tests (xUnit)
- [ ] T007 [P] Create tests/Taskify.Web.Tests (bUnit)
- [ ] T008 Configure .gitignore
- [ ] T009 Add NuGet packages per research.md
```

#### Phase 2: Foundational (차단 전제 조건)
```markdown
## Phase 2: Foundational (Blocking Prerequisites)

**⚠️ CRITICAL**: 모든 User Story 구현 전에 완료 필수

### Database Foundation
- [ ] T015 Create User entity
- [ ] T016 [P] Create Project entity
- [ ] T017 [P] Create Task entity
- [ ] T018 [P] Create Comment entity
- [ ] T019 Create TaskifyDbContext
- [ ] T020 Configure entity relationships
- [ ] T021 Create InitialCreate migration
- [ ] T022 Create DbContextSeed (5 users, 3 projects, 45 tasks)
- [ ] T023 Apply migration and verify seed
```

#### Phase 3: User Story 1 (TDD)
```markdown
## Phase 3: User Story 1 - User Selection and Navigation

### Tests (TDD - Write FIRST)
- [ ] T042 [P] [US1] Unit test for UserRepository.GetAllUsers()
- [ ] T043 [P] [US1] Unit test for ProjectRepository.GetAllProjects()
- [ ] T044 [P] [US1] Contract test for GET /api/projects
- [ ] T045 [P] [US1] Contract test for GET /api/projects/{id}
- [ ] T046 [P] [US1] Integration test for user selection (bUnit)
- [ ] T047 [P] [US1] Integration test for project list (bUnit)
- [ ] T048 [P] [US1] E2E test for navigation flow

### Implementation
- [ ] T049 [P] [US1] Create ProjectService
- [ ] T050 [US1] Create ProjectsController
- [ ] T051 [US1] Add validation and error handling
- [ ] T052 [US1] Add structured logging
- [ ] T053 [P] [US1] Create UserSelection.razor
- [ ] T054 [P] [US1] Create ProjectList.razor
- [ ] T055 [US1] Implement user selection logic
- [ ] T056 [US1] Implement project navigation
```

### Taskify Tasks.md 실제 결과
```
📄 tasks.md (800 lines)
├── Phase 1: Setup (14 tasks)
├── Phase 2: Foundational (27 tasks)
├── Phase 3: User Story 1 (18 tasks)
├── Phase 4: User Story 2 (28 tasks)
├── Phase 5: User Story 3 (22 tasks)
├── Phase 6: User Story 4 (20 tasks)
├── Phase 7: User Story 5 (25 tasks)
└── Phase 8: Real-time & Polish (16 tasks)

**Total**: 170 tasks
```

### 핵심 특징
1. **TDD 순서**: 모든 User Story는 "Tests FIRST" 섹션 포함
2. **병렬화 가능**: [P] 태그로 병렬 실행 가능한 작업 표시
3. **User Story 연결**: [US1], [US2] 태그로 어떤 기능에 속하는지 명시
4. **Phase 구분**: 의존성에 따라 Phase로 그룹화

---

## 5️⃣ Implement (구현 실행)

### 목적
Tasks.md의 작업을 **실제 코드로 변환**합니다.

### 실행 방법
```bash
/speckit.implement
```

**입력**: tasks.md, plan.md, spec.md
**출력**: 실제 소스 코드 파일들

### Implementation 프로세스

#### 1. Prerequisites Check
```bash
✅ Checking prerequisites...
├── tasks.md exists
├── plan.md exists
├── spec.md exists
└── .NET SDK 8.0.404 installed
```

#### 2. Checklist Validation
```bash
✅ Validating checklists/requirements.md...
├── [15/15] All requirements met
└── ✓ PASS - Implementation approved
```

#### 3. Task Execution
```bash
Executing Phase 1: Setup (T001-T014)
├── T001 ✅ Created Taskify.sln
├── T002 ✅ Created Taskify.AppHost
├── T003 ✅ Created Taskify.ServiceDefaults
├── ...
└── T014 ✅ Configured User Secrets

Executing Phase 2: Foundational (T015-T041)
├── T015 ✅ Created User entity
├── T016 ✅ Created Project entity
├── ...
└── T023 ✅ Applied migration and verified seed

Build Status: ✅ SUCCESS
```

### Taskify 구현 결과 (현재까지)

**완료된 작업**: T001-T023 (23/170 tasks, 13.5%)

**생성된 코드**:
```
Taskify/
├── Taskify.sln                                  ✅
├── Taskify.AppHost/
│   ├── Program.cs                               ✅ (PostgreSQL + Service refs)
│   └── Taskify.AppHost.csproj                   ✅
├── Taskify.ServiceDefaults/
│   ├── Extensions.cs                            ✅ (OpenTelemetry, Health checks)
│   └── Taskify.ServiceDefaults.csproj           ✅
├── Taskify.ApiService/
│   ├── Data/
│   │   ├── Entities/
│   │   │   ├── User.cs                          ✅
│   │   │   ├── Project.cs                       ✅
│   │   │   ├── TaskItem.cs                      ✅
│   │   │   └── Comment.cs                       ✅
│   │   ├── TaskifyDbContext.cs                  ✅
│   │   ├── DbContextSeed.cs                     ✅ (5 users, 3 projects, 45 tasks)
│   │   └── Migrations/
│   │       └── 20251024_InitialCreate.cs        ✅
│   ├── Program.cs                               ✅ (EF Core, Swagger, CORS)
│   └── Taskify.ApiService.csproj                ✅
├── Taskify.Web/
│   ├── Program.cs                               ✅
│   └── Taskify.Web.csproj                       ✅
└── tests/
    ├── Taskify.ApiService.Tests/                ✅
    └── Taskify.Web.Tests/                       ✅

**Build**: ✅ dotnet build - SUCCESS (0 errors, 0 warnings)
```

### MVP Strategy (Option A)
```
📦 MVP = Phase 1 (Setup) + Phase 2 (Foundational) + Phase 3 (User Story 1)
├── 59 tasks total
├── 23 tasks completed ✅
├── 36 tasks remaining
└── Estimated time: 2-3 hours
```

---

## 6️⃣ Validate (품질 검증)

### 목적
구현된 코드가 명세와 헌법을 준수하는지 검증합니다.

### Optional Commands

#### `/speckit.clarify`
명세서의 **불명확한 부분**을 식별하고 해결합니다.

```bash
/speckit.clarify
```

**출력**:
```markdown
## Clarification Questions

### Q1: Task Assignment
**Question**: Can a task be assigned to multiple users simultaneously?
**Options**:
  A. Single assignee only
  B. Multiple assignees allowed
**User Answer**: A
**Updated in spec.md**: FR-009 updated to "single assignee"

### Q2: Comment Editing
**Question**: Is there a time limit for editing comments?
**Options**:
  A. No limit (anytime editing)
  B. Within 5 minutes
  C. Within 24 hours
**User Answer**: A
**Updated in spec.md**: FR-011 clarified
```

#### `/speckit.analyze`
**교차 아티팩트 일관성 검사**

```bash
/speckit.analyze
```

**출력**:
```markdown
## Analysis Report

### Consistency Check
✅ spec.md defines 5 User Stories
✅ plan.md includes all 5 User Stories in architecture
✅ tasks.md has phases for all 5 User Stories
⚠️ WARNING: spec.md mentions "notification system" but plan.md doesn't include it

### Coverage Analysis
✅ All FR requirements mapped to tasks
✅ All entities in data-model.md have corresponding tasks
⚠️ WARNING: tasks.md has T089 but no corresponding test task

### Recommendations
1. Add notification system to plan.md Phase 4
2. Add test task T089-test before T089 implementation
```

#### `/speckit.checklist`
**커스텀 품질 체크리스트 생성**

```bash
/speckit.checklist
```

**생성 파일**: `checklists/requirements.md`

```markdown
# Requirements Checklist: Create Taskify

**Status**: 15/15 ✓ PASS

## Prerequisites
- [x] .NET 8.0 SDK installed (version 8.0.404)
- [x] Docker Desktop running
- [x] PostgreSQL 16 available
- [x] Git repository initialized

## Phase 1: Setup
- [x] Solution file created
- [x] All projects created
- [x] NuGet packages installed
- [x] User Secrets configured

## Phase 2: Foundational
- [x] Entity models created
- [x] DbContext configured
- [x] Migration created
- [x] Seed data implemented
- [x] Build succeeds

## Constitution Compliance
- [x] TDD approach followed
- [x] Code quality standards met
- [x] Performance goals defined
```

---

## 🔄 워크플로우 반복 패턴

### 1. 명세 → 계획 → 작업 → 구현
가장 일반적인 선형 흐름:
```
/speckit.specify → /speckit.plan → /speckit.tasks → /speckit.implement
```

### 2. 명세 수정 후 재계획
요구사항 변경 시:
```
/speckit.specify (updated) → /speckit.clarify → /speckit.plan → /speckit.tasks
```

### 3. 구현 중 검증
구현 단계에서 품질 확인:
```
/speckit.implement (partial) → /speckit.analyze → fix issues → /speckit.implement (continue)
```

### 4. Constitution 업데이트
프로젝트 원칙 변경 시:
```
/speckit.constitution (updated) → /speckit.plan (re-run) → verify Constitution Check
```

---

## 📈 진행 상황 추적

### 1. Task Completion Tracking
```bash
grep -c "✅" tasks.md  # 완료된 작업 수
grep -c "\[ \]" tasks.md  # 남은 작업 수
```

### 2. Phase Progress
```markdown
Phase 1: Setup          ✅ 14/14 (100%)
Phase 2: Foundational   ✅ 9/27 (33%)
Phase 3: User Story 1   ⏳ 0/18 (0%)
Phase 4: User Story 2   ⏸️ 0/28 (0%)
...
```

### 3. Constitution Compliance
```markdown
## Constitution Check

### I. Code Quality First
- [x] All functions ≤ 50 lines
- [x] All files ≤ 500 lines
- [x] Public APIs documented

### II. Test-Driven Development
- [x] Tests written before implementation
- [ ] 80% unit test coverage (현재: 0%)
- [ ] 70% integration test coverage (현재: 0%)
```

---

## 💡 Best Practices

### 1. Constitution 먼저 작성
- 모든 프로젝트는 Constitution으로 시작
- 팀 전체가 동의한 원칙만 포함
- 구체적이고 측정 가능한 기준 사용

### 2. Spec은 상세하게
- User Story는 우선순위 포함
- Success Criteria는 측정 가능하게
- Edge Cases 반드시 고려

### 3. Plan은 명확하게
- 기술 스택 선정 근거 명시
- Architecture Diagram 포함
- API Contracts 사전 정의

### 4. Tasks는 작게
- 각 작업은 1-4시간 내 완료 가능하게
- 병렬 실행 가능 여부 명시 ([P])
- User Story별로 그룹화

### 5. TDD 엄격히 준수
- 모든 User Story는 테스트 먼저
- 테스트 실패 확인 후 구현
- 리팩토링 전 항상 테스트 통과 확인

---

## 🚨 일반적인 함정

### ❌ Constitution 건너뛰기
```
문제: 구현 중 품질 기준 논쟁 발생
해결: 프로젝트 시작 전 Constitution 작성 필수
```

### ❌ Spec이 너무 모호함
```
문제: Plan 단계에서 많은 가정 필요
해결: /speckit.clarify로 불명확한 부분 해결
```

### ❌ Tasks가 너무 큼
```
문제: T042 "Implement entire Kanban board"
해결: 더 작게 분해 - T042a "Create KanbanBoard.razor", T042b "Add drag-drop logic", ...
```

### ❌ TDD 건너뛰기
```
문제: 테스트 없이 구현 후 버그 다수 발견
해결: Constitution에 TDD 의무화, Plan에서 검증
```

---

## 📞 도움말

### 각 단계별 상세 가이드
- [commands.md](./commands.md) - 8개 명령어 상세 설명
- [files.md](./files.md) - 생성되는 파일들의 역할
- [taskify-example.md](./taskify-example.md) - Taskify 사례 연구
- [best-practices.md](./best-practices.md) - 베스트 프랙티스

---

**작성일**: 2025-10-24
**버전**: 1.0
