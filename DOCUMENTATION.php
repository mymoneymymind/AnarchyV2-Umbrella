<?php
/**
 * AnarchyV2-Umbrella — Documentation
 * ---------------------------------------------------------------------------
 * A fork of Anarchy (https://github.com/not-ilinked/Anarchy), a raw Discord
 * API wrapper for .NET, updated to Discord API v10 and extended with the major
 * feature gaps that shipped after the upstream's last update (2023).
 *
 * This file is a plain-PHP rendering of the repo README / NOTICE / TODO so the
 * project documentation is also viewable outside the GitHub/markdown context.
 * It contains no executable logic — pure documentation.
 *
 * Credit: updates and maintenance of this fork are credited to yackinnn
 *         (https://t.me/yackinnn).
 *
 * Original author: not-ilinked (https://github.com/not-ilinked/Anarchy).
 * License: MIT (see LICENSE in this repo).
 */

namespace AnarchyV2Umbrella\Docs;

final class Documentation
{
    /**
     * What this wrapper is.
     */
    public const OVERVIEW = <<<'MD'
# AnarchyV2-Umbrella

A fork of **Anarchy** — an open-source Discord API wrapper for .NET — updated to
**Discord API v10** and extended with the feature gaps that shipped after the
upstream's last update (2023).

Anarchy is a lightweight, "raw" Discord API wrapper (REST + Gateway + media)
written in C#. Unlike most wrappers it also supports **user-account** endpoints.
This repo (`AnarchyV2-Umbrella`) is the *complete* fork: it contains the v10
fixes, all seven post-2023 feature gaps, and **Subscription API v2**.

The original Anarchy source was from 2022; this fork brings it current.
MD;

    /**
     * The v10 breaking changes that had to be fixed just to talk to Discord.
     *
     * @return array<string,string>  change => reason
     */
    public static function v10Fixes(): array
    {
        return [
            'Messages serialize via the `embeds` array (singular `embed` removed in v10)'
                => 'v10 dropped the singular `embed` field',
            'Moderation reasons sent via the `X-Audit-Log-Reason` header'
                => 'v10 removed the `?reason=` query parameter',
            'Active threads fetched from `/guilds/{guild}/threads/active`'
                => 'v10 decommissioned the channel-scoped route',
            'Added the `MessageContent` (1 << 15) gateway intent'
                => 'v10 requires it (privileged) to receive message content',
            'Default `ApiVersion` bumped 9 -> 10'
                => 'v9 is deprecated',
        ];
    }

    /**
     * Features added beyond the v10 fixes.
     *
     * @return array<int,array{name:string,detail:string}>
     */
    public static function featureGaps(): array
    {
        return [
            ['name' => 'Gradient role colors', 'detail' => 'Role.colors[] multi-color gradients + read role tags'],
            ['name' => 'Guild tags / primary_guild', 'detail' => 'Set guild_tags, read primary_guild (hub info)'],
            ['name' => 'Guild Onboarding', 'detail' => 'GET/PUT /guilds/{id}/onboarding + GUILD_ONBOARDING_UPDATE gateway event'],
            ['name' => 'Polls', 'detail' => 'DiscordPoll model, message poll field, send + vote endpoint'],
            ['name' => 'Soundboard', 'detail' => 'Sounds CRUD + send to channel'],
            ['name' => 'Guild Scheduled Events', 'detail' => 'CRUD + users + 5 gateway events (create/update/delete/user-add/user-remove)'],
            ['name' => 'Auto Moderation', 'detail' => 'Rule CRUD, trigger/action types, 4 gateway events'],
            ['name' => 'Go Live / voice E2EE', 'detail' => 'Reworked to aead_xchacha20_poly1305_rtpsize (compile-verified only)'],
            ['name' => 'Subscription API v2', 'detail' => 'SKUs, subscription plans, entitlements v2, v2 subscription shapes + REST'],
        ];
    }

    /**
     * Honest verification status.
     *
     * @return array<int,string>
     */
    public static function verificationStatus(): array
    {
        return [
            'Build: clean (`dotnet build Anarchy.sln`, 0 errors).',
            'Models: JSON (de)serialization round-trips verified for v2 + feature-gap shapes.',
            'NOT exercised against live Discord (no bot token / test app available).',
            'Voice E2EE (aead_xchacha20_poly1305_rtpsize) is RUNTIME-UNVERIFIED — requires a '
                . 'real voice session to confirm libsodium AEAD symbols + 12-byte RTP-header nonce.',
        ];
    }

    /**
     * Not-in-scope items tracked as TODO (canonical in this umbrella repo).
     *
     * @return array<int,array{title:string,detail:string,state:string}>
     */
    public static function todoItems(): array
    {
        return [
            [
                'title' => 'Application Command Permissions V2 + name_localizations',
                'detail' => 'Command Permissions V2 (replaces v1 per-guild allow/deny lists); '
                    . 'per-locale name_localizations / description_localizations.',
                'state' => 'not started',
            ],
            [
                'title' => 'Paginated Pins API',
                'detail' => 'GET /channels/{id}/pins is paginated; wrapper should support cursor '
                    . 'pagination and the newer pin-message route shape.',
                'state' => 'not started',
            ],
            [
                'title' => 'Guild-Create deprecation handling',
                'detail' => 'Bot POST /guilds was deprecated for bots; reflect current restrictions '
                    . '/ user-account-only behavior.',
                'state' => 'not started',
            ],
            [
                'title' => 'Onboarding / message media tweaks',
                'detail' => 'default_reaction_emoji on auto-mod / onboarding; prompt default_values '
                    . 'and media-shape updates shipped after this work.',
                'state' => 'not started',
            ],
            [
                'title' => 'Post-update Discord API surface (catch-all)',
                'detail' => 'Any Discord API features shipped after this umbrella was finalized '
                    . '(newer gateway events, entitlements refinements) should be triaged here.',
                'state' => 'not started',
            ],
        ];
    }

    /**
     * Render the full documentation as Markdown (for CLI / echo use).
     */
    public static function renderMarkdown(): string
    {
        $md = self::OVERVIEW . "\n\n";

        $md .= "## What changed vs. upstream (API v10)\n\n";
        foreach (self::v10Fixes() as $change => $why) {
            $md .= "- **{$change}** — {$why}\n";
        }

        $md .= "\n## Feature gaps added (beyond the v10 fixes)\n\n";
        foreach (self::featureGaps() as $f) {
            $md .= "- **{$f['name']}** — {$f['detail']}\n";
        }

        $md .= "\n## Verification status\n\n";
        foreach (self::verificationStatus() as $v) {
            $md .= "- {$v}\n";
        }

        $md .= "\n## TODO — not-in-scope items (canonical in this umbrella repo)\n\n";
        $md .= "> File PRs here, not in AnarchyV2 (which pins this repo as a submodule and "
            . "lacks Subscription API v2).\n\n";
        foreach (self::todoItems() as $i => $t) {
            $n = $i + 1;
            $md .= "### {$n}. {$t['title']}\n";
            $md .= "- {$t['detail']}\n";
            $md .= "- State: **{$t['state']}**\n\n";
        }

        $md .= "## Credit\n\n";
        $md .= "Updates and maintenance credited to **yackinnn** (https://t.me/yackinnn).\n";
        $md .= "Original work by not-ilinked. Licensed under MIT.\n";

        return $md;
    }
}

// When run from the CLI (`php DOCUMENTATION.php`), print the Markdown docs.
if (PHP_SAPI === 'cli' && realpath($argv[0] ?? '') === realpath(__FILE__)) {
    echo Documentation::renderMarkdown();
}
