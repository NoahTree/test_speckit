# Spec-Kit 베스트 프랙티스

실제 프로젝트 경험을 바탕으로 한 실용적인 가이드입니다.

---

## 🎯 Constitution (헌법) 작성

### ✅ DO

#### 1. 구체적이고 측정 가능하게
```markdown
❌ "코드는 빨라야 한다"
✅ "API 응답 시간 p95 < 200ms"

❌ "좋은 테스트 커버리지"
✅ "Unit test coverage ≥ 80%, Integration ≥ 70%"
```

#### 2. 팀 전체 합의
```markdown
Constitution 작성 전:
1. 팀 미팅 소집
2. 각 섹션 논의
3. 합의 도출
4. 문서화

이유: Constitution은 NON-NEGOTIABLE
     모두가 동의한 원칙만 포함
```

#### 3. 프로젝트 특성 반영
```markdown
웹 애플리케이션:
- Performance: <3s on 3G
- Accessibility: WCAG 2.1 AA
- Security: HTTPS, XSS prevention

마이크로서비스:
- Reliability: 99.9% uptime
- Observability: Distributed tracing
- Resilience: Circuit breaker
```

### ❌ DON'T

#### 1. 추상적인 원칙
```markdown
❌ "품질이 중요하다"
❌ "성능을 고려해야 한다"
❌ "보안에 신경써야 한다"

→ 구체적인 기준으로 변환 필요
```

#### 2. 너무 많은 원칙
```markdown
❌ 50개 이상의 규칙
✅ 5-10개 핵심 원칙

이유: 기억하고 실행 가능한 수준으로
```

#### 3. 일방적인 결정
```markdown
❌ CTO 혼자 결정
✅ 팀 전체 논의

이유: 팀원 모두가 따라야 하므로
```

---

## 📝 Specification (명세서) 작성

### ✅ DO

#### 1. User Story 우선순위 명시
```markdown
✅ User Story 1 (P1 - Critical)
   As a user, I need to login...

   **Why this priority**:
   - Entry point to entire application
   - Blocks all other features
   - Security requirement

✅ User Story 5 (P5 - Nice-to-have)
   As a user, I want to export data...

   **Why this priority**:
   - Not essential for MVP
   - Can be added later
```

#### 2. Given-When-Then 형식 활용
```markdown
✅ **Given** user is logged in
   **When** they click "New Project" button
   **Then** project creation modal appears

✅ **Given** modal is open
   **When** they enter project name and click "Create"
   **Then** new project is created and modal closes
```

#### 3. Edge Cases 포함
```markdown
✅ Edge Cases 섹션 추가:

- What happens when user drags task outside valid drop zone?
  → Return to original position with animation

- What happens when multiple users edit same task?
  → Last write wins, show conflict notification

- What happens with 50+ comments on single task?
  → Paginate comments, show 10 at a time
```

### ❌ DON'T

#### 1. 모호한 표현
```markdown
❌ "사용자는 작업을 관리할 수 있다"
✅ "사용자는 작업을 생성, 수정, 삭제, 상태 변경할 수 있다"

❌ "빠른 응답 속도"
✅ "API 응답 p95 < 200ms, UI 인터랙션 < 100ms"
```

#### 2. 기술 구현 세부사항
```markdown
❌ "Redux를 사용한 상태 관리"
❌ "PostgreSQL에 저장"
→ Plan 단계에서 결정

✅ "실시간 업데이트 필요"
✅ "관계형 데이터 모델 필요"
→ 요구사항만 명시
```

#### 3. Success Criteria 누락
```markdown
❌ User Story만 작성하고 끝
✅ 각 User Story에 측정 가능한 성공 기준 포함

예:
- SC-001: User selection to project list < 5 seconds
- SC-002: Drag-drop perceived latency < 100ms
```

---

## 🗺️ Plan (계획) 작성

### ✅ DO

