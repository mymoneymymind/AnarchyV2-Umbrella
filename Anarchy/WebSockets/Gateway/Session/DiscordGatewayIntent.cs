using System;

namespace Discord.Gateway
{
    [Flags]
    public enum DiscordGatewayIntent
    {
        Guilds = 1 << 0,
        GuildMembers = 1 << 1,
        GuildBans = 1 << 2,
        GuildEmojis = 1 << 3,
        GuildIntegrations = 1 << 4,
        GuildWebhooks = 1 << 5,
        GuildInvites = 1 << 6,
        GuildVoiceStates = 1 << 7,
        GuildPresences = 1 << 8,
        GuildMessages = 1 << 9,
        GuildMessageReactions = 1 << 10,
        GuildMessageTyping = 1 << 11,
        DirectMessages = 1 << 12,
        DirectMessageReactions = 1 << 13,
        DirectMessageTyping = 1 << 14,
        // API v10+: required (privileged) to receive non-empty message content fields
        // (content, embeds, attachments, components) in gateway message events.
        MessageContent = 1 << 15
    }
}