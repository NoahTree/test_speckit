<!--
Sync Impact Report:
Version change: Initial → 1.0.0
Modified principles: N/A (initial constitution)
Added sections:
  - Core Principles (5 principles)
  - Performance & Quality Standards
  - Development Workflow & Governance
  - Governance
Removed sections: N/A
Templates requiring updates:
  ✅ Updated: .specify/templates/plan-template.md (Constitution Check section aligned)
  ✅ Updated: .specify/templates/spec-template.md (Requirements section aligned)
  ✅ Updated: .specify/templates/tasks-template.md (Task categorization aligned)
  ✅ Verified: All command files in .claude/commands/ (no agent-specific references found)
Follow-up TODOs: None

Version bump rationale: MAJOR version 1.0.0 for initial constitution establishment.
This is the first ratified version defining core principles for code quality, testing standards,
user experience consistency, and performance requirements with clear governance.
-->

# Test SpecKit Constitution

## Core Principles

### I. Code Quality First

All code MUST meet the following non-negotiable standards:
- **Readability**: Code must be self-documenting with clear variable/function names
- **Maintainability**: Functions/methods limited to 50 lines; files limited to 500 lines
- **Consistency**: Follow established project patterns and conventions
- **Documentation**: All public APIs must include inline documentation
- **Type Safety**: Static typing required where language supports it
- **No Code Smells**: Eliminate duplication, magic numbers, and complex conditionals

**Rationale**: Technical debt accumulates faster than it can be paid down. Quality standards prevent debt accumulation and ensure long-term maintainability.

### II. Test-Driven Development (NON-NEGOTIABLE)

TDD is mandatory for all feature development:
- **Red-Green-Refactor**: Write failing test → Implement → Refactor
- **Test First**: Tests must be written and approved BEFORE implementation begins
- **Coverage Standards**: Minimum 80% unit test coverage, 70% integration test coverage
- **Test Categories Required**:
  - **Unit Tests**: Isolated component testing
  - **Integration Tests**: Component interaction testing
  - **Contract Tests**: API/interface contract validation
  - **E2E Tests**: Critical user journey validation (for UI features)

**Rationale**: TDD ensures testable design, prevents regression, and validates requirements before implementation effort is invested.

### III. User Experience Consistency

All user-facing features MUST maintain consistent experience:
- **Design System Adherence**: Follow established component library and design tokens
- **Accessibility Standards**: WCAG 2.1 AA compliance minimum for all UI
- **Performance Budget**: Page load <3s on 3G, <1s on WiFi
- **Mobile First**: Responsive design tested on mobile, tablet, desktop viewports
- **Error Handling**: User-friendly error messages with recovery guidance
- **Loading States**: Visual feedback for operations >200ms

**Rationale**: Consistent UX builds trust, reduces cognitive load, and improves user satisfaction and retention.

### IV. Performance Requirements

Performance is a feature, not an afterthought:
- **Response Time Targets**:
  - API responses: <200ms p95
  - Database queries: <100ms p95
  - UI interactions: <16ms (60fps)
- **Resource Constraints**:
  - Memory usage: <100MB for mobile, <500MB for desktop
  - Bundle size: <500KB initial, <2MB total for web apps
  - Database connections: Connection pooling required
- **Monitoring Required**: All production services must emit performance metrics
- **Optimization Process**: Profile before optimizing; measure results

**Rationale**: Performance directly impacts user experience, conversion rates, and operational costs. Establishing budgets prevents degradation.

### V. Security by Default

Security cannot be retrofitted - it must be built in:
- **Authentication & Authorization**: Implemented for all non-public endpoints
- **Input Validation**: All user input validated and sanitized
- **Dependency Scanning**: Automated vulnerability scanning in CI/CD
- **Secret Management**: No secrets in code; use environment variables or vault
- **Security Headers**: Implement CSP, HSTS, X-Frame-Options for web apps
- **Audit Logging**: Log all security-relevant events (auth, data access, errors)

**Rationale**: Security breaches are expensive and damage trust. Proactive security is orders of magnitude cheaper than reactive remediation.

## Performance & Quality Standards

### Code Review Requirements

All code changes MUST pass review before merge:
- **Automated Checks Pass**: Linting, type checking, tests, security scans
- **Manual Review Required**: At least one reviewer approval
- **Constitution Compliance**: Reviewer verifies adherence to these principles
- **Documentation Updated**: README, API docs, and changelog updated as needed
- **Performance Validated**: No regressions in performance benchmarks