#### 1. 기술 선택 근거 명시
```markdown
✅ research.md 활용:

### Blazor Server vs WASM
**Question**: Which Blazor hosting model?
**Investigation**:
  - Server: Real-time, smaller bundle, server dependency
  - WASM: Offline, larger bundle, client-side performance

**Decision**: Blazor Server
**Rationale**:
  1. Real-time collaboration required
  2. No offline requirement (initial phase)
  3. Smaller bundle for faster initial load

**Trade-offs**:
  - Pro: SignalR built-in, easier debugging
  - Con: Server load scales with users
  - Mitigation: Use connection pooling, scale horizontally
```

#### 2. Architecture Diagram 포함
```markdown
✅ ASCII 다이어그램:

┌─────────────┐
│ Blazor Web  │ ─── SignalR ──→ Real-time updates
│  (Port 7124)│                  (WebSocket)
└──────┬──────┘
       │ HTTPS/REST
       ↓
┌─────────────┐
│  API Service│ ─── REST API
│  (Port 7001)│     (OpenAPI 3.0)
└──────┬──────┘
       │ EF Core 8.0
       ↓
┌─────────────┐
│ PostgreSQL  │ ─── Container
│  (Port 5432)│     (postgres:16-alpine)
└─────────────┘
```

#### 3. OpenAPI 계약 사전 정의
```markdown
✅ contracts/projects-api.yaml:

paths:
  /api/projects:
    get:
      summary: Get all projects
      responses:
        '200':
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/ProjectListDto'

이유:
- Contract-First Development
- API 변경 시 자동 검증
- 클라이언트 코드 생성 가능
```

### ❌ DON'T

#### 1. Constitution Check 건너뛰기
```markdown
❌ Plan만 작성하고 끝
✅ Constitution Check 섹션 필수:

## Constitution Check

### I. Code Quality First
- [x] 가독성 기준 정의
- [x] 함수 50줄 이하
...

**Gate Result**: [x] PASS

이유: 헌법 준수 여부를 Plan 단계에서 검증
     FAIL 시 Plan 재작성 필요
```

#### 2. Version Matrix 누락
```markdown
❌ "EF Core 사용"
✅ "EF Core 8.0.10 사용 (.NET 8.0 LTS 호환)"

research.md에 Version Matrix 포함:
| Package | Version | Reason |
|---------|---------|--------|
| EF Core | 8.0.10 | LTS, Aspire 8.2.2 compatible |
```

#### 3. Quickstart Guide 생략
```markdown
❌ Plan만 작성
✅ quickstart.md 함께 생성

이유:
- 새 팀원 온보딩 시간 단축 (2시간 → 10분)
- 로컬 환경 설정 표준화
- 문제 해결 가이드 포함
```

---

## ✅ Tasks (작업 목록) 분해

### ✅ DO

#### 1. TDD 구조 엄격히 준수
```markdown
✅ User Story 작업 구조:

## Phase 3: User Story 1

### Tests (TDD - Write FIRST)
- [ ] T042 [P] [US1] Unit test: UserRepository
- [ ] T043 [P] [US1] Integration test: GET /api/projects
- [ ] T044 [P] [US1] E2E test: User selection flow

### Implementation (After tests FAIL)
- [ ] T049 [P] [US1] Create UserRepository
- [ ] T050 [US1] Create ProjectsController
- [ ] T051 [US1] Create UserSelection.razor

이유: Tests FIRST 섹션이 먼저 오도록 강제
```

#### 2. 작업 크기 1-4시간
```markdown
✅ 적절한 크기:
- T050 "Create ProjectsController with GET /api/projects endpoint"
  예상: 1-2시간

❌ 너무 큼:
- T100 "Implement entire Kanban board"
  예상: 8-16시간
  → 더 작게 분해 필요

❌ 너무 작음:
- T200 "Add semicolon to line 42"
  예상: 1분
  → 다른 작업과 병합
```

#### 3. 병렬 실행 가능 표시
```markdown
✅ [P] 태그 활용:

Phase 2: Database Foundation
- [ ] T015 Create User entity
- [ ] T016 [P] Create Project entity    ← 병렬 가능
- [ ] T017 [P] Create Task entity       ← 병렬 가능
- [ ] T018 [P] Create Comment entity    ← 병렬 가능
- [ ] T019 Create TaskifyDbContext      ← T015-T018 후

이유: 4개 엔티티는 독립적, DbContext는 의존성
```

