# ADR 0002 — Preserve original voices and exclude v0 AI rewriting

- **Status:** Accepted
- **Date:** 2026-10-02

## Context

Family recipes carry culture and memory through a speaker’s words, including imprecise quantities and sensory judgment. Automated extraction can invent precision or erase meaning. The recording itself is a central part of the product.

## Decision

Preserve original audio and original-language transcripts as canonical source content. Exclude AI recipe extraction, rewriting, and summaries from v0. Do not market the product as an AI recipe app.

Future transcription may support accessibility; translation is an optional viewing layer. Any advanced interpretation must be explicitly requested, labeled as derived, and kept separate from the source. Never silently replace the recording or original-language text with an interpretation.

## Consequences

The product accepts natural speech such as “add enough until it feels right” without fabricating measurements. Photos, playback, transcripts, and context take priority over structured ingredients or cooking steps. v0 does not need an AI provider.

Transcript provenance, corrections, and conflicts with the recording still need a concrete workflow; this decision does not assume a machine transcript is accurate. Any future transformation feature must preserve this boundary or explicitly supersede this decision after reconsidering the product principles.