### Quality Gates (8-Step Validation)

Each feature must pass all gates before production deployment:

1. **Syntax Validation**: Code parses and compiles without errors
2. **Type Checking**: No type errors in statically typed languages
3. **Linting**: Passes project linting rules with zero warnings
4. **Security Scan**: No high/critical vulnerabilities in dependencies or code
5. **Test Coverage**: Meets minimum coverage thresholds (80% unit, 70% integration)
6. **Performance Testing**: Meets response time and resource budget requirements
7. **Documentation Review**: All public APIs documented, README updated
8. **Integration Testing**: End-to-end tests pass for affected user journeys

**Checkpoint**: Features failing any gate MUST be fixed before proceeding to the next gate.

### Technical Debt Management

Technical debt must be tracked and managed:
- **Debt Budget**: Maximum 10% of sprint capacity allocated to new debt
- **Debt Inventory**: Maintain backlog of known technical debt items
- **Complexity Justification**: Violations of simplicity principle require documented justification in plan.md
- **Refactoring Time**: Allocate 20% of development time to debt reduction
- **No Debt Accrual**: New features cannot add debt without explicit approval and mitigation plan

## Development Workflow & Governance

### Feature Development Process

All features follow this workflow:

1. **Specification Phase** (`/speckit.specify`):
   - Define user stories with acceptance criteria
   - Identify functional requirements and success metrics
   - Obtain stakeholder approval before proceeding

2. **Planning Phase** (`/speckit.plan`):
   - Research technical approach and dependencies
   - Design data models and API contracts
   - Pass Constitution Check (verify compliance with all principles)
   - Document complexity justifications if needed

3. **Task Generation** (`/speckit.tasks`):
   - Break down implementation into atomic tasks
   - Organize by user story for independent delivery
   - Identify parallel execution opportunities

4. **Implementation Phase** (`/speckit.implement`):
   - Execute tasks in dependency order
   - Follow TDD: write tests first, ensure they fail, implement
   - Commit frequently with clear messages

5. **Validation Phase** (`/speckit.analyze`):
   - Run complete quality gate validation
   - Fix any failures before considering feature complete
   - Verify all acceptance criteria met

6. **Deployment**:
   - Deploy to staging for final validation
   - Run smoke tests and monitor metrics
   - Deploy to production only after stakeholder sign-off

### Amendment Process

Constitution changes require structured process:

1. **Proposal**: Document proposed change with rationale and impact analysis
2. **Review**: Technical leadership reviews for consistency and necessity
3. **Approval**: Requires majority approval from core team
4. **Migration Plan**: Document how existing code/features will be brought into compliance
5. **Version Increment**:
   - **MAJOR**: Breaking changes to principles or workflow
   - **MINOR**: New principles added or material expansions
   - **PATCH**: Clarifications, typos, non-semantic fixes
6. **Template Sync**: Update all dependent templates and command files
7. **Communication**: Announce changes to all team members

### Compliance Review

Regular compliance audits ensure adherence:
- **Pull Request Review**: Every PR checked for constitution compliance
- **Monthly Audit**: Sample 10% of codebase for compliance
- **Quarterly Assessment**: Full review of all principles and update if needed
- **Violation Response**: Document violations, create remediation tasks, track to completion

## Governance

### Authority & Enforcement

- This constitution supersedes all other practices, conventions, and guidelines
- All pull requests MUST be verified for compliance during code review
- Complexity violations require explicit justification documented in `specs/[feature]/plan.md` Complexity Tracking section
- Security and testing principles are NON-NEGOTIABLE and cannot be waived
- Performance and UX principles may be temporarily waived with documented technical justification and remediation plan

### Decision Framework

When technical decisions are required:
1. **Consult Constitution**: Does this decision align with core principles?
2. **Evidence-Based**: Support decisions with metrics, benchmarks, or research
3. **User Impact**: Prioritize decisions that improve user experience
4. **Long-Term Thinking**: Consider maintenance burden over 2+ year horizon
5. **Team Consensus**: Seek input from affected team members
6. **Document Rationale**: Record decision reasoning in ADR (Architecture Decision Record) format

### Continuous Improvement

- Team retrospectives review constitution effectiveness quarterly
- Principles may be amended via the Amendment Process above
- Success metrics reviewed monthly to ensure principles drive desired outcomes
- Feedback from code reviews informs principle refinements

**Version**: 1.0.0 | **Ratified**: 2025-10-23 | **Last Amended**: 2025-10-23