### ❌ DON'T

#### 1. User Story 없이 작업 나열
```markdown
❌ 작업만 나열:
- [ ] T001 Create database
- [ ] T002 Create API
- [ ] T003 Create UI
...

✅ User Story별 그룹화:
- Phase 3: User Story 1 (User Selection)
- Phase 4: User Story 2 (Kanban Board)
- Phase 5: User Story 3 (Drag-Drop)
...

이유: 어떤 기능에 속하는지 명확화
```

#### 2. 파일 경로 누락
```markdown
❌ "Create UserRepository"
✅ "Create UserRepository (path: Taskify.ApiService/Repositories/UserRepository.cs)"

이유: 정확한 위치 지정으로 혼란 방지
```

#### 3. 의존성 불명확
```markdown
❌ 순서 없이 나열
✅ 명확한 Phase 구분:

Phase 1: Setup (모든 후속 작업의 전제)
Phase 2: Foundational (모든 User Story의 기반)
Phase 3-N: User Story별 (독립적)

이유: 작업 순서 명확화, 병렬 실행 가능 파악
```

---

## 💻 Implementation (구현) 실행

### ✅ DO

#### 1. Prerequisites 먼저 확인
```bash
✅ 구현 전 체크:
- [ ] .NET SDK 설치 확인
- [ ] Docker 실행 확인
- [ ] PostgreSQL 컨테이너 실행
- [ ] User Secrets 설정
- [ ] Build 성공 확인

명령어:
dotnet --version
docker ps
dotnet build
```

#### 2. Phase별 검증
```markdown
✅ 각 Phase 완료 후:

Phase 1 완료 후:
- dotnet build ← 빌드 성공 확인
- dotnet test ← 테스트 통과 확인 (있다면)
- Git commit ← 작업 단위로 커밋

Phase 2 완료 후:
- dotnet ef database update ← 마이그레이션 적용
- 시드 데이터 확인
- Git commit
```

#### 3. MVP 전략 활용
```markdown
✅ MVP First 접근:

Option A: Setup + Foundational + User Story 1
├── 59 tasks (전체의 35%)
├── 예상 시간: 4-6시간
└── 결과: 기능하는 최소 버전

이유:
- 빠른 검증 가능
- 초기 피드백 수집
- 리스크 조기 발견
```

### ❌ DON'T

#### 1. TDD 건너뛰기
```markdown
❌ 테스트 없이 구현부터
✅ Tests FIRST 섹션 먼저 완료

순서:
1. T042 Unit test 작성 → ❌ FAIL 확인
2. T049 Implementation → ✅ PASS 확인
3. 리팩토링 (필요시)
4. 다음 테스트로

이유: Constitution에서 TDD 의무화
```

#### 2. 전체 구현 시도
```markdown
❌ 170개 작업 모두 구현
✅ MVP 먼저 (59개 작업)

이유:
- 전체 구현 시 피드백 늦음
- 요구사항 변경 시 리워크 발생
- MVP로 검증 후 확장
```

#### 3. Build 실패 무시
```markdown
❌ Build 실패해도 계속 진행
✅ 각 작업 후 Build 확인

이유:
- 문제 조기 발견
- 의존성 문제 파악
- 품질 게이트 역할
```

---

## 🔄 선택적 명령어 활용

### `/speckit.clarify` 사용 시점

#### ✅ DO: 명세가 불명확할 때
```markdown
시나리오 1: Spec 작성 직후
/speckit.specify "..."
→ spec.md 생성됨
→ 리뷰 시 모호한 부분 발견
→ /speckit.clarify 실행
→ 명확화 후 Plan 진행

시나리오 2: Plan 중 불명확
/speckit.plan 실행 중
→ "Task assignment: 단일 vs 복수?"
→ 일시 중단, /speckit.clarify
→ 답변 후 Plan 재개
```

#### ❌ DON'T: 모든 프로젝트에 사용
```markdown
❌ 항상 /speckit.clarify 실행
✅ 필요할 때만 사용

이유:
- 시간 추가 소요 (10-15분)
- 간단한 프로젝트는 불필요
- Spec이 명확하면 건너뛰기
```

