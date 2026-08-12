# AnarchyV2

A fork of [**Anarchy**](https://github.com/not-ilinked/Anarchy) — an open-source
Discord API wrapper for .NET — updated to **Discord API v10**.

Anarchy is a lightweight, "raw" Discord API wrapper (REST + Gateway + media)
written in C#. Unlike most wrappers it also supports **user-account** endpoints.
This repository (`AnarchyV2`) adapts the original source so it talks to
Discord's current API surface.

⚠️ **Discord Terms of Service:** user-account ("self-bot") automation violates
Discord's [Developer Terms of Service](https://discord.com/developers/docs/legal)
and ToS. Use bot accounts for anything you deploy. The user-account tooling here
is included for educational/authorized-lab purposes only.

The original Anarchy Source was from 2022, so I decided to update it.
Documentation and a tutorial for the original can be found [here](https://ilinked1337.gitbook.io/anarchy/).

## What changed vs. upstream (API v10)

| Change | Why |
|---|---|
| Messages serialize via the `embeds` **array** (singular `embed` removed in v10) | v10 dropped the singular `embed` field |
| Moderation reasons sent via the **`X-Audit-Log-Reason`** header | v10 removed the `?reason=` query parameter |
| Active threads fetched from **`/guilds/{guild}/threads/active`** | v10 decommissioned the channel-scoped route |
| Added the **`MessageContent` (1 << 15)** gateway intent | v10 requires it (privileged) to receive message content |
| Default **`ApiVersion` bumped 9 → 10** | v9 is deprecated |

See `NOTICE` for original-author attribution.

## Building

Requires the [.NET SDK](https://dotnet.microsoft.com/download) (built and tested
with the .NET 10 SDK; the library targets `net6.0`).

```bash
git clone https://github.com/mymoneymymind/AnarchyV2.git
cd AnarchyV2
dotnet build Anarchy.sln
```

The core library (`Anarchy/Anarchy.csproj`) and the example projects under
`Examples/` all build.

> Note: `net6.0` is **out of support** (EOL June 2024). It still compiles and
> runs, but receives no security updates. Retargeting to `net8.0`/`net9.0` is
> straightforward if you want a supported runtime.

## Known gaps (not yet updated)

These Discord features shipped *after* the upstream's last update (2023) and are
**not** implemented in this fork:

- Guild Scheduled Events, Auto Moderation, Onboarding
- Polls, Soundboard, gradient role colors / guild tags
- Subscription API v2 (only v1 entitlements are present)
- Go Live / voice re-work for required E2EE encryption modes

## License

See [`LICENSE`](./LICENSE). The original `Anarchy` ships with **no license**
(all rights reserved by `not-ilinked`); this derivative work inherits that
status. Do not redistribute without the original author's permission.

---

Original work by [not-ilinked](https://github.com/not-ilinked/Anarchy).
Adapted to API v10 by `mymoneymymind` as `AnarchyV2`.
