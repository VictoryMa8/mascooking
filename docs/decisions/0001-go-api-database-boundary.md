# ADR 0001 — Use Go as the application and database boundary

- **Status:** Accepted
- **Date:** 2026-10-02

## Context

This project must teach backend engineering as well as deliver a recipe application. The developer already uses C#, Vue, and SQL Server and intentionally wants experience with Go, PostgreSQL, HTTP APIs, and explicit SQL.

## Decision

Use Vue 3/TypeScript for the frontend, a conventional Go HTTP API, and PostgreSQL. Vue calls Go; only Go accesses the database. Prefer a single backend application and standard library components where reasonable.

## Consequences

The developer owns validation, database integration, and eventual authorization at the backend boundary. This adds backend work compared with a direct-to-database frontend or an existing C# skill set, and that work is intentional. Hosting convenience alone is not a reason to bypass Go.

This does not select hosting, a driver, migration tooling, authentication, or a directory layout. Revisit the boundary only if the learning/product goals change, recording the reason in a superseding ADR.