### `/speckit.analyze` 사용 시점

#### ✅ DO: 큰 프로젝트나 팀 프로젝트
```markdown
시나리오: 복잡한 프로젝트 (50+ User Stories)
/speckit.tasks 완료 후
→ /speckit.analyze 실행
→ 일관성 검사:
  ✅ spec.md에 50개 User Story
  ✅ tasks.md에 50개 Phase
  ⚠️ WARNING: spec.md FR-042 없지만 tasks.md T042 존재
→ 수정 후 재확인
```

#### ❌ DON'T: 작은 프로젝트에 매번
```markdown
❌ TODO 앱 (5개 User Story)에도 /speckit.analyze
✅ 수동 검토로 충분

이유:
- 작은 프로젝트는 육안 검토 빠름
- 도구 오버헤드 불필요
```

---

## 🏆 프로젝트 유형별 전략

### 소규모 프로젝트 (< 20 tasks)

```markdown
전략: 빠른 실행

1. Constitution: 간단히 (5개 원칙)
2. Specify: 짧게 (2-3 User Stories)
3. Plan: 핵심만 (research.md 생략 가능)
4. Tasks: 1 Phase로 통합
5. Implement: 전체 한번에

시간: 2-3시간 (문서 + 구현)
```

### 중규모 프로젝트 (20-100 tasks)

```markdown
전략: MVP First

1. Constitution: 표준 (10개 원칙)
2. Specify: 상세 (5-10 User Stories)
3. Plan: 완전 (research.md + contracts/ 포함)
4. Tasks: Phase 구분 (Setup + Foundational + 3-5 User Stories)
5. Implement: MVP (Setup + Foundational + US1)

시간: 1-2일 (문서) + 1주일 (MVP)
```

### 대규모 프로젝트 (100+ tasks)

```markdown
전략: 단계적 접근

1. Constitution: 상세 (15+ 원칙, 팀 합의)
2. Specify: 우선순위화 (10-20 User Stories, P1-P5)
3. Plan: 완전 + 검증 (/speckit.analyze)
4. Tasks: 세밀한 Phase (10+ phases)
5. Implement: 점진적 (MVP → V1.0 → V2.0)

시간: 1주일 (문서) + 2-3개월 (전체 구현)
```

---

## 💡 팀 협업 베스트 프랙티스

### 1. Constitution은 팀 미팅에서
```markdown
✅ 프로젝트 킥오프 미팅:
- 각 섹션 논의 (각 15분, 총 1.5시간)
- 합의 도출
- 문서화
- 전원 서명 (상징적)

이유: 모두가 동의한 원칙만 효과적
```

### 2. Spec 리뷰 프로세스
```markdown
✅ Spec 작성 후:
1. Draft 작성 (/speckit.specify)
2. 팀 리뷰 요청 (30분 회의)
3. 피드백 반영 (/speckit.clarify)
4. 최종 승인
5. Plan 진행

이유: 요구사항 오해 방지
```

### 3. Tasks 할당 전략
```markdown
✅ Tasks 생성 후:

Phase 1: Setup
→ DevOps 엔지니어 또는 시니어 개발자

Phase 2: Foundational
→ 백엔드/프론트엔드 팀 분담

Phase 3-N: User Stories
→ 기능별 팀 할당 (독립적으로 진행 가능)

[P] 태그 작업:
→ 병렬 실행, 다른 개발자에게 할당
```

---

## 🚨 일반적인 실수와 해결

### 실수 1: Constitution 건너뛰기

**문제**:
```markdown
Constitution 없이 바로 Specify → Plan
→ Plan 단계에서 "TDD 할까? 말까?" 논쟁
→ 팀 간 의견 차이
→ 시간 낭비
```

**해결**:
```markdown
✅ 프로젝트 시작 전 Constitution 작성
✅ TDD, 테스트 커버리지, 성능 목표 명시
✅ 팀 전체 합의

결과: Plan 단계에서 자동 검증, 논쟁 없음
```

### 실수 2: Spec이 너무 모호

