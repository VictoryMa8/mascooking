# Learning

Track engineering growth separately from product completion. A shipped feature or a topic appearing in the roadmap is not evidence of understanding. Keep entries brief, grounded in actual implementation, explanation, review, or debugging.

## Existing strengths and experience

- Professional experience with C#, Vue, and SQL Server, as reported by the developer.
- Use those as reference points for unfamiliar Go/PostgreSQL concepts; depth in individual topics has not been assessed.

## Currently learning

- Active approach: PAIR mode with small practice programs in `learning/go/`, before application development. Conversation and exercises are the primary teaching method; tutorials are optional references.
- First focus: program entry point, string variables, function parameters, return values, and the difference between producing a value and printing it.
- Next topics follow demonstrated understanding: modules/tooling and basic tests, then collections, structs, errors, file/JSON I/O, and small interfaces as needed.
- The developer extended the assistant's example with a string parameter and updated the call correctly. They explained that returning a value allows reuse elsewhere. The program ran successfully with the now-available Go toolchain.
- Assistant demonstrated module initialization and a passing test with `testing`, `got`/`want`, and `t.Errorf`. Developer practice with writing tests is next; understanding of testing has not yet been assessed.

## Topics needing more practice

None identified through project work yet. Add specific observed gaps and a small practice opportunity rather than copying the whole syllabus here.

## Upcoming concepts, introduced when needed

| Product need | Learning opportunities |
| --- | --- |
| Local request path | Handlers, JSON, HTTP status codes, Go errors, configuration, connectivity |
| Persist a recipe | PostgreSQL schemas, constraints, migrations, explicit SQL from Go, likely `pgx` |
| Reliable recipe lifecycle | Foreign keys, transactions, focused tests, REST design, error handling |
| Growing handlers | Dependency injection through ordinary Go values/constructors, `context.Context`, middleware, interfaces where justified |
| Accounts and ownership | Authentication, authorization, sessions and JWT tradeoffs, access-control tests |
| Media | Object storage, signed uploads, browser APIs, failure recovery |
| Simple Mode and languages | Accessibility, internationalization, UI/content language separation |
| Running a service | Deployment, logging, debugging, secrets, backups, resource/cost awareness |

Indexes belong when query patterns justify them. `sqlc` may help later when repeated SQL integration work warrants it. Neither interfaces nor extra tools are learning checkboxes that must be added before a need exists.

## Concepts to revisit

None recorded yet. Use this section for something previously practiced that needs another attempt, not hypothetical weaknesses.

## Evidence from sessions

- First pairing exercise: developer added `language`, passed `Khmer`, and included it in the returned label. Review and execution confirmed the expected output. They identified reuse as the benefit of returning a string; the initial design question needed plainer wording. Next: modules and a small test. This is evidence of one successful change, not broad proficiency.

For meaningful progress, record what the developer did or explained, where help was needed, and a next practice step. Ask for the developer’s self-assessment where useful; do not infer mastery from AI-written code.
