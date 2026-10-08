# Roadmap

Milestones express outcomes, not a complete backlog. Go foundations precede application development; every application milestone is pending. [STATUS](STATUS.md) owns the next concrete task. Later milestones can be split or reordered as evidence accumulates.

## Go foundations — Learn through pairing

**Goal:** Become comfortable writing, explaining, testing, and debugging small Go programs before combining the application stack.

Use `learning/go/` for small experiments. Their learning value matters more than whether the application will reuse them. Begin with functions and values; gradually introduce collections, structs, errors, tests, file/JSON I/O, and simple interfaces when an exercise benefits from them. Teach in conversation, with frequent developer changes and reasoning. No required tutorial reading or large completed implementations.

**Exit criteria:** The developer can implement a modest program with documentation and occasional hints, handle a failure, write useful tests, and explain data flow and design choices. Assess this through their work rather than a fixed schedule or completed chapters. Then revisit readiness and collaboration mode before Milestone 0; advanced concurrency is not a prerequisite.

## Milestone 0 — Application skeleton

**Goal:** Understand Browser → Vue → Go → PostgreSQL → Go → Vue.

**Exit criteria:**

- Vue/TypeScript, a Go API, and PostgreSQL run locally with documented setup commands.
- Go connects to PostgreSQL; an endpoint such as `GET /api/health` reports API/database availability.
- Vue calls Go and displays the result, including a useful failure state when the database is unavailable.
- The developer can trace a request and explain where configuration, HTTP handling, and database access occur.

Keep this boring: no recipes, auth, media, hosting, or AI. Begin with a small Go HTTP response; add the other parts incrementally. This milestone need not fit in one session.

## Milestone 1 — One recipe round trip

**Goal:** Build the smallest useful data-backed vertical slice.

**Exit criteria:** A basic recipe record can be created in Vue, validated and saved through Go, retrieved through Go, and displayed in Vue. The table is reproducible through a migration; a focused check demonstrates valid persistence and invalid-input handling. The developer can explain the request, SQL, and response.

Use local sample data. No auth, recording, storage, or AI. Do not expand this slice into the whole recipe product.

## Milestone 2 — Recipe lifecycle and access

**Goal:** Give recipe records an owner and safe editing boundaries.

**Exit criteria:** Account/identity and visibility decisions are recorded; users can manage their own records; unauthorized changes and access are rejected by Go and tested. The supported create/read/update/delete and publication behavior is documented. The developer can explain authentication versus authorization.

Choose an authentication approach when needed. Keep incomplete product flows local or in restricted test environments.

## Milestone 3 — Preserve media

**Goal:** Make the cook’s photos and voice usable source content.

**Exit criteria:** Users can add photos, record or upload audio, play recordings, and optionally supply an original-language transcript. Authorized direct uploads, metadata association, upload limits, failed-upload recovery, and removal of stored media work for the supported flow. Original content is preserved without recipe rewriting.

Choose storage and browser recording details here. Resolve source correction and required-media rules before finalizing publication validation.

## Milestone 4 — Usable v0 and first deployment

**Goal:** Share and discover recipes with the intended photo/voice/story emphasis.

**Exit criteria:** A user can complete the v0 flow, share a recipe page under the chosen access policy, and browse deliberately public posts in a visual grid. Keyboard use, labels, contrast, mobile interaction, and transcript accessibility tradeoffs are reviewed. Speaker permission, privacy, deletion, and a basic abuse/reporting approach are resolved. Deployment, secrets, backups/recovery, useful logs, and cost/media limits are documented and exercised appropriately for the chosen services.

Review the flow with real users before expanding scope. Automated transcription is not a release prerequisite. Bring a small restricted deployment earlier if it serves a concrete learning need.

## After v0 — Invitations, Simple Mode, and language support

**Goal:** Help a less technically comfortable cook contribute in their preferred language.

**Exit criteria:** A recipient can follow an invitation and record through a forgiving, minimal flow without an account before recording. Invitation access/expiry and who controls publication are defined and checked. The flow is tried with representative users. UI language remains separate from original content language.

Choose the first supported languages and narrow this milestone before starting. Automatic transcription and optional translation are separate later candidates; each needs its own review, labeling, correction, and source-preservation criteria. Advanced AI features have no committed milestone.
