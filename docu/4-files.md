# Spec-Kit 파일 구조 완벽 가이드

Spec-Kit이 생성하는 모든 파일의 역할과 구조를 설명합니다.

---

## 📁 전체 파일 구조

```
프로젝트/
├── .specify/                        # Spec-Kit 시스템 파일
│   └── memory/
│       └── constitution.md          # 프로젝트 헌법
├── specs/                           # 기능별 명세 디렉토리
│   └── 001-create-taskify/          # 기능 브랜치별 폴더
│       ├── spec.md                  # 기능 명세서
│       ├── plan.md                  # 구현 계획
│       ├── tasks.md                 # 작업 목록
│       ├── research.md              # 기술 조사
│       ├── data-model.md            # 데이터 모델
│       ├── quickstart.md            # 빠른 시작 가이드
│       ├── contracts/               # API 계약
│       │   ├── projects-api.yaml
│       │   ├── tasks-api.yaml
│       │   └── notifications-api.yaml
│       └── checklists/              # 품질 체크리스트
│           └── requirements.md
└── [소스 코드...]                   # 실제 구현 파일
```

---

## 🏛️ Constitution (헌법)

### 파일 위치
```
.specify/memory/constitution.md
```

### 역할
프로젝트의 **불변 원칙**과 품질 기준을 정의합니다.

### 구조
```markdown
# Project Constitution: [프로젝트명]

## I. Code Quality First
- 가독성 기준
- 함수/파일 크기 제한
- 문서화 요구사항

## II. Test-Driven Development (NON-NEGOTIABLE)
- TDD 워크플로우
- 테스트 커버리지 목표
- 테스트 카테고리

## III. User Experience Consistency
- 디자인 시스템
- 접근성 기준
- 성능 예산

## IV. Performance Requirements
- 응답 시간 목표
- 리소스 제약
- 모니터링 방법

## V. Security by Default
- 인증/권한 부여
- 입력 검증
- 취약점 관리
```

### 실제 예제 (Taskify)
```markdown
## II. Test-Driven Development (NON-NEGOTIABLE)
- ✅ TDD workflow confirmed: tests written FIRST
- ✅ Unit test coverage ≥ 80%
- ✅ Integration test coverage ≥ 70%
- ✅ E2E tests for critical flows

## IV. Performance Requirements
- ✅ API responses: p95 < 200ms
- ✅ Database queries: p95 < 100ms
- ✅ UI interactions: < 16ms (60fps)
```

### 활용
1. **Plan 단계**: Constitution Check 섹션에서 자동 검증
2. **Implement 단계**: 품질 게이트로 활용
3. **팀 온보딩**: 새 팀원 필독 문서

---

## 📋 Spec.md (기능 명세서)

### 파일 위치
```
specs/001-<기능명>/spec.md
```

### 역할
**무엇을 만들 것인가**를 상세하게 정의합니다.

### 구조 (3개 필수 섹션)

#### 1. User Scenarios & Testing
```markdown
### User Story N - [기능명] (Priority: PN)

As a [사용자], I need to [목표] so that [이유].

**Why this priority**: [근거]

**Independent Test**: [테스트 방법]

**Acceptance Scenarios**:
1. **Given** [전제], **When** [작업], **Then** [결과]
```

#### 2. Requirements
```markdown
### Functional Requirements
- **FR-001**: [요구사항]

### Key Entities
- **[Entity]**: [설명]
```

#### 3. Success Criteria
```markdown
### Measurable Outcomes
- **SC-001**: [측정 가능한 기준]

### Assumptions
- **Assumption 001**: [가정]
```

### Taskify spec.md 통계
- **크기**: 164 lines
- **User Stories**: 5개 (P1-P5)
- **Functional Requirements**: 17개
- **Key Entities**: 5개
- **Success Criteria**: 8개
- **Edge Cases**: 8개

---

## 🗺️ Plan.md (구현 계획)

### 파일 위치
```
specs/001-<기능명>/plan.md
```

### 역할
**어떻게 만들 것인가**의 기술적 청사진입니다.

### 주요 섹션

#### 1. Summary (3-5 lines)
프로젝트 개요, 기술 스택, 아키텍처 패턴

#### 2. Technical Context
```markdown
**Language/Version**: C# / .NET 8.0 LTS
**Primary Framework**: .NET Aspire 9.2.1
**Frontend**: Blazor Server
**Backend**: ASP.NET Core Web API
**Storage**: PostgreSQL 16
```

#### 3. Constitution Check ⭐
```markdown
### I. Code Quality First
- [x] 가독성 기준 정의
- [x] 함수 50줄 이하

**Gate Result**: [x] PASS / [ ] FAIL
```

