using Newtonsoft.Json;

namespace Discord
{
    /// <summary>
    /// An Auto Moderation rule (API vX).
    /// </summary>
    public class DiscordAutoModRule : Controllable
    {
        [JsonProperty("id")]
        public ulong Id { get; private set; }

        [JsonProperty("guild_id")]
        public ulong GuildId { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("creator_id")]
        public ulong CreatorId { get; private set; }

        [JsonProperty("event_type")]
        public AutoModEventType EventType { get; private set; }

        [JsonProperty("trigger_type")]
        public AutoModTriggerType TriggerType { get; private set; }

        [JsonProperty("trigger_metadata")]
        public AutoModTriggerMetadata TriggerMetadata { get; private set; }

        [JsonProperty("actions")]
        public AutoModAction[] Actions { get; private set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; private set; }

        [JsonProperty("exempt_roles")]
        public ulong[] ExemptRoles { get; private set; }

        [JsonProperty("exempt_channels")]
        public ulong[] ExemptChannels { get; private set; }
    }

    public enum AutoModEventType
    {
        MessageSend = 1
    }

    public enum AutoModTriggerType
    {
        Keyword = 1,
        Spam = 3,
        KeywordPreset = 4,
        MentionSpam = 5
    }

    public enum AutoModKeywordPresetType
    {
        Profanity = 1,
        SexualContent = 2,
        Slurs = 3
    }

    public enum AutoModActionType
    {
        BlockMessage = 1,
        SendAlertMessage = 2,
        Timeout = 3
    }

    public class AutoModTriggerMetadata
    {
        [JsonProperty("keyword_filter")]
        public string[] KeywordFilter { get; set; }

        [JsonProperty("regex_patterns")]
        public string[] RegexPatterns { get; set; }

        [JsonProperty("presets")]
        public AutoModKeywordPresetType[] Presets { get; set; }

        [JsonProperty("allow_list")]
        public string[] AllowList { get; set; }

        [JsonProperty("mention_total_limit")]
        public uint? MentionTotalLimit { get; set; }

        [JsonProperty("mention_raid_protection_enabled")]
        public bool? MentionRaidProtectionEnabled { get; set; }
    }

    public class AutoModAction
    {
        [JsonProperty("type")]
        public AutoModActionType Type { get; set; }

        [JsonProperty("metadata")]
        public AutoModActionMetadata Metadata { get; set; }
    }

    public class AutoModActionMetadata
    {
        [JsonProperty("channel_id")]
        public ulong? ChannelId { get; set; }

        [JsonProperty("duration_seconds")]
        public uint? DurationSeconds { get; set; }

        [JsonProperty("custom_message")]
        public string CustomMessage { get; set; }
    }

    /// <summary>
    /// Payload for creating/modifying an Auto Moderation rule.
    /// </summary>
    public class AutoModRuleProperties
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("event_type")]
        public AutoModEventType EventType { get; set; } = AutoModEventType.MessageSend;

        [JsonProperty("trigger_type")]
        public AutoModTriggerType TriggerType { get; set; }

        [JsonProperty("trigger_metadata")]
        public AutoModTriggerMetadata TriggerMetadata { get; set; }

        [JsonProperty("actions")]
        public AutoModAction[] Actions { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; } = true;

        [JsonProperty("exempt_roles")]
        public ulong[] ExemptRoles { get; set; }

        [JsonProperty("exempt_channels")]
        public ulong[] ExemptChannels { get; set; }
    }

    /// <summary>
    /// Sent when an auto-moderation rule is triggered and an action executes.
    /// </summary>
    public class AutoModActionExecution
    {
        [JsonProperty("guild_id")]
        public ulong GuildId { get; private set; }

        [JsonProperty("action")]
        public AutoModAction Action { get; private set; }

        [JsonProperty("rule_id")]
        public ulong RuleId { get; private set; }

        [JsonProperty("rule_trigger_type")]
        public AutoModTriggerType RuleTriggerType { get; private set; }

        [JsonProperty("user_id")]
        public ulong UserId { get; private set; }

        [JsonProperty("channel_id")]
        public ulong? ChannelId { get; private set; }

        [JsonProperty("message_id")]
        public ulong? MessageId { get; private set; }

        [JsonProperty("alert_system_message_id")]
        public ulong? AlertSystemMessageId { get; private set; }

        [JsonProperty("content")]
        public string Content { get; private set; }

        [JsonProperty("matched_keyword")]
        public string MatchedKeyword { get; private set; }

        [JsonProperty("matched_content")]
        public string MatchedContent { get; private set; }
    }
}