**문제**:
```markdown
Spec: "사용자는 작업을 관리할 수 있다"
→ Plan 단계에서 가정 필요:
  - 생성만? 수정도? 삭제도?
  - 단일 할당? 복수 할당?
  - 실시간 업데이트?
→ /speckit.clarify 반복 실행
→ 시간 낭비
```

**해결**:
```markdown
✅ Spec 작성 시 구체적으로:
- "사용자는 작업을 생성, 수정, 삭제할 수 있다"
- "한 작업은 단일 담당자만 가질 수 있다"
- "작업 상태 변경은 실시간으로 반영된다"

결과: Clarify 단계 생략 가능
```

### 실수 3: Tasks가 너무 큼

**문제**:
```markdown
T100 "Implement entire Kanban board feature"
→ 예상 시간: 16시간
→ 진행 상황 파악 어려움
→ 블로커 발생 시 전체 지연
```

**해결**:
```markdown
✅ 더 작게 분해:
- T100a "Create KanbanBoard.razor component (empty)"
- T100b "Add 4 columns (To Do, In Progress, In Review, Done)"
- T100c "Display task cards in columns"
- T100d "Implement drag source events"
- T100e "Implement drop target events"
- T100f "Update task status on drop"

각 1-2시간, 총 6-12시간
결과: 진행 상황 명확, 병렬 실행 가능
```

### 실수 4: TDD 무시

**문제**:
```markdown
Tests 건너뛰고 Implementation 먼저
→ 버그 다수 발견 (구현 후)
→ 수정 시간 2-3배 증가
→ Constitution 위반
```

**해결**:
```markdown
✅ TDD 엄격히 준수:
1. T042 Test 작성 → ❌ FAIL
2. T049 Implementation → ✅ PASS
3. Refactor (필요시)
4. Next test

결과: 버그 조기 발견, 리팩토링 안전
```

---

## 📊 성공 지표

### 문서 품질
```markdown
✅ 좋은 문서:
- Constitution: 5-15개 구체적 원칙
- Spec: 5-20 User Stories, 우선순위 명확
- Plan: Constitution Check PASS
- Tasks: 평균 작업 시간 1-4시간

❌ 나쁜 문서:
- Constitution: 추상적 원칙만
- Spec: 모호한 요구사항
- Plan: Constitution Check 누락
- Tasks: 평균 작업 시간 8+ 시간
```

### 구현 효율
```markdown
✅ 효율적 구현:
- MVP 완성: 전체의 30-40%
- Build 항상 성공
- Test coverage ≥ 목표
- 작업당 평균 완료 시간 ≤ 예상 시간

❌ 비효율적 구현:
- 전체 구현 시도
- Build 실패 방치
- Test coverage << 목표
- 작업당 평균 완료 시간 >> 예상 시간
```

---

## 🎓 학습 추천 경로

### 1주차: 기초
```markdown
Day 1-2: 문서 읽기
- README.md
- workflow.md
- commands.md

Day 3-4: 간단한 프로젝트
- TODO 앱 (10-15 tasks)
- Constitution → Specify → Plan → Tasks → Implement
- 전 과정 경험

Day 5-7: 복습 및 정리
- 배운 내용 정리
- 팀과 공유
```

### 2주차: 실전
```markdown
Day 1-3: 중간 프로젝트
- E-commerce 또는 Blog (50-80 tasks)
- MVP First 전략
- TDD 엄격히 준수

Day 4-5: 팀 협업
- 팀원과 함께 Constitution 작성
- Spec 리뷰 프로세스
- Tasks 분담

Day 6-7: 고급 기능
- /speckit.clarify 활용
- /speckit.analyze 활용
- Custom Constitution 작성
```

---

## 📞 참고 자료

- [README.md](./README.md) - Spec-Kit 개요
- [workflow.md](./workflow.md) - 워크플로우 상세
- [commands.md](./commands.md) - 명령어 가이드
- [files.md](./files.md) - 파일 구조
- [taskify-example.md](./taskify-example.md) - 실제 사례

---

**작성일**: 2025-10-24
**버전**: 1.0
