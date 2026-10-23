# Learning

Practice code for the two learning tracks in the [study roadmap](ROADMAP.md). Study time is split roughly three quarters to C#/ASP.NET Core and Vue, reinforcing the work stack, and one quarter to barebones Go and Vue, which rebuilds by hand what the framework hides. Learn one concept, predict behavior, make a change, and discuss the result. Nothing here defines Ma's Cooking production architecture or product requirements.

| Path | Track | Contents |
| --- | --- | --- |
| [`go/`](go/) | Go, about 25% | Go language practice (roadmap stages G0 to G9). No external dependencies. |
| `dotnet/` | .NET, about 75% | Recipe Box, an ASP.NET Core, NPoco, PostgreSQL, and Vue practice app. Not created yet; it's set up in roadmap stage N1. |

After the Go language foundations, the Go track moves into Ma's Cooking itself (Go and Vue), starting at Milestone 0. Reconfirm the collaboration mode before that move.

## Go practice

The first [example](go/main.go) explores string values, function parameters, returning a value, and printing it. The developer added a language parameter using Khmer. The assistant supplied the first [test](go/main_test.go), and the developer added a second test case. Combining them into a table-driven test is roadmap stage G2.

From `learning/go/`, run the program with `go run .` and the tests with `go test -v`. In the agent sandbox, the default build cache is read-only, so tests run with a temporary cache. STATUS has the command.

Keep current progress, including the time split between tracks, in [STATUS](../docs/STATUS.md). Record actual learning evidence in [LEARNING](../docs/LEARNING.md). Grow the practice areas only as exercises require.
