# Product

## Purpose and principles

Preserve family recipes that may never have been written down, together with the voices, relationships, languages, and memories that give them meaning. The uploader may be the cook, a relative, or a friend; the uploader and speaker are distinct roles even if one person fills both.

**Preserve first. Transform only when explicitly requested.** Original audio and original-language transcripts are canonical source content. A phrase such as "add a little cumin" or "cook it until it smells right" must not silently become a fabricated measurement or standardized instruction. This is what it means to be human.

Ma's Cooking is not positioned as an AI recipe app. Automatic transcription may later support accessibility and usability. Future translations are optional viewing layers. Any advanced AI interpretation must be optional, explicitly requested, labeled as derived, and never replace the source. See [ADR 0002](decisions/0002-preserve-original-content.md).

## v0 experience

1. Visit the site and identify the uploader where necessary; the exact account flow is undecided.
2. Create a recipe post with dish name, cook/speaker name, relationship to uploader, and a short story or description. Required fields remain to be decided.
3. Record or upload audio, add one or more photos, and optionally provide a transcript.
4. Publish and receive a shareable recipe page.
5. Browse public posts in a visually rich, Pinterest-style grid.

The recipe page prioritizes photos, the cook, audio playback, the transcript, and story/context. Transcript entry is in scope; automatic transcription is not a v0 requirement. No v0 feature depends on an AI service.

### Non-goals for v0

- Structured ingredients, standardized quantities or cooking steps, recipe extraction, AI summaries or rewriting.
- Comments, followers, likes, recommendations, sophisticated ranking, or notifications.
- Meal planning, grocery lists, native mobile apps, or advertising infrastructure.
- Invitation flows, Simple Mode, automatic transcription, and translation; these are future directions.

## Future: Simple Mode and invitations

Simple Mode is a simpler interaction model, perhaps best for older parents. Use a respectful product name such as "Simple Mode" or "Easy Mode." A future invitation might say:

> Alex wants to save your chicken curry recipe.
>
> **Record My Recipe**

The recipient should ideally record without first creating an account, navigating the site, configuring settings, or understanding technical terminology. Invitations may carry a requested recipe, interface language, Simple Mode preference, and inviter identity. These are concepts, not a committed API or security design.

Use large touch targets, one main action per screen, clear text labels, high contrast, minimal navigation, explicit back buttons, forgiving workflows, no hidden gestures, and few decisions. Baseline accessibility belongs in v0 too.

## Languages and source content

Interface language and recipe language are separate. A cook may use a Mandarin interface and record in Mandarin while a relative uses an English interface. The original recording and transcript remain Mandarin. A later translation must be clearly distinguishable and must not replace the original.

Keep this distinction in the design from the start; a full multilingual interface can follow v0. The initial UI language, supported languages, transcript correction workflow, and treatment of multiple speakers remain undecided.

## Privacy and open assumptions

Public browsing applies only to posts deliberately made public. A shareable URL does not itself define privacy or access control. Before real family content is published, decide:

- Which visibility states v0 supports, which is the default, and whether shared links are public or restricted.
- How uploaders confirm speaker permission to record, store, and publish voices, names, stories, and photos, including content involving children or other people.
- Who can edit, remove, or request removal of a post, and what deletion means for stored media and backups.
- How personal information in recordings/transcripts is handled, and which basic abuse/reporting path is needed for public posts.
- Whether publication requires audio and photos, or whether either may be omitted. The target experience includes both; that is not yet a validation rule.

Optional transcripts leave an accessibility gap for audio-only content; make that tradeoff explicit when reviewing v0 accessibility. Decide how corrections and machine-generated drafts are labeled without silently changing source content. These questions do not block a local skeleton, but relevant publication and access decisions must precede a public release.