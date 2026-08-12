using Newtonsoft.Json;

namespace Discord
{
    /// <summary>
    /// Guild onboarding configuration (API vX). Returned by GET /guilds/{guild.id}/onboarding
    /// and modified via PUT /guilds/{guild.id}/onboarding.
    /// </summary>
    public class DiscordOnboarding : Controllable
    {
        [JsonProperty("guild_id")]
        public ulong GuildId { get; private set; }

        [JsonProperty("prompts")]
        public DiscordOnboardingPrompt[] Prompts { get; private set; }

        [JsonProperty("default_channel_ids")]
        public ulong[] DefaultChannelIds { get; private set; }

        /// <summary>
        /// Whether onboarding is enabled
        /// </summary>
        [JsonProperty("enabled")]
        public bool Enabled { get; private set; }

        /// <summary>
        /// Mode of the onboarding
        /// </summary>
        [JsonProperty("mode")]
        public OnboardingMode Mode { get; private set; }
    }

    public enum OnboardingMode
    {
        OnboardingDefault = 0,
        OnboardingAdvanced = 1
    }

    /// <summary>
    /// A single onboarding prompt (e.g. "Choose your roles").
    /// </summary>
    public class DiscordOnboardingPrompt
    {
        [JsonProperty("id")]
        public ulong Id { get; private set; }

        [JsonProperty("type")]
        public OnboardingPromptType Type { get; private set; }

        [JsonProperty("options")]
        public DiscordOnboardingPromptOption[] Options { get; private set; }

        [JsonProperty("title")]
        public string Title { get; private set; }

        [JsonProperty("single_select")]
        public bool SingleSelect { get; private set; }

        [JsonProperty("required")]
        public bool Required { get; private set; }

        [JsonProperty("in_onboarding")]
        public bool InOnboarding { get; private set; }

        [JsonProperty("emoji")]
        public DiscordEmoji Emoji { get; private set; }
    }

    public enum OnboardingPromptType
    {
        MultipleChoice = 0,
        Dropdown = 1
    }

    /// <summary>
    /// An option within an onboarding prompt (maps to a role and/or channel).
    /// </summary>
    public class DiscordOnboardingPromptOption
    {
        [JsonProperty("id")]
        public ulong Id { get; private set; }

        [JsonProperty("title")]
        public string Title { get; private set; }

        [JsonProperty("description")]
        public string Description { get; private set; }

        [JsonProperty("emoji")]
        public DiscordEmoji Emoji { get; private set; }

        [JsonProperty("role_ids")]
        public ulong[] RoleIds { get; private set; }

        [JsonProperty("channel_ids")]
        public ulong[] ChannelIds { get; private set; }
    }

    /// <summary>
    /// Payload for modifying guild onboarding (PUT /guilds/{guild.id}/onboarding).
    /// </summary>
    public class OnboardingProperties
    {
        [JsonProperty("prompts")]
        public DiscordOnboardingPrompt[] Prompts { get; set; }

        [JsonProperty("default_channel_ids")]
        public ulong[] DefaultChannelIds { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("mode")]
        public OnboardingMode Mode { get; set; }
    }
}
