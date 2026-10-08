# Feature plans

Create a plan only when a feature needs implementation context across sessions. A small task can live in [STATUS](../STATUS.md). No active plan is needed for the documentation setup or first Go response.

Use `active/<short-feature-name>.md`. Include only what helps someone continue:

- Goal, scope, non-goals, and relevant product/architecture/ADR links.
- Unresolved assumptions and decisions needed before dependent work.
- A few implementation steps and exit criteria; mark actual progress.
- Verification performed, results, remaining risks/blockers, and the next manageable step.

Update the plan at meaningful handoffs and link it from STATUS. Once its exit criteria are verified, record the result and move it to `completed/`; update links. Completed plans are historical context, not current requirements. Keep current behavior in the owning product/architecture documents.

For a significant lasting decision, add `docs/decisions/NNNN-short-title.md` with status, date, context, decision, and consequences. Supersede an old ADR explicitly rather than erasing its rationale. Do not create ADRs for routine implementation choices.
