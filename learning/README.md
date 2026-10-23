# Go practice

Small PAIR-mode experiments before Ma's Cooking application development. Learn one concept, predict behavior, make a change, and discuss the result. Reuse is optional; no exercise defines production architecture or product requirements.

The [study roadmap](ROADMAP.md) sequences the concepts from first functions to a well-made PostgreSQL-backed CRUD API, with exercises, checkpoints, and a pairing protocol for agent sessions.

The first [example](go/main.go) explores string values, function parameters, returning a value, and printing it. The developer added a language parameter using Khmer. The assistant supplied the first [test](go/main_test.go); a second case is the next developer exercise.

From `learning/go/`, use `go run .` to run the program and `go test -v` to run tests. The practice module has no external dependencies. The first test passed using a temporary build cache because the agent sandbox cannot write the default cache; see STATUS for the command.

Keep current progress in [STATUS](../docs/STATUS.md) and actual learning evidence in [LEARNING](../docs/LEARNING.md). Grow the practice area only as exercises require it.
