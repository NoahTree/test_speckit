# Spec-Kit 완벽 가이드

**Spec-Kit**은 AI 기반 코드 생성을 위한 **스펙 중심 개발(Specification-Driven Development)** 프레임워크입니다.

상세한 명세서를 작성하면, AI가 이를 실행 가능한 코드로 변환합니다. 단순 문서가 아닌 **실행 가능한 청사진**입니다.

---

## 📚 문서 구조

| 문서 | 내용 |
|------|------|
| [workflow.md](./workflow.md) | 전체 워크플로우 6단계 상세 설명 |
| [commands.md](./commands.md) | 8개 명령어 사용법 및 예제 |
| [files.md](./files.md) | 생성되는 파일들의 역할과 구조 |
| [taskify-example.md](./taskify-example.md) | 실제 Taskify 프로젝트 사례 연구 |
| [best-practices.md](./best-practices.md) | 베스트 프랙티스 및 팁 |

---

## 🚀 빠른 시작 (5분)

### 1. Spec-Kit이란?

```
사용자 요구사항 (자연어)
        ↓
    /speckit.specify
        ↓
    상세 명세서 (spec.md)
        ↓
    /speckit.plan
        ↓
    구현 계획 (plan.md)
        ↓
    /speckit.tasks
        ↓
    작업 목록 (tasks.md)
        ↓
    /speckit.implement
        ↓
    실제 코드 생성 ✨
```

### 2. 핵심 워크플로우

```bash
# 1단계: 헌법 작성 (프로젝트 원칙)
/speckit.constitution

# 2단계: 기능 명세 작성
/speckit.specify "Taskify 팀 생산성 플랫폼 만들기..."

# 3단계: 기술 계획 수립
/speckit.plan

# 4단계: 작업 분해
/speckit.tasks

# 5단계: 구현 실행
/speckit.implement
```

### 3. 실제 사례: Taskify 프로젝트

**입력** (자연어 요구사항):
> "Taskify라는 팀 생산성 플랫폼을 만들어줘. 5명의 사용자(PM 1명, 엔지니어 4명)가 3개 프로젝트의 작업을 Kanban 보드로 관리할 수 있어야 해..."

**출력** (생성된 결과):
- ✅ 상세 명세서 (164줄, 5개 User Story)
- ✅ 구현 계획 (850줄, .NET Aspire + Blazor Server)
- ✅ 작업 목록 (170개 작업)
- ✅ 실제 코드 (23개 작업 완료, 빌드 성공)

**소요 시간**: 약 2시간 (수동 작업 대비 70% 시간 절감)

---

## 🎯 주요 특징

### 1. **명세서 기반 개발**
- 문서가 곧 실행 가능한 코드의 청사진
- AI가 명세서를 정확하게 해석하고 구현

### 2. **구조화된 워크플로우**
- Constitution → Specify → Plan → Tasks → Implement
- 각 단계마다 명확한 입력/출력

### 3. **품질 검증 시스템**
- 헌법(Constitution) 기반 자동 검증
- 체크리스트를 통한 품질 게이트

### 4. **다양한 AI 도구 지원**
- Claude Code, GitHub Copilot, Cursor, Windsurf 등
- 15개 이상의 AI 코딩 도구와 호환

---

## 📦 설치

### 방법 1: 영구 설치 (추천)
```bash
uv tool install specify-cli --from git+https://github.com/github/spec-kit.git
```

### 방법 2: 일회성 실행
```bash
uvx --from git+https://github.com/github/spec-kit.git specify init <프로젝트명>
```

### 방법 3: Claude Code에서 직접 사용
```bash
# 설치 불필요 - 슬래시 커맨드로 바로 실행
/speckit.specify "기능 설명..."
```

---

## 🔧 기본 사용법

### 예제 1: 간단한 TODO 앱
```bash
/speckit.specify "할 일 관리 앱 만들기. CRUD 기능, 우선순위 설정, 필터링 가능"
```

**생성되는 파일**:
- `specs/001-todo-app/spec.md` - 기능 명세서
- `specs/001-todo-app/plan.md` - 구현 계획 (기술 스택, 아키텍처)
- `specs/001-todo-app/tasks.md` - 작업 목록

### 예제 2: 복잡한 E-commerce 시스템
```bash
/speckit.specify "전자상거래 플랫폼. 사용자 인증, 상품 관리, 장바구니, 결제, 주문 관리, 관리자 대시보드"
```

