# Spec-Kit 빠른 시작 (5분)

**목표**: Spec-Kit 설치부터 첫 코드 생성까지 5분 안에 완료하기

---

## 📋 사전 준비 (1분)

### 필수 도구 확인

```bash
# Python 3.11+ 확인
python --version  # 또는 python3 --version

# Git 확인 (선택)
git --version

# uv 설치 여부 확인
uv --version
```

**uv가 없다면**:
```bash
# macOS/Linux
curl -LsSf https://astral.sh/uv/install.sh | sh

# Windows (PowerShell)
powershell -c "irm https://astral.sh/uv/install.ps1 | iex"
```

---

## 🚀 1단계: Spec-Kit 설치 (30초)

```bash
uv tool install specify-cli --from git+https://github.com/github/spec-kit.git
```

**예상 출력**:
```
Installed 20 packages in 5s
✓ Successfully installed specify-cli
```

**설치 확인**:
```bash
specify --help
```

---

## 🎯 2단계: 프로젝트 초기화 (30초)

```bash
# 필수 도구 확인
specify check

# 프로젝트 생성
specify init todo-app --ai claude
cd todo-app
```

**예상 출력**:
```
[1/6] 필수 도구 확인...
✅ Git installed
✅ Claude Code detected

[2/6] AI 도구 선택...
Selected: claude

[3/6] 템플릿 다운로드...
✅ Downloaded

[4/6] 프로젝트 구조 생성...
✅ Created: specs/, .specify/

[5/6] Git 저장소 초기화...
✅ Initialized

[6/6] AI 명령어 설정...
✅ Configured

✨ Project 'todo-app' ready!
```

**생성된 구조**:
```
todo-app/
├── specs/              # 기능 명세서 저장
├── .specify/           # Spec-Kit 설정
│   ├── memory/         # Constitution 등
│   ├── scripts/        # 자동화 스크립트
│   └── templates/      # 문서 템플릿
└── .git/               # Git 저장소
```

---

## 📝 3단계: 프로젝트 원칙 정의 (1분)

Claude Code를 열고 `/speckit.constitution` 실행:

```
🤖: 코드 품질에서 가장 중요하게 생각하는 것은 무엇인가요?
👤: 가독성과 유지보수성. 함수는 50줄 이하

🤖: 테스트 전략은 어떻게 하시겠습니까?
👤: TDD 의무화. Unit 80%, Integration 70% 커버리지

🤖: 성능 목표는 무엇인가요?
👤: API 응답 200ms 이하, 페이지 로딩 3초 이하

🤖: 보안 요구사항은?
👤: HTTPS 필수, JWT 인증, SQL Injection 방지
```

**생성 파일**: `.specify/memory/constitution.md`

---

## ✍️ 4단계: 기능 명세 작성 (1분)

Claude Code에서 `/speckit.specify` 실행:

```bash
/speckit.specify "TODO 앱 만들기.

사용자 기능:
- 할 일 추가/수정/삭제
- 완료 표시 (체크박스)
- 우선순위 설정 (High, Medium, Low)
- 필터링 (전체, 완료, 미완료)
- 검색 기능

기술 요구사항:
- 반응형 웹 (모바일 최적화)
- 로컬 스토리지 사용 (서버 불필요)
- 다크 모드 지원
"
```

**생성 파일**: `specs/001-todo-app/spec.md`

**예상 내용**:
```markdown
# Feature Specification: TODO App

## User Stories

### User Story 1 - Task Management (Priority: P1)
As a user, I need to add, edit, and delete tasks...

### User Story 2 - Task Completion (Priority: P1)
As a user, I need to mark tasks as complete...

## Requirements

### Functional Requirements
- FR-001: System shall allow task creation
- FR-002: System shall support priority levels
...
```

---

## 🏗️ 5단계: 구현 계획 수립 (1분)

```bash
/speckit.plan
```

**생성 파일들** (5개):
- `specs/001-todo-app/plan.md` - 구현 계획
- `specs/001-todo-app/research.md` - 기술 조사
- `specs/001-todo-app/data-model.md` - 데이터 모델
- `specs/001-todo-app/quickstart.md` - 빠른 시작 가이드
- `specs/001-todo-app/contracts/` - API 계약 (필요시)

**plan.md 주요 내용**:
```markdown
## Technical Context
**Language/Version**: JavaScript / ES2023
**Frontend**: React 18.2
**State Management**: React Context API
**Storage**: LocalStorage
**Styling**: Tailwind CSS
**Build Tool**: Vite

## Project Structure
todo-app/
├── src/
│   ├── components/
│   ├── contexts/
│   ├── hooks/
│   └── utils/
└── tests/
```

