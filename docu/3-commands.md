# Spec-Kit 명령어 완벽 가이드

Spec-Kit의 8개 슬래시 커맨드를 실제 예제와 함께 상세히 설명합니다.

---

## 📋 명령어 개요

| 명령어 | 목적 | 입력 | 출력 | 필수 여부 |
|--------|------|------|------|-----------|
| `/speckit.constitution` | 프로젝트 원칙 정의 | 대화형 질문 | constitution.md | ✅ 필수 |
| `/speckit.specify` | 기능 명세 작성 | 자연어 설명 | spec.md | ✅ 필수 |
| `/speckit.plan` | 구현 계획 수립 | spec.md | plan.md + 4개 파일 | ✅ 필수 |
| `/speckit.tasks` | 작업 목록 생성 | plan.md | tasks.md | ✅ 필수 |
| `/speckit.implement` | 코드 구현 | tasks.md | 소스 코드 | ✅ 필수 |
| `/speckit.clarify` | 명세 명확화 | spec.md | 업데이트된 spec.md | ⭕ 선택 |
| `/speckit.analyze` | 일관성 검사 | 모든 파일 | 분석 리포트 | ⭕ 선택 |
| `/speckit.checklist` | 체크리스트 생성 | 요구사항 | checklists/*.md | ⭕ 선택 |

---

## 1️⃣ /speckit.constitution

### 개요
프로젝트의 **불변 원칙**을 정의합니다. 모든 후속 작업의 품질 기준이 됩니다.

### 사용법
```bash
/speckit.constitution
```

### 대화형 프로세스

#### 예제 1: 웹 애플리케이션 프로젝트
```
🤖: 코드 품질에서 가장 중요하게 생각하는 것은 무엇인가요?
👤: 가독성과 유지보수성. 모든 함수는 50줄 이하, 파일은 500줄 이하

🤖: 테스트 전략은 어떻게 하시겠습니까?
👤: TDD 의무화. Unit 테스트 80%, Integration 70% 커버리지 목표

🤖: 성능 목표는 무엇인가요?
👤: API 응답 200ms 이하, 페이지 로딩 3초 이하 (3G 기준)

🤖: 보안 요구사항은?
👤: HTTPS 필수, JWT 인증, SQL Injection 방지, XSS 방지

🤖: 사용자 경험 기준은?
👤: WCAG 2.1 AA 준수, 모바일 우선 설계, 다크 모드 지원
```

#### 예제 2: 마이크로서비스 프로젝트
```
🤖: 아키텍처 원칙은?
👤: 독립 배포 가능, 서비스당 DB 분리, API Gateway 패턴

🤖: 장애 복구 전략은?
👤: Circuit Breaker 패턴, Retry with exponential backoff, Health checks

🤖: 로깅 및 모니터링 기준은?
👤: 구조화된 로깅 (JSON), Distributed tracing, SLA 99.9%
```

### 생성 파일
```
.specify/memory/constitution.md
```

### Constitution.md 템플릿
```markdown
# Project Constitution: [프로젝트명]

## I. Code Quality First
- [ ] 가독성 기준 정의
- [ ] 함수 크기 제한 (예: 50줄)
- [ ] 파일 크기 제한 (예: 500줄)
- [ ] 문서화 요구사항
- [ ] 타입 안정성 접근법
- [ ] 코드 스멜 방지 패턴

## II. Test-Driven Development (NON-NEGOTIABLE)
- [ ] TDD 워크플로우 정의
- [ ] 테스트 커버리지 목표
- [ ] 테스트 카테고리:
  - [ ] Unit tests
  - [ ] Integration tests
  - [ ] Contract tests
  - [ ] E2E tests

## III. User Experience Consistency
- [ ] 디자인 시스템 컴포넌트
- [ ] WCAG 2.1 AA 접근성
- [ ] 성능 예산 (예: <3s on 3G)
- [ ] 반응형 디자인 브레이크포인트
- [ ] 에러 처리 및 복구 흐름
- [ ] 로딩 상태 처리 (>200ms 작업)

## IV. Performance Requirements
- [ ] 응답 시간 목표 (API, DB, UI)
- [ ] 리소스 제약 (메모리, 번들 크기)
- [ ] 성능 모니터링 방법
- [ ] 프로파일링 및 최적화 프로세스

## V. Security by Default
- [ ] 인증 및 권한 부여
- [ ] 입력 검증 전략
- [ ] 의존성 취약점 스캐닝
- [ ] 시크릿 관리 방법
- [ ] 보안 헤더 (CSP, HSTS)
- [ ] 감사 로깅
```

### 고급 옵션
```markdown
## VI. Deployment & Infrastructure
- [ ] CI/CD 파이프라인 요구사항
- [ ] Blue-Green vs Canary deployment
- [ ] 롤백 전략
- [ ] 인프라 as Code (Terraform, Pulumi)

## VII. Observability
- [ ] 로깅 전략 (구조화된 로깅)
- [ ] 메트릭 수집 (Prometheus, Grafana)
- [ ] 분산 추적 (OpenTelemetry)
- [ ] 알림 정책 (PagerDuty, Slack)
```

### 활용 방법
1. **Plan 단계에서 자동 검증**
   - `/speckit.plan`은 Constitution Check 섹션 포함
   - 각 원칙이 설계에 반영되었는지 체크

2. **Implement 단계에서 품질 게이트**
   - 각 작업 완료 시 Constitution 준수 확인
   - 테스트 커버리지, 성능, 보안 기준 검증

3. **팀 간 합의 도구**
   - 새 팀원 온보딩 시 필독 문서
   - 기술 결정 시 참조 기준

---

## 2️⃣ /speckit.specify

### 개요
**무엇을 만들 것인가**를 상세하게 정의합니다. AI가 이해할 수 있는 구조화된 명세서를 생성합니다.

### 사용법
```bash
/speckit.specify "<자연어 기능 설명>"
```

### 입력 예제

#### 짧은 설명 (권장: 처음 사용자)
```bash
/speckit.specify "TODO 앱 만들기. 할 일 추가/수정/삭제, 완료 표시, 우선순위 설정 가능"
```

#### 중간 길이 설명 (추천)
```bash
/speckit.specify "Taskify 팀 생산성 플랫폼. 5명 사용자(PM 1명, 엔지니어 4명)가
3개 프로젝트의 작업을 Kanban 보드로 관리. 드래그 앤 드롭으로 작업 이동,
코멘트 추가/수정/삭제, 자신의 작업은 다른 색으로 표시. 로그인 없음 (테스트용)"
```

#### 긴 설명 (복잡한 프로젝트)
```bash
/speckit.specify "E-commerce 플랫폼 구축.

사용자 기능:
- 회원가입/로그인 (이메일, 소셜 로그인)
- 상품 검색 및 필터링 (카테고리, 가격, 평점)
- 장바구니 관리 (수량 조절, 쿠폰 적용)
- 주문 및 결제 (신용카드, PayPal)
- 주문 내역 조회 및 배송 추적
- 상품 리뷰 및 평점

관리자 기능:
- 상품 등록/수정/삭제
- 재고 관리 및 알림
- 주문 관리 및 상태 변경
- 매출 통계 대시보드
- 사용자 관리

기술 요구사항:
- 모바일 최적화 (반응형)
- 실시간 재고 업데이트
- 결제 보안 (PCI DSS 준수)
- 대량 트래픽 대응 (10,000+ 동시 사용자)"
```

### 생성 파일
```
specs/001-<기능명>/spec.md
```

### Spec.md 구조

#### 섹션 1: User Scenarios & Testing ⭐
```markdown
### User Story 1 - [기능명] (Priority: P1)

As a [사용자 유형], I need to [목표] so that [이유].

**Why this priority**: [우선순위 근거]

**Independent Test**: [독립적으로 테스트 가능한 방법]

**Acceptance Scenarios**:
1. **Given** [전제조건], **When** [작업], **Then** [결과]
2. **Given** [전제조건], **When** [작업], **Then** [결과]
```

#### 섹션 2: Requirements
```markdown
### Functional Requirements
- **FR-001**: [기능 요구사항]
- **FR-002**: [기능 요구사항]

### Key Entities
- **[Entity 1]**: [설명]
- **[Entity 2]**: [설명]
```

#### 섹션 3: Success Criteria
```markdown
### Measurable Outcomes
- **SC-001**: [측정 가능한 성공 기준]
- **SC-002**: [측정 가능한 성공 기준]

### Assumptions
- **Assumption 001**: [가정 사항]
```

### Taskify 실제 예제

**입력**:
```bash
/speckit.specify "Taskify 팀 생산성 플랫폼. 5명 사용자(PM 1명, 엔지니어 4명)가
3개 프로젝트의 작업을 Kanban 보드로 관리..."
```

**출력** (`spec.md` 164줄):
```markdown
# Feature Specification: Create Taskify

## User Scenarios & Testing

### User Story 1 - User Selection and Project Navigation (Priority: P1)
...5 User Stories 총...

## Requirements

### Functional Requirements
- FR-001: 시스템은 5명의 사전정의된 사용자 표시
- FR-002: 비밀번호 없이 사용자 선택 가능
...FR-017까지...

### Key Entities
- **User**: 팀 멤버 (Name, Role, Email)
- **Project**: 프로젝트 (Name, Description)
- **Task**: 작업 항목 (Title, Status, Assignee, Comments)
- **Comment**: 코멘트 (Content, Author, Timestamp)

## Success Criteria

### Measurable Outcomes
- SC-001: 사용자 선택 후 프로젝트 목록 <5초
- SC-002: 자신의 작업 식별 <3초 (색상 구분)
...SC-008까지...
```

### 베스트 프랙티스

1. **구체적으로 작성**
   ```
   ❌ "사용자가 로그인할 수 있어야 함"
   ✅ "이메일/비밀번호 또는 Google OAuth2.0으로 로그인,
       JWT 토큰 발급, 7일 유효기간"
   ```

2. **우선순위 명시**
   ```markdown
   - P1 (Critical): MVP에 필수
   - P2 (High): 초기 버전에 포함
   - P3 (Medium): 향후 추가 가능
   - P4 (Low): Nice-to-have
   ```

3. **측정 가능한 Success Criteria**
   ```
   ❌ "빠른 응답 속도"
   ✅ "API 응답 p95 < 200ms"
   ```

---

## 3️⃣ /speckit.plan

### 개요
**어떻게 만들 것인가**에 대한 기술적 청사진을 작성합니다.

### 사용법
```bash
/speckit.plan
```

**필수 전제조건**: `spec.md` 존재

### 생성 파일 (5개)
```
specs/001-<기능명>/
├── plan.md          # 구현 계획 (주 파일)
├── research.md      # 기술 조사 결과
├── data-model.md    # 데이터 모델 설계
├── quickstart.md    # 빠른 시작 가이드
└── contracts/       # API 계약 (OpenAPI)
    ├── api-1.yaml
    └── api-2.yaml
```

### Plan.md 주요 섹션

#### 1. Summary
```markdown
## Summary
[프로젝트 개요 3-5문장]
- 주요 기능
- 기술 스택
- 아키텍처 패턴
```

#### 2. Technical Context
```markdown
**Language/Version**: C# / .NET 8.0 LTS
**Primary Framework**: .NET Aspire 9.2.1
**Frontend**: Blazor Server
**Backend**: ASP.NET Core Web API
**Storage**: PostgreSQL 16
**Testing**: xUnit, bUnit
```

#### 3. Constitution Check ⭐⭐⭐
```markdown
## Constitution Check

*GATE: Must pass before implementation*

### I. Code Quality First
- [x] 가독성 기준 정의
- [x] 함수 50줄 이하 설계
...

**Gate Result**: [x] PASS / [ ] FAIL
```

#### 4. Project Structure
```markdown
### Source Code
프로젝트명/
├── src/
│   ├── frontend/
│   ├── backend/
│   └── shared/
└── tests/
```

#### 5. Architecture Diagrams
```markdown
### System Architecture
[ASCII 다이어그램 또는 Mermaid]
```

### Research.md 구조
```markdown
# Research: Create Taskify

## Phase 0: Technology Research

### 1. .NET Aspire Investigation
**Question**: .NET Aspire 9.2.1 vs 8.2.2?
**Answer**: 8.2.2 사용 (.NET 8.0 LTS 호환성)

### 2. Blazor Server vs WASM
**Options**:
- Blazor Server: 실시간, 낮은 번들 크기
- Blazor WASM: 오프라인, 높은 클라이언트 성능
**Decision**: Blazor Server (실시간 업데이트 필요)

### Version Matrix
| Package | Version | Reason |
|---------|---------|--------|
| .NET SDK | 8.0.404 | LTS |
| Aspire | 8.2.2 | .NET 8 호환 |
| EF Core | 8.0.10 | LTS |
```

### Data-Model.md 구조
```markdown
# Data Model: Create Taskify

## Entity Relationship Diagram
[ASCII ERD 또는 Mermaid]

## Tables

### users
| Column | Type | Constraints |
|--------|------|-------------|
| id | INT | PK, AUTO |
| name | VARCHAR(100) | NOT NULL |
| email | VARCHAR(200) | UNIQUE |

### projects
...
```

### Contracts/api-1.yaml 예제
```yaml
openapi: 3.0.0
info:
  title: Projects API
  version: 1.0.0
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
components:
  schemas:
    ProjectDto:
      type: object
      properties:
        id:
          type: integer
        name:
          type: string
```

### 활용 시점
- **Specify 직후**: 기술 스택 결정 전
- **Tasks 전**: 구체적 작업 분해 전
- **팀 리뷰**: Architecture Decision Record (ADR)로 활용

---

## 4️⃣ /speckit.tasks

### 개요
구현 계획을 **실행 가능한 작업**으로 분해합니다.

### 사용법
```bash
/speckit.tasks
```

**필수 전제조건**: `plan.md`, `spec.md` 존재

### 생성 파일
```
specs/001-<기능명>/tasks.md
```

### Tasks.md 구조

#### Format
```markdown
## Phase N: [Phase 이름]

**Purpose**: [이 Phase의 목적]

- [ ] T001 [P] [US1] 작업 설명 (파일명: path/to/file.ext)
- [ ] T002 작업 설명 (depends on: T001)
```

**태그 설명**:
- `[P]`: 병렬 실행 가능 (Parallel)
- `[US1]`: User Story 1에 속함
- `T001`: Task ID

#### Phase 구분
```markdown
## Phase 1: Setup (공통 인프라)
- 프로젝트 구조 초기화
- 의존성 설치
- 환경 설정

## Phase 2: Foundational (차단 전제조건)
- 데이터베이스 스키마
- 공통 유틸리티
- API 인프라

## Phase 3-N: User Story별 구현
- User Story 1 구현
- User Story 2 구현
...
```

#### TDD 구조 ⭐
```markdown
## Phase 3: User Story 1

### Tests (TDD - Write FIRST)
- [ ] T042 [P] [US1] Unit test for UserRepository
- [ ] T043 [P] [US1] Unit test for ProjectRepository
- [ ] T044 [P] [US1] Integration test for GET /api/projects
- [ ] T045 [P] [US1] E2E test for user selection flow

### Implementation (After tests FAIL)
- [ ] T049 [P] [US1] Create ProjectService
- [ ] T050 [US1] Create ProjectsController
- [ ] T051 [US1] Create UserSelection.razor
```

### Taskify 실제 예제

**생성 결과**:
```markdown
## Phase 1: Setup (14 tasks)
- [ ] T001 Create Taskify.sln
- [ ] T002 Create Taskify.AppHost (Aspire)
- [ ] T003 [P] Create Taskify.ServiceDefaults
...

## Phase 2: Foundational (27 tasks)
### Database Foundation
- [ ] T015 Create User entity (path: Taskify.ApiService/Models/Entities/User.cs)
- [ ] T016 [P] Create Project entity
- [ ] T017 [P] Create Task entity
- [ ] T018 [P] Create Comment entity
- [ ] T019 Create TaskifyDbContext
- [ ] T021 Create InitialCreate migration
- [ ] T022 Create DbContextSeed (5 users, 3 projects, 45 tasks)

## Phase 3: User Story 1 (18 tasks)
### Tests FIRST
- [ ] T042 [P] [US1] Unit test UserRepository.GetAllUsers()
- [ ] T043 [P] [US1] Unit test ProjectRepository.GetAllProjects()
...

**Total**: 170 tasks across 8 phases
```

### 작업 크기 가이드

```
✅ 적절: T042 "Create UserRepository.GetAllUsers() unit test"
   (예상 시간: 30분-1시간)

✅ 적절: T050 "Create ProjectsController with GET /api/projects endpoint"
   (예상 시간: 1-2시간)

❌ 너무 큼: T100 "Implement entire Kanban board feature"
   (예상 시간: 8-16시간)
   → 더 작게 분해 필요

❌ 너무 작음: T200 "Add semicolon to line 42"
   (예상 시간: 1분)
   → 다른 작업에 병합
```

---

## 5️⃣ /speckit.implement

### 개요
Tasks.md의 작업을 **실제 코드로 변환**합니다.

### 사용법
```bash
/speckit.implement
```

**필수 전제조건**: `tasks.md` 존재

### 실행 프로세스

#### 1. Prerequisites Check
```bash
Checking prerequisites...
✅ tasks.md exists
✅ plan.md exists
✅ spec.md exists
✅ .NET SDK 8.0.404 installed
```

#### 2. Checklist Validation
```bash
Validating checklists/requirements.md...
├── [15/15] All requirements checked
└── ✓ PASS - Implementation approved

If FAIL:
  ❌ Missing: PostgreSQL not running
  → Fix and retry
```

#### 3. Task Execution
```bash
Executing Phase 1: Setup (T001-T014)
├── T001 ✅ Created Taskify.sln
├── T002 ✅ Created Taskify.AppHost/
│   └── Program.cs (configured with PostgreSQL resource)
├── T003 ✅ Created Taskify.ServiceDefaults/
...
└── T014 ✅ Configured User Secrets

Build Status: dotnet build Taskify.sln
✅ Build succeeded - 0 errors, 0 warnings

Executing Phase 2: Foundational (T015-T041)
├── T015 ✅ Created User.cs entity
│   └── Properties: Id, Name, Email, Role, CreatedAt
├── T016 ✅ Created Project.cs entity
...
```

#### 4. Progress Tracking
```bash
Overall Progress: 23/170 tasks (13.5%)
├── Phase 1: ✅ 14/14 (100%)
├── Phase 2: ⏳ 9/27 (33%)
├── Phase 3: ⏸️ 0/18 (0%)
...
```

### 실행 옵션

#### Option A: 순차 실행 (기본)
```bash
/speckit.implement
```
- 모든 작업을 순서대로 실행
- 각 Phase 완료 후 다음 Phase

#### Option B: 특정 Phase만 실행
```bash
# 사용자 프롬프트: "Phase 1만 실행해줘"
```

#### Option C: 특정 User Story만 실행
```bash
# 사용자 프롬프트: "User Story 1만 구현해줘"
→ T001-T014 (Setup) + T015-T041 (Foundational) + T042-T059 (US1)
```

### Taskify 구현 결과

**완료 현황** (T001-T023):
```
Taskify/
├── Taskify.sln                              ✅
├── Taskify.AppHost/
│   ├── Program.cs                           ✅ PostgreSQL + service refs
│   └── Properties/launchSettings.json       ✅ Port 17275
├── Taskify.ServiceDefaults/
│   └── Extensions.cs                        ✅ OpenTelemetry, Health
├── Taskify.ApiService/
│   ├── Data/
│   │   ├── Entities/
│   │   │   ├── User.cs                      ✅
│   │   │   ├── Project.cs                   ✅
│   │   │   ├── TaskItem.cs                  ✅
│   │   │   └── Comment.cs                   ✅
│   │   ├── TaskifyDbContext.cs              ✅
│   │   ├── DbContextSeed.cs                 ✅ 5 users, 3 projects, 45 tasks
│   │   └── Migrations/InitialCreate.cs      ✅
│   ├── Program.cs                           ✅ EF Core, Swagger, CORS
│   └── Properties/launchSettings.json       ✅ Port 7001
├── Taskify.Web/
│   └── Properties/launchSettings.json       ✅ Port 7124
└── tests/                                   ✅ 2 test projects

Build: ✅ dotnet build - SUCCESS
```

---

## 6️⃣ /speckit.clarify (선택)

### 개요
명세서의 **불명확한 부분**을 식별하고 해결합니다.

### 사용법
```bash
/speckit.clarify
```

### 실행 시점
1. **Specify 직후**: 명세가 모호할 때
2. **Plan 중**: 기술 결정 시 불명확할 때
3. **Implement 중**: 구현 방법이 여러 가지일 때

### 질문 예제

```markdown
## Clarification Questions for spec.md

### Q1: Task Assignment
**Current spec**: "사용자는 작업을 할당할 수 있다"
**Question**: 한 작업에 여러 사용자 할당 가능한가요?
**Options**:
  A. 단일 담당자만 (권장)
  B. 여러 담당자 가능
  C. 주 담당자 1명 + 보조 담당자 N명
**User Answer**: [대기 중...]

After answer:
**User Answer**: A
**Updated in spec.md**: FR-009 "Each task must have exactly one assignee"

### Q2: Comment Editing Time Limit
**Current spec**: "사용자는 자신의 댓글을 수정할 수 있다"
**Question**: 댓글 수정 가능 시간 제한이 있나요?
**Options**:
  A. 제한 없음 (언제든 수정 가능)
  B. 5분 이내
  C. 24시간 이내
  D. 다른 사용자가 답글 달기 전까지
**User Answer**: [대기 중...]

### Q3: Drag-Drop Invalid Location
**Edge case**: 사용자가 작업을 Kanban 열 밖으로 드래그하면?
**Question**: 어떻게 처리할까요?
**Options**:
  A. 원래 위치로 되돌림 (권장)
  B. 작업 삭제 (위험)
  C. 무시 (아무 일도 안 일어남)
**User Answer**: [대기 중...]
```

### 업데이트 결과
```markdown
## Clarification Summary

**Total Questions**: 5
**Answered**: 5
**Spec Updates**: 3 sections modified

### Changes Made
1. FR-009: "Each task MUST have exactly one assignee" (was: "can be assigned")
2. FR-011: "Comments can be edited anytime without time limit" (was: unspecified)
3. Edge Case added: "Invalid drag-drop returns task to original position"

**Updated file**: specs/001-create-taskify/spec.md
```

---

## 7️⃣ /speckit.analyze (선택)

### 개요
생성된 모든 문서 간 **일관성을 검사**합니다.

### 사용법
```bash
/speckit.analyze
```

### 검사 항목

#### 1. Cross-Artifact Consistency
```markdown
## Analysis Report

### Consistency Check
✅ spec.md defines 5 User Stories
✅ plan.md includes all 5 User Stories
✅ tasks.md has phases for all 5 User Stories
⚠️ WARNING: spec.md mentions "real-time notifications"
   but plan.md doesn't include notification architecture
```

#### 2. Coverage Analysis
```markdown
### Coverage Analysis
✅ All 17 Functional Requirements mapped to tasks
✅ All 4 entities in data-model.md have creation tasks
⚠️ WARNING: Comment.UpdatedAt field in data-model.md
   but not in spec.md FR requirements
```

#### 3. Dependency Analysis
```markdown
### Dependency Analysis
✅ All tasks have clear dependencies
⚠️ WARNING: T089 depends on T042 but T042 is in different phase
❌ ERROR: T123 references non-existent task T999
```

#### 4. TDD Compliance
```markdown
### TDD Compliance Check
✅ User Story 1: 7 tests before 11 implementation tasks
✅ User Story 2: 6 tests before 22 implementation tasks
❌ ERROR: User Story 3: No test tasks found!
   Recommendation: Add T087-T093 as test tasks before T094
```

### 추천 액션
```markdown
## Recommendations

### High Priority
1. Add notification system to plan.md Phase 4
2. Add test tasks for User Story 3 (T087-T093)
3. Fix task T123 dependency on T999 (should be T089)

### Medium Priority
4. Clarify Comment.UpdatedAt field usage in spec.md
5. Consider breaking T089 into smaller tasks (currently 8-hour estimate)

### Low Priority
6. Add API response time requirements to Success Criteria
7. Document PostgreSQL version choice rationale in research.md
```

---

## 8️⃣ /speckit.checklist (선택)

### 개요
**커스텀 품질 체크리스트**를 생성합니다.

### 사용법
```bash
/speckit.checklist
```

### 생성 위치
```
specs/001-<기능명>/checklists/requirements.md
```

### 체크리스트 구조

```markdown
# Requirements Checklist: Create Taskify

**Date**: 2025-10-23
**Status**: 15/15 ✓ PASS

## Prerequisites (환경 준비)
- [x] .NET 8.0 SDK installed (version 8.0.404)
- [x] Docker Desktop running
- [x] PostgreSQL 16 container available
- [x] Git repository initialized
- [x] IDE configured (VS Code or Visual Studio)

## Phase 1: Setup (프로젝트 구조)
- [x] Solution file created (Taskify.sln)
- [x] AppHost project created
- [x] ServiceDefaults project created
- [x] ApiService project created
- [x] Web project created
- [x] Test projects created (2)
- [x] NuGet packages installed
- [x] User Secrets configured

## Phase 2: Foundational (기초 인프라)
- [x] Entity models created (4)
- [x] DbContext configured
- [x] Relationships defined
- [x] Migration created
- [x] Seed data implemented
- [x] Build succeeds (0 errors)

## Constitution Compliance (헌법 준수)
- [x] TDD approach followed
- [ ] 80% unit test coverage (현재: 0%)
- [ ] 70% integration test coverage (현재: 0%)
- [x] Code quality standards defined
- [x] Performance goals defined
- [x] Security measures planned

## User Story 1 (첫 번째 기능)
- [ ] Tests written FIRST
- [ ] All tests FAIL initially
- [ ] Implementation complete
- [ ] All tests PASS
- [ ] Manual testing complete
- [ ] Documentation updated

## Quality Gates (품질 검증)
- [x] Build succeeds
- [ ] All tests pass
- [ ] Code coverage ≥ targets
- [ ] No critical security issues
- [ ] Performance benchmarks met
- [ ] Accessibility WCAG AA compliant
```

### 활용 시점

#### Before `/speckit.implement`
```bash
/speckit.checklist  # 체크리스트 생성
# 수동으로 Prerequisites 확인
/speckit.implement  # 구현 시작
```

#### During Implementation
```bash
# 각 Phase 완료 후 체크리스트 업데이트
# Phase 1 완료 → Phase 1 항목 체크
# Phase 2 완료 → Phase 2 항목 체크
```

#### Before Deployment
```bash
# Quality Gates 모두 ✅ 확인
# 미완료 항목 해결
# 최종 승인
```

---

## 🔄 명령어 조합 패턴

### 패턴 1: 기본 워크플로우 (필수)
```bash
/speckit.constitution
/speckit.specify "..."
/speckit.plan
/speckit.tasks
/speckit.implement
```

### 패턴 2: 명세 불명확 시
```bash
/speckit.specify "..."
/speckit.clarify          # 명확화
/speckit.specify "..."    # 재작성 (필요시)
/speckit.plan
```

### 패턴 3: 구현 중 품질 검증
```bash
/speckit.implement  # Phase 1-2 완료
/speckit.analyze    # 일관성 검사
# 문제 수정
/speckit.implement  # 계속 진행
```

### 패턴 4: 요구사항 변경 시
```bash
/speckit.specify "..." # 업데이트
/speckit.clarify       # 명확화
/speckit.plan          # 재계획
/speckit.tasks         # 재분해
/speckit.analyze       # 일관성 검사
/speckit.implement     # 재구현
```

---

## 📞 도움말

### 더 알아보기
- [workflow.md](./workflow.md) - 전체 워크플로우 설명
- [files.md](./files.md) - 생성되는 파일 구조
- [taskify-example.md](./taskify-example.md) - 실제 사례 분석
- [best-practices.md](./best-practices.md) - 베스트 프랙티스

---

**작성일**: 2025-10-24
**버전**: 1.0
