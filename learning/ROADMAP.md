# Go study roadmap: from first function to a well-made CRUD API

A senior engineer's study plan for learning the Go you need to build a well-made CRUD application: one that is correct, tested, safe with input, honest about failures, and easy to explain. It is written for a junior developer with beginner-to-intermediate experience in **Python, JavaScript, SQL, HTML, CSS, and C++**.

You can use this plan in two ways:

- **Read it on your own.** Work through each stage, type the examples yourself, do the exercises, and answer the checkpoint questions.
- **Pair-program with an agent.** Open a session, point the agent at the current stage, and follow the [pairing protocol](#pairing-with-an-agent). The agent teaches and reviews, and you write the code.

This is a curriculum, not a project-state document. [STATUS](../docs/STATUS.md) owns the current task, [LEARNING](../docs/LEARNING.md) owns evidence of what you have actually learned, and the [project roadmap](../docs/ROADMAP.md) owns Ma's Cooking milestones. Reading a stage or finishing an exercise with heavy help doesn't count as mastery. Record honest evidence in LEARNING.

## Contents

- [What "well made" means](#what-well-made-means)
- [How to use this plan](#how-to-use-this-plan)
- [Your existing languages, translated](#your-existing-languages-translated)
- [Stage overview](#stage-overview)
- Part I: The language
  - [Stage 0: Toolchain and workflow](#stage-0-toolchain-and-workflow)
  - [Stage 1: Values, types, and control flow](#stage-1-values-types-and-control-flow)
  - [Stage 2: Functions and the testing habit](#stage-2-functions-and-the-testing-habit)
  - [Stage 3: Slices, maps, and strings](#stage-3-slices-maps-and-strings)
  - [Stage 4: Structs, methods, and pointers](#stage-4-structs-methods-and-pointers)
  - [Stage 5: Errors as values](#stage-5-errors-as-values)
  - [Stage 6: Interfaces, packages, and a little generics](#stage-6-interfaces-packages-and-a-little-generics)
- Part II: Programs that touch the world
  - [Stage 7: Files, JSON, and a CLI recipe box](#stage-7-files-json-and-a-cli-recipe-box)
  - [Stage 8: Just enough concurrency for servers](#stage-8-just-enough-concurrency-for-servers)
- Part III: Building the service
  - [Stage 9: HTTP with net/http](#stage-9-http-with-nethttp)
  - [Stage 10: Structuring and running the service](#stage-10-structuring-and-running-the-service)
  - [Stage 11: PostgreSQL for application developers](#stage-11-postgresql-for-application-developers)
  - [Stage 12: Go and PostgreSQL together](#stage-12-go-and-postgresql-together)
  - [Stage 13: Making CRUD well made](#stage-13-making-crud-well-made)
  - [Stage 14: Testing, debugging, and tooling](#stage-14-testing-debugging-and-tooling)
- Part IV: Capstone
  - [Stage 15: Recipe Box API capstone](#stage-15-recipe-box-api-capstone)
- [Bridge to Ma's Cooking](#bridge-to-mas-cooking)
- Appendices: [Gotchas](#appendix-a-gotchas-cheat-sheet), [Idioms](#appendix-b-idioms-and-style), [Glossary](#appendix-c-glossary), [References](#appendix-d-references)

## What "well made" means

By the end, you should be able to build and explain a CRUD API that:

1. **Works:** creates, reads, lists, updates, and deletes records through HTTP and JSON, backed by PostgreSQL.
2. **Has a clear contract:** predictable routes, status codes, a consistent JSON error shape, and documented behavior.
3. **Distrusts input:** validates at the boundary, limits request size, uses parameterized SQL, and backs up rules with database constraints.
4. **Fails honestly:** distinguishes "not found", "invalid", "conflict", and "our fault", logs the details internally, and reveals nothing sensitive to clients.
5. **Is tested at the right levels:** unit tests for rules, handler tests for HTTP behavior, and integration tests against a real database.
6. **Is safe under concurrency:** has no data races, passes request contexts and timeouts through, and uses transactions where several writes must succeed or fail together.
7. **Can be operated:** configuration comes from the environment, logs are structured, the server shuts down gracefully, a health check works, and a stranger can run it from the README.
8. **Is conventional Go:** formatted by `gofmt`, uses the standard library first, keeps SQL visible, and avoids abstractions before they're needed.
9. **Is understood:** you can trace a request from `curl` to a database row and back, and justify each design choice.

Item 9 matters as much as the rest. A working app you can't explain means the learning goal hasn't been met.

## How to use this plan

### Pacing

Advance by **checkpoint, not calendar**. A stage is done when you can do its checkpoint tasks with documentation open but without step-by-step help. Some stages take one session; Stages 9 and 12 may take several. Going back to an earlier stage is normal and expected.

### Reading it alone

1. Read the stage's "Why it matters" and "Concepts" sections.
2. **Type** every example rather than pasting it. Before running it, write down what you expect it to print.
3. Do the exercises in order. Each one is small on purpose.
4. Answer the checkpoint questions out loud or in writing, without notes. Any question you can't answer tells you what to reread.
5. The references are optional. Use them when a concept needs a second explanation.

### Pairing with an agent

The project's [AGENTS.md](../AGENTS.md) sets the collaboration modes. The Go foundations phase uses **PAIR** mode, which means small examples from the agent alternating with changes from you.

**Session loop (about 45 to 90 minutes):**

1. **Warm-up.** The agent asks you to explain the last concept in two or three sentences.
2. **One concept.** The agent explains a single idea, connects it to Python, JavaScript, or C++, and shows an example of at most about 15 lines. The example uses different data than your exercise.
3. **Predict.** You say what the code will do before running it.
4. **Change.** You make the next exercise change yourself. The agent doesn't edit your exercise files unless you explicitly switch to UNBLOCK.
5. **Run and explain.** You run it and explain why it behaved the way it did.
6. **Review.** The agent reviews your change like a pull request: correctness first, then idiomatic Go, then tests. It points out problems and lets you fix them rather than rewriting your code.
7. **Record.** At a stopping point, update [STATUS](../docs/STATUS.md) and add honest evidence to [LEARNING](../docs/LEARNING.md).

**Hint ladder.** When you're stuck, the agent goes up one rung at a time and stops as soon as you're moving again:

1. A question that points you at the problem.
2. The name of the relevant concept or package documentation.
3. A concrete hint, such as "look at what `append` returns."
4. A partial snippet using different names than your code.
5. A full solution, only in UNBLOCK mode and followed by an explanation.

**Prompts you can paste:**

- Start or resume a stage:

  > We're on Stage N of learning/ROADMAP.md in PAIR mode. Read AGENTS.md, STATUS.md, and LEARNING.md, inspect my code, then teach the next concept. Show at most one small example, then stop and let me predict and implement.

- Ask for a hint:

  > I'm stuck on <exercise>. Here's what I tried and what happened: <details>. Give me the next rung of the hint ladder, not the answer.

- Ask for a review:

  > Review my change in <file> like a senior engineer: correctness first, then idiomatic Go, then tests. Don't rewrite it; point out issues and let me fix them.

- Debug:

  > My test fails with <output>. Help me establish the cause before changing any code.

- Run a checkpoint:

  > Quiz me on the Stage N checkpoint. One question at a time. Don't give the answer until I've tried.

**Rules for the agent:**

- Teach one concept per step and stop after each example.
- Don't write solutions to the developer's exercises. Example code must use different names and data than the exercise.
- Code written by the agent is not evidence of the developer's skill. Don't record it in LEARNING as if it were.
- Connect new ideas to Python, JavaScript, or C++ when that genuinely helps.
- When debugging, establish the cause before suggesting a replacement.
- Reconfirm the collaboration mode before moving from practice code into Ma's Cooking application code.

### Where the code lives

| Location | Use | Notes |
| --- | --- | --- |
| `learning/go/` | Stages 0 to 6 | The existing practice module, which has no dependencies. Put each topic in its own folder, such as `learning/go/collections/`, and run it with `go run ./collections`. Run every test with `go test ./...`. |
| `learning/recipebox/` | Stages 7 to 15 if you build the practice capstone | A separate module created in Stage 7, because it will eventually depend on `pgx`. |
| Ma's Cooking application | Milestone 0 onward | The location is decided at Milestone 0. See [Bridge to Ma's Cooking](#bridge-to-mas-cooking). |

In the agent sandbox, Go's default build cache may be read-only. [STATUS](../docs/STATUS.md) records the `GOCACHE` workaround. A normal terminal usually doesn't need it.

## Your existing languages, translated

Use this table as a map, not a rulebook. The biggest mental shifts are marked in bold.

| Topic | Python | JavaScript | C++ | Go |
| --- | --- | --- | --- | --- |
| Typing | Dynamic | Dynamic | Static | Static, with type inference (`x := 5`) |
| Running code | Interpreter | Node or browser runtime | Compile and link | Compiles to a single native binary; `go run` compiles and runs in one step |
| Dependencies | pip, venv | npm, `package.json` | CMake, vcpkg, or similar | `go.mod` and `go.sum`; `go get`, `go mod tidy` |
| Missing value | `None` | `undefined` and `null` | Uninitialized or `nullptr` | **Every type has a zero value** (`0`, `""`, `false`, `nil`). Nothing is ever uninitialized. |
| Classes | `class` | `class` and prototypes | `class` | **No classes.** Structs plus methods. |
| Inheritance | Yes | Yes | Yes | **None.** Composition through embedding. |
| Polymorphism | Duck typing | Duck typing | Virtual functions and templates | **Interfaces satisfied implicitly**, which is duck typing checked at compile time |
| Failures | Exceptions | `throw` and rejected promises | Exceptions or error codes | **Errors are ordinary return values.** `panic` is reserved for bugs. |
| Cleanup | `with` | `try`/`finally` | RAII and destructors | `defer` |
| Lists | `list` | `Array` | `std::vector` | Slices (`[]T`) |
| Dictionaries | `dict` | `Object` and `Map` | `std::unordered_map` | Maps (`map[K]V`) |
| Strings | Unicode code points | UTF-16 code units | Bytes | **Immutable UTF-8 bytes.** `len` counts bytes, not characters. |
| Memory | Garbage collected | Garbage collected | Manual or RAII | Garbage collected. Pointers exist, but there is no pointer arithmetic. |
| Concurrency | Threads, asyncio, GIL | Event loop, `async`/`await` | Threads | **Goroutines and channels.** Blocking code is normal and cheap. |
| Visibility | Convention (`_name`) | Modules and `#private` | `public` and `private` | **Capitalized names are exported** from the package. Lowercase names are package-private. |
| Formatting | black and ruff | prettier | clang-format | `gofmt`, which everyone uses. There's nothing to argue about. |
| Generics | Type hints | TypeScript | Templates | Type parameters, used sparingly |

## Stage overview

| Stage | Theme | You can... |
| --- | --- | --- |
| 0 | Toolchain | Create a module, then run, build, test, format, and vet code |
| 1 | Values and control flow | Write typed code with zero values, loops, and switches, and explain bytes versus runes |
| 2 | Functions and tests | Write table-driven tests and use multiple return values, closures, and `defer` |
| 3 | Collections | Use slices and maps correctly, including aliasing and nil behavior |
| 4 | Structs and pointers | Model data, choose value or pointer receivers, and build an in-memory store |
| 5 | Errors | Return, wrap, inspect, and design errors, including validation errors |
| 6 | Interfaces and packages | Use small interfaces and design packages without premature abstraction |
| 7 | I/O and JSON | Build a file-backed CLI that does CRUD on recipes |
| 8 | Concurrency | Protect shared state, use the race detector, and pass `context.Context` |
| 9 | HTTP | Build a JSON CRUD API with `net/http` and test it with `httptest` |
| 10 | Service structure | Wire configuration, logging, middleware, and graceful shutdown |
| 11 | PostgreSQL | Design schemas with constraints and migrations, and inspect data with `psql` |
| 12 | Go and PostgreSQL | Use `pgx`, map errors across layers, write transactions and integration tests |
| 13 | Quality | Add validation, pagination, optimistic locking, an error contract, and an authorization preview |
| 14 | Testing and debugging | Plan a test strategy and use fuzzing, the race detector, coverage, and Delve |
| 15 | Capstone | Ship and explain a well-made recipe CRUD API |

---

# Part I: The language

## Stage 0: Toolchain and workflow

> **Current progress:** Partly done. The practice module exists, the program runs, and two tests pass. See [STATUS](../docs/STATUS.md).

**Why it matters for CRUD:** Every later stage depends on a fast loop of editing, running, and testing. Go's tooling is simple and standard, so learn it once and you'll use it everywhere.

### Concepts

- **Module:** A versioned unit of code defined by `go.mod`. The `module` line gives its import path, and the `go` line sets the minimum language version. Third-party dependencies are listed in `go.mod` and verified by `go.sum`.
- **Package:** One directory is one package. Every file in a directory declares the same `package` name. `package main` with a `func main()` builds an executable. Any other package name is a library.
- **Core commands:**
  - `go run .` compiles and runs the package in the current directory.
  - `go build` produces a binary.
  - `go test ./...` runs tests in every package below the current directory.
  - `go vet ./...` catches suspicious code, such as wrong `Printf` verbs or copied locks.
  - `gofmt -l .` lists files that aren't formatted. Most editors format on save.
  - `go doc strings.TrimSpace` shows documentation in the terminal.
  - `go mod tidy` adds missing dependencies and removes unused ones.
- **Strict compiler:** Unused imports and unused local variables are compile errors. This keeps code clean, and you'll find it annoying at first.
- **Editor support:** `gopls`, the Go language server used by the Go extension in VS Code and Cursor, provides completion, go-to-definition, and inline errors.

### Coming from

- **Python and JavaScript:** There's no virtualenv or `node_modules` per project. Dependencies go into a shared module cache, and the module file pins versions.
- **C++:** There are no headers, makefiles, or linker flags for ordinary programs. `go build` handles everything.

### Exercises

1. **You:** Explain each line of `learning/go/go.mod` in your own words.
2. **You:** Run `go build` in `learning/go/`, run the resulting binary directly, and compare that with `go run .`. Where did the binary go, and why does it matter for deployment?
3. **You:** Add an unused variable and an unused import, read both compiler errors, and remove them. Why might the Go designers have made these errors instead of warnings?
4. **You:** Misformat a file deliberately, run `gofmt -l .`, then `gofmt -w .`.
5. **You:** Run `go doc fmt` and `go doc fmt.Printf`. Find the formatting verb that prints a Go-syntax representation of a value.

### Checkpoint

- What is the difference between a module and a package?
- What makes a Go program executable?
- How does `go test` find tests?
- Without notes: create a new folder with a `main` package, run it, and add a passing test.

**References:** [Tutorial: Get started](https://go.dev/doc/tutorial/getting-started), [How to write Go code](https://go.dev/doc/code), [Organizing a Go module](https://go.dev/doc/modules/layout)

---

## Stage 1: Values, types, and control flow

**Why it matters for CRUD:** Request fields, IDs, timestamps, and limits are all typed values. Zero values decide what a missing JSON field becomes. Byte and character counting decides what "title up to 200 characters" means for a Khmer recipe title.

### Concepts

- **Declarations:**
  - `var servings int` declares a variable that starts at its zero value, 0.
  - `title := "Kuy teav"` declares a variable and infers its type. `:=` works only inside functions.
  - `const maxTitleLen = 200` declares a compile-time constant.
- **Basic types:** `int`, `int64`, `float64`, `bool`, `string`, `byte` (an alias for `uint8`), and `rune` (an alias for `int32`, holding one Unicode code point).
- **Zero values:** `0`, `""`, `false`, and `nil` for pointers, slices, maps, channels, functions, and interfaces. Go code is designed so that the zero value is useful.
- **Explicit conversion:** Go never converts between numeric types implicitly. Write `float64(n)` and `int64(id)`. Use `strconv.Itoa(n)` to turn a number into its text. `string(n)` produces the character with that code point, not the number as text.
- **Strings are UTF-8 bytes:**
  - `len(s)` counts bytes.
  - `utf8.RuneCountInString(s)` counts code points.
  - `for i, r := range s` walks runes. Each `i` is a byte offset.
  - What a reader perceives as one character (a grapheme) can be several runes. Khmer often combines them. The standard library doesn't count graphemes.
- **Control flow:**
  - `for` is the only loop: `for i := 0; i < n; i++`, `for condition {}`, `for {}`, `for i := range 10` (range over an integer), and `for k, v := range collection`.
  - `if` takes an optional short statement: `if n := len(s); n > 200 { ... }`.
  - `switch` cases don't fall through, and a `switch` with no condition replaces long `if`/`else` chains.
- **Formatting verbs:** `%v` prints the value, `%+v` adds struct field names, `%#v` prints Go syntax, `%q` quotes strings, `%T` prints the type, and `%d` prints integers.
- **Scope and shadowing:** `:=` in an inner block creates a *new* variable with the same name. This is a frequent source of bugs, especially with `err`.

### Coming from

- **Python:** There is no truthiness. Write `if s != ""`, not `if s`. Integer division truncates (`7/2 == 3`).
- **JavaScript:** There is no `undefined`, no type coercion, and no `==` versus `===` distinction. `"5" + 1` doesn't compile.
- **C++:** Zero values replace uninitialized memory, and there are no implicit narrowing conversions. `i++` is a statement, not an expression, so `x = i++` doesn't compile.

### Example

```go
package main

import (
	"fmt"
	"unicode/utf8"
)

func main() {
	var servings int    // zero value
	title := "Kuy teav" // inferred string
	language := "ខ្មែរ"

	fmt.Println(servings, title)
	fmt.Println(len(language))                    // predict before running
	fmt.Println(utf8.RuneCountInString(language)) // predict before running
	for i, r := range language {
		fmt.Printf("byte %d: %q\n", i, r)
	}
}
```

### Exercises

1. **You:** Run the example and predict both counts first. Explain why the byte offsets jump.
2. **You:** In a new folder, write `spiceLevel(n int) string` that returns `"mild"`, `"medium"`, `"hot"`, or `"unknown"` using a `switch` with no condition. Choose the ranges yourself.
3. **You:** Write a loop that prints the numbers 1 to 5 using `range` over an integer, then rewrite it as a classic three-part `for`.
4. **You:** Create a shadowing bug: declare `count := 0`, then inside an `if` block write `count := 5`. Print `count` after the block and explain the result. Run `go vet`. Does it catch this?
5. **Discuss:** If a product rule says "title up to 200 characters", should Go count bytes, runes, or graphemes? What would each choice mean for Khmer, Mandarin, and English titles?

### Checkpoint

- What is the zero value of `string`, `int`, `bool`, `[]string`, and `*int`?
- Why does `len("ខ្មែរ")` differ from the number of characters you see?
- When does `:=` create a new variable rather than assign to an existing one?

**References:** [A Tour of Go: Basics](https://go.dev/tour/basics/1), [Strings, bytes, runes and characters in Go](https://go.dev/blog/strings), [Go by Example](https://gobyexample.com/)

---

## Stage 2: Functions and the testing habit

**Why it matters for CRUD:** Handlers and stores are functions that return results and errors. Tests are how you prove CRUD behavior instead of clicking around. Make them a habit now, while the code is simple.

### Concepts

- **Multiple return values:** `func divide(a, b int) (int, error)`. This is the foundation of Go's error handling.
- **Named results:** Use them sparingly, mainly for documentation. Avoid bare `return` in long functions.
- **Variadic functions:** `func join(parts ...string)`. Call it as `join("a", "b")` or `join(parts...)`.
- **Functions as values and closures:** Functions can be assigned, passed, and returned, and they capture variables from the surrounding scope. Middleware in Stage 10 is built from this.
- **`defer`:** Schedules a call to run when the surrounding function returns. Deferred calls run in last-in, first-out order, and **their arguments are evaluated immediately**. It's used for cleanup such as `defer f.Close()`, `defer mu.Unlock()`, and `defer tx.Rollback(ctx)`.
- **Early returns:** Handle a failure and `return` immediately, so the successful path stays at the left margin.
- **Testing basics:**
  - Test files end in `_test.go`, and test functions look like `func TestXxx(t *testing.T)`.
  - `t.Errorf` reports a failure and keeps going. `t.Fatalf` reports and stops the test. Use `Fatalf` when the rest of the test can't run meaningfully.
  - **Table-driven tests** list cases in a slice of structs and run each one with `t.Run(name, ...)` as a subtest.
  - `t.Helper()` makes failures in helper functions report the caller's line.
  - `go test -run TestName -v` runs one test with verbose output. `-count=1` bypasses cached results, and `-cover` shows coverage.
  - Write failure messages in the form `got X, want Y`, including the inputs.
  - **Watch a test fail before trusting it.** Break the code briefly and confirm the test notices.

### Coming from

- **Python:** `pytest.mark.parametrize` is the closest match to table-driven tests. There are no fixtures. Use plain helper functions and `t.Cleanup`.
- **JavaScript:** There's no Jest or Mocha and no `describe` or `it`. `t.Run` gives you nesting, and `testing` is built into the toolchain.
- **C++:** There's no test framework to install. `defer` covers much of what RAII destructors do, but explicitly and per function.

### Example

The function under test is different from your exercises:

```go
func isBlank(s string) bool {
	return strings.TrimSpace(s) == ""
}

func TestIsBlank(t *testing.T) {
	tests := []struct {
		name  string
		input string
		want  bool
	}{
		{name: "empty", input: "", want: true},
		{name: "spaces and tabs", input: " \t ", want: true},
		{name: "khmer word", input: "សម្ល", want: false},
	}

	for _, tt := range tests {
		t.Run(tt.name, func(t *testing.T) {
			if got := isBlank(tt.input); got != tt.want {
				t.Errorf("isBlank(%q) = %v, want %v", tt.input, got, tt.want)
			}
		})
	}
}
```

### Exercises

1. **You:** Merge the two existing `recipeLabel` tests into one table-driven test with named subtests. Run `go test -v` and look at how subtest names appear.
2. **You:** Break `recipeLabel` deliberately, for example by removing the parentheses. Is the failure message good enough to find the problem without opening the code? Improve it if not, then restore the function.
3. **You:** Write `cookTime(minutes int) string` that formats `75` as `"1 hr 15 min"`, `45` as `"45 min"`, and `60` as `"1 hr"`. Write the table test **first**, including the edge cases 0 and 59. Negative input is handled in Stage 5.
4. **You:** Predict the output, then run it:

   ```go
   func main() {
   	for i := range 3 {
   		defer fmt.Println("deferred", i)
   	}
   	fmt.Println("done")
   }
   ```

5. **You:** Write `newCounter() func() int`, which returns a closure that counts up on each call. Test that two counters don't share state.

### Checkpoint

- Why are table-driven tests the default in Go?
- When do you use `t.Fatalf` instead of `t.Errorf`?
- When are a deferred call's arguments evaluated? In what order do deferred calls run?
- Without notes: write a table-driven test with three cases for a function you've never seen.

**References:** [Tutorial: Add a test](https://go.dev/doc/tutorial/add-a-test), [`testing` package](https://pkg.go.dev/testing), [Defer, panic, and recover](https://go.dev/blog/defer-panic-and-recover)

---

## Stage 3: Slices, maps, and strings

**Why it matters for CRUD:** A list endpoint returns a slice. An in-memory store is a map. Query parameters and request fields are strings to parse. Slice aliasing and nil maps cause real production bugs.

### Concepts

- **Arrays versus slices:**
  - `[3]string` is an array. Its size is part of its type, it's copied by value, and you'll rarely use it directly.
  - `[]string` is a slice: a small header holding a pointer to a backing array, a length, and a capacity.
- **`make` and `append`:**
  - `make([]T, len, cap)` creates a slice and `make(map[K]V)` creates a map.
  - `append` may allocate a new backing array, so **always assign its result**: `s = append(s, x)`.
- **Aliasing:** Slicing (`s[1:3]`) shares the backing array, so writes through one slice can show up in another. Use `slices.Clone` or `copy` when you need independence.
- **Nil versus empty slices:** `var s []T` is nil and `s := []T{}` is empty. Both have length 0 and both work with `range` and `append`, but **JSON encodes a nil slice as `null` and an empty slice as `[]`**. Your Vue client will care.
- **The `slices` package:** `slices.Contains`, `slices.Index`, `slices.Sort`, `slices.SortFunc`, `slices.Delete`, and `slices.Clone`.
- **Maps:**
  - `v, ok := m[k]` is the "comma ok" form. It tells "missing" apart from "present with the zero value".
  - `delete(m, k)` removes a key.
  - **Iteration order is randomized**, so sort the keys when order matters: `slices.Sorted(maps.Keys(m))`.
  - **Writing to a nil map panics.** Reading from one returns the zero value.
  - Maps aren't safe for concurrent writes (Stage 8).
- **The `strings` package:** `TrimSpace`, `Fields`, `Split`, `Contains`, `ToLower`, `EqualFold`, and `strings.Builder` for efficient concatenation.
- **The `strconv` package:** `Atoi` and `ParseInt` parse text into numbers and return an error on bad input. `Itoa` and `FormatInt` go the other way. You'll parse path IDs and `?limit=` values with these.

### Coming from

- **Python:** Slicing a list copies it. Slicing a Go slice shares memory, which is closer to a NumPy view.
- **JavaScript:** Map iteration order isn't insertion order, unlike JavaScript's `Map`.
- **C++:** A slice is roughly a `std::span` that can grow. `append` behaves like `vector::push_back` and may reallocate, but the old slice header still points at the old array.

### Example: predict before running

```go
a := []string{"rice", "fish", "lime"}
b := a[:2]
b = append(b, "chili")
fmt.Println(a) // ?
fmt.Println(len(b), cap(b))

var counts map[string]int
fmt.Println(counts["khmer"]) // ?
counts["khmer"]++            // ?
```

### Exercises

1. **You:** Run the example and explain each output using length, capacity, and the backing array.
2. **You:** Write `countByLanguage(languages []string) map[string]int` with table tests. How do you compare two maps in a test? Look at `maps.Equal`.
3. **You:** Write `normalizeTags(tags []string) []string`, which trims, lowercases, removes duplicates, and sorts, without modifying the caller's slice. Write a test that proves the input is unchanged.
   - **Discuss:** Normalizing *search tags* is fine. Why would normalizing a cook's *story or transcript* violate [ADR 0002](../docs/decisions/0002-preserve-original-content.md)?
4. **You:** Build a tiny store using `map[int]string` that maps an ID to a title, with `add`, `get` (using comma ok), `remove`, and `listSorted` functions. This previews CRUD.
5. **You:** Parse `"20"`, `"abc"`, and `"-5"` with `strconv.Atoi` and print the value and error for each.

### Checkpoint

- Why must you write `s = append(s, x)` rather than just `append(s, x)`?
- What's the difference between a nil slice and an empty slice, and when does it matter?
- What happens when you write to a nil map? When you read from one?
- Why can a test that ranges over a map pass sometimes and fail other times?

**References:** [Go slices: usage and internals](https://go.dev/blog/slices-intro), [Go maps in action](https://go.dev/blog/maps), [`slices` package](https://pkg.go.dev/slices), [`strings` package](https://pkg.go.dev/strings)

---

## Stage 4: Structs, methods, and pointers

**Why it matters for CRUD:** A recipe is a struct. Stores and handlers are structs with methods. Choosing between a value and a pointer decides whether an update actually changes anything.

### Concepts

- **Structs:** `type Recipe struct { ... }`. Create them with composite literals that name each field (`Recipe{Title: "Kuy teav"}`). Fields you leave out get their zero values. Structs are copied when you assign or pass them.
- **Methods:** A method is a function with a receiver, `func (r Recipe) Label() string`. Methods can be defined on any named type in your package, not only structs.
- **Value or pointer receiver:**
  - Use a pointer receiver (`func (b *Book) Add(...)`) when the method mutates the receiver, when the struct is large, or when it contains a mutex.
  - Use a value receiver for small values that methods only read.
  - **Be consistent within a type.** If any method needs a pointer receiver, give them all one.
- **Pointers:** `&x` takes an address and `*p` dereferences it. There's no pointer arithmetic, and the garbage collector manages memory. **Returning a pointer to a local variable is safe.** Escape analysis moves it to the heap, which is the opposite of a dangling pointer in C++. Dereferencing a nil pointer panics.
- **Constructors:** Go has no constructors. By convention, write `func NewRecipeBook() *RecipeBook` when the zero value isn't usable, for example because a map needs `make`. Make the zero value useful where you can.
- **Exported versus unexported:** `Title` is visible outside the package and `nextID` isn't. Visibility is per package, not per type.
- **Embedding:** Putting a type inside a struct without a field name promotes its fields and methods. This is composition, not inheritance. You'll use it in Stage 10 to wrap `http.ResponseWriter`.
- **Struct tags:** Metadata strings such as `` `json:"title"` ``, read by packages like `encoding/json`. Covered in Stage 7.

### Coming from

- **Python and JavaScript:** There's no `self` or `this` keyword. The receiver is an ordinary named parameter. Objects aren't shared by reference automatically: assigning a struct copies it.
- **C++:** It's like a `struct` with member functions, but without constructors, destructors, overloading, or inheritance. A pointer receiver is like a non-`const` member function, and a value receiver is like a `const` one that works on a copy.

### Example

This uses a different type than your exercises:

```go
type Speaker struct {
	Name     string
	Relation string
}

func (s Speaker) Describe() string { // reads only: value receiver
	return s.Name + " (" + s.Relation + ")"
}

func (s *Speaker) Rename(name string) { // mutates: pointer receiver
	s.Name = name
}

func main() {
	speakers := []Speaker{{Name: "Ma", Relation: "mother"}, {Name: "Yay", Relation: "grandmother"}}
	for _, s := range speakers {
		s.Rename("Changed")
	}
	fmt.Println(speakers) // predict: did anything change?
}
```

### Exercises

1. **You:** Run the example and explain the result. Then rewrite the loop so the rename actually changes the slice.
2. **You:** Define `Recipe` with `ID int64`, `Title`, `Speaker`, `Language`, `Story string`, and `CreatedAt time.Time`. Replace `recipeLabel` with a `Label()` method and update its tests. Why is a value receiver appropriate here?
3. **You:** Build `RecipeBook`, an in-memory store containing a `map[int64]Recipe` and a `nextID`. Give it these methods, each with tests:
   - `Add(r Recipe) Recipe` assigns an ID and a creation time.
   - `Get(id int64) (Recipe, bool)`
   - `List() []Recipe` sorts by ID.
   - `Update(r Recipe) bool`
   - `Delete(id int64) bool`

   You'll reuse this store through Stage 10.
4. **You:** Temporarily change `Add` to a value receiver and watch the tests fail. Explain why.
5. **Discuss:** Should `Get` return `Recipe` or `*Recipe`? What could a caller do to the store's internal data with a pointer?
6. **You:** Try `p := &book.recipes[1]`. Read the compiler error and explain why Go forbids taking the address of a map element.

### Checkpoint

- When do you use a pointer receiver?
- Why is it safe to return `&localVar` in Go?
- What does the zero value of `RecipeBook` look like, and why does it need a constructor?
- What's the difference between embedding and inheritance?

**References:** [A Tour of Go: Methods](https://go.dev/tour/methods/1), [Effective Go: Pointers vs. values](https://go.dev/doc/effective_go#pointers_vs_values), [Go FAQ: Should I define methods on values or pointers?](https://go.dev/doc/faq#methods_on_values_or_pointers)

---

## Stage 5: Errors as values

**Why it matters for CRUD:** Every CRUD failure has to become the right HTTP response. A missing recipe is 404, bad input is 400 or 422, a stale edit is 409, and a database outage is 500 or 503. You can only do that if errors carry meaning that survives as they move up through your layers.

### Concepts

- **`error` is an interface:** Anything with an `Error() string` method is an error. Return it as the last result. `nil` means success.
- **Check, add context, return:**

  ```go
  r, err := store.Get(id)
  if err != nil {
  	return fmt.Errorf("load recipe %d: %w", id, err)
  }
  ```

  `%w` wraps the original error so callers can still inspect it. `%v` flattens it into text.
- **Sentinel errors:** Package-level values such as `var ErrNotFound = errors.New("recipe not found")`. Check them with `errors.Is(err, ErrNotFound)`, which looks through any wrapping. Don't compare error strings.
- **Custom error types:** Use a struct when callers need data from the error, such as which fields failed validation. Extract it with `errors.As`.
- **Handle each error once:** Either handle it, by logging it and responding, or return it with context. **Don't do both.** Logging and also returning produces duplicate log noise.
- **`panic` is for bugs:** Use it for impossible states and programmer mistakes, not for expected failures like bad input or a missing row. `recover` belongs only at boundaries such as HTTP middleware.
- **`errors.Join`** combines several errors into one.
- **Validation is not mutation:** A validator reports problems and doesn't silently change what the user wrote.

### Coming from

- **Python and JavaScript:** There's no `try`/`except` or `throw`/`catch`. Every failure point is visible in the code. The price is verbosity. The benefit is that you can see and test every failure path, which is exactly what an API needs.
- **C++:** It's like returning error codes, but with rich values and wrapping. `std::expected` in C++23 is the closest match.

### Example

```go
var ErrNotFound = errors.New("not found")

type ValidationError struct {
	Fields map[string]string // field name -> problem
}

func (e *ValidationError) Error() string {
	return fmt.Sprintf("invalid input: %d field(s)", len(e.Fields))
}

// At a boundary, for example an HTTP handler:
func describe(err error) string {
	var verr *ValidationError
	switch {
	case errors.Is(err, ErrNotFound):
		return "missing"
	case errors.As(err, &verr):
		return fmt.Sprintf("invalid: %v", verr.Fields)
	default:
		return "unexpected"
	}
}
```

**The nil interface trap.** Predict whether `check("Kuy teav") == nil`:

```go
func check(title string) error {
	var verr *ValidationError
	if title == "" {
		verr = &ValidationError{Fields: map[string]string{"title": "required"}}
	}
	return verr
}
```

An interface value is only `nil` when both its type and its value are nil. A nil `*ValidationError` stored in an `error` is not nil. The fix is to return a literal `nil` on success.

### Exercises

1. **You:** Change `RecipeBook.Get` to return `(Recipe, error)` with a wrapped `ErrNotFound`. Update the tests to use `errors.Is`. Do the same for `Update` and `Delete`.
2. **You:** Change `cookTime` to return `(string, error)` and reject negative minutes. Add test cases for the error.
3. **You:** Write `ValidateRecipe(r Recipe) error`, returning a `*ValidationError` that lists **every** failing field, not only the first:
   - The title is required and at most 200 runes.
   - The speaker and language are required.
   - The story is optional and **must never be trimmed or rewritten** (see [ADR 0002](../docs/decisions/0002-preserve-original-content.md)).

   Include Khmer and Mandarin cases in the tests.
4. **You:** Wrap an error through three function layers and print it. Read the message chain. Does `errors.Is` still find the sentinel?
5. **You:** Reproduce the nil interface trap in a test, then fix it.

### Checkpoint

- When do you use `errors.Is` and when do you use `errors.As`?
- What do you lose if you wrap with `%v` instead of `%w`?
- Why shouldn't you both log an error and return it?
- When, if ever, is `panic` appropriate in a web service?

**References:** [Error handling and Go](https://go.dev/blog/error-handling-and-go), [Working with errors in Go 1.13](https://go.dev/blog/go1.13-errors), [`errors` package](https://pkg.go.dev/errors)

---

## Stage 6: Interfaces, packages, and a little generics

**Why it matters for CRUD:** `http.Handler`, `io.Reader`, and `error` are all interfaces, and the standard library is built from small ones. Package boundaries decide what your HTTP code and storage code are allowed to know about each other.

### Concepts

- **Implicit interfaces:** A type satisfies an interface by having its methods. There's no `implements` keyword. Small interfaces are the most useful: `io.Reader` (one method), `io.Writer` (one method), `fmt.Stringer`, `error`, and `http.Handler`.
- **Define interfaces where they're used:** The code that *needs* a behavior declares a small interface for exactly that need. The code that provides it returns a concrete type. The saying is "accept interfaces, return structs".
- **Don't create interfaces in advance:** Add one when you have two implementations or a real testing need, not because it feels like good architecture. A Java-style interface for every service is a common mistake when coming from other languages.
- **Compile-time check:** `var _ Getter = (*RecipeBook)(nil)` fails to compile if `RecipeBook` stops satisfying `Getter`.
- **`any`, type assertions, and type switches:** `any` is the empty interface. `v, ok := x.(string)` asserts a type, and `switch v := x.(type)` branches on it. These are rare in application code.
- **Packages:**
  - Use short, lowercase package names with no underscores.
  - Avoid stutter: write `recipes.Store`, not `recipes.RecipeStore`.
  - An `internal/` directory can't be imported from outside its parent.
  - **Import cycles are compile errors.** Design the direction of dependencies on purpose: the HTTP layer depends on storage, never the other way round.
- **Doc comments:** A comment directly above an exported name, starting with that name, becomes its documentation.
- **Generics:** `func Filter[T any](items []T, keep func(T) bool) []T`. They're useful for containers and algorithms like those in `slices` and `maps`. They're rarely the right tool for business logic or a "generic repository".

### Coming from

- **Python:** It's duck typing that the compiler checks, or `typing.Protocol` enforced at build time.
- **JavaScript and TypeScript:** It's TypeScript's structural typing, applied to methods.
- **C++:** It's like an abstract base class, except the concrete type never names it. Go generics are simpler and more restricted than templates.

### Example

```go
// The consumer declares only what it needs.
type recipeGetter interface {
	Get(id int64) (Recipe, error)
}

func printTitle(w io.Writer, g recipeGetter, id int64) error {
	r, err := g.Get(id)
	if err != nil {
		return err
	}
	_, err = fmt.Fprintln(w, r.Title)
	return err
}
```

`printTitle` works with `os.Stdout`, a `bytes.Buffer` in tests, a file, or an HTTP response, because all of them are `io.Writer`s.

### Exercises

1. **You:** Implement `String()` on `Recipe` and print one with `%v` and `%s`. What changed?
2. **You:** Write `printAll(w io.Writer, recipes []Recipe) error`. Test it with a `bytes.Buffer`, then call it with `os.Stdout`.
3. **You:** Move `Recipe`, `RecipeBook`, and the validation into a package at `learning/go/recipebook/` and call it from a `main` package. Fix whatever breaks. What did capitalization have to do with it?
4. **You:** Write a fake `recipeGetter` in a test that always returns `ErrNotFound`, and test `printTitle`'s error path. Then **discuss**: was the interface worth it here, or would the real `RecipeBook` have been simpler?
5. **You:** Write a generic `Map[T, U any](items []T, f func(T) U) []U`. Then find a place where a plain `for` loop reads better.

### Checkpoint

- How does a type satisfy an interface in Go?
- Why define interfaces in the consuming package?
- When does an interface become premature abstraction?
- What is an import cycle, and how do you avoid one?

**References:** [Effective Go: Interfaces](https://go.dev/doc/effective_go#interfaces), [Go Code Review Comments: Interfaces](https://go.dev/wiki/CodeReviewComments#interfaces), [Tutorial: Generics](https://go.dev/doc/tutorial/generics), [When to use generics](https://go.dev/blog/when-generics)

---

# Part II: Programs that touch the world

## Stage 7: Files, JSON, and a CLI recipe box

**Why it matters for CRUD:** JSON is your API's language. Building CRUD as a command-line tool first separates "how data is shaped, validated, and stored" from "how HTTP works". When you add HTTP in Stage 9, only the transport changes.

> **Mini-capstone.** Create `learning/recipebox/` as a new module with `go mod init example.com/recipebox`. Stages 7 to 15 grow inside it.

### Concepts

- **`io.Reader` and `io.Writer` everywhere:** Files, network connections, HTTP bodies, and buffers all implement them.
- **Files:**
  - `os.ReadFile` and `os.WriteFile` cover small files.
  - `os.Open` plus `defer f.Close()` is for streaming.
  - `errors.Is(err, fs.ErrNotExist)` detects a missing file.
  - `t.TempDir()` gives each test a clean directory.
- **`encoding/json`:**
  - `json.Marshal` and `json.Unmarshal` convert whole values. `json.NewEncoder` and `json.NewDecoder` stream.
  - Struct tags rename fields: `` `json:"createdAt"` ``. `omitempty` skips zero values, and `-` skips a field entirely.
  - **Only exported fields are encoded.** Unexported fields are silently ignored.
  - `time.Time` is encoded as an RFC 3339 string.
  - **Missing JSON fields become zero values.** You can't tell "not sent" from "sent empty" unless you use a pointer field (`*string`).
  - `Decoder.DisallowUnknownFields()` rejects fields you don't recognize.
  - A nil slice encodes as `null` (see Stage 3).
- **Separate the wire format from internal types:** Request and response structs, such as `createRecipeRequest`, are the API contract. Internal structs are free to change. Mixing them couples the database, the domain, and the API.
- **Command-line basics:** `os.Args`, the `flag` package (with `flag.NewFlagSet` per subcommand), writing errors to `os.Stderr`, and `os.Exit(1)` on failure.

### Example

```go
type Recipe struct {
	ID        int64     `json:"id"`
	Title     string    `json:"title"`
	Language  string    `json:"language"`
	Story     string    `json:"story,omitempty"`
	CreatedAt time.Time `json:"createdAt"`
	draft     bool      // unexported: never appears in JSON
}

// Optional fields for a partial update: nil means "not sent".
type patchRecipeRequest struct {
	Title *string `json:"title"`
	Story *string `json:"story"`
}
```

### Exercises

1. **You:** Marshal a `Recipe` with and without tags, and with an empty `Story`. Compare the outputs.
2. **You:** Unmarshal `{"title": "Amok", "colour": "green"}` into a `Recipe`. Where did `colour` go? Now decode it with `DisallowUnknownFields` and read the error.
3. **You:** Unmarshal `{}` into `patchRecipeRequest` and into a struct without pointers. Explain why the pointers matter for a partial update.
4. **You:** Write `Save(path string, recipes []Recipe) error` and `Load(path string) ([]Recipe, error)`. A missing file means an empty list, not an error. Test the round trip with `t.TempDir()`.
5. **You:** Build the CLI:

   ```text
   recipebox add -title "Kuy teav" -speaker "Ma" -language Khmer
   recipebox list
   recipebox show 1
   recipebox edit 1 -title "Kuy teav Phnom Penh"
   recipebox delete 1
   ```

   Reuse your `RecipeBook`, validation, and errors from earlier stages. Errors go to stderr with exit code 1. Test the logic, not `main`.
6. **Stretch:** Make saving atomic by writing to a temporary file and then renaming it. Why does that protect the data if the program crashes mid-write?

### Checkpoint

- Why should API request and response structs be separate from internal types?
- How do you tell a missing JSON field from an empty one?
- What happens to unexported fields during marshaling?
- Without notes: add a new field to the CLI end to end, covering the flag, the struct, validation, JSON, and a test.

> **Readiness review.** After this stage, revisit the Go foundations exit criteria in the [project roadmap](../docs/ROADMAP.md): implement a modest program with occasional hints, handle a failure, write useful tests, and explain data flow and design choices. Assess this through your work, record the evidence in LEARNING, and decide the [track](#bridge-to-mas-cooking) and collaboration mode for the next stages.

**References:** [JSON and Go](https://go.dev/blog/json), [`encoding/json` package](https://pkg.go.dev/encoding/json), [`flag` package](https://pkg.go.dev/flag), [`os` package](https://pkg.go.dev/os)

---

## Stage 8: Just enough concurrency for servers

**Why it matters for CRUD:** `net/http` handles **each request in its own goroutine**. The moment two requests touch your in-memory `RecipeBook`, you have concurrent map writes, which is a data race and can crash the program. You also need `context.Context` so that a cancelled request stops its database query.

Advanced concurrency patterns are **not** a prerequisite for a CRUD app. Learn this stage well and leave pipelines and worker pools for later.

### Concepts

- **Goroutines:** `go f()` starts a function concurrently. They're cheap, so thousands are normal. A program exits when `main` returns, even if goroutines are still running.
- **Data races:** Two goroutines access the same memory and at least one writes. The result is undefined behavior: wrong data, or a `fatal error: concurrent map writes`.
- **`sync.Mutex`:** `mu.Lock()` followed by `defer mu.Unlock()`. Keep the critical section short, protect whole invariants (such as `nextID` and the map together), and **never copy a struct containing a mutex** (`go vet` reports this). `sync.RWMutex` allows many readers or one writer.
- **The race detector:** `go test -race ./...` instruments the code and reports races it actually observes. Run it regularly.
- **`sync.WaitGroup`:** Waits for a group of goroutines to finish. `wg.Go(func() { ... })`, available since Go 1.25, combines `Add`, `go`, and `Done`.
- **Channels:** Typed pipes between goroutines. Learn enough to read code: unbuffered channels hand off one value at a time, buffered channels have capacity, `close` ends a channel, `range ch` drains it, and `select` waits on several channels at once. "Share memory by communicating" is the slogan, but a mutex is the right tool for protecting a map.
- **`context.Context`:**
  - It carries cancellation, deadlines, and a few request-scoped values.
  - By convention it's the first parameter, `ctx context.Context`, and it's never stored in a struct.
  - In a server, it starts at `r.Context()` and flows into every database call.
  - `context.WithTimeout(ctx, 2*time.Second)` returns a `cancel` function. Always `defer cancel()`.
- **Goroutine leaks:** A goroutine blocked forever on a channel nobody will use. Every goroutine you start needs a way to finish.

### Coming from

- **JavaScript:** There's no `async`/`await` coloring. Blocking calls are normal because the runtime schedules goroutines. `context` cancellation is similar to `AbortController`.
- **Python:** There's no GIL. Goroutines really do run in parallel across CPU cores, which is exactly why races matter.
- **C++:** It's like `std::thread` with `std::mutex`, but far cheaper, and with a race detector built in.

### Example

These use different types than your exercises:

```go
type visitCounter struct {
	mu    sync.Mutex
	count int
}

func (c *visitCounter) Inc() {
	c.mu.Lock()
	defer c.mu.Unlock()
	c.count++
}

func waitForOven(ctx context.Context, d time.Duration) error {
	select {
	case <-time.After(d):
		return nil
	case <-ctx.Done():
		return ctx.Err() // context.Canceled or context.DeadlineExceeded
	}
}
```

### Exercises

1. **You:** Write a test that calls `RecipeBook.Add` from 100 goroutines using a `WaitGroup`, then checks that `List` returns 100 recipes. Run it with and without `-race`, and read the race report carefully.
2. **You:** Add a mutex to `RecipeBook` and make every method safe. Run the test with `-race` until it's clean. Which fields does the mutex protect, and why both of them?
3. **You:** Predict, then run: send on an unbuffered channel with no receiver. Read the `all goroutines are asleep` error.
4. **You:** Test `waitForOven` with a context that times out before the oven finishes and with one that doesn't. What error do you get in each case?
5. **Discuss:** A user closes the browser tab during a slow database query. With and without context propagation, what happens to the query?

### Checkpoint

- Why does an in-memory store in an HTTP server need a mutex?
- What does the race detector catch, and what can it miss?
- Why is `ctx` the first parameter and not a struct field?
- What happens if you forget `defer cancel()`?

**References:** [Effective Go: Concurrency](https://go.dev/doc/effective_go#concurrency), [Data race detector](https://go.dev/doc/articles/race_detector), [Go concurrency patterns: Context](https://go.dev/blog/context), [`sync` package](https://pkg.go.dev/sync)

---

# Part III: Building the service

## Stage 9: HTTP with net/http

**Why it matters for CRUD:** This is the "C, R, U, D over HTTP" part. You'll build the full API against your in-memory `RecipeBook` first, so you can learn HTTP without database complexity. In Stage 12 you'll replace only the storage.

### Concepts

- **HTTP refresher:** A request has a method, path, query, headers, and body. A response has a status, headers, and body. Watch them with `curl -i` or `curl -v`.
- **`http.Handler`:** An interface with a single method, `ServeHTTP(w http.ResponseWriter, r *http.Request)`. `http.HandlerFunc` adapts an ordinary function into a handler. Everything in `net/http` is built from this one interface.
- **`http.ServeMux` routing:** Patterns combine a method and a path, such as `"GET /api/recipes/{id}"`. Read path parameters with `r.PathValue("id")`. A wrong method automatically gets `405 Method Not Allowed`, and more specific patterns win over general ones. No router library is needed.
- **Order of a response:** Set headers with `w.Header().Set(...)`, then call `w.WriteHeader(status)`, then write the body. Headers set after `WriteHeader` are ignored. Writing a body without calling `WriteHeader` sends 200.
- **After writing an error response, `return`.** Forgetting to is a classic bug.
- **Reading JSON safely:**
  - Limit the body with `http.MaxBytesReader`.
  - Call `DisallowUnknownFields` before decoding.
  - Reject trailing data after the first JSON value.
  - Map decoding failures to 400, or 413 for bodies that are too large.
- **Writing JSON:** Use one small helper that sets `Content-Type`, writes the status, and encodes the value. Return `[]`, never `null`, for empty lists.
- **Status codes for CRUD:**

  | Operation | Route | Success | Common failures |
  | --- | --- | --- | --- |
  | Create | `POST /api/recipes` | `201 Created` with a `Location` header and the created body | 400 malformed JSON; 422 or 400 validation failure |
  | List | `GET /api/recipes` | `200 OK` with an array | 400 bad query parameters |
  | Read | `GET /api/recipes/{id}` | `200 OK` | 400 bad ID; 404 |
  | Replace | `PUT /api/recipes/{id}` | `200 OK` with the updated body | 400, 404, 422; 409 conflict (Stage 13) |
  | Delete | `DELETE /api/recipes/{id}` | `204 No Content` | 404, or 204 if you make it idempotent; choose and document |
  | Health | `GET /api/health` | `200 OK` | 503 when a dependency is down (Stage 12) |

  Choose one convention for validation failures (400 or 422) and document it.
- **A consistent error shape:** For example `{"error": {"message": "...", "fields": {"title": "required"}}}`. [RFC 9457 problem details](https://www.rfc-editor.org/rfc/rfc9457) is a standard alternative. **Never send raw internal errors to clients.** Log them and return a generic 500 message.
- **Dependencies through a struct:** An `application` struct holds the store and logger, and handlers are its methods. No globals and no `http.DefaultServeMux`.
- **`net/http/httptest`:** `httptest.NewRequest` plus `httptest.NewRecorder` tests a handler without a network. Send requests **through the mux** so that `PathValue` is populated.

### Example

```go
type application struct {
	logger  *slog.Logger
	recipes *RecipeBook
}

func (app *application) routes() http.Handler {
	mux := http.NewServeMux()
	mux.HandleFunc("GET /api/health", app.health)
	mux.HandleFunc("GET /api/recipes", app.listRecipes)
	mux.HandleFunc("POST /api/recipes", app.createRecipe)
	mux.HandleFunc("GET /api/recipes/{id}", app.getRecipe)
	mux.HandleFunc("PUT /api/recipes/{id}", app.updateRecipe)
	mux.HandleFunc("DELETE /api/recipes/{id}", app.deleteRecipe)
	return mux
}

func writeJSON(w http.ResponseWriter, status int, v any) error {
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(status)
	return json.NewEncoder(w).Encode(v)
}

func (app *application) health(w http.ResponseWriter, r *http.Request) {
	writeJSON(w, http.StatusOK, map[string]string{"status": "ok"})
}
```

A handler test:

```go
func TestGetRecipeNotFound(t *testing.T) {
	app := &application{
		logger:  slog.New(slog.DiscardHandler),
		recipes: NewRecipeBook(),
	}
	req := httptest.NewRequest(http.MethodGet, "/api/recipes/999", nil)
	rec := httptest.NewRecorder()

	app.routes().ServeHTTP(rec, req)

	if rec.Code != http.StatusNotFound {
		t.Fatalf("status = %d, want %d", rec.Code, http.StatusNotFound)
	}
}
```

### Exercises

1. **You:** Start a server with only `/api/health` and inspect it with `curl -i`. Try `POST /api/health` and explain the 405 response.
2. **You:** Implement `GET /api/recipes` and `GET /api/recipes/{id}` on top of `RecipeBook`. Handle an invalid ID, a missing recipe, and an empty list (which must return `[]`). Write handler tests for each.
3. **You:** Write a `decodeJSON` helper with a size limit, unknown-field rejection, and trailing-data rejection. Test it with malformed JSON, an unknown field, and an oversized body.
4. **You:** Implement `POST /api/recipes`. Decode a request struct, validate it with your Stage 5 validator, convert it to `Recipe`, store it, and respond 201 with a `Location` header. Test valid input, invalid input, and malformed JSON.
5. **You:** Implement `PUT` and `DELETE`. Update your endpoint table if you changed any decisions.
6. **You:** Write one `writeError` helper and make every failure use the same error shape. Add one test that asserts the JSON error body, not just the status.
7. **Experiment:** Remove a `return` after an error response. Run the request and read the server log's `superfluous response.WriteHeader call`. Restore it.

### Checkpoint

- What is `http.Handler`, and why can middleware and muxes be handlers too?
- Why route test requests through the mux instead of calling the handler directly?
- Without notes: list the status code for each CRUD operation's success and two of its failures.
- Why should a 500 response never contain `err.Error()`?

**References:** [`net/http` package](https://pkg.go.dev/net/http), [Routing enhancements for Go 1.22](https://go.dev/blog/routing-enhancements), [`httptest` package](https://pkg.go.dev/net/http/httptest), [MDN: HTTP response status codes](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status)

---

## Stage 10: Structuring and running the service

**Why it matters for CRUD:** A well-made service is configurable, observable, and stops cleanly. It also has a `main` you can read top to bottom. That's the difference between "it runs on my laptop" and something you can deploy.

### Concepts

- **`main` as the composition root:**
  1. Load configuration.
  2. Build the logger.
  3. Open dependencies.
  4. Build the `application`.
  5. Start the server.
  6. Wait for a shutdown signal.
  7. Shut down.

  A common pattern is to keep `main` tiny: it calls `run(ctx, getenv) error` and exits with code 1 on error, which makes `run` testable.
- **Configuration from environment variables:** Use `PORT`, `DATABASE_URL`, `LOG_LEVEL`, and so on. Validate them at startup and **fail fast** with a clear message. Provide sensible local defaults where that's safe. Never put secrets in code or logs.
- **Binding:** Many hosts, including Render, set `PORT` and expect you to listen on `0.0.0.0:$PORT`. The address `":" + port` listens on all interfaces. Locally, `localhost:8080` is fine.
- **`http.Server` with timeouts:** The bare `http.ListenAndServe` has **no timeouts**, so slow clients can hold connections forever. Set `ReadHeaderTimeout`, `ReadTimeout`, `WriteTimeout`, and `IdleTimeout`.
- **Graceful shutdown:** `signal.NotifyContext` gives you a context that's cancelled on Ctrl-C or `SIGTERM`. `srv.Shutdown(ctx)` stops accepting new connections and lets in-flight requests finish.
- **Structured logging with `log/slog`:** Logs are key-value pairs, such as `logger.Info("recipe created", "id", id)`. Use the text handler locally and the JSON handler in production. Use levels, and pass the logger in instead of using a global.
- **Middleware:** A function of the form `func(next http.Handler) http.Handler`, which is closures from Stage 2 applied to HTTP. Common uses are request logging (method, path, status, duration), panic recovery that returns a JSON 500, and request IDs. Chain them by plain function wrapping, no framework needed.
- **Capturing the status code:** Wrap `http.ResponseWriter` by embedding it, which is the Stage 4 concept, and override `WriteHeader`.
- **CORS and the Vue dev server:** In development, a Vite proxy avoids CORS entirely. If the frontend and API are on different origins in production, configure CORS deliberately with explicit origins, not `*` with credentials.
- **Layout:**
  - Start **flat**: `main.go`, `routes.go`, `handlers.go`, `store.go`, `errors.go`, and `middleware.go` in a single `main` package.
  - Move to `cmd/api/` and `internal/...` when the flat layout causes friction, not before.
  - Document the run commands in the README.

### Example

```go
type statusRecorder struct {
	http.ResponseWriter // embedded: all methods promoted
	status int
}

func (r *statusRecorder) WriteHeader(code int) {
	r.status = code
	r.ResponseWriter.WriteHeader(code)
}
```

Shutdown sketch:

```go
srv := &http.Server{
	Addr:              cfg.addr,
	Handler:           app.routes(),
	ReadHeaderTimeout: 5 * time.Second,
	ReadTimeout:       15 * time.Second,
	WriteTimeout:      15 * time.Second,
	IdleTimeout:       60 * time.Second,
}

serverErr := make(chan error, 1)
go func() { serverErr <- srv.ListenAndServe() }()

select {
case err := <-serverErr:
	return err // for example, the port is already in use
case <-ctx.Done(): // Ctrl-C or SIGTERM
}

shutdownCtx, cancel := context.WithTimeout(context.Background(), 10*time.Second)
defer cancel()
return srv.Shutdown(shutdownCtx)
```

### Exercises

1. **You:** Write `loadConfig(getenv func(string) string) (config, error)`. Test the defaults, a valid override, and an invalid `PORT`, passing a fake `getenv` instead of setting real environment variables. Then compare that with `t.Setenv`.
2. **You:** Add an `slog.Logger` to `application` and log one line per created recipe. Switch between the text and JSON handlers with configuration.
3. **You:** Write logging middleware using `statusRecorder`. What should `status` start as, and why? Test it with `httptest`.
4. **You:** Add recovery middleware. Make a handler panic deliberately and compare the behavior with and without it.
5. **You:** Implement graceful shutdown. Add a temporary endpoint that sleeps 5 seconds, call it, press Ctrl-C right away, and confirm the request still completes.
6. **You:** Write the README "Run locally" section for `learning/recipebox/`. Ask someone, or a fresh agent session, to follow it exactly.

### Checkpoint

- Walk through `main` and say what each step is for.
- Why are server timeouts important for a public service?
- How does middleware use closures and the `http.Handler` interface?
- What happens to in-flight requests on `SIGTERM` with and without `Shutdown`?

**References:** [Structured logging with slog](https://go.dev/blog/slog), [`log/slog` package](https://pkg.go.dev/log/slog), [`http.Server`](https://pkg.go.dev/net/http#Server), [`os/signal` package](https://pkg.go.dev/os/signal)

---

## Stage 11: PostgreSQL for application developers

**Why it matters for CRUD:** The database is the source of truth. Constraints are the last line of defense when application validation has a bug, and migrations make the schema reproducible for you, your tests, and production.

You already know SQL. This stage covers what's specific to PostgreSQL and what matters for applications.

### Concepts

- **Running PostgreSQL locally:** The options are a Docker `postgres` image, Postgres.app, or Homebrew. The project hasn't chosen yet (see [ARCHITECTURE](../docs/ARCHITECTURE.md)). Pick one, document the setup commands, and record an ADR if the choice has lasting consequences. Keep separate **development** and **test** databases.
- **`psql`:** `\l` lists databases, `\c db` connects to one, `\dt` lists tables, `\d recipes` describes a table, `\x` toggles expanded output, and `\q` quits.
- **Connection URLs:** `postgres://user:password@localhost:5432/recipebox_dev?sslmode=disable`. Keep them in environment variables, never in code.
- **Types worth knowing:**
  - `text`: no need for `varchar(n)`. Express length rules with `CHECK`.
  - `bigint GENERATED ALWAYS AS IDENTITY`: the modern auto-increment.
  - `uuid`: an alternative ID with different tradeoffs.
  - `timestamptz`: always use this for points in time.
  - `boolean`, `integer`, and `numeric` for exact decimals.
  - `jsonb`: useful, but not an escape hatch from designing a schema.
- **Constraints:**
  - `NOT NULL`, `CHECK`, `UNIQUE`, `PRIMARY KEY`, and `REFERENCES` (foreign keys) with `ON DELETE RESTRICT`, `CASCADE`, or `SET NULL`.
  - `DEFAULT` values.
  - Choose `ON DELETE` behavior by asking what *should* happen to the product data.
- **NULL semantics:** `NULL = NULL` is not true, so use `IS NULL`. Prefer `NOT NULL` with a meaningful default when "missing" doesn't mean anything different from "empty".
- **Useful PostgreSQL features:** `RETURNING` (insert and get the ID and timestamps back in one round trip), `ON CONFLICT` (upsert), and `$1, $2` placeholders.
- **Unquoted identifiers are folded to lowercase.** Use `snake_case` and you'll never need quotes.
- **Transactions:** `BEGIN`, `COMMIT`, and `ROLLBACK`. The default isolation level is Read Committed. Know that the lost-update problem exists (Stage 13).
- **Indexes:** Primary keys and `UNIQUE` constraints are indexed automatically. **Foreign key columns are not.** Add indexes when a query pattern justifies them, and check with `EXPLAIN ANALYZE`.
- **Migrations:**
  - Numbered SQL files applied in order and recorded in a tracking table.
  - **Never edit a migration that has already been applied anywhere.** Add a new one instead.
  - First apply plain SQL files with `psql -f` so you understand the mechanics.
  - Then choose a tool only for a concrete need: [golang-migrate](https://github.com/golang-migrate/migrate), [goose](https://github.com/pressly/goose), or [tern](https://github.com/jackc/tern).

### Coming from

- **MySQL or SQLite:** PostgreSQL is stricter about types, uses `$1` instead of `?`, has identity columns instead of `AUTO_INCREMENT`, and has real `timestamptz`.
- **SQL in general:** Treat constraints as part of your application's correctness, not as administration.

### Practice schema

This is practice only, not a decision about the product schema:

```sql
CREATE TABLE speakers (
    id         bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name       text NOT NULL CHECK (char_length(name) BETWEEN 1 AND 100),
    created_at timestamptz NOT NULL DEFAULT now()
);

CREATE TABLE recipes (
    id         bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    speaker_id bigint NOT NULL REFERENCES speakers (id) ON DELETE RESTRICT,
    title      text NOT NULL CHECK (char_length(title) BETWEEN 1 AND 200),
    language   text NOT NULL,
    story      text NOT NULL DEFAULT '',
    version    integer NOT NULL DEFAULT 1,
    created_at timestamptz NOT NULL DEFAULT now(),
    updated_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX recipes_speaker_id_idx ON recipes (speaker_id);
```

### Exercises

1. **You:** Install or start PostgreSQL, then create `recipebox_dev` and `recipebox_test`. Write the exact commands in the README.
2. **You:** Put the schema in `migrations/0001_init.sql`, apply it with `psql -f`, and inspect it with `\d recipes`.
3. **You:** Try to insert rows that break each constraint: an empty title, a missing speaker, an unknown `speaker_id`, and deleting a speaker who still has recipes. Note each error's SQLSTATE code. You'll map them in Go.
4. **You:** Write `INSERT ... RETURNING id, created_at`, a list query ordered by `created_at DESC, id DESC` with `LIMIT 20`, and a join that returns the speaker's name with each recipe.
5. **You:** In `psql`, run `BEGIN`, insert a row, then `ROLLBACK`. Confirm the row is gone. Repeat with two `psql` sessions and observe what each one can see before the commit.
6. **Discuss:** Should deleting a recipe cascade to its media later on? What does [PRODUCT](../docs/PRODUCT.md) say deletion has to mean for privacy?
7. **You:** Run `EXPLAIN ANALYZE` on the list query and the join. Find where the index on `speaker_id` gets used, or doesn't, and why.

### Checkpoint

- Why use database constraints if Go already validates?
- Why `timestamptz` instead of `timestamp`?
- What must you never do to an applied migration, and why?
- Which columns get indexes automatically, and which commonly need one added?

**References:** [PostgreSQL tutorial](https://www.postgresql.org/docs/current/tutorial.html), [Constraints](https://www.postgresql.org/docs/current/ddl-constraints.html), [Data types](https://www.postgresql.org/docs/current/datatype.html), [Error codes](https://www.postgresql.org/docs/current/errcodes-appendix.html), [Using EXPLAIN](https://www.postgresql.org/docs/current/using-explain.html)

---

## Stage 12: Go and PostgreSQL together

**Why it matters for CRUD:** This is where data persists. Getting it right means visible, parameterized SQL, errors mapped cleanly across layers, contexts on every query, and tests that run against a real database.

### Concepts

- **Choosing a driver:**
  - `database/sql` is Go's generic database interface, similar to Python's DB-API.
  - [`pgx`](https://pkg.go.dev/github.com/jackc/pgx/v5) is a PostgreSQL-specific driver with better type support and its own pool, `pgxpool`. The project leans toward `pgx` (see [ARCHITECTURE](../docs/ARCHITECTURE.md)).
  - This is likely your first dependency: `go get github.com/jackc/pgx/v5`. Look at what changes in `go.mod` and `go.sum`.
- **The pool:**
  - Create one `pgxpool.Pool` at startup, share it everywhere (it's safe for concurrent use), and close it on shutdown.
  - `Ping` it at startup with a timeout so a bad configuration fails fast.
- **Three query shapes:**
  - `QueryRow(...).Scan(&a, &b)` for exactly one row. `pgx.ErrNoRows` means none was found.
  - `Query(...)` followed by `for rows.Next() { rows.Scan(...) }`, then `rows.Err()`, with `defer rows.Close()`.
  - `Exec(...)` for writes without returned rows. Check `RowsAffected()` for updates and deletes.
- **Parameters, always:** Write `WHERE id = $1` and pass `id` as an argument. **Never build SQL with `fmt.Sprintf` or `+`**, because that's SQL injection. Identifiers such as column names in `ORDER BY` can't be parameters, so map user input through an allowlist instead.
- **Map errors at the storage boundary:**

  | Storage result | Store returns | Handler responds |
  | --- | --- | --- |
  | `pgx.ErrNoRows`, or 0 rows affected | `ErrNotFound` | 404 |
  | `23505` unique violation | `ErrConflict`, or a specific error | 409 |
  | `23503` foreign key violation | An error such as `ErrSpeakerNotFound` or `ErrInUse` | 422 or 409 |
  | `23514` check violation | A validation-style error, which means a bug in Go validation | 422 |
  | Anything else | A wrapped error with context | 500 (logged, generic message) |

  Extract the code with `var pgErr *pgconn.PgError; errors.As(err, &pgErr)`. **The store knows SQL and the handler knows HTTP. Neither leaks into the other.**
- **NULL and Go:** Columns that are `NOT NULL` map directly to Go types. Nullable columns need pointers (`*string`) or `pgtype` types. This is one more reason to prefer `NOT NULL`.
- **Time:** `timestamptz` maps to `time.Time`. Store and compare in UTC, and format for display at the edges.
- **Transactions:** Begin, then `defer tx.Rollback(ctx)`, then do the work, then `tx.Commit(ctx)`. The deferred rollback is harmless after a commit and saves you on every early return.
- **Contexts on every call:** Pass the request context down. Add a timeout for anything that could hang.
- **Integration tests against a real database:** Mocking SQL proves very little.
  - Read `TEST_DATABASE_URL` and call `t.Skip` if it isn't set. Never point tests at the dev database.
  - Apply migrations, and reset the tables between tests, for example with `TRUNCATE ... RESTART IDENTITY CASCADE`.
  - Packages run their tests **in parallel**. If several packages touch the same database, use `go test -p 1` or a database per package.
- **Later:** [`sqlc`](https://sqlc.dev/) generates type-safe Go from SQL you write. Consider it once repetitive scanning becomes painful, not before. Avoid heavy ORMs.

### Example

```go
type Store struct {
	pool *pgxpool.Pool
}

func (s *Store) GetRecipe(ctx context.Context, id int64) (Recipe, error) {
	const q = `
		SELECT id, speaker_id, title, language, story, version, created_at, updated_at
		FROM recipes
		WHERE id = $1`

	var r Recipe
	err := s.pool.QueryRow(ctx, q, id).Scan(
		&r.ID, &r.SpeakerID, &r.Title, &r.Language, &r.Story,
		&r.Version, &r.CreatedAt, &r.UpdatedAt,
	)
	if errors.Is(err, pgx.ErrNoRows) {
		return Recipe{}, ErrNotFound
	}
	if err != nil {
		return Recipe{}, fmt.Errorf("get recipe %d: %w", id, err)
	}
	return r, nil
}
```

Shape of a transaction:

```go
tx, err := s.pool.Begin(ctx)
if err != nil {
	return fmt.Errorf("begin: %w", err)
}
defer tx.Rollback(ctx) // harmless after Commit

// ... several tx.Exec / tx.QueryRow calls; return early on any error ...

if err := tx.Commit(ctx); err != nil {
	return fmt.Errorf("commit: %w", err)
}
return nil
```

Test helper shape:

```go
func newTestStore(t *testing.T) *Store {
	t.Helper()
	url := os.Getenv("TEST_DATABASE_URL")
	if url == "" {
		t.Skip("TEST_DATABASE_URL not set; skipping database test")
	}
	pool, err := pgxpool.New(context.Background(), url)
	if err != nil {
		t.Fatalf("connect: %v", err)
	}
	t.Cleanup(pool.Close)
	// reset tables here so each test starts clean
	return &Store{pool: pool}
}
```

### Exercises

1. **You:** Write a tiny program that connects and pings. Break the password, the port, and the database name in turn, and read each error. Which ones would you want a startup log to show, and which parts of the URL must never be logged?
2. **You:** Implement `CreateRecipe` with `INSERT ... RETURNING`. Write an integration test that creates a recipe and reads it back.
3. **You:** Implement `GetRecipe`, `ListRecipes` (returning an empty slice, not nil, when there are no rows), `UpdateRecipe`, and `DeleteRecipe` using `RowsAffected`. Test both the found and not-found cases.
4. **You:** Map the PostgreSQL errors from Stage 11, exercise 3, to domain errors. Write a test that creates a recipe for a nonexistent speaker and asserts your domain error.
5. **You:** Swap the HTTP handlers from `RecipeBook` to `Store`. **Discuss:** did the handler code change much? If you had introduced an interface in Stage 6, did it help? Do you still need `RecipeBook`?
6. **You:** Make `GET /api/health` ping the database with a one-second timeout, returning 503 if it fails. Stop PostgreSQL and watch the response change.
7. **Experiment:** In a scratch function, build a query by concatenating user input and pass in `1 OR 1=1`. See what happens, then fix it with a parameter. Delete the scratch function afterwards.
8. **You:** Add a `recipe_revisions` table and make `UpdateRecipe` transactional: copy the current row into `recipe_revisions`, update the recipe, and increment `version`. Force an error between the two steps and prove nothing was written. This pattern preserves original content when people edit, in the spirit of [ADR 0002](../docs/decisions/0002-preserve-original-content.md).

### Checkpoint

- Trace `GET /api/recipes/7`: handler, then store, then SQL, then `Scan`, then the error mapping, then JSON. Say which layer owns each decision.
- Why is `defer tx.Rollback(ctx)` safe after a successful commit?
- What's the difference between `rows.Err()` and the error from `Scan`?
- Why test against a real PostgreSQL database instead of mocking `pgx`?

**References:** [Accessing relational databases (Go docs)](https://go.dev/doc/database/), [`pgx` v5](https://pkg.go.dev/github.com/jackc/pgx/v5), [`pgxpool`](https://pkg.go.dev/github.com/jackc/pgx/v5/pgxpool), [`pgconn.PgError`](https://pkg.go.dev/github.com/jackc/pgx/v5/pgconn#PgError)

---

## Stage 13: Making CRUD well made

**Why it matters:** Most CRUD code "works". Well-made CRUD code survives bad input, simultaneous edits, large lists, and malicious users, and it documents what it promises. This stage turns your API into one you'd be comfortable showing a senior engineer.

### Concepts

- **Three layers of validation:**
  1. Decoding checks the shape: is it JSON, with known fields and a size within limits?
  2. Validation checks the rules: required fields and lengths in runes.
  3. Database constraints are the backstop.

  Return **all** field errors at once so a form can show them together. Check `utf8.ValidString` on text input.
- **Preserve what the user wrote:** Don't silently trim, normalize, or rewrite stories and transcripts. If you trim a title only to check whether it's empty, decide whether to store the original or the trimmed value, and make that choice deliberately and documented ([ADR 0002](../docs/decisions/0002-preserve-original-content.md)).
- **The API contract:**
  - One error shape.
  - A documented status code for every case.
  - One JSON naming convention, `camelCase` or `snake_case`. Pick one.
  - **JavaScript numbers lose precision above 2^53.** An `int64` ID is fine at this scale, but know why some APIs send IDs as strings or use UUIDs.
- **Pagination:**
  - Always cap `limit`, with a default and a maximum.
  - **Offset pagination** (`LIMIT/OFFSET`) is simple, but it gets slower deep into the list and can skip or repeat rows when data changes.
  - **Keyset pagination** (`WHERE (created_at, id) < ($1, $2) ORDER BY created_at DESC, id DESC LIMIT $3`) is stable and fast.
  - Always add a unique tiebreaker to `ORDER BY`.
- **Filtering and sorting safely:** Allowlist the sort options with something like `map[string]string{"newest": "created_at DESC, id DESC", "title": "title ASC, id ASC"}`. Never interpolate raw user input into SQL.
- **PUT versus PATCH:** `PUT` replaces the whole resource. `PATCH` applies a partial update, using pointer fields to detect what was sent (Stage 7). Choose what you support and document it. Set `updated_at = now()` in SQL rather than trusting the client's clock.
- **Lost updates and optimistic locking:**
  - The problem: two people load version 3 and both save, and the second save silently erases the first.
  - The fix: `UPDATE ... WHERE id = $1 AND version = $2`, then `SET version = version + 1`.
  - Zero affected rows means not found (404) or stale (409 Conflict). Check which one.
  - ETags with `If-Match` are the HTTP-standard version of the same idea.
- **What deletion means:**
  - A hard delete removes the row. A soft delete sets `deleted_at`, and every query then has to filter it out.
  - For family content, "delete" may have to really mean delete, including media and eventually backups (see [PRODUCT](../docs/PRODUCT.md)).
  - Decide on purpose, and decide whether `DELETE` is idempotent.
- **Idempotency:** `GET`, `PUT`, and `DELETE` are idempotent and `POST` isn't. Plan for double-submits from the UI.
- **Authorization preview (Milestone 2):**
  - **Authentication** establishes who you are. **Authorization** decides what you may do.
  - Enforce ownership in the query itself: `WHERE id = $1 AND owner_id = $2`. Never trust an owner ID sent by the client.
  - Returning 404 instead of 403 for another user's record hides whether it exists. Choose one and test it.
  - Sessions in `HttpOnly`, `Secure`, `SameSite` cookies are a different design from JWTs, with different tradeoffs.
  - Hash passwords with a vetted library such as `golang.org/x/crypto` (argon2id or bcrypt), or use an identity provider. **Don't build security primitives yourself.**
  - Cookie-based sessions need CSRF protection.
- **Security basics checklist:** Parameterized SQL, body size limits, server timeouts, secrets only in environment variables, `.env` files in `.gitignore`, no secrets or personal data in logs, and generic 500 messages.

### Exercises

1. **You:** Write `learning/recipebox/API.md` documenting every endpoint, its request and response shapes, status codes, error format, pagination, and update semantics. Keep it in sync from now on.
2. **You:** Add `limit` and cursor-based pagination to `GET /api/recipes`. Test the default limit, the maximum limit, invalid values, a stable order with equal timestamps, and an empty final page.
3. **You:** Add optimistic locking to `PUT`. The client must send `version`. Write a test that simulates two clients updating from the same version and asserts that the second gets 409.
4. **You:** Implement `PATCH /api/recipes/{id}` with pointer fields. Test that "story not sent" leaves the story unchanged while `"story": ""` clears it.
5. **You:** Add an allowlisted `sort` parameter. Try `sort=title;DROP TABLE recipes` and show that it's rejected.
6. **Threat-model exercise:** List five ways a hostile client could misuse your API, such as a huge body, a giant `limit`, an injection attempt, a race between updates, or guessing IDs. For each, point to the code or constraint that stops it, or admit that nothing does yet.
7. **Discuss:** For Ma's Cooking, should deletion be hard or soft, and what about edits? Draft the questions an ADR would need to answer. You don't have to decide yet.

### Checkpoint

- Explain the lost-update problem and how version checks solve it.
- Offset or keyset pagination: what are the tradeoffs, and which would you choose for an infinite-scroll grid?
- Why can't `ORDER BY` take a parameter, and what do you do instead?
- What's the difference between authentication and authorization, and where in your code would ownership be enforced?

**References:** [OWASP API Security Top 10](https://owasp.org/API-Security/), [OWASP Password Storage Cheat Sheet](https://cheatsheetseries.owasp.org/cheatsheets/Password_Storage_Cheat_Sheet.html), [Use The Index, Luke: Paging](https://use-the-index-luke.com/no-offset), [RFC 9110: HTTP semantics, method idempotency](https://www.rfc-editor.org/rfc/rfc9110#name-idempotent-methods)

---

## Stage 14: Testing, debugging, and tooling

**Why it matters:** Tests make change safe, and debugging skill makes failures cheap. This stage pulls together what you've been doing since Stage 2 into a deliberate strategy.

### Concepts

- **A test strategy for this app:**
  - **Unit tests** for pure logic: validation, formatting, and pagination cursors. These are fast and plentiful.
  - **Handler tests** with `httptest`, covering status codes, error bodies, and headers.
  - **Store integration tests** against real PostgreSQL, covering SQL correctness, constraints, transactions, and error mapping.
  - **A smoke test:** a short `curl` script, or a Go test that starts the real server, exercising one full CRUD cycle.
- **What CRUD tests must cover:**
  - The success path of every endpoint.
  - Every documented failure status.
  - Validation edge cases: empty values, exact length limits, multi-byte text, and invalid UTF-8.
  - Not found, conflicts, and the constraint backstop.
- **Test tools:**
  - `t.Helper`, `t.Cleanup`, `t.TempDir`, `t.Setenv`, and `t.Context()`. `t.Context()` is cancelled when the test ends, but before cleanup functions run.
  - A `testdata/` directory for fixtures.
  - Golden files for large expected outputs.
- **Comparing values:** Use `==` for comparable types, `reflect.DeepEqual` or `slices.Equal`/`maps.Equal` for collections, and optionally [`go-cmp`](https://pkg.go.dev/github.com/google/go-cmp/cmp) for readable diffs between structs. Compare times with `time.Time.Equal`, not `==`.
- **The race detector:** `go test -race ./...` belongs in your normal routine.
- **Fuzzing:** `func FuzzValidate(f *testing.F)` generates inputs automatically. Run it with `go test -fuzz=FuzzValidate -fuzztime=30s`. It's very good at finding validation and parsing bugs.
- **Coverage:** `go test -coverprofile=c.out ./...` followed by `go tool cover -html=c.out`. Use coverage like a flashlight to find untested error paths, not as a target number.
- **Static analysis:** `go vet` always. [`staticcheck`](https://staticcheck.dev/) is optional and highly regarded, and `golangci-lint` is optional and heavier.
- **Debugging:**
  - Reproduce the problem, form a hypothesis, check it, and only then change code. **Establish the cause before replacing code.**
  - Read panic stack traces: the first frame in *your* code is usually the place to look.
  - Use [Delve](https://github.com/go-delve/delve) (`dlv test`, `dlv debug`) or the editor's debugger to step through code and set breakpoints.
  - Print values with `%+v` and log at `slog` debug level.
  - Inspect the wire with `curl -v` and the data with `psql`.
- **Benchmarks (optional):** `func BenchmarkX(b *testing.B) { for b.Loop() { ... } }`. Measure before you optimize.

### Exercises

1. **You:** Make a table of endpoints and their documented outcomes, and mark which ones have tests. Fill the gaps.
2. **You:** Ask your pair, or a friend, to introduce a subtle bug such as an off-by-one in pagination or a missing `rows.Err()`. Find it using only tests, logs, and Delve, and explain how you narrowed it down.
3. **You:** Fuzz `ValidateRecipe` and the JSON decoding helper. Fix anything they find, and keep the crashing inputs as regular test cases.
4. **You:** Generate a coverage report. Pick one untested error path and decide, with reasons, whether it deserves a test.
5. **You:** Run `go vet ./...` and `staticcheck ./...`. Understand every finding before you fix it.

### Checkpoint

- Which kind of test would catch each of these: a wrong status code, a typo in a SQL column, a validation rule that's off by one, a data race?
- How do you debug a failing test you don't understand yet?
- What does fuzzing find that table tests don't?

**References:** [Tutorial: Getting started with fuzzing](https://go.dev/doc/tutorial/fuzz), [`go test` flags](https://pkg.go.dev/cmd/go#hdr-Testing_flags), [Delve getting started](https://github.com/go-delve/delve/blob/master/Documentation/cli/getting_started.md), [Go coverage](https://go.dev/doc/build-cover)

---

# Part IV: Capstone

## Stage 15: Recipe Box API capstone

Build, finish, and explain a small but complete CRUD service. The capstone is not a new product spec for Ma's Cooking. It's a proving ground for the skills the milestones need.

### Scope

- **Speakers:** create, list, and get. Deleting a speaker who still has recipes returns 409.
- **Recipes:** full CRUD with a `speaker_id` reference, validation, pagination, optimistic locking, and a revision history written inside a transaction.
- **Health:** `GET /api/health` reports both API and database availability.
- **Non-goals:** authentication (unless you take that stretch goal), media, a frontend, and deployment. These belong to the Ma's Cooking milestones.

### Suggested layout

Start flat and split only when it helps:

```text
learning/recipebox/
  go.mod
  README.md            how to run, test, and migrate
  API.md               the contract
  migrations/
    0001_init.sql
    0002_recipe_revisions.sql
  main.go              config, wiring, server, shutdown
  routes.go            mux and middleware chain
  handlers.go          HTTP layer: decode, validate, call store, respond
  store.go             SQL layer: queries, transactions, error mapping
  recipe.go            types and validation
  errors.go            domain errors and the HTTP error helper
  middleware.go
  *_test.go
```

### Definition of done

**Functionality**

- [ ] Every endpoint in `API.md` works as documented, verified with `curl` and with tests.
- [ ] Lists are paginated, with a stable order and a capped limit.
- [ ] Updates use version checks, and a stale update returns 409.
- [ ] Updating a recipe writes a revision row and the recipe change in one transaction.

**Contract and errors**

- [ ] One JSON error shape everywhere, including the field errors from validation.
- [ ] 400, 404, 409, 413, 422 (or your chosen validation status), 500, and 503 each occur in exactly the documented situations.
- [ ] 500 responses never expose internal errors. The details are logged with a request ID.

**Data**

- [ ] The schema is fully reproducible from the migrations on an empty database.
- [ ] Constraints back up every important validation rule.
- [ ] All SQL is parameterized, and sorting is allowlisted.
- [ ] Stories and other free text are stored exactly as submitted.

**Tests**

- [ ] Unit tests cover validation, including multi-byte text and exact limits.
- [ ] Handler tests cover each endpoint's success and documented failures.
- [ ] Store integration tests run against `recipebox_test` and skip clearly when it isn't configured.
- [ ] `go test -race ./...` and `go vet ./...` pass. At least one fuzz target exists.

**Operations**

- [ ] Configuration comes from the environment, and startup fails fast on invalid values.
- [ ] Logs are structured and every request is logged with its status and duration.
- [ ] Panics are recovered into JSON 500 responses.
- [ ] Server timeouts and graceful shutdown are in place.
- [ ] The README lets a stranger set up the database, run the migrations, start the server, and run the tests.

**Code quality**

- [ ] `gofmt` is clean, names are idiomatic, and there are no globals for dependencies.
- [ ] Every interface has a reason to exist.
- [ ] The commit history consists of small, meaningful steps.

**Understanding, the part that matters most**

- [ ] Without notes, trace `POST /api/recipes` from `curl` through middleware, the handler, validation, the store, SQL, and the response.
- [ ] Explain three design decisions you made and one alternative you rejected for each.
- [ ] Explain one bug you hit, how you found its cause, and how a test now prevents it.

### Capstone review

1. **Self-review** against the definition of done.
2. **Agent review prompt:**

   > Review learning/recipebox against the Stage 15 definition of done in learning/ROADMAP.md. Report gaps by severity, then correctness issues, then idiomatic Go. Don't fix anything; I'll address each item.

3. **Explain-back session:** The agent asks you to walk through a request, a transaction, and an error path. Record what you could explain unaided in [LEARNING](../docs/LEARNING.md).

### Stretch goals

Each of these previews a Ma's Cooking milestone:

- A tiny Vue and TypeScript page that lists and creates recipes through the API, using a Vite proxy (Milestone 0 and 1).
- Session-based authentication with recipe ownership enforced in SQL and tests for unauthorized access (Milestone 2).
- Docker Compose for PostgreSQL so setup is one command.
- [`sqlc`](https://sqlc.dev/) for one query family, compared with handwritten scanning.
- CI that runs tests against a PostgreSQL service container.
- An OpenAPI description generated from, or checked against, `API.md`.

---

## Bridge to Ma's Cooking

| Stages | Practice result | Prepares for |
| --- | --- | --- |
| 0 to 6 | Exercises in `learning/go/` | The Go foundations phase in the [project roadmap](../docs/ROADMAP.md) |
| 7 to 8 | The CLI recipe box | The Go foundations exit criteria: a modest program, failure handling, tests, explanation |
| 9 to 10 | The in-memory HTTP API | Milestone 0: a Go HTTP response and the request path |
| 11 to 12 | The PostgreSQL-backed API | Milestone 0 database connectivity and health check; Milestone 1 recipe round trip with a migration |
| 13 | Validation, contract, and concurrency | Milestone 1 invalid-input handling; Milestone 2 lifecycle and access |
| 14 to 15 | The capstone | Confidence for Milestone 2 onward |

**Choosing a track after the Stage 7 readiness review:**

- **Track A, practice first:** Finish Stages 9 to 15 in `learning/recipebox/`, then build Ma's Cooking. This gives you a safe place to make mistakes, at the cost of building similar things twice.
- **Track B, learn in the product:** Do Stages 9 to 15 directly as Ma's Cooking Milestones 0 to 2, using this document as lesson notes. You make real progress, but each decision carries product weight and may need an ADR.
- **Hybrid (a reasonable default):** Do Stage 9 in practice, since an in-memory HTTP API is cheap and disposable, then decide based on how it went.

Whichever track you choose, reconfirm the collaboration mode in [AGENTS.md](../AGENTS.md) before writing application code, and let the [project roadmap](../docs/ROADMAP.md) and [STATUS](../docs/STATUS.md) guide the product work.

---

## Appendix A: Gotchas cheat sheet

| Gotcha | Symptom | Fix |
| --- | --- | --- |
| `:=` shadows `err` or another variable in an inner block | The outer variable never changes, so an error is silently lost | Use `=` for existing variables. Watch the scopes. |
| Writing to a nil map | `panic: assignment to entry in nil map` | `make` the map, usually in a constructor |
| Slices share a backing array | A change to one slice appears in another | Use `slices.Clone` when you need a separate copy |
| `for _, v := range` gives copies | Changes inside the loop disappear | Index into the slice: `s[i].Field = ...` |
| A nil slice encodes as JSON `null` | The frontend crashes on `null.map` | Initialize with `[]T{}` |
| Unexported struct fields | The field is missing from the JSON | Capitalize it and add a `json` tag |
| A nil pointer inside an interface | `err != nil` even though nothing went wrong | Return a literal `nil` |
| No `return` after writing an error response | `superfluous response.WriteHeader` in logs, mixed bodies | Always `return` immediately |
| Setting headers after `WriteHeader` | The header is missing from the response | Set headers first |
| Forgetting `rows.Close()` or `rows.Err()` | Connections leak, iteration errors are missed | `defer rows.Close()` and check `rows.Err()` |
| `defer` inside a long loop | Resources pile up until the function returns | Move the loop body into a function |
| Copying a struct that contains a mutex | Races or deadlocks; `go vet` reports `copylocks` | Use pointer receivers and pass pointers |
| Map iteration order | Flaky tests | Sort the keys before comparing |
| `string(65)` | Produces `"A"`, not `"65"` | `strconv.Itoa` |
| Forgetting `cancel` from `context.WithTimeout` | Leaked timers; `go vet` reports `lostcancel` | `defer cancel()` |
| Building SQL from user input | SQL injection | Use `$1` placeholders and allowlist identifiers |
| `timestamp` without a time zone | Times shift unexpectedly | Use `timestamptz` and `time.Time` |
| Parallel test packages share one database | Random failures in integration tests | Use `go test -p 1` or a database per package |
| Old tutorials copy loop variables (`i := i`) | Confusing, unnecessary code | Since Go 1.22, each loop iteration gets its own variable |
| Unbuffered send with no receiver | `fatal error: all goroutines are asleep - deadlock!` | Make sure something receives, or use a buffered channel deliberately |

## Appendix B: Idioms and style

- Let `gofmt` decide formatting. Don't argue about it.
- Names use `MixedCaps`. Short names fit short scopes (`r`, `i`, `err`), and descriptive names fit wider ones.
- Acronyms keep a consistent case: `ID`, `URL`, `HTTP`, as in `recipeID` and `ServeHTTP`.
- Receiver names are short and consistent, such as `(b *RecipeBook)`, never `this` or `self`.
- Error strings are lowercase with no trailing punctuation: `fmt.Errorf("load recipe: %w", err)`.
- Doc comments are full sentences that start with the name: `// Store persists recipes in PostgreSQL.`
- Handle errors immediately and keep the success path at the left margin.
- Return concrete types and accept small interfaces.
- Make the zero value useful, or provide `NewX`.
- Use the standard library first. Add a dependency only to solve a concrete problem.
- Keep SQL visible. Prefer clear, slightly repetitive code over clever abstraction.
- Every comment should explain *why* or a constraint the code can't show, not what the next line does.

## Appendix C: Glossary

- **Backing array:** The array a slice points into.
- **Composition root:** The single place, usually `main`, where dependencies are created and wired together.
- **Context:** A value carrying cancellation, deadlines, and request-scoped data through a call chain.
- **Embedding:** Including a type inside a struct without a field name, which promotes its fields and methods.
- **Escape analysis:** The compiler's decision about whether a value lives on the stack or the heap.
- **Goroutine:** A lightweight concurrent function managed by the Go runtime.
- **Handler:** Anything with `ServeHTTP(w, r)`.
- **Idempotent:** Repeating the request has the same effect as doing it once.
- **Integration test:** A test that uses a real dependency, such as PostgreSQL.
- **Keyset pagination:** Paging by "after this sort key" instead of by offset.
- **Migration:** A versioned, ordered schema change.
- **Optimistic locking:** Detecting conflicting writes with a version check instead of holding locks.
- **Rune:** A Unicode code point (`int32`).
- **Sentinel error:** A package-level error value compared with `errors.Is`.
- **SQLSTATE:** PostgreSQL's five-character error code, for example `23505`.
- **Zero value:** The default value every Go type has.

## Appendix D: References

All of these are optional. Use them when a concept needs a second explanation.

**Core Go**

- [A Tour of Go](https://go.dev/tour/): an interactive introduction to the language.
- [Effective Go](https://go.dev/doc/effective_go): idioms and the reasons behind them.
- [Go by Example](https://gobyexample.com/): short, runnable examples.
- [The Go Programming Language Specification](https://go.dev/ref/spec): the precise answer to any language question.
- [Go Code Review Comments](https://go.dev/wiki/CodeReviewComments): common review feedback.
- [Go doc comments](https://go.dev/doc/comment)
- [Standard library](https://pkg.go.dev/std)

**Web and data**

- [`net/http`](https://pkg.go.dev/net/http), [`encoding/json`](https://pkg.go.dev/encoding/json), [`log/slog`](https://pkg.go.dev/log/slog), [`context`](https://pkg.go.dev/context)
- [Accessing relational databases](https://go.dev/doc/database/)
- [`pgx`](https://github.com/jackc/pgx)
- [PostgreSQL documentation](https://www.postgresql.org/docs/current/)
- [MDN HTTP guide](https://developer.mozilla.org/en-US/docs/Web/HTTP)

**Quality and security**

- [Data race detector](https://go.dev/doc/articles/race_detector), [Fuzzing](https://go.dev/doc/security/fuzz/), [Delve](https://github.com/go-delve/delve)
- [OWASP API Security Top 10](https://owasp.org/API-Security/), [OWASP Cheat Sheet Series](https://cheatsheetseries.owasp.org/)