---

## 📋 6단계: 작업 목록 생성 (30초)

```bash
/speckit.tasks
```

**생성 파일**: `specs/001-todo-app/tasks.md`

**예상 내용**:
```markdown
## Phase 1: Setup (8 tasks)
- [ ] T001 Create React app with Vite
- [ ] T002 [P] Install Tailwind CSS
- [ ] T003 [P] Setup project structure
...

## Phase 2: Foundational (15 tasks)
### Storage Layer
- [ ] T009 Create LocalStorage utility
- [ ] T010 Create Task interface (TypeScript)
...

## Phase 3: User Story 1 (25 tasks)
### Tests FIRST
- [ ] T020 [P] [US1] Unit test for addTask()
- [ ] T021 [P] [US1] Unit test for editTask()
...
```

**Total**: ~50 tasks

---

## ⚙️ 7단계: 코드 구현 (1분)

```bash
/speckit.implement
```

**실행 과정**:
```
Checking prerequisites...
✅ plan.md exists
✅ tasks.md exists

Executing Phase 1: Setup (T001-T008)
├── T001 ✅ Created React app
├── T002 ✅ Installed Tailwind CSS
├── T003 ✅ Setup project structure
...
└── T008 ✅ Configured ESLint

Build Status: npm run build
✅ Build succeeded

Executing Phase 2: Foundational (T009-T023)
├── T009 ✅ Created LocalStorage utility
...

Overall Progress: 8/50 tasks (16%)
├── Phase 1: ✅ 8/8 (100%)
├── Phase 2: ⏳ 0/15 (0%)
...
```

---

## ✅ 완료! 다음 단계

### 즉시 사용 가능
```bash
# 개발 서버 실행 (Phase 1 완료 시)
npm run dev

# 브라우저에서 확인
# http://localhost:5173
```

### 추가 구현 계속하기
```bash
# Phase 2-3 계속 구현
/speckit.implement

# 또는 특정 User Story만 구현
"User Story 1만 구현해줘"
```

### 품질 검증
```bash
# 명세와 계획 일관성 확인
/speckit.analyze

# 불명확한 부분 명확화
/speckit.clarify

# 체크리스트 생성
/speckit.checklist
```

---

## 🎯 실제 예제: 5분 데모

```bash
# 1. 설치 (30초)
uv tool install specify-cli --from git+https://github.com/github/spec-kit.git

# 2. 초기화 (30초)
specify init demo-app --ai claude && cd demo-app

# 3. Constitution (1분)
/speckit.constitution
# → 간단히 답변: "TDD, 가독성, 200ms 응답 속도"

# 4. Specify (1분)
/speckit.specify "카운터 앱. 증가, 감소, 리셋 버튼"

# 5. Plan (1분)
/speckit.plan

# 6. Tasks (30초)
/speckit.tasks

# 7. Implement (30초)
/speckit.implement

# 8. 실행 확인
npm run dev  # 또는 해당 프레임워크 실행 명령
```

**결과**: 5분 안에 작동하는 앱 생성!

---

## 💡 Tips

### AI 도구 선택
```bash
# Claude Code (추천)
specify init my-app --ai claude

# GitHub Copilot
specify init my-app --ai copilot

# Cursor
specify init my-app --ai cursor-agent

# Windsurf
specify init my-app --ai windsurf
```

### Git 없이 사용
```bash
specify init my-app --ai claude --no-git
```

### 현재 폴더에 초기화
```bash
specify init . --ai claude
# 또는
specify init --here --ai claude
```

---

## 🔗 다음 학습

| 다음 단계 | 문서 | 소요 시간 |
|-----------|------|-----------|
| **상세 설치** | [setup.md](./setup.md) | 15분 |
| **첫 프로젝트** | [getting-started.md](./getting-started.md) | 30분 |
| **워크플로우** | [workflow.md](./workflow.md) | 1시간 |

---

## ❓ 문제 해결

### Python 버전 오류
```bash
python3 --version  # 3.11+ 필요
# 업그레이드 방법: https://www.python.org/downloads/
```

### uv 설치 실패
```bash
# 수동 설치
pip install uv
```

### specify 명령어 찾을 수 없음
```bash
# PATH 추가 (macOS/Linux)
export PATH="$HOME/.local/bin:$PATH"

# PATH 추가 (Windows)
# 환경 변수에 %USERPROFILE%\.local\bin 추가
```

### AI 도구 감지 안됨
```bash
# 도구 체크 생략
specify init my-app --ai claude --ignore-agent-tools
```

---

**작성일**: 2025-10-27
**소요 시간**: 5분
**난이도**: ⭐☆☆☆☆
