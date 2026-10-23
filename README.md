# Ma's Cooking

> Save your loved ones' recipes in their original language. Share it to the world, understandable to anyone.

Ma's Cooking (`mascooking.com`, domain pending) is a family recipe preservation project. You can upload photos, the cook's own recorded voice, an optional transcript, and the story behind a dish. **Preserve your heritage and culture, the world needs it.**

The project has two equal goals: 

1. Build an actually useful application
2. Develop full-stack software engineering skills

The intended stack is Vue + TypeScript → Go → PostgreSQL, with object storage for media later.

**How study time is split:** about three quarters goes to C#/ASP.NET Core and Vue, reinforcing the developer's work stack through a practice app called Recipe Box. About one quarter goes to barebones Go and Vue, rebuilding by hand what ASP.NET Core does automatically. That Go work becomes Ma's Cooking. Each concept is learned with the framework first and then built without one. See the [study roadmap](learning/ROADMAP.md).

**Current state:** Go foundations in PAIR mode, with small practice programs in [learning/](learning/README.md). The .NET track and the application haven't started. See [STATUS](docs/STATUS.md) for the current task and tooling limitations.

## Repository guide


| Path                                         | Responsibility                                                        |
| -------------------------------------------- | --------------------------------------------------------------------- |
| [AGENTS.md](AGENTS.md)                       | Agent operating manual and COACH / PAIR / UNBLOCK modes               |
| [docs/PRODUCT.md](docs/PRODUCT.md)           | Product principles, scope, future experiences, open product questions |
| [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md) | Intended system boundaries and undecided technical choices            |
| [docs/ROADMAP.md](docs/ROADMAP.md)           | Milestones and observable exit criteria                               |
| [docs/STATUS.md](docs/STATUS.md)             | Small, frequently updated handoff: now, next, blockers                |
| [docs/LEARNING.md](docs/LEARNING.md)         | Evidence-based learning progress, separate from shipped features      |
| [docs/decisions/](docs/decisions/)           | Significant accepted decisions and their rationale                    |
| [docs/plans/](docs/plans/README.md)          | Conventions for larger feature plans in `active/` and `completed/`    |
| [learning/ROADMAP.md](learning/ROADMAP.md)   | Study plan: .NET + Vue track (~75%) and Go + Vue track (~25%)         |
| [learning/](learning/README.md)              | Practice code for both tracks, kept separate from the application     |


Application folders will be chosen when we implement the skeleton. No empty frontend/backend scaffolds are needed now.

## Start the next session

> Continue my study plan. Read AGENTS.md, STATUS.md, LEARNING.md, and learning/ROADMAP.md, then check the time split in STATUS to decide whether this is a .NET or a Go session. Pick up the current stage, explain one concept, and let me reason through and implement the next small change.

Default workflow: AI plans heavily → developer implements → AI reviews, teaches, and unblocks. Keep commits small and meaningful, such as `initialize Go backend` or `add health endpoint`. Add verified local setup instructions here as they become available.
