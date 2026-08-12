# AnarchyV2
by https://t.me/yackinnn


A fork of [**Anarchy**](https://github.com/not-ilinked/Anarchy) which was a very good open source API wrapper for Discord.
Discord API wrapper for .NET — updated to **Discord API v10**.

Anarchy is a lightweight, "raw" Discord API wrapper (REST + Gateway + media)
written in C# - unlike most wrappers it also supports account endpoints.
This repository (`AnarchyV2`) adapts the original source so it talks to
Discord's current API surface vs the old 2022 version.

That original Anarchy Source was from 2022, so I decided to update it.
Documentation and a tutorial for the original can be found [here](https://ilinked1337.gitbook.io/anarchy/).

## What changed vs. upstream (API v10)

| Change | Why |
|---|---|
| Messages serialize via the `embeds` **array** (singular `embed` removed in v10) | v10 dropped the singular `embed` field |
| Moderation reasons sent via the **`X-Audit-Log-Reason`** header | v10 removed the `?reason=` query parameter |
| Active threads fetched from **`/guilds/{guild}/threads/active`** | v10 decommissioned the channel-scoped route |
| Added the **`MessageContent` (1 << 15)** gateway intent | v10 requires it (privileged) to receive message content |
| Default **`ApiVersion` bumped 9 → 10** | v9 is deprecated |

### Feature gaps added (beyond the v10 fixes)

- Gradient role colors (`Role.colors`), role tags, guild tags + `primary_guild` fields
- Guild Onboarding (get/modify) + `GUILD_ONBOARDING_UPDATE` gateway event
- Polls (`DiscordPoll` model, message poll field, send + vote)
- Soundboard sounds (CRUD + send)
- Guild Scheduled Events (CRUD + users + 5 gateway events)
- Auto Moderation (rules CRUD, trigger/action types, 4 gateway events)
- Go Live / voice E2EE rework to `aead_xchacha20_poly1305_rtpsize`

Updates and maintenance of this fork are credited to **[yackinnn](https://t.me/yackinnn)**.
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

## Remaining gaps

All major feature gaps have been implemented (see "Feature gaps added" above), including
**Subscription API v2** (SKUs, subscription plans, entitlements v2, and the v2 subscription
shapes). The only Discord features still absent are those shipped after this work that were
out of scope:

- (none currently tracked — open an issue if you find one)

## License

Licensed under the [MIT License](./LICENSE). The original `Anarchy` code is
copyright (c) not-ilinked; this `AnarchyV2` adaptation is copyright (c)
mymoneymymind. Both copyright notices are retained in the LICENSE file.

---

Original work by [not-ilinked](https://github.com/not-ilinked/Anarchy).
Adapted to API v10 by `mymoneymymind` as `AnarchyV2`.