#### 4. Project Structure
```markdown
Taskify.sln
├── Taskify.AppHost/
├── Taskify.ApiService/
└── Taskify.Web/
```

#### 5. Architecture Diagrams
ASCII 또는 Mermaid 다이어그램

#### 6. Implementation Phases
```markdown
### Phase 0: Research (Pre-implementation)
### Phase 1: Design (Architecture & Contracts)
### Phase 2: Implementation (Development)
### Phase 3: Testing & Quality Assurance
```

### Taskify plan.md 통계
- **크기**: 850 lines
- **기술 스택**: 12개 주요 기술
- **프로젝트 구조**: 6개 프로젝트
- **Implementation Phases**: 4개
- **Constitution Check**: 5개 섹션 모두 PASS

---

## 🔬 Research.md (기술 조사)

### 파일 위치
```
specs/001-<기능명>/research.md
```

### 역할
기술 선택의 **근거와 조사 결과**를 문서화합니다.

### 구조

#### Phase 0: Technology Research
```markdown
### 1. [기술 선택 주제]
**Question**: [선택 질문]
**Options**:
  A. [옵션 1]
  B. [옵션 2]
**Decision**: [선택]
**Rationale**: [근거]
```

#### Version Matrix
```markdown
| Package | Version | Reason |
|---------|---------|--------|
| .NET SDK | 8.0.404 | LTS |
| Aspire | 8.2.2 | .NET 8 호환 |
```

### Taskify research.md 예제
```markdown
### 2. Frontend Framework Selection
**Question**: Blazor Server vs Blazor WebAssembly?
**Options**:
  A. Blazor Server: Real-time updates, smaller bundle
  B. Blazor WASM: Offline capability, client-side performance
**Decision**: Blazor Server
**Rationale**:
  1. Real-time collaboration required (SignalR built-in)
  2. No offline requirement in initial phase
  3. Smaller bundle size for faster initial load
  4. Simpler deployment (no WASM hosting)

### Version Matrix
| Package | Version | Reason |
|---------|---------|--------|
| .NET SDK | 8.0.404 | LTS, Aspire compatibility |
| .NET Aspire | 8.2.2 | .NET 8.0 LTS compatible |
| EF Core | 8.0.10 | LTS alignment |
| FluentValidation | 12.0+ | .NET 8 minimum |
| bUnit | 1.40.0 | .NET 8 support |
```

---

## 📊 Data-Model.md (데이터 모델)

### 파일 위치
```
specs/001-<기능명>/data-model.md
```

### 역할
데이터베이스 스키마와 **엔티티 관계**를 정의합니다.

### 구조

#### 1. Entity Relationship Diagram
```
User (1) ──< (N) Task
User (1) ──< (N) Comment
Project (1) ──< (N) Task
Task (1) ──< (N) Comment
```

#### 2. Tables
```markdown
### users
| Column | Type | Constraints |
|--------|------|-------------|
| id | INT | PK, AUTO_INCREMENT |
| name | VARCHAR(100) | NOT NULL |
| email | VARCHAR(200) | NOT NULL, UNIQUE |
| role | VARCHAR(50) | NOT NULL |
| created_at | TIMESTAMP | DEFAULT NOW() |
```

#### 3. Indexes
```markdown
### Performance Indexes
- users.email (UNIQUE) - Authentication lookup
- tasks.project_id - Project tasks query
- tasks.assigned_to_id - User tasks query
- tasks.status - Kanban board filtering
- comments.task_id - Task comments query
```

#### 4. Relationships
```markdown
### Foreign Keys
- tasks.project_id → projects.id (CASCADE DELETE)
- tasks.assigned_to_id → users.id (SET NULL)
- comments.task_id → tasks.id (CASCADE DELETE)
- comments.user_id → users.id (CASCADE DELETE)
```

---

## 🚀 Quickstart.md (빠른 시작 가이드)

### 파일 위치
```
specs/001-<기능명>/quickstart.md
```

### 역할
**5-10분 내** 프로젝트를 실행할 수 있는 가이드입니다.

### 구조

#### 1. Prerequisites
```markdown
- .NET 8.0 SDK
- Docker Desktop
- Git
```

#### 2. Quick Start (5 steps)
```markdown
### 1. Clone Repository
git clone <repo>

### 2. Start PostgreSQL
docker run --name taskify-postgres ...

### 3. Set User Secrets
dotnet user-secrets set "ConnectionStrings:..."

### 4. Run Migrations
dotnet ef database update

### 5. Run Application
dotnet run
```

