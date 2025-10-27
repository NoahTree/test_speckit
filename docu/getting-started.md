# Spec-Kit 첫 프로젝트 시작하기

**목표**: 실제 프로젝트를 통해 Spec-Driven Development 워크플로우 체득하기

**소요 시간**: 30-60분

**프로젝트**: 간단한 북마크 관리 앱

---

## 📋 목차

1. [프로젝트 선택하기](#-프로젝트-선택하기)
2. [프로젝트 초기화](#-프로젝트-초기화)
3. [Constitution 작성](#-constitution-작성프로젝트-원칙)
4. [Specification 작성](#-specification-작성기능-명세)
5. [Plan 생성](#-plan-생성구현-계획)
6. [Tasks 생성](#-tasks-생성작업-분해)
7. [Implementation 실행](#-implementation-실행코드-생성)
8. [검증 및 테스트](#-검증-및-테스트)
9. [다음 기능 추가](#-다음-기능-추가)

---

## 🎯 프로젝트 선택하기

### 초보자에게 적합한 프로젝트

| 프로젝트 | 복잡도 | 소요 시간 | 학습 내용 |
|----------|--------|-----------|-----------|
| **카운터 앱** | ⭐☆☆☆☆ | 10분 | 기본 워크플로우 |
| **TODO 앱** | ⭐⭐☆☆☆ | 30분 | CRUD, 상태 관리 |
| **북마크 관리** | ⭐⭐⭐☆☆ | 1시간 | 데이터 모델, 검색 |
| **블로그 시스템** | ⭐⭐⭐⭐☆ | 2-3시간 | 인증, API, DB |

**이 가이드에서는 "북마크 관리 앱"을 만들어 봅니다.**

---

## 🚀 프로젝트 초기화

### 1. 새 프로젝트 생성

```bash
# 프로젝트 디렉토리 생성
specify init bookmark-manager --ai claude
cd bookmark-manager

# 구조 확인
ls -la
```

**생성된 구조**:
```
bookmark-manager/
├── specs/                  # 기능 명세서가 저장될 위치
├── .specify/
│   ├── memory/
│   │   └── constitution.md # (비어있음)
│   ├── scripts/            # 자동화 스크립트
│   └── templates/          # 문서 템플릿
├── .gitignore
└── README.md
```

### 2. AI 도구 열기

```bash
# VS Code (Claude Code)
code .

# 또는 Cursor
cursor .
```

---

## 📜 Constitution 작성 (프로젝트 원칙)

### 왜 Constitution이 중요한가?

Constitution은 **프로젝트의 불변 원칙**입니다. 모든 후속 작업의 품질 기준이 되며:
- AI가 코드를 생성할 때 자동으로 참조
- Plan 단계에서 자동 검증
- 팀 간 개발 표준 통일

### 명령어 실행

Claude Code에서:
```
/speckit.constitution
```

### 대화형 프로세스

#### Q1: 코드 품질 기준
```
🤖: 코드 품질에서 가장 중요하게 생각하는 것은 무엇인가요?

✅ 좋은 답변:
"가독성과 유지보수성을 최우선으로 합니다.
- 함수는 50줄 이하로 제한
- 파일은 500줄 이하
- 명확한 네이밍 (동사+명사, 설명적)
- 주석은 '왜'를 설명 ('무엇'은 코드로)
- 중첩 깊이 3단계 이하"

❌ 나쁜 답변:
"코드를 깔끔하게 작성"  # 너무 모호함
```

#### Q2: 테스트 전략
```
🤖: 테스트 전략은 어떻게 하시겠습니까?

✅ 좋은 답변:
"TDD(Test-Driven Development) 의무화
- Unit 테스트 커버리지 80% 이상
- Integration 테스트 70% 이상
- E2E 테스트 주요 User Flow만
- 테스트는 항상 구현 전에 작성 (Red-Green-Refactor)
- Given-When-Then 구조 사용"

❌ 나쁜 답변:
"테스트 코드를 작성한다"  # 구체성 부족
```

#### Q3: 성능 목표
```
🤖: 성능 목표는 무엇인가요?

✅ 좋은 답변:
"사용자 경험 중심 성능 목표
- 초기 페이지 로딩: <3초 (3G 기준)
- 북마크 검색 응답: <200ms
- 북마크 추가/수정: <500ms
- 번들 크기: <500KB (초기), <2MB (전체)
- Lighthouse 점수: Performance 90+ 목표"
```

#### Q4: 보안 요구사항
```
🤖: 보안 요구사항은?

✅ 좋은 답변:
"기본 보안 원칙 준수
- LocalStorage에 민감 정보 저장 금지
- XSS 방지: 사용자 입력 sanitization
- HTTPS 필수 (프로덕션)
- 의존성 취약점 주간 스캔 (npm audit)
- CSP 헤더 설정"
```

#### Q5: 사용자 경험
```
🤖: 사용자 경험 기준은?

✅ 좋은 답변:
"접근성과 반응성 중심
- WCAG 2.1 AA 준수
- 모바일 우선 설계 (Mobile-First)
- 키보드 내비게이션 지원
- 다크 모드 지원
- 로딩 상태 표시 (>200ms 작업)"
```

### 생성된 Constitution 확인

```bash
# 파일 확인
cat .specify/memory/constitution.md
```

**예상 내용** (50-100줄):
```markdown
# Project Constitution: Bookmark Manager

## I. Code Quality First
- [x] 가독성: 함수 50줄, 파일 500줄 이하
- [x] 네이밍: 동사+명사, 설명적
- [x] 주석: '왜'를 설명
- [x] 중첩: 3단계 이하
...

## II. Test-Driven Development (NON-NEGOTIABLE)
- [x] TDD Red-Green-Refactor 주기
- [x] Unit 80%, Integration 70% 커버리지
- [x] Given-When-Then 구조
...

## III. User Experience Consistency
- [x] WCAG 2.1 AA 준수
- [x] 모바일 우선 설계
- [x] 키보드 내비게이션
- [x] 다크 모드 지원
...
```

---

## ✍️ Specification 작성 (기능 명세)

### 좋은 명세 작성 원칙

1. **What & Why에 집중** (How는 Plan 단계에서)
2. **측정 가능한 기준** 제시
3. **우선순위 명확화** (P1-P4)
4. **Edge Case 포함**
5. **User Story 중심**

### 명령어 실행

```
/speckit.specify "북마크 관리 앱 만들기.

사용자 기능:
- 북마크 추가 (URL, 제목, 설명, 태그)
- 북마크 목록 보기 (그리드/리스트 뷰)
- 북마크 검색 (제목, URL, 태그로)
- 북마크 수정/삭제
- 태그로 필터링
- 즐겨찾기 표시

기술 요구사항:
- 반응형 웹 (모바일 최적화)
- LocalStorage 사용 (서버 불필요)
- 다크 모드 지원
- 키보드 단축키 (Ctrl+K 검색 등)
- 북마크 내보내기/가져오기 (JSON)

비기능 요구사항:
- 검색 응답 200ms 이하
- 1,000개 북마크까지 지원
- 오프라인 작동
"
```

### 생성된 Specification 확인

```bash
ls specs/

# 예상 출력:
# 001-bookmark-manager/

cat specs/001-bookmark-manager/spec.md
```

**예상 내용** (150-200줄):

```markdown
# Feature Specification: Bookmark Manager

## User Scenarios & Testing

### User Story 1 - Bookmark Creation (Priority: P1)

**As a** user,
**I need to** save interesting websites as bookmarks,
**so that** I can easily access them later.

**Why this priority**: Core functionality - app is useless without this.

**Independent Test**: Can test with empty localStorage, verify bookmark saved and retrieved.

**Acceptance Scenarios**:
1. **Given** user is on the main page
   **When** user clicks "Add Bookmark" and enters URL, title
   **Then** bookmark appears in the list immediately

2. **Given** user enters duplicate URL
   **When** user tries to save
   **Then** system warns "URL already exists" with option to update

3. **Given** user enters invalid URL
   **When** user tries to save
   **Then** system shows "Invalid URL format" error

### User Story 2 - Bookmark Search (Priority: P1)
...

### User Story 3 - Tag Filtering (Priority: P2)
...

### User Story 4 - Import/Export (Priority: P3)
...

## Requirements

### Functional Requirements
- **FR-001**: System shall allow bookmark creation with URL, title, description, tags
- **FR-002**: System shall validate URL format (http/https)
- **FR-003**: System shall prevent duplicate URLs
- **FR-004**: System shall support search by title, URL, tags (<200ms)
- **FR-005**: System shall support tag-based filtering
...

### Key Entities
- **Bookmark**: id, url, title, description, tags[], favorite, createdAt, updatedAt
- **Tag**: name, count

## Success Criteria

### Measurable Outcomes
- **SC-001**: User can add bookmark in <3 clicks
- **SC-002**: Search returns results in <200ms for 1,000 bookmarks
- **SC-003**: App loads in <2s on 3G
- **SC-004**: Mobile usage success rate >95%

### Assumptions
- **Assumption 001**: Users have modern browsers (Chrome 90+, Firefox 88+, Safari 14+)
- **Assumption 002**: Users understand basic bookmark concept
- **Assumption 003**: Average user has <500 bookmarks
```

---

## 🏗️ Plan 생성 (구현 계획)

### 명령어 실행

```
/speckit.plan
```

AI가 자동으로 다음을 생성합니다:
1. **기술 스택 선정** (Constitution 기반)
2. **아키텍처 설계**
3. **데이터 모델**
4. **API 계약** (필요 시)
5. **프로젝트 구조**

### 생성된 파일들 (5개)

```bash
ls specs/001-bookmark-manager/

# 예상 출력:
# spec.md
# plan.md              ✨ 새로 생성
# research.md          ✨ 새로 생성
# data-model.md        ✨ 새로 생성
# quickstart.md        ✨ 새로 생성
# contracts/           ✨ 새로 생성 (필요시)
```

### plan.md 주요 내용

```markdown
# Implementation Plan: Bookmark Manager

## Summary
Modern bookmark management web app using React with TypeScript,
LocalStorage for persistence, and Tailwind CSS for styling.
Implements mobile-first responsive design with dark mode support.

## Technical Context

**Language/Version**: TypeScript / 5.2+
**Primary Framework**: React 18.2
**State Management**: React Context API + useReducer
**Storage**: LocalStorage with JSON serialization
**Styling**: Tailwind CSS 3.3
**Build Tool**: Vite 4.5
**Testing**: Vitest + React Testing Library
**Linting**: ESLint + Prettier

**Rationale**:
- React: Wide adoption, excellent ecosystem, component reusability
- TypeScript: Type safety prevents runtime errors
- LocalStorage: No backend needed, instant setup
- Tailwind: Rapid UI development, mobile-first utilities
- Vite: Fast builds, HMR, modern tooling

## Constitution Check

*GATE: Must pass before implementation*

### I. Code Quality First
- [x] React components ≤50 lines per function
- [x] Single Responsibility Principle per component
- [x] TypeScript strict mode enabled
- [x] ESLint + Prettier configured

### II. Test-Driven Development
- [x] TDD approach: tests before implementation
- [x] Coverage target: Unit 80%, Integration 70%
- [x] Vitest configured with React Testing Library

### III. User Experience Consistency
- [x] Mobile-first Tailwind breakpoints
- [x] Dark mode via CSS custom properties
- [x] Keyboard shortcuts (Ctrl+K, Ctrl+N)
- [x] Loading states for >200ms operations

**Gate Result**: [x] PASS

## Project Structure

bookmark-manager/
├── src/
│   ├── components/
│   │   ├── BookmarkCard.tsx
│   │   ├── BookmarkForm.tsx
│   │   ├── SearchBar.tsx
│   │   ├── TagFilter.tsx
│   │   └── Layout.tsx
│   ├── contexts/
│   │   └── BookmarkContext.tsx  # State management
│   ├── hooks/
│   │   ├── useLocalStorage.ts
│   │   ├── useSearch.ts
│   │   └── useKeyboardShortcuts.ts
│   ├── types/
│   │   └── bookmark.ts          # Type definitions
│   ├── utils/
│   │   ├── storage.ts           # LocalStorage wrapper
│   │   ├── validation.ts        # URL validation
│   │   └── export.ts            # JSON import/export
│   ├── App.tsx
│   └── main.tsx
├── tests/
│   ├── unit/
│   ├── integration/
│   └── setup.ts
├── public/
├── package.json
├── vite.config.ts
├── tailwind.config.js
└── tsconfig.json

## Architecture

### Component Hierarchy
```
App
├── Layout
│   ├── Header (SearchBar, AddButton)
│   ├── Sidebar (TagFilter)
│   └── Main
│       └── BookmarkGrid/List
│           └── BookmarkCard[]
```

### State Management
- **BookmarkContext**: Global bookmark state
- **LocalStorage**: Persistence layer
- **Reducers**: Add, Update, Delete, Filter actions

### Data Flow
```
User Action → Reducer → Context State → LocalStorage → Re-render
```
```

### research.md 예제

```markdown
# Research: Bookmark Manager

## Phase 0: Technology Research

### 1. LocalStorage vs IndexedDB
**Question**: Which client storage for 1,000 bookmarks?
**Analysis**:
- LocalStorage: 5-10MB limit, synchronous, simple API
- IndexedDB: >50MB, asynchronous, complex API

**Decision**: LocalStorage
**Rationale**: 1,000 bookmarks ≈ 500KB, well under limit. Simple API aligns with project scope.

### 2. State Management
**Options**:
- Redux: Robust, boilerplate-heavy
- Zustand: Minimal, modern
- Context + useReducer: Built-in, no dependencies

**Decision**: Context + useReducer
**Rationale**: Constitution favors minimal dependencies. Built-in solution sufficient for scope.

### 3. Search Implementation
**Options**:
- Fuse.js: Fuzzy search, 12KB
- Native filter: 0KB, exact match

**Decision**: Native filter with .toLowerCase()
**Rationale**: <1,000 items, performance acceptable, 0 dependencies.

### Version Matrix
| Package | Version | Reason |
|---------|---------|--------|
| React | 18.2.0 | Latest stable |
| TypeScript | 5.2.2 | Latest |
| Vite | 4.5.0 | Latest |
| Tailwind CSS | 3.3.5 | Latest |
| Vitest | 0.34.6 | Vite ecosystem |
```

### data-model.md 예제

```markdown
# Data Model: Bookmark Manager

## Entity: Bookmark

```typescript
interface Bookmark {
  id: string;              // UUID v4
  url: string;             // Valid HTTP/HTTPS URL
  title: string;           // Max 200 chars
  description?: string;    // Max 500 chars, optional
  tags: string[];          // Array of tag names
  favorite: boolean;       // Star flag
  createdAt: number;       // Unix timestamp
  updatedAt: number;       // Unix timestamp
}
```

## LocalStorage Schema

```json
{
  "bookmarks": [
    {
      "id": "550e8400-e29b-41d4-a716-446655440000",
      "url": "https://github.com/github/spec-kit",
      "title": "Spec-Kit Repository",
      "description": "Spec-Driven Development toolkit",
      "tags": ["dev", "tools"],
      "favorite": true,
      "createdAt": 1698765432000,
      "updatedAt": 1698765432000
    }
  ],
  "tags": {
    "dev": 15,
    "tools": 8,
    "design": 5
  },
  "settings": {
    "view": "grid",        // "grid" | "list"
    "darkMode": false,
    "sortBy": "createdAt"  // "createdAt" | "title" | "url"
  }
}
```

## Validation Rules

### URL Validation
```typescript
function validateUrl(url: string): boolean {
  try {
    const parsed = new URL(url);
    return parsed.protocol === 'http:' || parsed.protocol === 'https:';
  } catch {
    return false;
  }
}
```

### Title Validation
- Required
- 1-200 characters
- Trim whitespace

### Tags Validation
- 0-10 tags per bookmark
- Each tag: 1-30 characters
- Lowercase conversion
- No special characters (only a-z, 0-9, -)
```

---

## 📋 Tasks 생성 (작업 분해)

### 명령어 실행

```
/speckit.tasks
```

### 생성된 tasks.md

```bash
cat specs/001-bookmark-manager/tasks.md
```

**예상 내용** (100-150 tasks):

```markdown
# Tasks: Bookmark Manager

**Total Estimated Tasks**: 125

## Phase 1: Setup (12 tasks)

**Purpose**: Initialize project infrastructure

- [ ] T001 Create Vite + React + TypeScript project
- [ ] T002 [P] Install dependencies (Tailwind, Vitest, RTL)
- [ ] T003 [P] Configure Tailwind CSS
- [ ] T004 [P] Configure Vitest
- [ ] T005 [P] Setup ESLint + Prettier
- [ ] T006 [P] Configure TypeScript (strict mode)
- [ ] T007 Create project structure (components/, contexts/, etc.)
- [ ] T008 [P] Create Bookmark interface (types/bookmark.ts)
- [ ] T009 [P] Create AppState interface
- [ ] T010 Setup Git hooks (pre-commit: lint, test)
- [ ] T011 Create README.md with setup instructions
- [ ] T012 Verify build: npm run build (must succeed)

**Verification**: `npm run build && npm run test`

## Phase 2: Foundational (22 tasks)

**Purpose**: Core utilities and infrastructure

### Storage Layer
- [ ] T013 [P] Unit test: LocalStorage wrapper get/set
- [ ] T014 [P] Unit test: LocalStorage error handling
- [ ] T015 Implement useLocalStorage hook
- [ ] T016 [P] Unit test: validateUrl function
- [ ] T017 Implement validation.ts utilities

### State Management
- [ ] T018 [P] Unit test: BookmarkContext reducer ADD_BOOKMARK
- [ ] T019 [P] Unit test: Reducer UPDATE_BOOKMARK
- [ ] T020 [P] Unit test: Reducer DELETE_BOOKMARK
- [ ] T021 [P] Unit test: Reducer FILTER_BY_TAG
- [ ] T022 Implement BookmarkContext provider
- [ ] T023 Integration test: Context + LocalStorage sync

### Utilities
- [ ] T024 [P] Unit test: generateId (UUID)
- [ ] T025 [P] Unit test: sanitizeInput (XSS prevention)
- [ ] T026 [P] Unit test: export/import JSON
- [ ] T027 Implement utils/export.ts
- [ ] T028 Implement utils/search.ts (filter logic)

### Build Verification
- [ ] T034 Run all tests (must pass)
- [ ] T035 Check coverage (Unit ≥80%, Integration ≥70%)

## Phase 3: User Story 1 - Bookmark Creation (28 tasks)

**Purpose**: Implement add bookmark flow

### Tests FIRST (TDD)
- [ ] T036 [P] [US1] Unit test: BookmarkForm validation
- [ ] T037 [P] [US1] Unit test: BookmarkForm submit success
- [ ] T038 [P] [US1] Unit test: BookmarkForm duplicate URL error
- [ ] T039 [P] [US1] Integration test: Add bookmark end-to-end
- [ ] T040 [P] [US1] E2E test: User adds bookmark via UI

### Implementation (After tests FAIL)
- [ ] T041 [US1] Create BookmarkForm.tsx component
- [ ] T042 [US1] Implement form validation logic
- [ ] T043 [US1] Implement tag input (comma-separated)
- [ ] T044 [US1] Add error message display
- [ ] T045 [US1] Add loading state (optimistic UI)
- [ ] T046 [US1] Style form (Tailwind, mobile-first)
- [ ] T047 [US1] Add keyboard shortcuts (Ctrl+N new bookmark)
- [ ] T048 [US1] Integrate form with Context

### Verification
- [ ] T063 [US1] All tests pass
- [ ] T064 [US1] Manual test: Add 10 bookmarks successfully

## Phase 4: User Story 2 - Bookmark Display (25 tasks)
### Tests
- [ ] T065 [P] [US2] Unit test: BookmarkCard renders correctly
- [ ] T066 [P] [US2] Unit test: BookmarkCard favorite toggle
- [ ] T067 [P] [US2] Unit test: BookmarkGrid layout
- [ ] T068 [P] [US2] Integration test: Display 100 bookmarks
...

### Implementation
- [ ] T070 [US2] Create BookmarkCard.tsx
- [ ] T071 [US2] Implement favorite toggle
- [ ] T072 [US2] Implement edit/delete buttons
- [ ] T073 [US2] Create BookmarkGrid.tsx (grid view)
- [ ] T074 [US2] Create BookmarkList.tsx (list view)
- [ ] T075 [US2] Add view switcher (grid/list toggle)
...

## Phase 5: User Story 3 - Search & Filter (20 tasks)
...

## Phase 6: User Story 4 - Import/Export (15 tasks)
...

## Phase 7: Polish & Optimization (8 tasks)
- [ ] T121 Lighthouse audit (target: 90+ performance)
- [ ] T122 Accessibility audit (WCAG AA)
- [ ] T123 Test on mobile devices (iOS, Android)
- [ ] T124 Add loading skeleton states
- [ ] T125 Final code review against Constitution
```

---

## ⚙️ Implementation 실행 (코드 생성)

### 명령어 실행

```
/speckit.implement
```

### 실행 과정

```
🔍 Checking prerequisites...
✅ tasks.md exists (125 tasks)
✅ plan.md exists
✅ spec.md exists

📋 Validating Constitution compliance...
✅ All quality gates passed

🚀 Executing Phase 1: Setup (T001-T012)

├── T001 ✅ Created Vite project
│   └── bookmark-manager/
│       ├── src/
│       ├── public/
│       ├── package.json
│       ├── vite.config.ts
│       └── tsconfig.json

├── T002 ✅ Installed dependencies
│   └── Packages: react, typescript, tailwind, vitest, @testing-library/react

├── T003 ✅ Configured Tailwind CSS
│   └── Files: tailwind.config.js, src/index.css

├── T004 ✅ Configured Vitest
│   └── Files: vite.config.ts (test section), tests/setup.ts

├── T005 ✅ Setup ESLint + Prettier
│   └── Files: .eslintrc.json, .prettierrc

├── T006 ✅ Configured TypeScript
│   └── tsconfig.json (strict: true)

├── T007 ✅ Created project structure
│   └── Directories: components/, contexts/, hooks/, types/, utils/

├── T008 ✅ Created Bookmark interface
│   └── src/types/bookmark.ts

... (T009-T012)

Build Status: npm run build
✅ Build succeeded - 0 errors, 0 warnings
📦 Bundle size: 143.2 KB

Test Status: npm run test
✅ All tests passed - 0/0 tests

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Phase 1 Complete: 12/12 tasks (100%)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

🚀 Executing Phase 2: Foundational (T013-T035)

├── T013 ✅ Unit test: useLocalStorage hook
│   └── tests/unit/useLocalStorage.test.ts
│       ✓ should save and retrieve data
│       ✓ should handle parse errors gracefully
│       ✓ should return default value if no data

├── T014 ✅ Unit test: LocalStorage error handling
│   └── tests/unit/storage.test.ts
│       ✓ should handle quota exceeded error
│       ✓ should handle invalid JSON

├── T015 ✅ Implemented useLocalStorage hook
│   └── src/hooks/useLocalStorage.ts (48 lines)

... (T016-T035)

Test Status: npm run test
✅ 23 tests passed
📊 Coverage: Unit 85%, Integration 72%

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Phase 2 Complete: 23/23 tasks (100%)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Overall Progress: 35/125 tasks (28%)
├── Phase 1: ✅ 12/12 (100%)
├── Phase 2: ✅ 23/23 (100%)
├── Phase 3: ⏳ 0/28 (0%)
├── Phase 4: ⏸️  0/25 (0%)
├── Phase 5: ⏸️  0/20 (0%)
├── Phase 6: ⏸️  0/15 (0%)
├── Phase 7: ⏸️  0/8 (0%)

💡 Next: Phase 3 - User Story 1 (Bookmark Creation)
   Continue with `/speckit.implement` or specify phase.
```

### 부분 실행 옵션

```bash
# 특정 Phase만 실행
"Phase 3만 구현해줘"

# 특정 User Story만 실행
"User Story 1-2만 구현해줘"

# 특정 Task 범위 실행
"T036부터 T064까지 구현해줘"

# 계속 진행
/speckit.implement
```

---

## ✅ 검증 및 테스트

### 1. 빌드 확인

```bash
cd bookmark-manager
npm run build

# 예상 출력:
# ✓ built in 2.34s
# ✓ dist/index.html 1.2 KB
# ✓ dist/assets/index.js 143.2 KB
```

### 2. 테스트 실행

```bash
npm run test

# 예상 출력:
# ✓ tests/unit/useLocalStorage.test.ts (3)
# ✓ tests/unit/validation.test.ts (5)
# ✓ tests/integration/bookmark-context.test.ts (8)
#
# Tests: 23 passed (23 total)
# Coverage: 85% (Unit), 72% (Integration)
```

### 3. 개발 서버 실행

```bash
npm run dev

# 예상 출력:
# VITE v4.5.0  ready in 523 ms
# ➜  Local:   http://localhost:5173/
# ➜  Network: use --host to expose
```

**브라우저에서 확인**: http://localhost:5173/

### 4. 기능 테스트

#### 북마크 추가
1. "Add Bookmark" 버튼 클릭
2. URL 입력: `https://github.com`
3. Title 입력: `GitHub`
4. Tags 입력: `dev, tools`
5. "Save" 클릭
6. ✅ 북마크 목록에 표시 확인

#### 검색
1. 검색창에 "github" 입력
2. ✅ 실시간 필터링 확인 (<200ms)

#### 다크 모드
1. 우측 상단 테마 토글 클릭
2. ✅ 다크 모드 전환 확인

---

## 🔄 다음 기능 추가

### 기존 프로젝트에 기능 추가하기

#### 1. 새 기능 명세 작성

```
/speckit.specify "북마크에 폴더 기능 추가.

사용자 기능:
- 폴더 생성/수정/삭제
- 북마크를 폴더로 이동
- 폴더 계층 구조 (2단계까지)
- 폴더별 북마크 필터링
"
```

#### 2. 기존 Plan 업데이트

```
/speckit.plan
```

AI가 자동으로:
- 기존 plan.md에 새 기능 섹션 추가
- data-model.md에 Folder 엔티티 추가
- 마이그레이션 전략 제안

#### 3. Tasks 업데이트

```
/speckit.tasks
```

새 Phase 추가:
- Phase 8: Folder Management (35 tasks)

#### 4. 구현

```
/speckit.implement
```

---

## 💡 Tips & Best Practices

### Constitution 작성

✅ **구체적으로**:
```
"API 응답 200ms 이하"  # 측정 가능
```

❌ **모호하게**:
```
"빠른 응답"  # 주관적
```

### Specification 작성

✅ **User Story 중심**:
```markdown
As a user, I need to search bookmarks by title
so that I can quickly find saved websites.
```

❌ **기술 중심**:
```markdown
Implement search function using Array.filter()
```

### 작업 크기

✅ **적절한 크기**:
```
T042: Implement form validation logic (1-2 hours)
```

❌ **너무 큼**:
```
T100: Implement entire bookmark feature (8+ hours)
```

### 테스트 우선

✅ **TDD 순서**:
```
1. Write test (T036) → Test FAILs
2. Implement code (T041) → Test PASSes
3. Refactor
```

❌ **구현 먼저**:
```
1. Implement code
2. Write test (나중에 또는 생략)
```

---

## 🔗 다음 학습

| 주제 | 문서 | 설명 |
|------|------|------|
| **워크플로우 심화** | [workflow.md](./workflow.md) | 6단계 상세 가이드 |
| **명령어 마스터** | [commands.md](./commands.md) | 모든 명령어 레퍼런스 |
| **실전 사례** | [examples.md](./examples.md) | Taskify 프로젝트 분석 |
| **고급 기법** | [best-practices.md](./best-practices.md) | 베스트 프랙티스 |

---

## ❓ 문제 해결

### Issue: Plan 생성 시 기술 스택이 마음에 안듦

```
# 해결: research.md 수정 후 재생성
# 1. specs/001-bookmark-manager/research.md 편집
# 2. 원하는 기술 스택 명시
# 3. /speckit.plan 재실행
```

### Issue: Tasks가 너무 많음

```
# 해결: Spec 축소 또는 우선순위 조정
# 1. spec.md에서 P3-P4 기능 제거
# 2. /speckit.plan 재실행
# 3. /speckit.tasks 재실행
```

### Issue: 구현 중 오류 발생

```
# 해결: 특정 Task부터 재시도
"T042부터 다시 구현해줘. 이전 오류 고려해서."
```

---

**작성일**: 2025-10-27
**소요 시간**: 30-60분
**난이도**: ⭐⭐⭐☆☆