**생성되는 파일**:
- `spec.md` - 20+ User Stories
- `plan.md` - Microservices 아키텍처, API Gateway, DB 설계
- `data-model.md` - ERD 및 테이블 명세
- `contracts/` - OpenAPI 스펙
- `tasks.md` - 300+ 작업

---

## 📁 생성되는 파일 구조

```
프로젝트/
├── specs/
│   └── 001-기능명/
│       ├── spec.md              # 기능 명세서 (/speckit.specify)
│       ├── plan.md              # 구현 계획 (/speckit.plan)
│       ├── tasks.md             # 작업 목록 (/speckit.tasks)
│       ├── research.md          # 기술 조사 결과
│       ├── data-model.md        # 데이터 모델 설계
│       ├── quickstart.md        # 빠른 시작 가이드
│       ├── contracts/           # API 계약
│       │   ├── api-1.yaml
│       │   └── api-2.yaml
│       └── checklists/          # 품질 체크리스트
│           └── requirements.md
└── .specify/
    └── memory/
        └── constitution.md      # 프로젝트 헌법
```

---

## 🎓 학습 경로

### 초급 (1시간)
1. ✅ [workflow.md](./workflow.md) 읽기 - 전체 흐름 이해
2. ✅ [commands.md](./commands.md) 읽기 - 명령어 학습
3. ✅ 간단한 TODO 앱으로 실습

### 중급 (3시간)
1. ✅ [files.md](./files.md) 읽기 - 파일 구조 이해
2. ✅ [taskify-example.md](./taskify-example.md) 읽기 - 실제 사례 분석
3. ✅ 중간 복잡도 프로젝트 직접 진행

### 고급 (1주일)
1. ✅ [best-practices.md](./best-practices.md) 읽기
2. ✅ 복잡한 프로젝트 진행 (Microservices, Multi-tenancy 등)
3. ✅ Custom Constitution 작성
4. ✅ 팀 프로세스에 Spec-Kit 통합

---

## 💡 핵심 개념

### 1. Constitution (헌법)
프로젝트의 **불변 원칙**:
- 코드 품질 기준
- 테스트 전략 (TDD 의무화)
- 성능 목표
- 보안 요구사항

### 2. Specification (명세서)
기능의 **상세한 요구사항**:
- User Stories (우선순위 포함)
- Functional Requirements
- Success Criteria
- Edge Cases

### 3. Plan (계획)
구현을 위한 **기술적 청사진**:
- 기술 스택 선정
- 아키텍처 설계
- 프로젝트 구조
- API 계약

### 4. Tasks (작업 목록)
실행 가능한 **구체적 작업**:
- 우선순위 및 의존성
- 병렬 실행 가능 여부 ([P] 태그)
- User Story별 그룹화
- TDD 순서 (테스트 먼저)

---

## 🌟 Spec-Kit vs 전통적 개발

| 비교 항목 | 전통적 개발 | Spec-Kit |
|-----------|-------------|----------|
| **시작점** | 코드 작성 | 명세서 작성 |
| **AI 활용** | 부분적 (코드 자동완성) | 전체 프로세스 |
| **문서화** | 사후 작성 (선택) | 사전 작성 (필수) |
| **품질 관리** | 수동 코드 리뷰 | 자동 헌법 검증 |
| **일관성** | 개발자 의존적 | 명세서 기반 일관성 |
| **학습 곡선** | 낮음 | 중간 (구조화 필요) |
| **대규모 프로젝트** | 복잡도 증가 | 구조화로 관리 용이 |

---

## 🔗 다음 단계

1. **워크플로우 이해하기**: [workflow.md](./workflow.md)
2. **명령어 마스터하기**: [commands.md](./commands.md)
3. **실제 사례 분석하기**: [taskify-example.md](./taskify-example.md)
4. **베스트 프랙티스 학습하기**: [best-practices.md](./best-practices.md)

---

## 📞 도움말

### 공식 문서
- [GitHub Spec-Kit Repository](https://github.com/github/spec-kit)
- [.NET Aspire Documentation](https://learn.microsoft.com/en-us/dotnet/aspire/)

### 커뮤니티
- GitHub Issues: 버그 리포트 및 기능 요청
- GitHub Discussions: 질문 및 토론

---

**작성일**: 2025-10-24
**기반 프로젝트**: Taskify (팀 생산성 플랫폼)
**버전**: 1.0
