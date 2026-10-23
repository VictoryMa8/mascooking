# Study roadmap: ASP.NET Core with the lid off

A senior engineer's study plan for a developer whose work stack is **C# and ASP.NET Core with Vue**. It has two goals:

1. **Reinforce the work stack.** About three quarters of study time goes to C#, ASP.NET Core, NPoco, PostgreSQL, and Vue, by building a practice app called **Recipe Box**.
2. **Learn what the framework hides.** About one quarter goes to **barebones Go** with Vue, rebuilding by hand what ASP.NET Core does automatically: routing, model binding, dependency injection, middleware, data mapping, authentication, and shutdown. The Go work is the real product, **Ma's Cooking**.

The method throughout is **learn it with the framework, then build it without one.** You use a concept in ASP.NET Core first, where it's familiar and fast. Then you write the same thing in plain Go and see what was happening underneath.

You can read this plan on your own, or follow it in sessions with an agent using the [pairing protocol](#pairing-with-an-agent).

This is a curriculum, not a project-state document:

- [STATUS](../docs/STATUS.md) owns the current task.
- [LEARNING](../docs/LEARNING.md) owns evidence of what you've actually learned.
- The [project roadmap](../docs/ROADMAP.md) owns the Ma's Cooking milestones.
- [ADR 0001](../docs/decisions/0001-go-api-database-boundary.md) still applies: Ma's Cooking is Vue, Go, and PostgreSQL. ASP.NET Core is for practice only.

## Contents

- [What "well made" means](#what-well-made-means)
- [Splitting your time](#splitting-your-time)
- [How to use this plan](#how-to-use-this-plan)
- [What ASP.NET Core hides, and where Go shows it](#what-aspnet-core-hides-and-where-go-shows-it)
- [C# to Go translation](#c-to-go-translation)
- [Phase plan](#phase-plan)
- [.NET track (about 75%, with Vue)](#net-track)
  - [N1: Modern C# and the .NET toolchain](#n1-modern-c-and-the-net-toolchain)
  - [N2: The ASP.NET Core request path](#n2-the-aspnet-core-request-path)
  - [N3: PostgreSQL for application developers](#n3-postgresql-for-application-developers)
  - [N4: Data access with Npgsql and NPoco](#n4-data-access-with-npgsql-and-npoco)
  - [N5: The framework's machinery](#n5-the-frameworks-machinery)
  - [N6: Making CRUD well made](#n6-making-crud-well-made)
  - [N7: Identity and access](#n7-identity-and-access)
  - [N8: Testing and operations](#n8-testing-and-operations)
  - [N9: Recipe Box capstone](#n9-recipe-box-capstone)
- [Vue track (shared by both backends)](#vue-track)
  - [V1: Vue 3, TypeScript, and Vite](#v1-vue-3-typescript-and-vite) · [V2: Talking to an API](#v2-talking-to-an-api) · [V3: Routing and forms](#v3-routing-and-forms) · [V4: State and structure](#v4-state-and-structure) · [V5: Authentication in the browser](#v5-authentication-in-the-browser) · [V6: Testing and quality](#v6-testing-and-quality)
- [Go track (about 25%, with Vue)](#go-track)
  - Language: [G0](#g0-toolchain-and-workflow) · [G1](#g1-values-types-and-control-flow) · [G2](#g2-functions-and-the-testing-habit) · [G3](#g3-slices-maps-and-strings) · [G4](#g4-structs-methods-and-pointers) · [G5](#g5-errors-as-values) · [G6](#g6-interfaces-packages-and-a-little-generics) · [G7](#g7-files-json-and-a-small-cli) · [G8](#g8-just-enough-concurrency)
  - By hand: [G9: HTTP](#g9-http-by-hand) · [G10: Service plumbing](#g10-service-plumbing-by-hand) · [G11: PostgreSQL](#g11-postgresql-by-hand-with-pgx) · [G12: Quality and access](#g12-quality-and-access-by-hand) · [G13: Testing and debugging](#g13-testing-and-debugging-go)
- Appendices: [Gotchas](#appendix-a-gotchas) · [Go idioms](#appendix-b-go-idioms-for-a-c-developer) · [Glossary](#appendix-c-glossary) · [References](#appendix-d-references)

## What "well made" means

Both backends aim for the same standard. A well-made CRUD application:

1. **Works:** creates, reads, lists, updates, and deletes records through HTTP and JSON, backed by PostgreSQL, with a Vue frontend that handles loading, empty, and error states.
2. **Has a clear contract:** predictable routes, status codes, a consistent JSON error shape, and documented behavior.
3. **Distrusts input:** validates at the boundary, limits request size, uses parameterized SQL, and backs up rules with database constraints.
4. **Fails honestly:** distinguishes "not found", "invalid", "conflict", and "our fault", logs the details internally, and reveals nothing sensitive to clients.
5. **Is tested at the right levels:** unit tests for rules, HTTP-level tests, and integration tests against a real database.
6. **Is safe under concurrency:** has no data races, passes cancellation through to the database, and uses transactions where several writes must succeed or fail together.
7. **Can be operated:** configuration comes from the environment, logs are structured, the server shuts down gracefully, a health check works, and a stranger can run it from the README.
8. **Is conventional for its stack:** idiomatic ASP.NET Core in C#, idiomatic standard-library Go, SQL kept visible, and no abstractions before they're needed.
9. **Is understood:** you can trace a request from the browser to a database row and back, and say which parts the framework did and which parts you did.

The second half of item 9 is the reason the Go track exists.

## Splitting your time

**The ratio.** Plan in cycles of four sessions: **three .NET + Vue sessions, then one Go + Vue session.** If you plan by hours instead, 8 hours a week becomes 6 hours of .NET and 2 of Go.

**The order within a cycle.** The .NET sessions come first. The Go session at the end of the cycle tackles the same idea, or the next Go language topic while the Go track catches up.

**Vue counts toward the backend it's paired with.** Vue work on Recipe Box counts as .NET time, and Vue work on Ma's Cooking counts as Go time.

**Balance over a month, not a day.** If a Go session runs long or a .NET topic takes an extra session, adjust the next cycle. A useful line to keep in [STATUS](../docs/STATUS.md) is `Split, last 8 sessions: 6 .NET / 2 Go`.

**Expect the Go track to lag.** Go needs language foundations (G0 to G8) before it can show you anything about HTTP. Early on, Go sessions teach the language while .NET sessions are already building APIs. From Phase 5 onward, Go sessions rebuild concepts you've been using in ASP.NET for weeks. That lag helps: you'll know exactly what you're replacing.

**Know the tradeoff.** Ma's Cooking, the product, advances only during Go sessions, so its milestones will take about four times as long in calendar time as they would full time. Recipe Box rehearses each slice in ASP.NET first, so each Go session should go faster than starting cold. If product progress starts to matter more, change the ratio deliberately and record it in STATUS.

## How to use this plan

### Pacing

Advance by **checkpoint, not calendar**. A stage is done when you can do its checkpoint tasks with documentation open but without step-by-step help. Going back to an earlier stage is normal and expected.

Your C# and ASP.NET experience means some .NET stages will go quickly. **Skim what you already know, but do the checkpoint anyway.** Daily use of a framework often hides gaps in the fundamentals underneath, and those gaps are exactly what this plan targets.

### Reading it alone

1. Read the stage's "Why it matters" and "Concepts" sections.
2. **Type** every example rather than pasting it, and predict its output before running it.
3. Do the exercises in order. Each one is small on purpose.
4. Answer the checkpoint questions out loud or in writing, without notes.
5. The references are optional. Use them when a concept needs a second explanation.

### Pairing with an agent

[AGENTS.md](../AGENTS.md) defines three collaboration modes: COACH, PAIR, and UNBLOCK. The Go foundations phase uses **PAIR**. **No mode has been agreed for the .NET track yet.** Choose one at the first .NET session and record it in STATUS and AGENTS.md. PAIR mirrors the Go track. COACH suits topics you already know well.

**Session loop (about 45 to 90 minutes):**

1. **Warm-up.** The agent asks you to explain the last concept in two or three sentences.
2. **One concept.** The agent explains a single idea and shows an example of at most about 15 lines. The example uses different data than your exercise. In Go sessions, the agent connects the idea to what ASP.NET does.
3. **Predict.** You say what the code will do before running it.
4. **Change.** You make the exercise change yourself. The agent doesn't edit your exercise files unless you explicitly switch to UNBLOCK.
5. **Run and explain.** You run it and explain why it behaved the way it did.
6. **Review.** The agent reviews your change like a pull request: correctness first, then idiomatic style, then tests. It points out problems and lets you fix them.
7. **Record.** At a stopping point, update STATUS, including the time split, and add honest evidence to LEARNING.

**Hint ladder.** When you're stuck, the agent goes up one rung at a time and stops as soon as you're moving again:

1. A question that points you at the problem.
2. The name of the relevant concept or documentation page.
3. A concrete hint.
4. A partial snippet using different names than your code.
5. A full solution, only in UNBLOCK mode and followed by an explanation.

**Prompts you can paste:**

- Start or resume a stage:

  > We're on stage <N2, V3, or G9> of learning/ROADMAP.md. Read AGENTS.md, STATUS.md, and LEARNING.md, inspect my code, then teach the next concept. Show at most one small example, then stop and let me predict and implement.

- Start a Go session that pulls back the curtain:

  > We're on G<n>. First ask me how ASP.NET Core handles this. Then show what the Go standard library gives us and what we'll have to write ourselves. Stop before I implement.

- Ask for a hint:

  > I'm stuck on <exercise>. Here's what I tried and what happened: <details>. Give me the next rung of the hint ladder, not the answer.

- Ask for a review:

  > Review my change in <file> like a senior engineer: correctness first, then idiomatic <C#/Go/Vue>, then tests. Don't rewrite it; point out issues and let me fix them.

- Debug:

  > My test fails with <output>. Help me establish the cause before changing any code.

- Run a checkpoint:

  > Quiz me on the <stage> checkpoint. One question at a time. Don't give the answer until I've tried.

**Rules for the agent:**

- Teach one concept per step and stop after each example.
- Don't write solutions to the developer's exercises. Example code must use different names and data than the exercise.
- Code written by the agent is not evidence of the developer's skill. Don't record it in LEARNING as if it were.
- In Go sessions, always connect back to what the developer knows from ASP.NET Core.
- When debugging, establish the cause before suggesting a replacement.
- Reconfirm the collaboration mode before moving from Go practice into Ma's Cooking application code.

### Where the code lives

| Location | Use |
| --- | --- |
| `learning/dotnet/` | Recipe Box, created in N1: an ASP.NET Core API project, an NUnit test project, and a Vue app in `learning/dotnet/web/` |
| `learning/go/` | Go language practice for G0 to G9, one folder per topic. Run with `go run ./<topic>` and test with `go test ./...`. |
| Ma's Cooking application | Go and Vue from Milestone 0 onward (G9 to G13). The location is decided at Milestone 0. |

Local tools found on this machine:

- .NET SDK 10, Node 24, Docker, and Go 1.27.
- `psql` isn't installed. Use the one inside the PostgreSQL container (see N3).
- In the agent sandbox, Go may need the `GOCACHE` workaround recorded in [STATUS](../docs/STATUS.md).

## What ASP.NET Core hides, and where Go shows it

This table is the heart of the plan. Every row is something you'll use in ASP.NET first and then build in Go.

| Concept | ASP.NET Core does it for you with | In Go, you write | Learn it in | Rebuild it in |
| --- | --- | --- | --- | --- |
| Server and hosting | `WebApplication`, Kestrel, the generic host | `http.Server` with timeouts and `ListenAndServe` | N2 | G9 |
| Routing | Attribute routes and constraints such as `{id:long}` | `ServeMux` patterns and parsing `r.PathValue("id")` yourself | N2 | G9 |
| Model binding | `[FromBody]`, `[FromRoute]`, `[FromQuery]`, and type conversion | `json.Decoder`, `MaxBytesReader`, `strconv` | N2 | G9 |
| Automatic 400 responses | `[ApiController]` checks `ModelState` and returns `ValidationProblemDetails` | A validator function and an error writer | N2, N6 | G9, G12 |
| Implicit required fields | Non-nullable reference properties are treated as `[Required]` | Explicit checks, plus pointers to detect missing fields | N2 | G7, G12 |
| JSON output | Output formatters, camelCase by default | A `writeJSON` helper and `json` struct tags | N2 | G9 |
| Exceptions to HTTP errors | `UseExceptionHandler`, `IExceptionHandler`, `ProblemDetails` | Errors as values, mapped explicitly; recovery middleware | N5 | G5, G10 |
| Dependency injection | The container, lifetimes, and per-request scopes | Wiring constructors by hand in `main` | N5 | G10 |
| Configuration | Layered `IConfiguration`, options, user secrets | `os.Getenv`, a config struct, validation at startup | N5 | G10 |
| Logging | `ILogger<T>`, providers, message templates, scopes | `log/slog`, passing the logger in | N5 | G10 |
| Middleware pipeline | `app.Use...` and ordering rules | `func(http.Handler) http.Handler` chains | N5 | G10 |
| Graceful shutdown | The host handles `SIGTERM`; `IHostApplicationLifetime` | `signal.NotifyContext` and `srv.Shutdown` | N5 | G10 |
| Health checks | `AddHealthChecks`, `MapHealthChecks` | A handler that pings the pool with a timeout | N5 | G11 |
| Connection pooling | Npgsql pools connections behind each connection string | Creating and closing a `pgxpool.Pool` explicitly | N4 | G11 |
| Row mapping | NPoco maps columns to properties by reflection and generates INSERT and UPDATE statements | Writing the SQL and scanning each column | N4 | G11 |
| Transactions | `db.GetTransaction()`, `Complete()`, rollback when disposed | `Begin`, `defer Rollback`, `Commit` | N4 | G11 |
| Optimistic concurrency | NPoco's `[VersionColumn]` throws `DBConcurrencyException` | `WHERE version = $2` and checking `RowsAffected` | N6 | G12 |
| Cancellation | A `CancellationToken` parameter is injected from `HttpContext.RequestAborted` | Passing `r.Context()` to every call | N2, N4 | G8, G11 |
| Concurrent requests | Kestrel runs requests on the thread pool | Every request gets a goroutine; you protect shared state | N2 | G8, G9 |
| Authentication | Cookie handler, Data Protection, `ClaimsPrincipal` | A sessions table, `crypto/rand` tokens, cookie flags, middleware | N7 | G12 |
| Authorization | `[Authorize]`, policies, `IAuthorizationService` | Ownership checks in SQL and handlers | N7 | G12 |
| HTTP testing | `WebApplicationFactory<Program>` | `httptest.NewRecorder` and `httptest.NewServer` | N8 | G9, G13 |
| API documentation | OpenAPI generated from the code | A hand-written `API.md` or OpenAPI file | N2 | G12 |

**Rule for every Go session:** before writing code, say out loud how ASP.NET Core handles this and what you'll have to write yourself.

## C# to Go translation

| Topic | C# and .NET | Go |
| --- | --- | --- |
| Building and running | `dotnet build` and `dotnet run`, `.csproj` files, solutions | `go build` and `go run`, `go.mod`, no project files |
| Dependencies | NuGet and `PackageReference` | Modules, `go get`, `go.sum` |
| Organization | Namespaces and assemblies | Packages (one per directory) and modules |
| Visibility | `public`, `internal`, `private` | **Capitalized names are exported.** Lowercase names are private to the package. An `internal/` directory restricts imports. |
| Types | Classes (reference types), structs, records | **Structs only.** They're values and are copied on assignment. |
| Inheritance | Yes | **No.** Embedding gives you composition. |
| Interfaces | Declared explicitly (`: IRecipeStore`) | **Satisfied implicitly.** Defined by the code that consumes them. |
| Null | Nullable reference types and `null` | **Zero values.** `nil` exists only for pointers, slices, maps, channels, functions, and interfaces. |
| Properties | `{ get; set; }` | Plain fields. Write methods only when you need behavior. |
| Construction | Constructors, primary constructors, object initializers | Composite literals and `NewX` functions |
| Failures | Exceptions | **Error values** returned alongside results |
| Cleanup | `using` and `IDisposable` | `defer` |
| Asynchrony | `async`/`await`, `Task`, the thread pool | **Goroutines.** Ordinary blocking calls; no function coloring. |
| Cancellation | `CancellationToken` | `context.Context` |
| Locking | `lock`, `ConcurrentDictionary` | `sync.Mutex` around a map |
| Collections | `List<T>`, `Dictionary<K,V>`, LINQ | Slices, maps, `for` loops, the `slices` and `maps` packages |
| Strings | UTF-16; `Length` counts UTF-16 code units | UTF-8 bytes; `len` counts bytes |
| Dependency injection | The built-in container | None. You wire it by hand in `main`. |
| Formatting | `dotnet format` and `.editorconfig` | `gofmt` |
| Testing | NUnit, `[Test]`, `[TestCase]` | The `testing` package and table-driven tests |
| Web | ASP.NET Core | `net/http` |
| Data access | NPoco, EF Core, Dapper | `pgx` or `database/sql` with SQL you write |

## Phase plan

Each phase is a few cycles of four sessions. Go stages trail the .NET concepts until Phase 5, then mirror them.

| Phase | .NET + Vue (about 75%) | Go + Vue (about 25%) | Phase outcome |
| --- | --- | --- | --- |
| 1. Foundations | N1, V1 | G0 to G2 | A Recipe Box solution with tests; Go basics with table-driven tests |
| 2. First API | N2, V2 | G3 to G4 | In-memory Recipe Box CRUD API with a Vue list page; Go collections and structs |
| 3. Data | N3, N4 | G5 to G6 | Recipe Box on PostgreSQL through NPoco; Go errors and interfaces |
| 4. Machinery | N5, V3 | G7 to G8, then the **Go readiness review** | Production-style plumbing in ASP.NET; Vue forms and routing; Go foundations done |
| 5. Well-made CRUD | N6, V4 | G9 to G10: **Ma's Cooking Milestone 0** starts | A well-made Recipe Box API; a Go HTTP API that Vue can call |
| 6. Identity | N7, V5 | G11: Milestone 0 database health, then **Milestone 1** | Authentication in ASP.NET; a Go recipe round trip on PostgreSQL |
| 7. Ship | N8, N9, V6 | G12 to G13: toward **Milestone 2** | The Recipe Box capstone; Go ownership and testing |

---

<a id="net-track"></a>

# .NET track (about 75%, with Vue)

Recipe Box is a practice app with the same domain as Ma's Cooking: speakers, recipes, and stories in their original language. Rehearsing each slice here makes the Go product sessions faster. It isn't the product, so feel free to experiment.

**Assumptions to confirm at the first session:**

- Your work uses **controllers** rather than minimal APIs, so the plan builds with controllers and compares them to minimal APIs once.
- Your work uses **NUnit** for tests and **Vue 3**. This plan follows the conventions Vue 3 recommends: the Composition API with `<script setup lang="ts">`, Vite, Vue Router, and Pinia. Vue 3 also still supports the older Options API (`data()`, `methods`). If your work's components use it, note the differences during V1.

## N1: Modern C# and the .NET toolchain

**Why it matters:** Work code often lags the language. Knowing modern C# well, especially nullable reference types, records, pattern matching, `async`, and LINQ's deferred execution, makes you faster at work, and it gives you a precise baseline to compare Go against.

### Concepts

- **The `dotnet` CLI:**
  - `dotnet new` creates projects and solutions, and `dotnet add reference` and `dotnet add package` connect them.
  - `dotnet build`, `dotnet run`, `dotnet watch`, `dotnet test`, and `dotnet format` cover the daily loop.
  - Look at what a `.csproj` file actually contains and what an SDK-style project infers on its own.
- **Nullable reference types:** Turn on `<Nullable>enable</Nullable>` and `<TreatWarningsAsErrors>true</TreatWarningsAsErrors>`. Understand `?`, `!`, and flow analysis. Note that annotations are compile-time only: `null` can still arrive at runtime from JSON or a database.
- **Records and immutability:** `record` and `record struct`, `init`, `required`, and `with` expressions. Records compare by value.
- **Pattern matching:** Switch expressions, property patterns, and relational patterns (`< 0`, `>= 60`).
- **LINQ:** Deferred versus immediate execution, so know when `ToList()` matters. Also learn when a plain loop is clearer than a LINQ chain.
- **`async`/`await`:** A `Task` represents work in progress. `await` releases the thread instead of blocking it. Avoid `.Result` and `.Wait()`. Pass `CancellationToken` all the way down. Know why `async void` is dangerous.
- **Exceptions:** When to throw, custom exception types, exception filters (`catch (X e) when (...)`), and their cost compared with returning a result.
- **NUnit:**
  - `[Test]` marks a single test. `[TestCase(...)]` runs one method once per row of arguments, which is the C# version of Go's table-driven tests. `[TestCaseSource]` supplies rows that are too complex for attribute arguments.
  - `[SetUp]` and `[TearDown]` run around each test. `[OneTimeSetUp]` and `[OneTimeTearDown]` run once per fixture.
  - NUnit 4 uses the constraint style: `Assert.That(actual, Is.EqualTo(expected))`. NUnit 3's `Assert.AreEqual` moved to `ClassicAssert`, so older work code may look different.
  - **By default, NUnit uses one fixture instance for all of its tests**, so instance fields leak state from one test to the next. Reset them in `[SetUp]`, or use `[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]`.
- **Text:** `string.Length` counts UTF-16 code units. `StringInfo` counts text elements, which approximate what a reader sees as characters. This matters for "title up to 200 characters" in Khmer.

### Example

```csharp
public static class Text
{
    public static bool IsBlank(string? s) => string.IsNullOrWhiteSpace(s);
}

public class TextTests
{
    [TestCase("", true)]
    [TestCase(" \t ", true)]
    [TestCase("សម្ល", false)]
    public void IsBlank_ReturnsExpected(string input, bool expected) =>
        Assert.That(Text.IsBlank(input), Is.EqualTo(expected));
}
```

Predict the output of this snippet:

```csharp
var titles = new List<string> { "Amok", "Kuy teav" };
var shortOnes = titles.Where(t => t.Length < 6);
titles.Add("Bobor");
Console.WriteLine(string.Join(", ", shortOnes));
```

### Exercises

1. **You:** Create `learning/dotnet/` with a solution, an API project (`dotnet new webapi --use-controllers`), and an NUnit project (`dotnet new nunit`) that references it. Read the generated `Program.cs` and `.csproj` line by line, and write down what you think each line does.
2. **You:** Enable nullable reference types with warnings treated as errors. Write a `Recipe` record (`Id`, `Title`, `SpeakerName`, `Language`, `Story`, `CreatedAt`) and fix every warning without using `!`.
3. **You:** Write `CookTime.Format(int minutes)` returning `"1 hr 15 min"`, `"45 min"`, or `"1 hr"`, using a switch expression. Write the `[TestCase]` rows first, including 0, 59, 60, and a negative number. Decide whether a negative input throws or returns a result type, and justify the choice.
4. **You:** Run the LINQ example and explain it. Change one thing so that it prints the list as it was when the query was defined.
5. **You:** Compare `"ខ្មែរ".Length` with `new StringInfo("ខ្មែរ").LengthInTextElements`, predicting both first. Save your answers; you'll compare them with Go in G1.

### Checkpoint

- What does `<Nullable>enable</Nullable>` guarantee, and what doesn't it guarantee?
- When does a LINQ query actually run?
- What does `await` do to the calling thread?
- How does `[TestCase]` relate to a table-driven test?
- Why can an instance field in an NUnit fixture make one test affect another?

**References:** [What's new in C#](https://learn.microsoft.com/dotnet/csharp/whats-new/), [Nullable reference types](https://learn.microsoft.com/dotnet/csharp/nullable-references), [Asynchronous programming](https://learn.microsoft.com/dotnet/csharp/asynchronous-programming/), [LINQ](https://learn.microsoft.com/dotnet/csharp/linq/), [Unit testing with NUnit](https://learn.microsoft.com/dotnet/core/testing/unit-testing-csharp-with-nunit), [NUnit documentation](https://docs.nunit.org/)

---

## N2: The ASP.NET Core request path

**Why it matters:** This is the CRUD-over-HTTP layer you use every day at work. The goal here is to **notice every decision the framework makes for you**, because in G9 you'll make each of them yourself.

### Concepts

- **Startup:** `WebApplication.CreateBuilder` sets up configuration, logging, dependency injection, and Kestrel. `builder.Build()` freezes the services, and `app.Run()` starts the host. Look at `launchSettings.json` for the ports and environment.
- **Routing:** Attribute routing (`[Route("api/recipes")]`, `[HttpGet("{id:long}")]`). What does a route constraint do when the value doesn't match: a 404 or a 400?
- **Controllers and minimal APIs:** `ControllerBase` with `[ApiController]`, compared with `app.MapGet(...)`. Minimal APIs are closer to Go's mux.
- **What `[ApiController]` adds:**
  - It infers where each parameter comes from, so complex types bind from the body.
  - It returns a 400 `ValidationProblemDetails` automatically when `ModelState` is invalid.
  - It turns client error results into `ProblemDetails` bodies.
  - With nullable reference types enabled, **non-nullable properties are implicitly `[Required]`**.
- **Model binding and JSON:** `System.Text.Json` uses camelCase in web defaults. Unknown fields are ignored unless configured otherwise. What happens when a field is missing, null, or the wrong type?
- **Results:** Use `ActionResult<T>`, `Ok`, `NotFound`, `CreatedAtAction` (which sets the `Location` header), `NoContent`, and `Conflict`. Learn the status codes you'll use for CRUD:

  | Operation | Route | Success | Common failures |
  | --- | --- | --- | --- |
  | Create | `POST /api/recipes` | `201 Created` with `Location` and the created body | 400 validation failure |
  | List | `GET /api/recipes` | `200 OK` with an array | 400 bad query parameters |
  | Read | `GET /api/recipes/{id}` | `200 OK` | 404 |
  | Replace | `PUT /api/recipes/{id}` | `200 OK` | 400, 404, 409 (N6) |
  | Delete | `DELETE /api/recipes/{id}` | `204 No Content` | 404, or 204 if idempotent; choose and document |

- **Request and response DTOs:** Keep the API's shapes separate from internal types and database rows.
- **Concurrency:** Kestrel serves requests concurrently on the thread pool. A singleton in-memory store therefore needs `lock` or `ConcurrentDictionary`. This is the same race you'll meet in Go in G8.
- **Cancellation:** Adding a `CancellationToken` parameter to an action binds it to `HttpContext.RequestAborted` automatically.
- **OpenAPI and trying requests:** The template's OpenAPI document, `.http` files in the editor, or `curl -i`.

### Example

This uses a different resource than your exercise:

```csharp
public sealed class CreateSpeakerRequest
{
    [Required, StringLength(100)]
    public string Name { get; init; } = "";
}

[ApiController]
[Route("api/speakers")]
public sealed class SpeakersController(SpeakerBook book) : ControllerBase
{
    [HttpGet("{id:long}")]
    public ActionResult<Speaker> Get(long id) =>
        book.Find(id) is { } speaker ? speaker : NotFound();

    [HttpPost]
    public ActionResult<Speaker> Create(CreateSpeakerRequest request)
    {
        var speaker = book.Add(request.Name);
        return CreatedAtAction(nameof(Get), new { id = speaker.Id }, speaker);
    }
}
```

### Exercises

1. **You:** Build an in-memory `RecipeBook` registered as a singleton, and implement full CRUD for recipes with request and response DTOs. Make it thread-safe and explain which data the lock protects.
2. **You:** For each experiment, write down what you *expect* first, then try it with `curl -i` and record what the framework did:
   - Send malformed JSON.
   - Leave out a required field.
   - Send an unknown field.
   - Send `"title": null`.
   - Use a non-numeric ID.
   - Send a 50 MB body.

   **Keep these notes.** G9 asks you to reproduce or deliberately change each behavior by hand.
3. **You:** Rewrite one endpoint as a minimal API. What did the controller give you that the minimal version doesn't?
4. **You:** Write tests for `RecipeBook` with `[TestCase]`, then write a test that calls `Add` from 100 tasks with `Parallel.ForEachAsync` and checks the count. Remove the lock and watch the test. Is the failure reliable? Compare that with Go's race detector later.
5. **You:** Add a `CancellationToken` to a deliberately slow endpoint, using `Task.Delay(5000, ct)`. Cancel `curl` midway and look at what gets logged.
6. **Vue:** Do V2 against this API.

### Checkpoint

- List four things `[ApiController]` does that you'd otherwise have to write.
- Why does a singleton store need a lock in ASP.NET Core?
- Where does an action's `CancellationToken` come from?
- Without notes: give the success status and two failure statuses for each CRUD operation.

**References:** [Create web APIs with ASP.NET Core](https://learn.microsoft.com/aspnet/core/web-api/), [Routing](https://learn.microsoft.com/aspnet/core/fundamentals/routing), [Model binding](https://learn.microsoft.com/aspnet/core/mvc/models/model-binding), [Model validation](https://learn.microsoft.com/aspnet/core/mvc/models/validation), [OpenAPI overview](https://learn.microsoft.com/aspnet/core/fundamentals/openapi/overview)

---

## N3: PostgreSQL for application developers

**Why it matters:** Both backends use PostgreSQL, so you learn it once, here, in the time-rich track. The database is the source of truth. Constraints are the last line of defense, and migrations make the schema reproducible.

### Concepts

- **Running it locally with Docker:**

  ```sh
  docker run --name recipebox-db -e POSTGRES_PASSWORD=devpassword -p 5432:5432 -d postgres:18
  docker exec -it recipebox-db psql -U postgres
  ```

  Add a named volume if you want the data to survive removing the container; check the image documentation for the mount path. Keep separate **development** and **test** databases. How Ma's Cooking runs PostgreSQL locally is still undecided ([ARCHITECTURE](../docs/ARCHITECTURE.md)).
- **`psql`:** `\l` lists databases, `\c db` connects to one, `\dt` lists tables, `\d recipes` describes a table, `\x` toggles expanded output, and `\q` quits.
- **Coming from SQL Server:**

  | SQL Server | PostgreSQL |
  | --- | --- |
  | `IDENTITY(1,1)` | `bigint GENERATED ALWAYS AS IDENTITY` |
  | `nvarchar(n)` | `text`, with length rules in a `CHECK`; it's always Unicode |
  | `datetime2` and `datetimeoffset` | `timestamptz`, which stores a UTC instant. Use it for every point in time. |
  | `bit` | `boolean` |
  | `TOP n` | `LIMIT n` |
  | `OUTPUT INSERTED.*` | `RETURNING *` |
  | `MERGE` for upserts | `INSERT ... ON CONFLICT` |
  | `@param` | `$1` on the wire. Npgsql and NPoco translate for you, and Go uses `$1` directly. |
  | `[Brackets]` and case-insensitive collation | Unquoted names fold to lowercase, so use `snake_case`. Text comparison is case-sensitive by default. |

- **Constraints:**
  - `NOT NULL`, `CHECK`, `UNIQUE`, `PRIMARY KEY`, and `REFERENCES` with `ON DELETE RESTRICT`, `CASCADE`, or `SET NULL`.
  - `DEFAULT` values.
  - Choose `ON DELETE` behavior by asking what *should* happen to the product data.
- **NULL semantics:** Use `IS NULL`. Prefer `NOT NULL` with a meaningful default.
- **Transactions:** The default isolation level is Read Committed. Know that the lost-update problem exists (N6).
- **Indexes:** Primary keys and `UNIQUE` constraints are indexed automatically. **Foreign key columns are not.** Check with `EXPLAIN ANALYZE`.
- **Migrations:**
  - Numbered SQL files applied in order and recorded in a tracking table.
  - **Never edit a migration that has already been applied anywhere.** Add a new one.
  - NPoco has no migrations of its own. Start with plain SQL files applied through `psql`, then consider a runner such as [DbUp](https://dbup.readthedocs.io/).
  - Plain SQL files also keep the schema portable to the Go side.

### Practice schema

This is practice only, not a decision about the Ma's Cooking schema:

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

1. **You:** Start the container and create `recipebox_dev` and `recipebox_test`. Document the exact commands in `learning/dotnet/README.md`.
2. **You:** Save the schema as `learning/dotnet/migrations/0001_init.sql`, apply it, and inspect it with `\d recipes`.
3. **You:** Insert rows that break each constraint: an empty title, an unknown `speaker_id`, and deleting a speaker who still has recipes. Record each error's SQLSTATE code; you'll map them in both C# and Go.
4. **You:** Write `INSERT ... RETURNING`, a list query ordered by `created_at DESC, id DESC` with `LIMIT 20`, and a join that includes the speaker's name.
5. **You:** In two `psql` sessions, `BEGIN` and update the same recipe in both. Watch the second session wait. Then commit the first and explain what the second sees.
6. **Discuss:** Should deleting a recipe cascade to its future media? What does [PRODUCT](../docs/PRODUCT.md) say deletion has to mean?

### Checkpoint

- Why use constraints if the API already validates?
- Name three PostgreSQL habits that differ from SQL Server.
- What must you never do to an applied migration?
- Which columns get indexes automatically, and which commonly need one added?

**References:** [PostgreSQL tutorial](https://www.postgresql.org/docs/current/tutorial.html), [Constraints](https://www.postgresql.org/docs/current/ddl-constraints.html), [Data types](https://www.postgresql.org/docs/current/datatype.html), [Error codes](https://www.postgresql.org/docs/current/errcodes-appendix.html), [Using EXPLAIN](https://www.postgresql.org/docs/current/using-explain.html)

---

## N4: Data access with Npgsql and NPoco

**Why it matters:** NPoco is what your team uses at work. Understanding exactly what SQL it generates, how it maps rows, and how it manages connections and transactions makes you better at work. It also sets up G11, where you do all of it by hand.

### Concepts

- **The layers:**
  - **Npgsql** is the ADO.NET provider. It owns connections, the wire protocol, type conversion, and **connection pooling**, which is on by default and keyed by connection string.
  - **NPoco** sits on top. It maps rows to objects, generates CRUD SQL, and manages transactions.
- **Creating the database object:** `new Database(connectionString, DatabaseType.PostgreSQL, NpgsqlFactory.Instance)`. A `Database` holds a connection and transaction state, so **register it as scoped**, one per request. Never register it as a singleton.
- **Mapping:**
  - `[TableName]`, `[PrimaryKey]` (auto-increment by default), `[Column]`, `[Ignore]`, `[ResultColumn]`, and `[ComputedColumn]`. The last two are read but never written.
  - Column names match property names case-insensitively, but **underscores don't match**, so `created_at` needs `[Column("created_at")]` or a fluent convention.
- **Querying:**
  - `Fetch<T>`, `SingleOrDefaultById<T>`, `SingleOrDefault<T>`, and `Page<T>` or `SkipTake<T>`.
  - The async versions (`FetchAsync`, `SingleOrDefaultByIdAsync`, and others) accept a `CancellationToken`.
  - Parameters are positional: `"WHERE speaker_id = @0"`. **Never concatenate input into SQL.**
- **Writing:** `Insert` fills in the generated ID, `Update` writes every mapped column, and `Delete` removes the row. Calling `Update` with a column list writes only those columns.
- **Seeing the SQL:** `LastSQL`, `LastArgs`, and `LastCommand` live on the concrete `Database` class, not on `IDatabase`. For logging, subclass `Database` and override `OnExecutingCommand`. **Look at the generated SQL for every operation at least once.**
- **Transactions:** `using var tx = db.GetTransaction(); ... tx.Complete();`. Disposing without `Complete()` rolls back. Async versions exist too.
- **Errors:** `PostgresException` exposes `SqlState`. Compare it with constants such as `PostgresErrorCodes.UniqueViolation` (`23505`) and `ForeignKeyViolation` (`23503`). Map them to domain results in the data layer, not in controllers.
- **Dates and times:** Npgsql maps `timestamptz` to `DateTime` with `Kind = Utc`, and **rejects writing a non-UTC `DateTime`** to it. Let the database default `created_at` and `updated_at`, or use UTC everywhere.
- **Where SQL lives:** Keep it visible in a small store or repository class per aggregate, not scattered through controllers.

### Example

This uses speakers rather than your exercise's recipes:

```csharp
[TableName("speakers")]
[PrimaryKey("id")]
public sealed class SpeakerRow
{
    [Column("id")] public long Id { get; set; }
    [Column("name")] public string Name { get; set; } = "";
    [ComputedColumn("created_at")] public DateTime CreatedAt { get; set; }
}

public sealed class SpeakerStore(IDatabase db)
{
    public Task<SpeakerRow?> FindAsync(long id, CancellationToken ct) =>
        db.SingleOrDefaultByIdAsync<SpeakerRow>(id, ct);

    public Task<List<SpeakerRow>> ListAsync(CancellationToken ct) =>
        db.FetchAsync<SpeakerRow>(new Sql("SELECT id, name, created_at FROM speakers ORDER BY name, id"), ct);
}
```

### Exercises

1. **You:** Register `IDatabase` as scoped and replace the in-memory `RecipeBook` with a `RecipeStore` built on NPoco. Keep the controllers unchanged if you can. Did you need an interface to do that?
2. **You:** Add SQL logging by overriding `OnExecutingCommand`. For `Insert`, `Update`, `Delete`, `SingleOrDefaultById`, and `Page`, paste the generated SQL into your notes. What does `Update` write when you change only the title?
3. **You:** Map `23503` (unknown speaker) and `23505` to domain results, and return 422 or 409 from the API. Write integration tests for both.
4. **You:** Build a `recipe_revisions` table (migration `0002`) and make updating a recipe transactional: copy the old row into revisions, then update. Force an exception between the two steps and prove that nothing was written. This preserves the original content when people edit ([ADR 0002](../docs/decisions/0002-preserve-original-content.md)).
5. **Experiment:** Register `IDatabase` as a singleton, run two concurrent requests that use transactions, and observe what goes wrong. Explain it in terms of shared connection state, then revert.
6. **Experiment:** Try to insert `DateTime.Now` into a `timestamptz` column through a mapped property. Read Npgsql's error and fix it properly.

### Checkpoint

- Which part is Npgsql's job and which is NPoco's?
- Why must `IDatabase` be scoped?
- What SQL does `db.Update(row)` send, and when is a column list better?
- Where does connection pooling happen, and how could you see it?

**References:** [NPoco wiki](https://github.com/schotime/NPoco/wiki), [Npgsql documentation](https://www.npgsql.org/doc/), [Npgsql date and time handling](https://www.npgsql.org/doc/types/datetime.html), [Npgsql connection string parameters](https://www.npgsql.org/doc/connection-string-parameters.html)

---

## N5: The framework's machinery

**Why it matters:** Dependency injection, configuration, logging, middleware, and hosting are where ASP.NET Core does the most for you without you noticing. Learn how each piece actually works, because G10 has you rebuild all of it in about a hundred lines of Go.

### Concepts

- **Dependency injection:**
  - **Lifetimes:** singleton (one for the app), scoped (one per request), and transient (a new one every time).
  - **Captive dependencies:** a singleton that holds onto a scoped service, which then lives far too long. `ValidateScopes` catches this in Development.
  - **Disposal:** the container disposes the services it created.
  - Constructor injection and primary constructors.
  - The container is just a dictionary from type to factory with lifetime rules. Keep that mental model in mind for G10.
- **Configuration:**
  - Layered sources: `appsettings.json`, `appsettings.{Environment}.json`, user secrets, environment variables, and the command line, with later sources winning.
  - `ConnectionStrings__Default` as an environment variable maps to `ConnectionStrings:Default`.
- **The options pattern:** `AddOptions<T>().Bind(...).ValidateDataAnnotations().ValidateOnStart()` makes the app fail fast when configuration is wrong.
- **Logging:**
  - `ILogger<T>` with **message templates**: write `"Created {RecipeId}"`, never an interpolated string. Templates keep the values as structured fields.
  - Log levels, configured per category in `appsettings`.
  - `BeginScope` attaches context to every log line within it.
- **The middleware pipeline:**
  - The order is the behavior. A typical order is exception handler, HTTPS redirection, CORS, authentication, authorization, then endpoints.
  - Write middleware inline with `app.Use(async (context, next) => ...)` or as a class.
  - Know the difference between middleware and filters: middleware sees every request, and filters see controller actions.
- **Errors:** `AddProblemDetails()`, `UseExceptionHandler()`, and `IExceptionHandler` turn unhandled exceptions into a consistent 500 response. Never include exception details in production responses.
- **Health checks:** `AddHealthChecks()` with a database check and `MapHealthChecks("/api/health")`. Return 503 when the database is down.
- **Hosting and shutdown:**
  - The generic host listens for `SIGTERM` and stops accepting new requests, giving in-flight ones time to finish (`HostOptions.ShutdownTimeout`).
  - `IHostApplicationLifetime` exposes hooks for start and stop.
  - `IHostedService` and `BackgroundService` run work alongside requests. A brief look is enough.
- **CORS and the Vue dev server:** A Vite proxy avoids CORS in development. If origins differ in production, configure CORS with explicit origins.

### Example

```csharp
app.Use(async (context, next) =>
{
    var start = Stopwatch.GetTimestamp();
    await next(context);
    app.Logger.LogInformation("{Method} {Path} responded {StatusCode} in {ElapsedMs:0.0} ms",
        context.Request.Method, context.Request.Path, context.Response.StatusCode,
        Stopwatch.GetElapsedTime(start).TotalMilliseconds);
});
```

### Exercises

1. **You:** Write down `Program.cs` as a numbered list of the steps the host performs before your first controller runs. Check it against the documentation.
2. **You:** Create a deliberate captive dependency (a singleton that takes `IDatabase`) and observe the error in Development. Explain the bug it prevents.
3. **You:** Add a `RecipeLimits` options class with `MaxPageSize` and `MaxStoryLength`, validated on start. Break the configuration and watch the app refuse to start.
4. **You:** Add request-logging middleware like the example, plus a request ID that appears in every log line through `BeginScope`. Move the middleware before and after `UseExceptionHandler` and explain the difference.
5. **You:** Add `IExceptionHandler` and `ProblemDetails`, throw from an endpoint, and confirm that the client sees a clean 500 while the logs contain the stack trace.
6. **You:** Add a health check that pings PostgreSQL. Stop the container and watch the endpoint report 503.
7. **You:** Add an endpoint that sleeps 10 seconds, call it, stop the app with Ctrl-C, and observe whether the request finishes. Change `ShutdownTimeout` and repeat.
8. **Vue:** Do V3.

### Checkpoint

- Explain the three lifetimes and a captive dependency.
- In what order do configuration sources apply, and how do you set a nested key from an environment variable?
- Why does middleware order matter? Give one example where the wrong order breaks something.
- What happens to in-flight requests on `SIGTERM`?

**References:** [Dependency injection](https://learn.microsoft.com/aspnet/core/fundamentals/dependency-injection), [Configuration](https://learn.microsoft.com/aspnet/core/fundamentals/configuration/), [Options pattern](https://learn.microsoft.com/aspnet/core/fundamentals/configuration/options), [Logging](https://learn.microsoft.com/aspnet/core/fundamentals/logging/), [Middleware](https://learn.microsoft.com/aspnet/core/fundamentals/middleware/), [Handle errors](https://learn.microsoft.com/aspnet/core/fundamentals/error-handling), [Health checks](https://learn.microsoft.com/aspnet/core/host-and-deploy/health-checks), [Generic host](https://learn.microsoft.com/aspnet/core/fundamentals/host/generic-host)

---

## N6: Making CRUD well made

**Why it matters:** Most CRUD code "works". Well-made CRUD code survives bad input, simultaneous edits, large lists, and malicious users, and it documents what it promises.

### Concepts

- **Three layers of validation:**
  1. Binding checks the shape.
  2. Validation checks the rules. Use data annotations, or a library such as FluentValidation if your work uses one.
  3. Database constraints are the backstop.

  Return **all** field errors at once in `ValidationProblemDetails`. `StringLength` counts UTF-16 code units. Decide whether that's acceptable for Khmer and emoji.
- **Preserve what the user wrote:** Never silently trim or rewrite stories and transcripts ([ADR 0002](../docs/decisions/0002-preserve-original-content.md)).
- **The API contract:** One error shape (`ProblemDetails`), a documented status for every case, camelCase JSON, and an `API.md` you keep current. JavaScript loses precision for integers above 2^53, which is fine for these IDs but worth knowing.
- **Pagination:**
  - NPoco's `Page<T>` is offset-based and runs a count query. Look at its SQL.
  - **Keyset pagination**, written by hand with `WHERE (created_at, id) < (@0, @1)`, is stable and fast for infinite scroll.
  - Always cap the page size and add a unique tiebreaker to `ORDER BY`.
- **Sorting safely:** Allowlist the sort options in a dictionary. Column names can't be parameters.
- **PUT versus PATCH:** `System.Text.Json` can't tell a missing property from `null` on a plain nullable property either. Compare options such as JSON Patch, a wrapper type, or a pointer-like `Optional<T>`. This is the same problem Go solves with pointers (G7).
- **Lost updates:**
  - The problem: two people load version 3 and both save, and the second save silently erases the first.
  - With NPoco, `[VersionColumn("version", VersionColumnType.Number)]` makes `Update` check and increment the version, throwing `DBConcurrencyException` on a mismatch. Map that to 409.
  - Know what SQL it generates. G12 writes it by hand.
- **Cancellation and timeouts:** Pass the `CancellationToken` into every NPoco async call. Consider the request-timeouts middleware.
- **Abuse limits:** Request body size limits and the built-in rate-limiting middleware, in brief.
- **What deletion means:** Hard versus soft delete, and the privacy implications for family content ([PRODUCT](../docs/PRODUCT.md)).

### Exercises

1. **You:** Write `learning/dotnet/API.md` covering every endpoint, its request and response shapes, statuses, errors, pagination, and update semantics.
2. **You:** Implement keyset pagination for `GET /api/recipes` and compare its SQL and behavior with `Page<T>` while inserting rows between page requests.
3. **You:** Add `[VersionColumn]` and require clients to send `version`. Write an integration test with two clients updating from the same version, asserting a 409 for the second.
4. **You:** Implement `PATCH` that distinguishes "story not sent" from `"story": ""`. Document the approach you chose.
5. **You:** Add an allowlisted `sort` parameter and prove that `sort=title;DROP TABLE recipes` is rejected.
6. **Threat model:** List five ways a hostile client could misuse the API, and the code or constraint that stops each one.
7. **Vue:** Do V4, and add 409-conflict handling to the edit form.

### Checkpoint

- Explain lost updates and the exact SQL that version checking uses.
- Offset or keyset pagination: what are the tradeoffs, and which suits an infinite-scroll grid?
- Why can't `ORDER BY` take a parameter?
- How does your API tell "not sent" from "set to empty"?

**References:** [Handle errors in APIs (ProblemDetails)](https://learn.microsoft.com/aspnet/core/fundamentals/error-handling), [Rate limiting middleware](https://learn.microsoft.com/aspnet/core/performance/rate-limit), [NPoco version column support](https://github.com/schotime/NPoco/wiki/Version-column-support), [Use The Index, Luke: Paging](https://use-the-index-luke.com/no-offset), [OWASP API Security Top 10](https://owasp.org/API-Security/)

---

## N7: Identity and access

**Why it matters:** Authentication is where frameworks hide the most, and where hiding it is most dangerous to get wrong. Learn the moving parts in ASP.NET Core so that in G12 you can build sessions in Go with vetted building blocks while understanding every step.

### Concepts

- **Authentication versus authorization:** Authentication establishes who you are. Authorization decides what you may do.
- **Cookie authentication:**
  - `AddAuthentication().AddCookie(...)` issues an encrypted cookie containing a `ClaimsPrincipal`.
  - The cookie is protected by **Data Protection** keys. Where are those stored, and what happens to every logged-in user when the keys are lost on a deploy?
  - Use `HttpOnly`, `Secure`, and `SameSite` flags, sliding expiration, and sign-out.
- **API behavior:** Older versions of cookie authentication redirect unauthenticated API calls to a login page. Check what .NET 10 does for `[ApiController]` endpoints and pin the behavior you want (401 or 403) with a test.
- **Passwords:** ASP.NET Core Identity's `PasswordHasher<TUser>` (PBKDF2), or full Identity, or an external provider. **Never build password hashing yourself.**
- **Authorization:**
  - `[Authorize]`, `[AllowAnonymous]`, and policies.
  - **Resource-based authorization** with `IAuthorizationService`, for rules like "only the owner may edit this recipe".
  - Also enforce ownership in SQL (`WHERE id = @0 AND owner_id = @1`) so that a missed check can't leak data.
  - Returning 404 versus 403 for other people's records is a tradeoff. Choose one and test it.
- **CSRF:** Cookie authentication with a single-page app needs antiforgery protection or a strict `SameSite` design. Know which one you rely on.
- **CORS with credentials:** Explicit origins and `AllowCredentials`, never a wildcard.
- **JWT tradeoffs:** Know why "store a JWT in localStorage" is a different security model from HttpOnly cookies, and why JWTs make revocation harder. Learning JWTs doesn't make them the right choice for Ma's Cooking.

### Exercises

1. **You:** Add a `users` table and `owner_id` on recipes (migration `0003`). Implement register, login, and logout with cookie authentication and `PasswordHasher`.
2. **You:** Protect recipe writes. Write tests for anonymous access (401), another user's recipe (your choice of 403 or 404), and the owner (success).
3. **You:** Decode the cookie in your browser's developer tools. What can you see, and what can't you? Restart the app with ephemeral Data Protection keys and explain why everyone got logged out.
4. **You:** Look for a CSRF hole: can a form on another origin trigger a state change with the user's cookie? Close it and explain the fix.
5. **Vue:** Do V5.

### Checkpoint

- Trace a login: password check, principal, cookie, Data Protection, and the next request.
- Where does ownership get enforced in your API? Name both places.
- Why must Data Protection keys persist across deploys?
- Cookies or JWTs for a single-page app talking to its own API: which would you pick, and why?

**References:** [Authentication overview](https://learn.microsoft.com/aspnet/core/security/authentication/), [Cookie authentication](https://learn.microsoft.com/aspnet/core/security/authentication/cookie), [Resource-based authorization](https://learn.microsoft.com/aspnet/core/security/authorization/resourcebased), [Data Protection](https://learn.microsoft.com/aspnet/core/security/data-protection/introduction), [Antiforgery](https://learn.microsoft.com/aspnet/core/security/anti-request-forgery), [OWASP Password Storage Cheat Sheet](https://cheatsheetseries.owasp.org/cheatsheets/Password_Storage_Cheat_Sheet.html)

---

## N8: Testing and operations

**Why it matters:** Tests make change safe, and operational basics make a service runnable by someone other than you.

### Concepts

- **The test levels:**
  - **Unit tests** with NUnit for validation and formatting.
  - **API tests** with `WebApplicationFactory<Program>`, which runs the real pipeline in memory. In NUnit, create it in `[OneTimeSetUp]` and dispose it in `[OneTimeTearDown]`. If the compiler says `Program` is inaccessible, add `public partial class Program;`.
  - **Integration tests** against real PostgreSQL. Use a test database, or [Testcontainers](https://dotnet.testcontainers.org/) to start a disposable container per test run.
  - Reset data between tests with a `TRUNCATE` helper, or a library such as Respawn.
- **What CRUD tests must cover:** Every documented status code, validation edge cases (empty values, exact limits, multi-byte text), conflicts, authorization, and the constraint backstop.
- **Test doubles:** Replace services in `WebApplicationFactory` with `ConfigureTestServices` only when a real dependency is impractical. Prefer the real database for data code.
- **Coverage and analysis:** `dotnet test --collect:"XPlat Code Coverage"`, analyzers, and `dotnet format --verify-no-changes`. Coverage is a flashlight, not a target.
- **Debugging:**
  - Breakpoints in your editor and conditional breakpoints.
  - Read an exception's stack trace starting from the first frame in your own code.
  - Log NPoco's SQL and inspect the wire with `curl -v`.
  - Reproduce, form a hypothesis, check it, and only then change code.
- **Packaging:** `dotnet publish` and SDK container publishing (`dotnet publish -t:PublishContainer`). Environment-specific configuration through environment variables. The production logging level.

### Exercises

1. **You:** Make a table of endpoints and their documented outcomes, mark which ones have tests, and fill the gaps with `WebApplicationFactory` tests.
2. **You:** Run the integration tests against a disposable PostgreSQL instance and make them independent of order.
3. **You:** Ask your pair to introduce a subtle bug, such as an off-by-one in pagination or a wrong lifetime. Find it with tests, logs, and the debugger, and explain how you narrowed it down.
4. **You:** Publish a container image and run it against the database container using only environment variables for configuration.

### Checkpoint

- Which test level catches each of these: a wrong status code, a typo in SQL, a captive dependency, a missing ownership check?
- Why prefer a real database over mocking `IDatabase` for store tests?

**References:** [Integration tests in ASP.NET Core](https://learn.microsoft.com/aspnet/core/test/integration-tests), [Testcontainers for .NET](https://dotnet.testcontainers.org/), [Containerize an app with dotnet publish](https://learn.microsoft.com/dotnet/core/docker/publish-as-container)

---

## N9: Recipe Box capstone

Finish, harden, and explain Recipe Box: ASP.NET Core, NPoco, PostgreSQL, and Vue.

### Scope

- **Speakers:** create, list, and get. Deleting a speaker who still has recipes returns 409.
- **Recipes:** full CRUD with validation, keyset pagination, optimistic concurrency, revision history written inside a transaction, and ownership.
- **Accounts:** register, log in, and log out with cookie authentication.
- **Health:** an endpoint that reports API and database status.
- **Vue:** list, detail, create, and edit pages with field errors, conflict handling, and loading, empty, and error states.

### Definition of done

**Functionality and contract**

- [ ] Every endpoint in `API.md` works as documented, verified with tests.
- [ ] One `ProblemDetails` error shape everywhere, with field errors from validation.
- [ ] 400, 401, 403 or 404, 409, 500, and 503 each occur in exactly the documented situations.

**Data**

- [ ] The schema can be rebuilt from the migrations on an empty database.
- [ ] Constraints back up every important rule.
- [ ] All SQL is parameterized and sorting is allowlisted.
- [ ] Stories are stored exactly as submitted.
- [ ] You've read the generated SQL for every NPoco operation you use.

**Tests and operations**

- [ ] Unit, API, and integration tests pass and don't depend on order.
- [ ] Options are validated on start and logs are structured with request IDs.
- [ ] Shutdown is graceful.
- [ ] The README lets a stranger set up the database, run the migrations, and run the API, the Vue app, and the tests.

**Understanding**

- [ ] Without notes, trace `POST /api/recipes` from a Vue form submission through middleware, model binding, validation, the controller, NPoco, SQL, and back to the rendered page.
- [ ] For every row of the [hidden machinery table](#what-aspnet-core-hides-and-where-go-shows-it), say what ASP.NET did in your app.

**Review prompt:**

> Review learning/dotnet against the N9 definition of done in learning/ROADMAP.md. Report gaps by severity, then correctness issues, then idiomatic C# and Vue. Don't fix anything; I'll address each item.

---

<a id="vue-track"></a>

# Vue track (shared by both backends)

Vue work happens inside whichever backend's project you're working on. Recipe Box's frontend is at `learning/dotnet/web/`, and Ma's Cooking's frontend is decided at Milestone 0. Because the Go and ASP.NET APIs return different error shapes, you'll learn to keep the frontend's API layer as the single place that knows those details.

## V1: Vue 3, TypeScript, and Vite

- **Concepts:**
  - `npm create vue@latest` with TypeScript.
  - Single-file components and `<script setup lang="ts">`.
  - `ref`, `reactive`, `computed`, and `watch`.
  - Typed `defineProps` and `defineEmits`, and `v-model` on components.
  - `v-for` with stable keys, `v-if` versus `v-show`, and lifecycle hooks.
  - Vue DevTools.
  - These are Vue 3's recommended conventions. If your work's components use the Options API, compare one component written both ways.
- **Exercises:** Build a static `RecipeCard` component and a list that renders sample data. Add a filter box using `computed`. Explain exactly what reacts when you type.
- **Checkpoint:** When do you use `ref` and when `reactive`? Why do list items need stable keys?
- **References:** [Vue guide](https://vuejs.org/guide/introduction.html), [TypeScript with the Composition API](https://vuejs.org/guide/typescript/composition-api.html)

## V2: Talking to an API

- **Concepts:**
  - One typed API module per resource, with DTO types that mirror the API contract.
  - `fetch` with `AbortController`.
  - Loading, empty, error, and success states.
  - Composables such as `useRecipes()`.
  - A Vite dev proxy (`server.proxy`) so the browser sees one origin and CORS doesn't arise in development.
  - `import.meta.env` for configuration.
- **Example:**

  ```ts
  export interface Speaker { id: number; name: string }

  export async function listSpeakers(signal?: AbortSignal): Promise<Speaker[]> {
    const res = await fetch('/api/speakers', { signal })
    if (!res.ok) throw new Error(`list speakers: ${res.status}`)
    return res.json()
  }
  ```

- **Exercises:** Show the Recipe Box recipe list from the API, with all four states. Stop the API and make sure the page says something useful. Abort the request when the component unmounts.
- **Checkpoint:** Why put fetch logic in a module instead of in components? What does the Vite proxy do, and why doesn't production need it?
- **References:** [Composables](https://vuejs.org/guide/reusability/composables.html), [Vite server proxy](https://vite.dev/config/server-options#server-proxy), [MDN: AbortController](https://developer.mozilla.org/en-US/docs/Web/API/AbortController)

## V3: Routing and forms

- **Concepts:**
  - `vue-router` with list, detail, create, and edit routes, and route parameters.
  - Form state and client-side validation for user experience, with **the server as the authority**.
  - Showing server field errors next to their inputs.
  - Disabling submit while saving to prevent double submits.
  - Accessible labels and error messages.
- **Exercises:** Build create and edit forms for recipes. Map `ProblemDetails` field errors onto their inputs. Make sure a keyboard-only user can complete the form.
- **Checkpoint:** Why can't client-side validation replace server-side validation?
- **References:** [Vue Router](https://router.vuejs.org/), [Form input bindings](https://vuejs.org/guide/essentials/forms.html)

## V4: State and structure

- **Concepts:** Local state first, then props and emits, then provide and inject. [Pinia](https://pinia.vuejs.org/) is for genuinely shared state, such as the current user. Organize folders by feature. Avoid a global store for data that only one page uses.
- **Exercises:** Handle 409 conflicts in the edit form by explaining what happened and offering to reload. Add pagination UI. Decide whether anything needs Pinia, and justify the answer.
- **Checkpoint:** What's the cost of putting everything in a global store?

## V5: Authentication in the browser

- **Concepts:**
  - Cookie sessions with `credentials: 'include'` when origins differ.
  - CSRF tokens.
  - Route guards for user experience only, since **the server enforces access**.
  - Handling 401 globally in the API module.
  - Never keeping secrets in frontend code.
- **Exercises:** Build login and logout pages for Recipe Box, a guard on the edit pages, and a redirect on 401. Prove that disabling the guard doesn't let you edit someone else's recipe.
- **Checkpoint:** Why is a route guard not security?

## V6: Testing and quality

- **Concepts:** [Vitest](https://vitest.dev/) with [Vue Test Utils](https://test-utils.vuejs.org/), mocking at the API-module boundary, `vue-tsc` type checking, linting, and optionally Playwright for one end-to-end flow.
- **Exercises:** Test the recipe form: field errors render, submit is disabled while saving, and a conflict message appears on 409.
- **Checkpoint:** Which frontend behaviors are worth testing, and which aren't?

---

<a id="go-track"></a>

# Go track (about 25%, with Vue)

G0 to G8 teach the Go language in `learning/go/`. G9 onward rebuilds the framework's machinery by hand, practicing briefly in `learning/go/` and then building it for real in **Ma's Cooking**. Each "by hand" stage opens with a table of what ASP.NET did for you.

## G0: Toolchain and workflow

> **Current progress:** Partly done. The practice module exists, the program runs, and two tests pass. See [STATUS](../docs/STATUS.md).

**Why it matters:** Every later stage depends on a fast loop of editing, running, and testing.

### Concepts

- **Module:** A versioned unit of code defined by `go.mod`. The `module` line gives the import path, and the `go` line sets the minimum language version. Dependencies are listed in `go.mod` and verified by `go.sum`. It's like a `.csproj` file and `PackageReference`s, but there's no project file listing source files.
- **Package:** One directory is one package. `package main` with `func main()` builds an executable, like an `OutputType` of `Exe`.
- **Core commands:**
  - `go run .` compiles and runs.
  - `go build` produces a binary.
  - `go test ./...` runs every test.
  - `go vet ./...` catches suspicious code.
  - `gofmt -l .` lists unformatted files.
  - `go doc strings.TrimSpace` shows documentation.
  - `go mod tidy` adds missing dependencies and removes unused ones.
- **Strict compiler:** Unused imports and unused local variables are errors, not warnings.

### Exercises

1. **You:** Explain each line of `learning/go/go.mod` and compare it with a `.csproj` file.
2. **You:** Run `go build`, run the binary directly, and compare that with `dotnet publish` output. What does a Go binary need installed on the server, and what does a .NET app need?
3. **You:** Add an unused variable and an unused import, read both errors, and remove them.
4. **You:** Run `gofmt -l .` and `go doc fmt.Printf`.

### Checkpoint

- What's the difference between a module and a package?
- How does `go test` find tests?
- Without notes: create a new `main` package folder, run it, and add a passing test.

**References:** [Tutorial: Get started](https://go.dev/doc/tutorial/getting-started), [How to write Go code](https://go.dev/doc/code)

---

## G1: Values, types, and control flow

### Concepts

- **Declarations:** `var servings int` starts at its zero value. `title := "Kuy teav"` infers the type, like `var` in C#, and works only inside functions. `const maxTitleLen = 200` declares a constant.
- **Zero values instead of null:** `0`, `""`, `false`, and `nil` for pointers, slices, maps, channels, functions, and interfaces. A `string` can never be nil, unlike C#.
- **Explicit conversion:** There are no implicit numeric conversions. `string(65)` is `"A"`. Use `strconv.Itoa` to turn a number into text.
- **Strings are UTF-8 bytes:**
  - `len(s)` counts bytes.
  - `utf8.RuneCountInString(s)` counts code points.
  - `for i, r := range s` walks runes.
  - Compare all of this with C#'s UTF-16 `Length`.
- **Control flow:**
  - `for` is the only loop: the three-part form, a condition, an infinite loop, `range 10`, or `range collection`.
  - `if` can take an initializer: `if n := len(s); n > 200 {}`.
  - `switch` cases don't fall through, and there's no `break` needed.
- **Shadowing:** `:=` in an inner block creates a new variable.

### Example

```go
func main() {
	language := "ខ្មែរ"
	fmt.Println(len(language))                    // predict
	fmt.Println(utf8.RuneCountInString(language)) // predict
	for i, r := range language {
		fmt.Printf("byte %d: %q\n", i, r)
	}
}
```

### Exercises

1. **You:** Run the example and compare it with your C# answers from N1. Explain each of the three different "lengths".
2. **You:** Write `spiceLevel(n int) string` using a `switch` with no condition, with a test.
3. **You:** Reproduce a shadowing bug with `:=` inside an `if` block. Does `go vet` catch it?
4. **Discuss:** "Title up to 200 characters" in C# `StringLength`, in Go, and in PostgreSQL `char_length`. Do all three agree for Khmer?

### Checkpoint

- What are the zero values of `string`, `int`, `[]string`, and `*int`?
- Why does `len("ខ្មែរ")` differ from C#'s `"ខ្មែរ".Length`?

**References:** [A Tour of Go](https://go.dev/tour/basics/1), [Strings, bytes, runes and characters](https://go.dev/blog/strings)

---

## G2: Functions and the testing habit

### Concepts

- **Multiple return values:** `func f() (Recipe, error)`. They replace both `out` parameters and exceptions.
- **Closures and functions as values:** Like C# lambdas and `Func<>`. Middleware in G10 is built from them.
- **`defer`:** Runs when the function returns, in last-in, first-out order, with **arguments evaluated immediately**. It's Go's `using` and `finally`.
- **Tests:**
  - `func TestXxx(t *testing.T)`. `t.Errorf` reports and continues, and `t.Fatalf` reports and stops.
  - **Table-driven tests** with `t.Run` subtests are Go's version of NUnit's `[TestCase]`.
  - `t.Helper()` marks helper functions.
  - `go test -run Name -v`, `-count=1`, and `-cover`.
  - Watch a test fail before trusting it.

### Example

```go
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

1. **You:** Merge the two existing `recipeLabel` tests into one table-driven test with named subtests.
2. **You:** Break `recipeLabel` and judge whether the failure message would be enough to find the problem without opening the code.
3. **You:** Port `CookTime.Format` from N1 to Go as `cookTime(minutes int) string`, writing the table test first. Compare the two versions.
4. **You:** Predict the output of `for i := range 3 { defer fmt.Println(i) }`, then run it.
5. **You:** Write `newCounter() func() int` and test that two counters don't share state.

### Checkpoint

- How does `defer` compare with `using` and `finally`?
- When do you use `t.Fatalf` instead of `t.Errorf`?

**References:** [Tutorial: Add a test](https://go.dev/doc/tutorial/add-a-test), [Defer, panic, and recover](https://go.dev/blog/defer-panic-and-recover)

---

## G3: Slices, maps, and strings

### Concepts

- **Slices:** A small header holding a pointer, a length, and a capacity. **Always write `s = append(s, x)`**, because `append` may allocate a new backing array. Slicing shares memory, unlike LINQ's `Skip` and `Take`, which produce new sequences.
- **Nil versus empty slices:** `var s []T` is nil and `[]T{}` is empty. **JSON encodes nil as `null` and empty as `[]`.**
- **Maps:**
  - `v, ok := m[k]` is Go's `TryGetValue`.
  - Writing to a nil map panics.
  - **Iteration order is randomized on purpose.**
  - Maps aren't safe for concurrent writes.
- **Packages:** `slices` (`Contains`, `Sort`, `SortFunc`, `Index`, `Clone`), `maps`, `strings`, and `strconv`.

### Example: predict before running

```go
a := []string{"rice", "fish", "lime"}
b := a[:2]
b = append(b, "chili")
fmt.Println(a)

var counts map[string]int
fmt.Println(counts["khmer"])
counts["khmer"]++
```

### Exercises

1. **You:** Run the example and explain each result in terms of length, capacity, and the backing array. Which C# type behaves most like a slice?
2. **You:** Write `countByLanguage([]string) map[string]int` and test it with `maps.Equal`.
3. **You:** Write `normalizeTags`, which trims, lowercases, removes duplicates, and sorts, without changing its input. Prove that in a test. Why would doing the same to a cook's *story* violate [ADR 0002](../docs/decisions/0002-preserve-original-content.md)?
4. **You:** Build a tiny `map[int]string` store with add, get, remove, and sorted listing.

### Checkpoint

- Why `s = append(s, x)`?
- What's the difference between a nil slice and an empty one, and when does it matter?

**References:** [Go slices: usage and internals](https://go.dev/blog/slices-intro), [Go maps in action](https://go.dev/blog/maps)

---

## G4: Structs, methods, and pointers

### Concepts

- **Structs are values:** Assignment copies them, like a C# `struct`, not a `class`. There are no properties, only fields.
- **Methods:** `func (r Recipe) Label() string`. A **pointer receiver** (`func (b *Book) Add(...)`) is needed to mutate the receiver, for large structs, or for structs containing a mutex. Keep the choice consistent within a type.
- **Pointers:** `&x` and `*p`, with no pointer arithmetic. Returning `&local` is safe because escape analysis moves it to the heap.
- **No constructors:** Write a `NewRecipeBook()` function when the zero value isn't usable.
- **Exported versus unexported:** Capitalization replaces `public` and `private`, and it applies per package.
- **Embedding:** Composition with promoted methods, not inheritance. You'll use it in G10 to wrap `http.ResponseWriter`.

### Example

```go
type Speaker struct {
	Name     string
	Relation string
}

func (s Speaker) Describe() string { return s.Name + " (" + s.Relation + ")" }
func (s *Speaker) Rename(name string) { s.Name = name }

func main() {
	speakers := []Speaker{{Name: "Ma", Relation: "mother"}}
	for _, s := range speakers {
		s.Rename("Changed")
	}
	fmt.Println(speakers) // predict
}
```

### Exercises

1. **You:** Explain the example's output and fix the loop. What would happen with a C# `class` instead, and with a C# `struct`?
2. **You:** Define a `Recipe` struct with a `Label()` method that replaces `recipeLabel`.
3. **You:** Build an in-memory `RecipeBook` with `Add`, `Get`, `List`, `Update`, and `Delete`, plus tests. It mirrors the N2 `RecipeBook` on purpose.
4. **You:** Change `Add` to a value receiver and watch the tests fail.

### Checkpoint

- When do you use a pointer receiver?
- How do Go structs differ from C# classes and from C# structs?

**References:** [A Tour of Go: Methods](https://go.dev/tour/methods/1), [Go FAQ: Methods on values or pointers](https://go.dev/doc/faq#methods_on_values_or_pointers)

---

## G5: Errors as values

**What ASP.NET hid:** Exceptions travel up the stack until the exception handler middleware catches them. In Go, **every failure is a value you can see at each step**, and you decide its HTTP meaning explicitly.

### Concepts

- **The `error` interface:** Return it last; `nil` means success. Check it with `if err != nil { return ..., fmt.Errorf("context: %w", err) }`.
- **Sentinel errors:** Values such as `var ErrNotFound = errors.New("not found")`, checked with `errors.Is`. This is the equivalent of catching a specific exception type, without unwinding the stack.
- **Custom error types:** Structs carrying data, extracted with `errors.As`. This is like an exception filter, `catch (ValidationException e)`.
- **Handle each error once:** Either log and respond, or wrap and return. Never both.
- **`panic` is for bugs only:** `recover` belongs at boundaries, like the exception handler middleware.
- **The nil interface trap:** A nil `*ValidationError` stored in an `error` is not nil.

### Example

```go
var ErrNotFound = errors.New("not found")

type ValidationError struct {
	Fields map[string]string
}

func (e *ValidationError) Error() string {
	return fmt.Sprintf("invalid input: %d field(s)", len(e.Fields))
}

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

### Exercises

1. **You:** Make `RecipeBook.Get`, `Update`, and `Delete` return a wrapped `ErrNotFound`, and test them with `errors.Is`.
2. **You:** Write `ValidateRecipe(r Recipe) error`, returning all field problems at once and never trimming the story. Compare it with your data annotations from N2. What did `[Required]` and `[StringLength]` do that you now have to write yourself?
3. **You:** Reproduce the nil interface trap in a test, then fix it.

### Checkpoint

- When do you use `errors.Is` and when `errors.As`? What are the C# equivalents?
- What do you gain and lose compared with exceptions?

**References:** [Error handling and Go](https://go.dev/blog/error-handling-and-go), [Working with errors in Go 1.13](https://go.dev/blog/go1.13-errors)

---

## G6: Interfaces, packages, and a little generics

**What ASP.NET hid:** C# culture often puts an interface in front of every service so the container can inject it. Go interfaces are **implicit and defined by the code that consumes them**. You add one when you need it, not because a container expects it.

### Concepts

- **Implicit satisfaction:** A type that has the methods satisfies the interface, with no `: IFoo`. The most useful interfaces are tiny: `io.Reader`, `io.Writer`, `fmt.Stringer`, `error`, and `http.Handler`.
- **"Accept interfaces, return structs":** The consumer declares the small interface it needs, and the provider returns a concrete type.
- **Compile-time check:** `var _ Getter = (*RecipeBook)(nil)` fails to compile if `RecipeBook` stops satisfying `Getter`.
- **Packages:**
  - Short lowercase names, and no stutter: write `recipes.Store`, not `recipes.RecipeStore`.
  - An `internal/` directory can't be imported from outside its parent, a little like `internal` in C#.
  - **Import cycles are compile errors**, so plan the direction of dependencies.
- **Generics:** `func Filter[T any](...)`. They're useful for algorithms and rarely for business logic. There's no "generic repository".

### Exercises

1. **You:** Implement `String()` on `Recipe`, then write `printAll(w io.Writer, recipes []Recipe)` and test it with a `bytes.Buffer`.
2. **You:** Move `RecipeBook` into a `learning/go/recipebook/` package and fix what breaks. What did capitalization have to do with it?
3. **You:** Write a consumer-side `recipeGetter` interface and a fake for a test. Then **discuss**: was it worth it here? How would your team decide this in C#?

### Checkpoint

- How does a type satisfy an interface in Go, compared with C#?
- When does an interface become premature abstraction?

**References:** [Effective Go: Interfaces](https://go.dev/doc/effective_go#interfaces), [Go Code Review Comments: Interfaces](https://go.dev/wiki/CodeReviewComments#interfaces), [When to use generics](https://go.dev/blog/when-generics)

---

## G7: Files, JSON, and a small CLI

**What ASP.NET hid:** `System.Text.Json` with web defaults gives you camelCase, case-insensitive matching, and model binding. In Go, **struct tags control names**, and you can see exactly how missing fields behave.

### Concepts

- **Files:** `os.ReadFile` and `os.WriteFile`, and `errors.Is(err, fs.ErrNotExist)` for a missing file. `t.TempDir()` gives each test a clean directory.
- **`encoding/json`:**
  - Tags such as `` `json:"createdAt"` `` rename fields, `omitempty` skips zero values, and `-` skips a field entirely. Only exported fields are encoded.
  - Go matches JSON keys to fields case-insensitively when decoding, but it **encodes with the exact field name unless you add a tag**.
  - **Missing fields become zero values.** Use pointer fields (`*string`) to detect "not sent", which is the Go answer to the PATCH problem in N6.
  - `Decoder.DisallowUnknownFields()` rejects fields you don't recognize.
- **Command-line basics:** `os.Args`, `flag.NewFlagSet` for each subcommand, errors written to `os.Stderr`, and `os.Exit(1)` on failure.

### Exercises

1. **You:** Marshal a `Recipe` with and without tags. Compare the output with what ASP.NET returned in N2.
2. **You:** Unmarshal `{}` into a struct with pointer fields and into one without. Explain the difference in terms of PATCH.
3. **Foundations exit project:** Build `learning/go/recipebox/`, a command-line tool for `add`, `list`, `show`, `edit`, and `delete` that saves to a JSON file. Reuse `RecipeBook`, validation, and errors from earlier stages, and write tests for everything except `main`.

### Checkpoint

- How do you tell a missing JSON field from an empty one in Go? And in C#?
- Why keep the shapes on the wire separate from internal types?

**References:** [JSON and Go](https://go.dev/blog/json), [`encoding/json`](https://pkg.go.dev/encoding/json), [`flag`](https://pkg.go.dev/flag)

---

## G8: Just enough concurrency

**What ASP.NET hid:** Kestrel schedules requests onto the thread pool, `async`/`await` keeps threads free, and `CancellationToken` arrives already bound to the request. In Go, **every request is a goroutine**, calls simply block, and you pass `context.Context` yourself.

### Concepts

- **Goroutines:** `go f()`. They're cheap, and blocking inside them is normal. Go has no `async` keyword and no function coloring, so a function that waits looks the same as one that doesn't.
- **Data races:** `sync.Mutex` around a map is Go's `lock` statement. `go test -race` detects races **reliably when they happen**, unlike flaky C# tests.
- **`sync.WaitGroup`:** `wg.Go(func() { ... })` starts a goroutine and tracks it, similar to `Task.WhenAll`.
- **Channels:** Learn enough to read code. Unbuffered channels hand off one value at a time, buffered channels have capacity, and `select` waits on several. They're similar to `System.Threading.Channels`.
- **`context.Context`:** Go's `CancellationToken` plus deadlines. It's the first parameter by convention, never stored in a struct. It starts from `r.Context()`. Always `defer cancel()` after `context.WithTimeout`.

### Example

```go
func waitForOven(ctx context.Context, d time.Duration) error {
	select {
	case <-time.After(d):
		return nil
	case <-ctx.Done():
		return ctx.Err()
	}
}
```

### Exercises

1. **You:** Call `RecipeBook.Add` from 100 goroutines in a test. Run it with `-race`, read the report, fix it with a mutex, and compare the experience with the C# test from N2.
2. **You:** Test `waitForOven` with a timeout that fires before the oven finishes and one that doesn't.
3. **Discuss:** Map `context.Context` onto `CancellationToken` and `CancellationTokenSource`. What does Go's version carry that C#'s doesn't?

### Checkpoint

- Why does an in-memory store in an HTTP server need a mutex in both stacks?
- What does the race detector catch, and what can it miss?

> **Go readiness review.** Check the Go foundations exit criteria in the [project roadmap](../docs/ROADMAP.md): implement a modest program with occasional hints, handle a failure, write useful tests, and explain data flow and design choices. The G7 command-line tool is the evidence. Record the result in LEARNING and **reconfirm the collaboration mode** before starting Ma's Cooking application code.

**References:** [Data race detector](https://go.dev/doc/articles/race_detector), [Go concurrency patterns: Context](https://go.dev/blog/context), [`sync`](https://pkg.go.dev/sync)

---

## G9: HTTP by hand

**Ma's Cooking target:** Milestone 0, a Go HTTP response and `GET /api/health`, then Vue calling it.

| ASP.NET Core did | You now write |
| --- | --- |
| Kestrel and the host | `http.Server` (timeouts come in G10) |
| Attribute routing and constraints | `mux.HandleFunc("GET /api/recipes/{id}", ...)`, then `strconv.ParseInt(r.PathValue("id"), 10, 64)` |
| `[FromBody]` binding and size limits | `http.MaxBytesReader`, `json.NewDecoder`, `DisallowUnknownFields` |
| `[ApiController]` automatic 400 responses | Calling your validator and writing the error yourself |
| `ProblemDetails` | Your own JSON error shape, or [RFC 9457](https://www.rfc-editor.org/rfc/rfc9457) |
| `CreatedAtAction`, `NoContent` | Setting `Location`, calling `WriteHeader(201)` or `204` |
| Output formatters | A `writeJSON` helper |
| `WebApplicationFactory` | `httptest.NewRecorder` sending requests through the mux |

### Concepts

- **`http.Handler`:** An interface with one method, `ServeHTTP(w, r)`. `http.HandlerFunc` adapts an ordinary function into one. Middleware, muxes, and endpoints are all handlers.
- **`ServeMux`:** Patterns combine a method and a path. A wrong method automatically gets `405 Method Not Allowed`.
- **Writing a response:** Set headers, then call `WriteHeader`, then write the body. **`return` right after writing an error.**
- **Reading JSON safely:** Limit the size, reject unknown fields, and reject trailing data.
- **Lists:** Return `[]`, never `null`.
- **Errors to clients:** Log the details and send a generic 500.

### Example

```go
type application struct {
	logger  *slog.Logger
	recipes *RecipeBook
}

func (app *application) routes() http.Handler {
	mux := http.NewServeMux()
	mux.HandleFunc("GET /api/health", app.health)
	mux.HandleFunc("GET /api/recipes/{id}", app.getRecipe)
	return mux
}

func writeJSON(w http.ResponseWriter, status int, v any) error {
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(status)
	return json.NewEncoder(w).Encode(v)
}
```

### Exercises

1. **Practice in `learning/go/httpbasics/`:**
   - Serve `/api/health` and inspect it with `curl -i`.
   - Implement in-memory `GET` and `POST` for recipes using `RecipeBook`.
   - Write a `decodeJSON` helper.
   - Write `httptest` tests for 200, 201, 400, 404, and 405.
2. **Replay your N2 notes:** For each experiment (malformed JSON, a missing field, an unknown field, `null`, a bad ID, a huge body), make Go behave the way you *want*, which might differ from ASP.NET's defaults. Write down each decision.
3. **Remove a `return`** after an error response and read the `superfluous response.WriteHeader call` log message.
4. **Ma's Cooking:** With the collaboration mode reconfirmed, start Milestone 0: a Go `GET /api/health` and a Vue page that displays its result, using V2 skills.

### Checkpoint

- What is `http.Handler`, and why can middleware and muxes be handlers too?
- List everything `[ApiController]` did for one `POST` that you wrote by hand.

**References:** [`net/http`](https://pkg.go.dev/net/http), [Routing enhancements for Go 1.22](https://go.dev/blog/routing-enhancements), [`httptest`](https://pkg.go.dev/net/http/httptest)

---

## G10: Service plumbing by hand

**Ma's Cooking target:** Milestone 0, with documented configuration, logging, and a clean startup and shutdown.

| ASP.NET Core did | You now write |
| --- | --- |
| The DI container and lifetimes | `main` constructs every dependency once and passes it into structs. "Scoped" becomes "created inside the handler" or "carried in the request context". |
| `IConfiguration` and validated options | `loadConfig(getenv func(string) string) (config, error)` that fails fast |
| `ILogger<T>` | A `*slog.Logger` passed in, with text output locally and JSON in production |
| `app.Use(...)` | `func(next http.Handler) http.Handler` |
| `UseExceptionHandler` | Recovery middleware that returns a JSON 500 |
| The host's `SIGTERM` handling | `signal.NotifyContext` and `srv.Shutdown(ctx)` |
| Kestrel's limits | `ReadHeaderTimeout`, `ReadTimeout`, `WriteTimeout`, and `IdleTimeout` on `http.Server` |

### Concepts

- **`main` as the composition root:** Load configuration, build the logger, open dependencies, build the `application`, serve, wait for a signal, and shut down. Keep `main` tiny and put the work in `run(ctx, getenv) error` so it can be tested.
- **Binding:** Hosts such as Render set `PORT` and expect the app on `0.0.0.0:$PORT`.
- **Capturing the status code:** Wrap `http.ResponseWriter` by embedding it (G4) to record the status for logging.
- **Layout:** Start flat, with `main.go`, `routes.go`, `handlers.go`, `store.go`, `errors.go`, and `middleware.go`. Split into packages when the flat layout hurts.

### Example

```go
type statusRecorder struct {
	http.ResponseWriter
	status int
}

func (r *statusRecorder) WriteHeader(code int) {
	r.status = code
	r.ResponseWriter.WriteHeader(code)
}

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
	return err
case <-ctx.Done():
}

shutdownCtx, cancel := context.WithTimeout(context.Background(), 10*time.Second)
defer cancel()
return srv.Shutdown(shutdownCtx)
```

### Exercises

1. **You:** Write `loadConfig` with tests that pass a fake `getenv`. Compare it with the N5 options validation.
2. **You:** Write logging middleware with `statusRecorder`. What should `status` start as, and why? Add a request ID.
3. **You:** Write recovery middleware and compare its behavior with N5's `IExceptionHandler`.
4. **You:** Implement graceful shutdown and repeat the N5 sleep experiment.
5. **Ma's Cooking:** Apply all of this to the Milestone 0 service and document the run commands in the README.
6. **Reflect:** Your Go `main` now does what `WebApplication.CreateBuilder` did. Count the lines. What did you deliberately leave out?

### Checkpoint

- Walk through `main` and say what each step is for and what the ASP.NET equivalent was.
- How do "scoped" services translate to Go?

**References:** [Structured logging with slog](https://go.dev/blog/slog), [`http.Server`](https://pkg.go.dev/net/http#Server), [`os/signal`](https://pkg.go.dev/os/signal)

---

## G11: PostgreSQL by hand with pgx

**Ma's Cooking target:** Milestone 0's database-aware health check, then **Milestone 1**, one recipe round trip with a migration.

| NPoco and Npgsql did | You now write |
| --- | --- |
| A pool behind each connection string | One `pgxpool.Pool` created at startup, pinged, shared, and closed on shutdown |
| Reflection mapping and `[Column]` attributes | `QueryRow(...).Scan(&r.ID, &r.Title, ...)`, one destination per column |
| Generated `INSERT` and `UPDATE` statements | The SQL itself, with `RETURNING` |
| `@0` parameters | `$1` parameters, passed straight through |
| `SingleOrDefaultById` returning `null` | `errors.Is(err, pgx.ErrNoRows)` mapped to `ErrNotFound` |
| `GetTransaction()` and `Complete()` | `tx, _ := pool.Begin(ctx)`, `defer tx.Rollback(ctx)`, `tx.Commit(ctx)` |
| `PostgresException.SqlState` | `var pgErr *pgconn.PgError; errors.As(err, &pgErr); pgErr.Code` |
| The `CancellationToken` overloads | `ctx` as the first argument of every call |
| `AddHealthChecks` with a database check | A handler that calls `pool.Ping` with a one-second timeout and returns 503 |

### Concepts

- **The driver:** [`pgx`](https://pkg.go.dev/github.com/jackc/pgx/v5) is PostgreSQL-specific and comes with `pgxpool`. The project leans toward it in [ARCHITECTURE](../docs/ARCHITECTURE.md). It's probably your first Go dependency, so look at what changes in `go.mod` and `go.sum`.
- **Three query shapes:**
  - `QueryRow().Scan()` for one row.
  - `Query()` followed by `for rows.Next()`, then `rows.Err()`, with `defer rows.Close()`.
  - `Exec()` with `RowsAffected()` for writes.
- **The layer rule:** The store knows SQL and the handler knows HTTP. Domain errors (`ErrNotFound`, `ErrConflict`) cross the boundary between them.
- **Nullable columns:** They need pointers or `pgtype` types, which is one more reason to prefer `NOT NULL`.
- **Integration tests:**
  - Use `TEST_DATABASE_URL` and call `t.Skip` when it isn't set.
  - Reset the tables between tests.
  - Packages run their tests in parallel, so use `go test -p 1` if several packages share one database.
- **Migrations:** Reuse the plain SQL approach from N3. Choose a migration tool only for a concrete need, and record the choice in an ADR if it lasts.

### Example

```go
func (s *Store) GetRecipe(ctx context.Context, id int64) (Recipe, error) {
	const q = `
		SELECT id, title, language, story, created_at
		FROM recipes
		WHERE id = $1`

	var r Recipe
	err := s.pool.QueryRow(ctx, q, id).Scan(&r.ID, &r.Title, &r.Language, &r.Story, &r.CreatedAt)
	if errors.Is(err, pgx.ErrNoRows) {
		return Recipe{}, ErrNotFound
	}
	if err != nil {
		return Recipe{}, fmt.Errorf("get recipe %d: %w", id, err)
	}
	return r, nil
}
```

### Exercises

1. **You:** Connect and ping, then break the password, the port, and the database name in turn and read each error. Which parts of the URL must never appear in logs?
2. **Ma's Cooking:** Make `GET /api/health` report database status and return 503 when PostgreSQL is stopped. Show it in the Vue page. This completes Milestone 0.
3. **Ma's Cooking, Milestone 1:** Write the first migration, implement create and read through Go, validate input, display the recipe in Vue, and write an integration test for valid persistence and invalid input.
4. **Compare:** Put your NPoco SQL notes from N4 next to your hand-written SQL. Where was NPoco doing more than you expected, and where less?
5. **Experiment:** Build a query by string concatenation in a scratch function, inject `1 OR 1=1`, fix it with a parameter, and delete the scratch function.

### Checkpoint

- Trace `GET /api/recipes/7` through the handler, store, SQL, `Scan`, error mapping, and JSON, and compare each step with the NPoco version.
- Why is `defer tx.Rollback(ctx)` safe after a commit? What is NPoco's equivalent?

**References:** [Accessing relational databases](https://go.dev/doc/database/), [`pgx` v5](https://pkg.go.dev/github.com/jackc/pgx/v5), [`pgxpool`](https://pkg.go.dev/github.com/jackc/pgx/v5/pgxpool), [`pgconn.PgError`](https://pkg.go.dev/github.com/jackc/pgx/v5/pgconn#PgError)

---

## G12: Quality and access by hand

**Ma's Cooking target:** Milestone 1's invalid-input handling, then **Milestone 2**, recipe lifecycle and access.

| ASP.NET Core and NPoco did | You now write |
| --- | --- |
| `[VersionColumn]` and `DBConcurrencyException` | `UPDATE ... SET version = version + 1 WHERE id = $1 AND version = $2`, then telling 404 from 409 when no rows changed |
| `Page<T>` | Keyset pagination SQL and a capped `limit` |
| Cookie authentication and Data Protection | A `sessions` table, a 32-byte `crypto/rand` token sent as an `HttpOnly`, `Secure`, `SameSite` cookie, and the token **hashed** in the database |
| `ClaimsPrincipal` and `HttpContext.User` | Middleware that loads the user and stores it in the request context |
| `[Authorize]` and resource-based authorization | A `requireUser` middleware and `WHERE owner_id = $2` in every write |
| `PasswordHasher<TUser>` | `golang.org/x/crypto` (argon2id or bcrypt). **Still never hand-rolled.** |
| Antiforgery | A deliberate CSRF strategy: a `SameSite` policy plus a token or origin check |
| OpenAPI generation | Keeping `API.md` up to date by hand |

### Concepts

Everything from N6 and N7 applies. The difference is that you now see the SQL, the cookie flags, and the context plumbing directly. **Building authentication by hand is for understanding.** Use vetted primitives for hashing and randomness, and record the authentication approach in an ADR before Ma's Cooking stores real accounts ([ARCHITECTURE](../docs/ARCHITECTURE.md)).

### Exercises

1. **Practice:** In `learning/go/`, add optimistic locking to the practice store with a test that simulates two clients.
2. **Practice:** Build a minimal session login against a test table. Use a random token, store only its hash, and send it in a cookie with the right flags. Write tests for no cookie, an expired session, and another user's record.
3. **Ma's Cooking, Milestone 2:** Only after the authentication approach and the identity and visibility decisions are recorded as the milestone requires, implement ownership and unauthorized-access tests.
4. **Compare:** Write a short note on which authentication responsibilities ASP.NET handled for you that you had to think about in Go.

### Checkpoint

- Why store a hash of the session token rather than the token itself?
- Where is ownership enforced in your Go API? Name both places.

**References:** [`crypto/rand`](https://pkg.go.dev/crypto/rand), [`golang.org/x/crypto/argon2`](https://pkg.go.dev/golang.org/x/crypto/argon2), [OWASP Session Management Cheat Sheet](https://cheatsheetseries.owasp.org/cheatsheets/Session_Management_Cheat_Sheet.html)

---

## G13: Testing and debugging Go

### Concepts

- **The test levels:** unit tests, handler tests with `httptest`, store integration tests against PostgreSQL, and one smoke test of a full CRUD cycle.
- **Test helpers:** `t.Helper`, `t.Cleanup`, `t.TempDir`, `t.Setenv`, `t.Context()`, and a `testdata/` directory.
- **Fuzzing:** `func FuzzValidate(f *testing.F)`, run with `go test -fuzz=FuzzValidate -fuzztime=30s`. C# has no built-in equivalent.
- **Race detection:** `go test -race ./...` as part of your normal routine.
- **Coverage:** `go test -coverprofile=c.out ./...` and `go tool cover -html=c.out`.
- **Static analysis:** `go vet`, plus optionally [`staticcheck`](https://staticcheck.dev/).
- **Debugging:** [Delve](https://github.com/go-delve/delve) (`dlv test`, `dlv debug`) or the editor's debugger. Read goroutine stack traces. Reproduce, hypothesize, and check before changing code.

### Exercises

1. **You:** Fuzz `ValidateRecipe` and the JSON decoding helper, and keep any crashing inputs as regular tests.
2. **You:** Have your pair plant a bug in the Ma's Cooking service, then find it with tests, logs, and Delve.
3. **Reflect:** Compare the testing experience in NUnit with `WebApplicationFactory` and in Go with `httptest`. Which was faster to write, faster to run, and clearer when it failed?

**References:** [Tutorial: Getting started with fuzzing](https://go.dev/doc/tutorial/fuzz), [Delve](https://github.com/go-delve/delve/blob/master/Documentation/cli/getting_started.md), [Go coverage](https://go.dev/doc/build-cover)

---

## Appendix A: Gotchas

### Go

| Gotcha | Symptom | Fix |
| --- | --- | --- |
| `:=` shadows `err` in an inner block | An error is silently lost | Use `=` for existing variables |
| Writing to a nil map | `panic: assignment to entry in nil map` | `make` the map, usually in a constructor |
| Slices share a backing array | A change to one slice appears in another | `slices.Clone` |
| `for _, v := range` gives copies | Changes inside the loop disappear | Index into the slice: `s[i].Field = ...` |
| A nil slice encodes as `null` | The Vue page crashes on `null` | Initialize with `[]T{}` |
| A nil pointer stored in `error` | `err != nil` on success | Return a literal `nil` |
| No `return` after an error response | `superfluous response.WriteHeader` in logs | Always `return` immediately |
| Forgetting `rows.Close()` or `rows.Err()` | Leaked connections, missed errors | `defer rows.Close()` and check `rows.Err()` |
| Copying a struct that contains a mutex | `go vet` reports `copylocks` | Use pointer receivers |
| Forgetting `cancel()` | `go vet` reports `lostcancel` | `defer cancel()` |
| Map iteration order | Flaky tests | Sort the keys first |

### C# and ASP.NET Core

| Gotcha | Symptom | Fix |
| --- | --- | --- |
| A singleton depending on a scoped service | Stale data or connection errors; caught in Development by `ValidateScopes` | Fix the lifetimes; inject a factory if needed |
| `IDatabase` registered as a singleton | Crossed transactions and connection errors under load | Register it as scoped |
| Deferred LINQ evaluated twice or late | Duplicate queries, or results that changed | `ToList()` deliberately |
| `.Result` or `.Wait()` on a task | Deadlocks and thread-pool starvation | `await` all the way down |
| A non-UTC `DateTime` written to `timestamptz` | An Npgsql exception | Use UTC, or let the database set defaults |
| Implicit `[Required]` from nullable reference types | Unexpected 400 responses | Make it optional on purpose with `string?` |
| An interpolated string in a log message | Logs lose their structured fields | Use message templates |
| Middleware in the wrong order | CORS or authentication silently not applied | Follow the documented order |
| Instance fields in an NUnit fixture | Tests pass alone but fail together | Reset in `[SetUp]`, or use `[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]` |

### Vue

| Gotcha | Symptom | Fix |
| --- | --- | --- |
| Destructuring a `reactive` object | The values stop updating | `toRefs`, or use `ref` |
| `v-for` without stable keys | Inputs keep the wrong values after reordering | Key by ID |
| Treating a route guard as security | Data is exposed when the guard is bypassed | Enforce access on the server |
| Missing loading and error states | A blank page when the API is down | Handle all four states (V2) |

## Appendix B: Go idioms for a C# developer

- `gofmt` decides formatting. There's no `.editorconfig` to argue about.
- Names use `MixedCaps`. There's no `I` prefix for interfaces and no `Async` suffix. One-method interfaces end in `-er`, such as `Reader` and `Getter`.
- Acronyms keep a consistent case: `ID`, `URL`, `HTTP`, as in `recipeID` and `ServeHTTP`.
- Receiver names are short (`b *RecipeBook`), never `this` or `self`.
- Error strings are lowercase with no trailing punctuation: `fmt.Errorf("load recipe: %w", err)`.
- Return early and keep the success path at the left margin. There are no nested `try` blocks.
- Return concrete types and accept small interfaces. Don't define an interface per service.
- Make the zero value useful, or provide `NewX`.
- Use the standard library first, and keep SQL visible. Slightly repetitive, clear code is preferred over clever abstraction.
- A comment explains *why* or a constraint the code can't show. Doc comments start with the name: `// Store persists recipes.`

## Appendix C: Glossary

- **Backing array:** The array a Go slice points into.
- **Captive dependency:** A longer-lived service holding a shorter-lived one, in .NET dependency injection.
- **Composition root:** The single place, `Program.cs` or `main`, where dependencies are wired together.
- **Context:** Go's carrier for cancellation, deadlines, and request-scoped values. It's the counterpart to `CancellationToken`.
- **Data Protection:** ASP.NET Core's key management for encrypting cookies and tokens.
- **Embedding:** Including a type inside a Go struct without a field name, which promotes its methods.
- **Goroutine:** A lightweight concurrent function managed by the Go runtime.
- **Handler:** In Go, anything with `ServeHTTP(w, r)`.
- **Idempotent:** Repeating the request has the same effect as doing it once.
- **Keyset pagination:** Paging by "after this sort key" instead of by offset.
- **Micro-ORM:** A thin mapper such as NPoco or Dapper: SQL you can see, with automatic row mapping.
- **Optimistic locking:** Detecting conflicting writes with a version check instead of holding locks.
- **ProblemDetails:** The standard JSON error format ([RFC 9457](https://www.rfc-editor.org/rfc/rfc9457)).
- **Rune:** A Unicode code point in Go (`int32`).
- **Sentinel error:** A package-level error value compared with `errors.Is`.
- **SQLSTATE:** PostgreSQL's five-character error code, for example `23505`.
- **Zero value:** The default value every Go type has.

## Appendix D: References

All of these are optional.

**C# and ASP.NET Core**

- [ASP.NET Core fundamentals](https://learn.microsoft.com/aspnet/core/fundamentals/)
- [Web API documentation](https://learn.microsoft.com/aspnet/core/web-api/)
- [C# documentation](https://learn.microsoft.com/dotnet/csharp/)

**Data access**

- [NPoco wiki](https://github.com/schotime/NPoco/wiki)
- [Npgsql](https://www.npgsql.org/doc/)
- [PostgreSQL documentation](https://www.postgresql.org/docs/current/)
- [DbUp](https://dbup.readthedocs.io/)

**Vue**

- [Vue guide](https://vuejs.org/guide/introduction.html)
- [Vue Router](https://router.vuejs.org/)
- [Pinia](https://pinia.vuejs.org/)
- [Vite](https://vite.dev/)
- [Vitest](https://vitest.dev/)

**Go**

- [A Tour of Go](https://go.dev/tour/)
- [Effective Go](https://go.dev/doc/effective_go)
- [Go by Example](https://gobyexample.com/)
- [Go Code Review Comments](https://go.dev/wiki/CodeReviewComments)
- [Standard library](https://pkg.go.dev/std)
- [`pgx`](https://github.com/jackc/pgx)

**HTTP and security**

- [MDN HTTP guide](https://developer.mozilla.org/en-US/docs/Web/HTTP)
- [OWASP API Security Top 10](https://owasp.org/API-Security/)
- [OWASP Cheat Sheet Series](https://cheatsheetseries.owasp.org/)
