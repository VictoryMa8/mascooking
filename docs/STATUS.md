# Status

Last updated: 2026-10-22

## Current .NET study session

- **Current stage:** [N1, exercise 1](../learning/ROADMAP.md#n1-modern-c-and-the-net-toolchain). Developer requested help understanding the generated ASP.NET Core project before proceeding.
- **Mode:** COACH by default for .NET; the existing PAIR agreement applies to Go foundations.
- **Verified by inspection:** `learning/dotnet/` contains a solution, a .NET 10 controller-based API, and an NUnit project referencing the API. The API still contains the generated weather example; the test is the generated `Assert.Pass()` placeholder. No build or requests were run in this session.
- **Next:** Orient to the generated files, then have the developer explain the first `WebApplication.CreateBuilder(args)` line in `Program.cs` before moving through startup in small steps. N1 exercise 1's explanation portion remains incomplete.
- **Time split:** This session is .NET; earlier session counts are unknown.
- **Documentation discrepancy:** README and the learning directory README still say the .NET project has not been created. The code now exists; other N1 learning outcomes have not been verified.

## Pending Go foundations work

- **Current milestone:** Go foundations — Learn through pairing. Milestone 0 is deferred until readiness is reviewed.
- **Active mode:** PAIR for the foundations phase, explicitly requested by the developer. Prioritize learning through small code experiments and conversation.
- **Last completed task:** Assistant initialized the practice module and demonstrated `TestRecipeLabel` in `learning/go/main_test.go`; the test passes.
- **Current task:** Explain Go test discovery, expected versus actual values, and failure reporting. Developer has not written a test yet.
- **Next task:** Developer adds `TestRecipeLabelWithDifferentInputs`, using a different dish, speaker, and language, and runs `go test -v` from `learning/go/`. Review before introducing further concepts.
- **Blockers:** None for learning. Go 1.27.1 is available through mise. In the agent sandbox the default build cache is read-only; tests passed using `GOCACHE=$(mktemp -d /tmp/mascooking-go-cache.XXXXXX) go test -v`. Ordinary terminal use may not need this override.
- **Useful context:** Practice code is separate from the unstarted application. No application dependencies, database, hosting, or providers are configured. Assistant-written code is not evidence of developer proficiency.
- **Relevant files:** [AGENTS](../AGENTS.md), [practice code](../learning/go/main.go), [ROADMAP](ROADMAP.md), [LEARNING](LEARNING.md), [ARCHITECTURE](ARCHITECTURE.md).
