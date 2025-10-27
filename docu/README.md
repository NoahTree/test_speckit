# Spec-Kit 완벽 가이드

**Spec-Kit**은 **Spec-Driven Development(명세 기반 개발)** 방법론을 지원하는 오픈소스 툴킷입니다.

명세서가 단순 문서를 넘어 **실행 가능한 청사진**이 되어, AI가 이를 직접 코드로 변환합니다.

---

## 📚 문서 구조

### 🚀 시작하기
| 문서 | 소요 시간 | 내용 |
|------|-----------|------|
| [quickstart.md](./quickstart.md) | 5분 | 설치부터 첫 실행까지 빠른 시작 |
| [setup.md](./setup.md) | 15분 | 상세 설치 및 환경 설정 가이드 |
| [getting-started.md](./getting-started.md) | 30분 | 첫 프로젝트 생성 및 적용 방법 |

### 📖 프로젝트 진행
| 문서 | 내용 |
|------|------|
| [workflow.md](./workflow.md) | 6단계 워크플로우 상세 가이드 |
| [commands.md](./commands.md) | CLI 및 슬래시 커맨드 레퍼런스 |

### 💡 심화 학습
| 문서 | 내용 |
|------|------|
| [examples.md](./examples.md) | Taskify 프로젝트 실제 사례 연구 |
| [best-practices.md](./best-practices.md) | 베스트 프랙티스 및 팁 |

---

## 🎯 학습 경로

### 초급 (1시간)
```
1. quickstart.md 읽고 따라하기 (5분)
2. setup.md로 환경 구성 (15분)
3. getting-started.md로 첫 프로젝트 생성 (30분)
4. 간단한 TODO 앱으로 실습 (10분)
```

### 중급 (3시간)
```
1. workflow.md 상세 학습 (1시간)
2. commands.md 명령어 숙지 (30분)
3. examples.md 사례 분석 (1시간)
4. 중간 복잡도 프로젝트 진행 (30분)
```

### 고급 (1주일)
```
1. best-practices.md 학습
2. 복잡한 프로젝트 진행 (Microservices 등)
3. Custom Constitution 작성
4. 팀 프로세스에 Spec-Kit 통합
```

---

## 🌟 핵심 개념

### Spec-Driven Development란?

전통적 개발과의 차이:

| 단계 | 전통적 개발 | Spec-Driven Development |
|------|-------------|------------------------|
| **1단계** | 요구사항 정리 | ✅ 상세한 명세서 작성 |
| **2단계** | 코드 작성 시작 | ✅ 기술 계획 수립 |
| **3단계** | 구현 중 명세 무시 | ✅ 작업 목록 생성 |
| **4단계** | 완료 후 문서화 | ✅ AI가 자동 구현 |

### 핵심 원칙

1. **명세가 곧 코드의 청사진**
   - 문서가 실행 가능한 형태로 존재
   - AI가 명세를 정확하게 해석하고 구현

2. **구조화된 6단계 워크플로우**
   ```
   Initialize → Constitution → Specify → Plan → Tasks → Implement
   ```

3. **품질 검증 시스템**
   - Constitution 기반 자동 검증
   - 체크리스트를 통한 품질 게이트

4. **다양한 AI 도구 지원**
   - Claude Code, GitHub Copilot, Cursor, Windsurf 등
   - 15개 이상의 AI 코딩 도구와 호환

---

## ⚡ 빠른 시작 (3분)

```bash
# 1. CLI 설치
uv tool install specify-cli --from git+https://github.com/github/spec-kit.git

# 2. 필수 도구 확인
specify check

# 3. 프로젝트 초기화
specify init my-project --ai claude
cd my-project

# 4. 첫 기능 개발
/speckit.constitution    # 프로젝트 원칙 정의
/speckit.specify "TODO 앱 만들기. CRUD 기능"
/speckit.plan            # 구현 계획
/speckit.tasks           # 작업 목록
/speckit.implement       # 코드 생성
```

