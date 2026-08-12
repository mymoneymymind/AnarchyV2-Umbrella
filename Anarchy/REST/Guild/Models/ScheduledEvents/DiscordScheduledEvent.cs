using System;
using Newtonsoft.Json;

namespace Discord
{
    /// <summary>
    /// A guild scheduled event (API vX).
    /// </summary>
    public class DiscordScheduledEvent : Controllable
    {
        [JsonProperty("id")]
        public ulong Id { get; private set; }

        [JsonProperty("guild_id")]
        public ulong GuildId { get; private set; }

        [JsonProperty("channel_id")]
        public ulong? ChannelId { get; private set; }

        [JsonProperty("creator_id")]
        public ulong? CreatorId { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("description")]
        public string Description { get; private set; }

        [JsonProperty("scheduled_start_time")]
        public DateTime ScheduledStartTime { get; private set; }

        [JsonProperty("scheduled_end_time")]
        public DateTime? ScheduledEndTime { get; private set; }

        [JsonProperty("privacy_level")]
        public ScheduledEventPrivacyLevel PrivacyLevel { get; private set; }

        [JsonProperty("status")]
        public ScheduledEventStatus Status { get; private set; }

        [JsonProperty("entity_type")]
        public ScheduledEventEntityType EntityType { get; private set; }

        [JsonProperty("entity_id")]
        public ulong? EntityId { get; private set; }

        [JsonProperty("entity_metadata")]
        public ScheduledEventEntityMetadata EntityMetadata { get; private set; }

        [JsonProperty("creator")]
        public DiscordUser Creator { get; private set; }

        [JsonProperty("user_count")]
        public uint? UserCount { get; private set; }

        [JsonProperty("image")]
        public string ImageHash { get; private set; }
    }

    public enum ScheduledEventPrivacyLevel
    {
        GuildOnly = 2
    }

    public enum ScheduledEventStatus
    {
        Scheduled = 1,
        Active = 2,
        Completed = 3,
        Canceled = 4
    }

    public enum ScheduledEventEntityType
    {
        StageInstance = 1,
        Voice = 2,
        External = 3
    }

    public class ScheduledEventEntityMetadata
    {
        [JsonProperty("location")]
        public string Location { get; set; }
    }

    /// <summary>
    /// Payload for creating/modifying a scheduled event.
    /// </summary>
    public class ScheduledEventProperties
    {
        [JsonProperty("channel_id")]
        public ulong? ChannelId { get; set; }

        [JsonProperty("entity_metadata")]
        public ScheduledEventEntityMetadata EntityMetadata { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("privacy_level")]
        public ScheduledEventPrivacyLevel PrivacyLevel { get; set; } = ScheduledEventPrivacyLevel.GuildOnly;

        [JsonProperty("scheduled_start_time")]
        public DateTime ScheduledStartTime { get; set; }

        [JsonProperty("scheduled_end_time")]
        public DateTime? ScheduledEndTime { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("entity_type")]
        public ScheduledEventEntityType EntityType { get; set; }

        [JsonProperty("image")]
        public string ImageData { get; set; }
    }
}