#### 3. Test Application
```markdown
1. User Selection: Click on user
2. Project List: See 3 projects
3. Kanban Board: See 4 columns
4. Drag and Drop: Move task
5. Comments: Add comment
```

#### 4. Troubleshooting
```markdown
### Issue: PostgreSQL connection failed
**Symptom**: Connection timeout
**Solution**: Check docker ps, verify connection string
```

---

## 📜 Contracts/ (API 계약)

### 파일 위치
```
specs/001-<기능명>/contracts/
├── projects-api.yaml
├── tasks-api.yaml
└── notifications-api.yaml
```

### 역할
**OpenAPI 3.0** 스펙으로 REST API를 사전 정의합니다.

### 구조 (projects-api.yaml 예제)
```yaml
openapi: 3.0.0
info:
  title: Projects API
  version: 1.0.0
  description: Taskify 프로젝트 관리 API

paths:
  /api/projects:
    get:
      summary: Get all projects
      tags: [Projects]
      responses:
        '200':
          description: Success
          content:
            application/json:
              schema:
                type: array
                items:
                  $ref: '#/components/schemas/ProjectDto'

  /api/projects/{projectId}:
    get:
      summary: Get project by ID
      parameters:
        - name: projectId
          in: path
          required: true
          schema:
            type: integer
      responses:
        '200':
          description: Success
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/ProjectWithTasksDto'
        '404':
          description: Project not found

components:
  schemas:
    ProjectDto:
      type: object
      required: [id, name]
      properties:
        id:
          type: integer
          example: 1
        name:
          type: string
          example: "E-commerce Platform"
        description:
          type: string
        createdAt:
          type: string
          format: date-time
```

### 활용
1. **Contract-First Development**: API를 먼저 설계
2. **Testing**: Contract 테스트에 사용
3. **Documentation**: Swagger UI로 자동 문서화
4. **Code Generation**: OpenAPI Generator로 클라이언트 SDK 생성

---

## 📝 Tasks.md (작업 목록)

### 파일 위치
```
specs/001-<기능명>/tasks.md
```

### 역할
구현 계획을 **실행 가능한 작업**으로 분해합니다.

### Format
```markdown
- [ ] T001 [P] [US1] 작업 설명 (path: file.ext)
```

**태그**:
- `T001`: Task ID
- `[P]`: Parallel (병렬 실행 가능)
- `[US1]`: User Story 1에 속함

### 구조

#### Phase 구분
```markdown
## Phase 1: Setup (14 tasks)
공통 인프라 설정

## Phase 2: Foundational (27 tasks)
차단 전제조건 (모든 User Story의 기반)

## Phase 3-N: User Story별
각 User Story 구현
```

#### TDD 구조
```markdown
## Phase 3: User Story 1

### Tests (TDD - Write FIRST)
- [ ] T042 [P] [US1] Unit test ...
- [ ] T043 [P] [US1] Integration test ...

### Implementation (After tests FAIL)
- [ ] T049 [P] [US1] Create service ...
- [ ] T050 [US1] Create controller ...
```

### Taskify tasks.md 통계
- **총 작업**: 170개
- **Phase 수**: 8개
- **평균 작업당 예상 시간**: 1-2시간
- **TDD 구조**: 모든 User Story에 "Tests FIRST" 섹션 포함

---

## ✅ Checklists/ (품질 체크리스트)

### 파일 위치
```
specs/001-<기능명>/checklists/requirements.md
```

### 역할
구현 전후 **품질 검증**을 위한 체크리스트입니다.

### 구조
```markdown
# Requirements Checklist: [기능명]

**Status**: 15/20 ⏳ IN PROGRESS

## Prerequisites
- [x] .NET SDK installed
- [x] Docker running
- [ ] PostgreSQL setup

## Phase 1: Setup
- [x] Solution created
- [x] Projects created
- [ ] NuGet packages installed

## Constitution Compliance
- [x] TDD approach planned
- [ ] 80% unit coverage (현재: 0%)
- [ ] 70% integration coverage (현재: 0%)

## User Story 1
- [ ] Tests written FIRST
- [ ] All tests FAIL initially
- [ ] Implementation complete
- [ ] All tests PASS
```

### 활용 시점
1. **Before `/speckit.implement`**: Prerequisites 확인
2. **During Implementation**: 각 Phase 완료 후 체크
3. **Before Deployment**: Quality Gates 모두 통과 확인

---

## 📂 실제 프로젝트 파일 구조 (Taskify)

