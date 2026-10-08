# Intended architecture

This describes direction, not implemented capabilities. [STATUS](STATUS.md) records what exists.

## Intentional boundaries

```text
Browser / Vue 3 + TypeScript
            │ HTTP / JSON
            ▼
      Go HTTP API ─────► PostgreSQL
            │
            └─────────► Object storage (media, later)

Later: browser ── signed upload URL ──► object storage
```

- Vue communicates with Go. Go owns database access, validation, and eventual authorization. No database credentials or direct database API in the frontend.
- PostgreSQL is the primary relational database. Learn explicit SQL, schema constraints, migrations, and transactions as the product requires them.
- Prefer one conventional Go application with `net/http` and the standard library where reasonable. Add libraries only when they solve an actual problem; do not build security-sensitive primitives from scratch for practice.
- Keep relational records and media metadata in PostgreSQL; store eventual audio and photos in object storage. Large uploads should eventually go directly from the browser to storage using narrowly scoped signed URLs issued after Go checks authorization. Finalization/validation and media access rules will need design then.
- Preserve original media and original-language text. Derived translations or interpretations must remain distinguishable; their data model is deferred.

See [ADR 0001](decisions/0001-go-api-database-boundary.md) and [ADR 0002](decisions/0002-preserve-original-content.md).

## First proof of the architecture

Milestone 0 demonstrates Browser → Vue → Go → PostgreSQL → Go → Vue locally. A small endpoint such as `GET /api/health` checks API/database availability and produces a visible frontend result. It should report unavailable dependencies truthfully without exposing credentials or database internals. This is a learning exercise and local availability check; production liveness/readiness design can wait.

Recipes begin only after this path works and the developer can explain it. Early milestones without authentication use local development data and are not ready for public exposure.

## Operating constraints

Aim for near-zero operating cost, ideally only the domain until usage justifies spending. Free tiers are acceptable, not a guaranteed capacity or cost commitment. Prefer few services, simple deployment, straightforward logs and local debugging, and replaceable providers without speculative abstraction layers.

No Kubernetes, microservices, speculative caching, elaborate event systems, or message brokers without a demonstrated need. Use appropriate testing at boundaries as behavior develops. Decide backup/recovery, secrets handling, media limits, and operational checks before storing real user data publicly.

## Intentionally undecided

- Repository application layout, exact tool versions, and local PostgreSQL setup method.
- Vite is likely; styling, routing, and frontend state tools have not been chosen.
- Database driver (`pgx` is likely), migration tool, and optional later `sqlc`. No heavy ORM planned.
- Authentication mechanism, session/token storage, account flow, and identity provider. Learning JWTs does not make JWT authentication a requirement.
- Deployment, database, and object storage providers; domains/origins and CORS strategy.
- API shapes beyond the illustrative health endpoint, error format, schema details, upload limits, and media formats.
- Transcription/translation providers, implementation, and timing.

Resolve each at the milestone where it matters. Add an ADR only when the choice has lasting consequences or a meaningful tradeoff.
