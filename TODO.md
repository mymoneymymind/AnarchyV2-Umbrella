# TODO — Not-in-scope items (tracked in this umbrella repo)

These Discord API features were **out of scope** for the AnarchyV2 / AnarchyV2-Umbrella
update and are **not yet implemented**. They are tracked here as the canonical TODO list.

> **Canonical home:** `AnarchyV2-Umbrella` is the complete fork (it already includes
> Subscription API v2). File any PRs for the items below **here**, not in `AnarchyV2`
> (which pins this repo as a submodule and lacks Subscription API v2).

State legend: `not started` = no code yet; `compile-unverified` = modeled but not
runtime-tested against live Discord.

## Open items

### 1. Application Command Permissions V2 + `name_localizations`
- Command Permissions V2 (richer model replacing v1 per-guild role/user allow/deny lists)
- Per-locale `name_localizations` / `description_localizations` on commands and options
- State: **not started**

### 2. Paginated Pins API
- `GET /channels/{id}/pins` is paginated; the wrapper should support cursor pagination
  and the newer pin-message route shape.
- State: **not started**

### 3. Guild-Create deprecation handling
- Bot `POST /guilds` (guild creation) was deprecated for bots; the wrapper should reflect
  the current restrictions / user-account-only behavior.
- State: **not started**

### 4. Onboarding / message media tweaks
- `default_reaction_emoji` on auto-mod / onboarding prompt media fields
- Onboarding prompt `default_values` and media-shape updates shipped after this work
- State: **not started**

### 5. Post-update Discord API surface (catch-all)
- Any Discord API features shipped after this umbrella was finalized (e.g. newer
  gateway events, entitlements refinements) should be triaged here.
- State: **not started**

## Verification status of everything already implemented
- Build: clean (`dotnet build Anarchy.sln`, 0 errors).
- Models: JSON (de)serialization round-trips verified for v2 + feature-gap shapes.
- **Not** exercised against live Discord (no bot token / test app available).
- Voice E2EE (`aead_xchacha20_poly1305_rtpsize`) is **runtime-unverified** — requires a
  real voice session to confirm `libsodium` AEAD symbols + 12-byte RTP-header nonce.