```
test_speckit/
├── .specify/
│   └── memory/
│       └── constitution.md                 ✅ 5개 섹션
├── specs/
│   └── 001-create-taskify/
│       ├── spec.md                         ✅ 164 lines, 5 User Stories
│       ├── plan.md                         ✅ 850 lines, Constitution Check
│       ├── tasks.md                        ✅ 170 tasks, 8 phases
│       ├── research.md                     ✅ .NET Aspire 9.2.1 조사
│       ├── data-model.md                   ✅ 4 tables, ERD
│       ├── quickstart.md                   ✅ 5-minute setup guide
│       ├── contracts/
│       │   ├── projects-api.yaml           ✅ OpenAPI 3.0
│       │   ├── tasks-api.yaml              ✅ CRUD endpoints
│       │   └── notifications-api.yaml      ✅ Real-time API
│       └── checklists/
│           └── requirements.md             ✅ 15/15 PASS
├── Taskify.sln                             ✅ Solution file
├── Taskify.AppHost/                        ✅ Aspire orchestration
│   ├── Program.cs                          ✅ PostgreSQL + services
│   └── appsettings.json
├── Taskify.ServiceDefaults/                ✅ Shared config
│   └── Extensions.cs                       ✅ OpenTelemetry, Health
├── Taskify.ApiService/                     ✅ REST API
│   ├── Controllers/                        ⏳ (upcoming)
│   ├── Services/                           ⏳ (upcoming)
│   ├── Repositories/                       ⏳ (upcoming)
│   ├── Models/
│   │   └── Entities/
│       │   ├── User.cs                     ✅
│       │   ├── Project.cs                  ✅
│       │   ├── TaskItem.cs                 ✅
│       │   └── Comment.cs                  ✅
│   ├── Data/
│   │   ├── TaskifyDbContext.cs             ✅ EF Core config
│   │   ├── DbContextSeed.cs                ✅ 5 users, 3 projects, 45 tasks
│   │   └── Migrations/
│   │       └── InitialCreate.cs            ✅ Schema creation
│   └── Program.cs                          ✅ Startup config
├── Taskify.Web/                            ✅ Blazor Server
│   ├── Components/
│   │   ├── Pages/                          ⏳ (upcoming)
│   │   └── Shared/                         ⏳ (upcoming)
│   ├── Services/                           ⏳ (upcoming)
│   └── Program.cs                          ✅
└── tests/                                  ✅ Test projects
    ├── Taskify.ApiService.Tests/           ✅ xUnit
    └── Taskify.Web.Tests/                  ✅ bUnit

**진행률**: 23/170 tasks (13.5%)
**빌드 상태**: ✅ SUCCESS
```

---

## 📊 파일 크기 및 복잡도

### Taskify 프로젝트 통계

| 파일 | 크기 | 복잡도 | 작성 시간 |
|------|------|--------|-----------|
| constitution.md | ~200 lines | Low | 10-15 min |
| spec.md | 164 lines | Medium | 20-30 min |
| plan.md | 850 lines | High | 40-60 min |
| research.md | 600 lines | High | 30-45 min |
| data-model.md | 300 lines | Medium | 15-20 min |
| quickstart.md | 350 lines | Low | 10-15 min |
| tasks.md | 800 lines | Very High | 45-60 min |
| contracts/*.yaml | ~150 lines/file | Medium | 10 min/file |

**총 문서 작성 시간**: ~3-4시간 (AI 없이 수동 작성 시 8-12시간)

---

## 💡 파일 관리 팁

### 1. 버전 관리
```bash
# Spec-Kit 파일은 Git으로 관리
git add .specify/ specs/
git commit -m "docs: Add Taskify specifications"
```

### 2. 파일 명명 규칙
```
specs/
├── 001-create-taskify/      # 3자리 숫자 + 기능명 (kebab-case)
├── 002-add-notifications/
└── 003-implement-analytics/
```

### 3. 문서 동기화
```bash
# spec.md 변경 시
/speckit.clarify         # 명확화
/speckit.plan            # plan.md 재생성
/speckit.tasks           # tasks.md 재생성
/speckit.analyze         # 일관성 검사
```

### 4. 아카이빙
```bash
# 완료된 기능은 별도 디렉토리로
specs/
├── active/
│   └── 003-implement-analytics/
└── completed/
    ├── 001-create-taskify/
    └── 002-add-notifications/
```

---

## 📞 도움말

### 더 알아보기
- [workflow.md](./workflow.md) - 전체 워크플로우
- [commands.md](./commands.md) - 명령어 상세
- [taskify-example.md](./taskify-example.md) - 실제 사례
- [best-practices.md](./best-practices.md) - 베스트 프랙티스

---

**작성일**: 2025-10-24
**버전**: 1.0