**📖 상세 가이드**: [quickstart.md](./quickstart.md)

---

## 🔧 지원 환경

### 운영체제
- Linux
- macOS
- Windows

### 필수 도구
- Python 3.11+
- Git (권장)
- uv 패키지 매니저

### 지원 AI 도구
```
✅ Claude Code (Anthropic)
✅ GitHub Copilot
✅ Cursor Agent
✅ Gemini CLI (Google)
✅ Windsurf
✅ Amazon Q Developer
✅ Qwen Code
✅ Kilo Code
... 및 15개 이상
```

---

## 📦 설치

### 방법 1: 영구 설치 (추천)
```bash
uv tool install specify-cli --from git+https://github.com/github/spec-kit.git
```

**장점**:
- `uv tool list`로 관리
- `uv tool upgrade specify-cli`로 업데이트
- `uv tool uninstall specify-cli`로 제거

### 방법 2: 일회성 실행
```bash
uvx --from git+https://github.com/github/spec-kit.git specify init my-project
```

**📖 상세 가이드**: [setup.md](./setup.md)

---

## 🎓 실제 사례

### Taskify 프로젝트

**입력** (자연어 요구사항):
> "Taskify라는 팀 생산성 플랫폼을 만들어줘. 5명의 사용자가 3개 프로젝트의 작업을 Kanban 보드로 관리..."

**출력** (2시간 작업):
- ✅ 상세 명세서 (164줄, 5개 User Story)
- ✅ 구현 계획 (850줄, .NET Aspire + Blazor)
- ✅ 작업 목록 (170개 작업)
- ✅ 실제 코드 (23개 작업 완료, 빌드 성공)

**시간 절감**: 수동 작업 대비 70% 감소

**📖 상세 분석**: [examples.md](./examples.md)

---

## 🌟 Spec-Kit vs 전통적 개발

| 비교 항목 | 전통적 개발 | Spec-Kit |
|-----------|-------------|----------|
| **시작점** | 코드 작성 | 명세서 작성 |
| **AI 활용** | 부분적 (자동완성) | 전체 프로세스 |
| **문서화** | 사후 작성 (선택) | 사전 작성 (필수) |
| **품질 관리** | 수동 코드 리뷰 | 자동 헌법 검증 |
| **일관성** | 개발자 의존적 | 명세서 기반 일관성 |
| **학습 곡선** | 낮음 | 중간 |
| **대규모 프로젝트** | 복잡도 증가 | 구조화로 관리 용이 |
| **시간 효율** | 기준 | 30-70% 절감 |

---

## 🔗 다음 단계

### 초보자
1. ✅ [quickstart.md](./quickstart.md) - 5분 빠른 시작
2. ✅ [setup.md](./setup.md) - 환경 구성
3. ✅ [getting-started.md](./getting-started.md) - 첫 프로젝트

### 숙련자
1. ✅ [workflow.md](./workflow.md) - 워크플로우 마스터
2. ✅ [commands.md](./commands.md) - 명령어 레퍼런스
3. ✅ [examples.md](./examples.md) - 실전 사례

### 전문가
1. ✅ [best-practices.md](./best-practices.md) - 베스트 프랙티스
2. ✅ Custom Constitution 작성
3. ✅ 팀 프로세스 통합

---

## 📞 도움말

### 공식 자료
- [GitHub Spec-Kit Repository](https://github.com/github/spec-kit)
- [.NET Aspire Documentation](https://learn.microsoft.com/en-us/dotnet/aspire/)

### 커뮤니티
- GitHub Issues: 버그 리포트 및 기능 요청
- GitHub Discussions: 질문 및 토론

---

**라이선스**: MIT License
**최종 업데이트**: 2025-10-27
**버전**: 2.0
**기반 프로젝트**: Taskify (팀 생산성 플랫폼)
