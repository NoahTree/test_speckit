# Spec-Kit 상세 설치 및 환경 설정

**목표**: Spec-Kit을 안정적으로 설치하고 팀 환경에 맞게 구성하기

**소요 시간**: 15-30분

---

## 📋 목차

1. [시스템 요구사항](#-시스템-요구사항)
2. [필수 도구 설치](#-필수-도구-설치)
3. [Spec-Kit CLI 설치](#-spec-kit-cli-설치)
4. [AI 도구 설정](#-ai-도구-설정)
5. [환경 검증](#-환경-검증)
6. [팀 환경 구성](#-팀-환경-구성)
7. [문제 해결](#-문제-해결)

---

## 🖥️ 시스템 요구사항

### 운영체제

| OS | 버전 | 상태 |
|-----|------|------|
| **Linux** | Ubuntu 20.04+, Debian 11+, Fedora 35+ | ✅ 완벽 지원 |
| **macOS** | macOS 11+ (Big Sur 이상) | ✅ 완벽 지원 |
| **Windows** | Windows 10/11, WSL2 | ✅ 완벽 지원 |

### 하드웨어

| 항목 | 최소 | 권장 |
|------|------|------|
| **CPU** | 2 Core | 4 Core+ |
| **RAM** | 4 GB | 8 GB+ |
| **저장공간** | 2 GB | 10 GB+ |
| **인터넷** | 필수 | 안정적 연결 |

---

## 🔧 필수 도구 설치

### 1. Python 3.11+ 설치

#### macOS (Homebrew)
```bash
# Homebrew 설치 (없는 경우)
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"

# Python 설치
brew install python@3.11

# 설치 확인
python3 --version  # Python 3.11.x 이상
```

#### Linux (Ubuntu/Debian)
```bash
# 패키지 목록 업데이트
sudo apt update

# Python 3.11 설치
sudo apt install python3.11 python3.11-venv python3-pip

# 설치 확인
python3.11 --version
```

#### Linux (Fedora/RHEL)
```bash
# Python 3.11 설치
sudo dnf install python3.11

# 설치 확인
python3.11 --version
```

#### Windows
```powershell
# 방법 1: Python 공식 사이트에서 다운로드
# https://www.python.org/downloads/windows/
# Python 3.11.x Windows installer (64-bit) 다운로드

# 설치 시 "Add Python to PATH" 체크 필수!

# 방법 2: Windows Store
# Microsoft Store에서 "Python 3.11" 검색 후 설치

# 설치 확인
python --version  # 또는 python3 --version
```

### 2. Git 설치 (권장)

#### macOS
```bash
# Xcode Command Line Tools 설치 (Git 포함)
xcode-select --install

# 또는 Homebrew로 설치
brew install git

# 설치 확인
git --version
```

#### Linux
```bash
# Ubuntu/Debian
sudo apt install git

# Fedora
sudo dnf install git

# 설치 확인
git --version
```

#### Windows
```powershell
# Git for Windows 다운로드 및 설치
# https://git-scm.com/download/win

# 또는 winget 사용
winget install Git.Git

# 설치 확인
git --version
```

### 3. uv 패키지 매니저 설치

#### macOS/Linux
```bash
# 자동 설치 (권장)
curl -LsSf https://astral.sh/uv/install.sh | sh

# PATH 추가 (자동으로 추가되지 않은 경우)
export PATH="$HOME/.local/bin:$PATH"

# .bashrc 또는 .zshrc에 추가하여 영구 적용
echo 'export PATH="$HOME/.local/bin:$PATH"' >> ~/.zshrc  # zsh
echo 'export PATH="$HOME/.local/bin:$PATH"' >> ~/.bashrc  # bash

# 설치 확인
uv --version
```

#### Windows (PowerShell)
```powershell
# 자동 설치
powershell -c "irm https://astral.sh/uv/install.ps1 | iex"

# 수동 PATH 추가 (필요 시)
# 환경 변수에 %USERPROFILE%\.local\bin 추가

# 설치 확인
uv --version
```

---

## 📦 Spec-Kit CLI 설치

### 방법 1: 영구 설치 (추천)

```bash
uv tool install specify-cli --from git+https://github.com/github/spec-kit.git
```

**예상 출력**:
```
Resolved 20 packages in 2.1s
Installed 20 packages in 5.3s
 + specify-cli==0.1.0 (from git+https://github.com/github/spec-kit.git)
 + click==8.1.7
 + rich==13.7.0
 ...
✓ Successfully installed specify-cli
```

**설치 확인**:
```bash
specify --version
specify --help
```

### 방법 2: 일회성 실행

프로젝트 초기화만 필요한 경우:
```bash
uvx --from git+https://github.com/github/spec-kit.git specify init <PROJECT_NAME>
```

### 관리 명령어

```bash
# 설치된 도구 목록 확인
uv tool list

# Spec-Kit 업그레이드
uv tool upgrade specify-cli

# Spec-Kit 제거
uv tool uninstall specify-cli

# 특정 버전 설치
uv tool install specify-cli@0.1.0 --from git+https://github.com/github/spec-kit.git@v0.1.0
```

---

## 🤖 AI 도구 설정

### 지원 AI 도구 목록

| AI 도구 | 플래그 | 설치 필요 | 상태 |
|---------|--------|-----------|------|
| **Claude Code** | `--ai claude` | ✅ | 완벽 지원 |
| **GitHub Copilot** | `--ai copilot` | ✅ | 완벽 지원 |
| **Cursor Agent** | `--ai cursor-agent` | ✅ | 완벽 지원 |
| **Gemini CLI** | `--ai gemini` | ✅ | 완벽 지원 |
| **Windsurf** | `--ai windsurf` | ✅ | 완벽 지원 |
| **Amazon Q** | `--ai q` | ✅ | 제한 지원 |
| **Qwen Code** | `--ai qwen` | ✅ | 완벽 지원 |
| **OpenCode** | `--ai opencode` | ✅ | 완벽 지원 |
| **Codex** | `--ai codex` | ✅ | 완벽 지원 |
| **Kilo Code** | `--ai kilocode` | ✅ | 완벽 지원 |
| **Auggie** | `--ai auggie` | ✅ | 완벽 지원 |
| **Code Buddy** | `--ai codebuddy` | ✅ | 완벽 지원 |
| **Amp** | `--ai amp` | ✅ | 완벽 지원 |

### Claude Code 설정 (추천)

#### 1. VS Code 설치
```bash
# macOS (Homebrew)
brew install --cask visual-studio-code

# Linux
# https://code.visualstudio.com/download

# Windows
# Microsoft Store 또는 https://code.visualstudio.com/
```

#### 2. Claude Code 확장 설치
```bash
# VS Code에서 확장 검색
# 검색: "Claude Code"
# 또는 명령어로 설치
code --install-extension Anthropic.claude-code
```

#### 3. API Key 설정
1. https://console.anthropic.com/account/keys 에서 API Key 생성
2. VS Code에서 `Ctrl+Shift+P` (macOS: `Cmd+Shift+P`)
3. "Claude: Set API Key" 검색 및 입력

### GitHub Copilot 설정

#### 1. 확장 설치
```bash
code --install-extension GitHub.copilot
code --install-extension GitHub.copilot-chat
```

#### 2. GitHub 계정 연동
1. VS Code에서 GitHub 로그인
2. Copilot 구독 활성화 필요 ($10/월 또는 학생/교사 무료)

### Cursor 설정

#### 1. Cursor 다운로드 및 설치
```bash
# macOS (Homebrew)
brew install --cask cursor

# 또는 공식 사이트
# https://cursor.sh/
```

#### 2. API Key 설정 (선택)
- OpenAI API Key 또는 Anthropic API Key 설정 가능

---

## ✅ 환경 검증

### 자동 검증

```bash
# 모든 필수 도구 확인
specify check
```

**예상 출력**:
```
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
  Spec-Kit Environment Check
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

System Requirements
✅ Python 3.11.5 (required: 3.11+)
✅ Git 2.43.0 (optional)
✅ uv 0.1.14 (required)

AI Tools
✅ Claude Code: Installed (v1.2.0)
✅ GitHub Copilot: Installed (v1.142.0)
⚠️  Cursor: Not found (optional)
ℹ️  Windsurf: Not installed (optional)

Spec-Kit CLI
✅ specify-cli 0.1.0

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
  All required tools are ready! ✨
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
```

### 수동 검증

```bash
# 1. Python 확인
python3 --version
# 예상: Python 3.11.x 이상

# 2. Git 확인
git --version
# 예상: git version 2.x.x

# 3. uv 확인
uv --version
# 예상: uv 0.1.x

# 4. Spec-Kit CLI 확인
specify --version
# 예상: specify-cli 0.1.0

# 5. AI 도구 확인 (VS Code 확장)
code --list-extensions | grep -E "claude|copilot|cursor"
```

### 테스트 프로젝트 생성

```bash
# 테스트 프로젝트로 설치 확인
specify init test-project --ai claude
cd test-project

# 구조 확인
ls -la

# 예상 출력:
# specs/
# .specify/
# .git/
# README.md
# .gitignore
```

---

## 👥 팀 환경 구성

### 1. 팀 공용 Constitution 템플릿

```bash
# 팀 레포지토리 구조
company-repo/
├── .specify-templates/
│   └── constitution-template.md  # 팀 공용 템플릿
└── README.md

# 프로젝트 초기화 시 템플릿 복사
specify init new-project --ai claude
cp ~/.specify-templates/constitution-template.md new-project/.specify/memory/constitution.md
```

### 2. CI/CD 환경 설정

#### GitHub Actions 예제

```yaml
# .github/workflows/spec-kit.yml
name: Spec-Kit Validation

on: [push, pull_request]

jobs:
  validate:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3

      - name: Setup Python
        uses: actions/setup-python@v4
        with:
          python-version: '3.11'

      - name: Install uv
        run: curl -LsSf https://astral.sh/uv/install.sh | sh

      - name: Install Spec-Kit
        run: uv tool install specify-cli --from git+https://github.com/github/spec-kit.git

      - name: Check Environment
        run: specify check --ignore-agent-tools

      - name: Validate Specs
        run: |
          if [ -d "specs" ]; then
            # Spec 파일 검증 로직
            echo "Validating spec files..."
          fi
```

### 3. 도커 환경

```dockerfile
# Dockerfile
FROM python:3.11-slim

# 필수 도구 설치
RUN apt-get update && \
    apt-get install -y git curl && \
    rm -rf /var/lib/apt/lists/*

# uv 설치
RUN curl -LsSf https://astral.sh/uv/install.sh | sh
ENV PATH="/root/.local/bin:$PATH"

# Spec-Kit 설치
RUN uv tool install specify-cli --from git+https://github.com/github/spec-kit.git

WORKDIR /workspace

CMD ["bash"]
```

**사용법**:
```bash
# 빌드
docker build -t spec-kit-env .

# 실행
docker run -it -v $(pwd):/workspace spec-kit-env

# 컨테이너 내부에서
specify check --ignore-agent-tools
specify init my-project --ai claude --no-git
```

---

## 🔍 문제 해결

### Python 관련 이슈

#### Issue 1: Python 버전 낮음
```bash
# 에러: "Python 3.11+ required, found 3.9.x"

# 해결: Python 3.11 설치
# macOS
brew install python@3.11
# 심볼릭 링크 생성
sudo ln -sf /opt/homebrew/bin/python3.11 /usr/local/bin/python3

# Linux (pyenv 사용 권장)
curl https://pyenv.run | bash
pyenv install 3.11.5
pyenv global 3.11.5
```

#### Issue 2: 여러 Python 버전 충돌
```bash
# 해결: venv로 격리
python3.11 -m venv ~/spec-kit-env
source ~/spec-kit-env/bin/activate  # Linux/macOS
# 또는
~/spec-kit-env/Scripts/activate  # Windows

# venv 내부에서 uv 및 specify 설치
pip install uv
uv tool install specify-cli --from git+https://github.com/github/spec-kit.git
```

### uv 관련 이슈

#### Issue 3: uv 명령어 찾을 수 없음
```bash
# 에러: "command not found: uv"

# 해결: PATH 추가
# Linux/macOS
export PATH="$HOME/.local/bin:$PATH"
echo 'export PATH="$HOME/.local/bin:$PATH"' >> ~/.zshrc  # 영구 적용

# Windows
# 시스템 환경 변수에 %USERPROFILE%\.local\bin 추가
```

#### Issue 4: uv 설치 실패 (네트워크)
```bash
# 해결: pip로 대체 설치
pip install uv
```

### Spec-Kit 관련 이슈

#### Issue 5: specify 명령어 찾을 수 없음
```bash
# 에러: "command not found: specify"

# 해결 1: uv tool 경로 확인
uv tool list

# 해결 2: PATH에 uv tool bin 추가
export PATH="$HOME/.local/share/uv/bin:$PATH"  # Linux/macOS

# 해결 3: 재설치
uv tool uninstall specify-cli
uv tool install specify-cli --from git+https://github.com/github/spec-kit.git
```

#### Issue 6: Git 저장소 초기화 실패
```bash
# 에러: "fatal: not a git repository"

# 해결 1: Git 없이 초기화
specify init my-project --ai claude --no-git

# 해결 2: 수동으로 Git 초기화
cd my-project
git init
```

### AI 도구 관련 이슈

#### Issue 7: AI 도구 감지 안됨
```bash
# 경고: "Claude Code not detected"

# 해결 1: 도구 체크 생략
specify init my-project --ai claude --ignore-agent-tools

# 해결 2: VS Code 확장 수동 설치
code --install-extension Anthropic.claude-code

# 해결 3: 재시작
# VS Code 재시작 후 다시 시도
```

#### Issue 8: API Key 오류
```bash
# 에러: "Invalid API key"

# 해결: API Key 재설정
# VS Code에서 Cmd+Shift+P → "Claude: Set API Key"
# 새 키 발급: https://console.anthropic.com/account/keys
```

### 네트워크 관련 이슈

#### Issue 9: GitHub 연결 실패
```bash
# 에러: "Failed to download from GitHub"

# 해결 1: TLS 검증 생략 (임시)
specify init my-project --ai claude --skip-tls

# 해결 2: 프록시 설정
export HTTP_PROXY=http://proxy.company.com:8080
export HTTPS_PROXY=http://proxy.company.com:8080

# 해결 3: GitHub 토큰 사용
export GITHUB_TOKEN=ghp_xxxxxxxxxxxx
specify init my-project --ai claude --github-token $GITHUB_TOKEN
```

---

## 📊 설치 확인 체크리스트

```markdown
### 필수 요구사항
- [ ] Python 3.11+ 설치 완료
- [ ] uv 설치 완료
- [ ] Spec-Kit CLI 설치 완료
- [ ] `specify check` 명령어 실행 성공

### 권장 요구사항
- [ ] Git 설치 완료
- [ ] AI 도구 (Claude Code/Copilot/Cursor) 설치 완료
- [ ] AI 도구 API Key 설정 완료

### 검증
- [ ] `specify --version` 실행 성공
- [ ] 테스트 프로젝트 생성 성공
- [ ] AI 도구와 연동 확인
```

---

## 🔗 다음 단계

| 단계 | 문서 | 설명 |
|------|------|------|
| **1** | [quickstart.md](./quickstart.md) | 5분 빠른 시작 |
| **2** | [getting-started.md](./getting-started.md) | 첫 프로젝트 생성 |
| **3** | [workflow.md](./workflow.md) | 상세 워크플로우 |

---

**작성일**: 2025-10-27
**소요 시간**: 15-30분
**난이도**: ⭐⭐☆☆☆
