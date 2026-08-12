using Newtonsoft.Json;

namespace Discord
{
    /// <summary>
    /// A guild soundboard sound (API vX).
    /// </summary>
    public class DiscordSoundboardSound : Controllable
    {
        [JsonProperty("guild_id")]
        public ulong? GuildId { get; private set; }

        [JsonProperty("sound_id")]
        public ulong SoundId { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("volume")]
        public float Volume { get; private set; }

        /// <summary>
        /// The emoji id or name of the sound's icon, if set.
        /// </summary>
        [JsonProperty("emoji_id")]
        public ulong? EmojiId { get; private set; }

        [JsonProperty("emoji_name")]
        public string EmojiName { get; private set; }

        [JsonProperty("available")]
        public bool Available { get; private set; }

        [JsonProperty("user_id")]
        public ulong? UserId { get; private set; }
    }

    /// <summary>
    /// Payload for creating/modifying a soundboard sound.
    /// </summary>
    public class SoundboardSoundProperties
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Sound file data (base64-encoded). Required when creating.
        /// </summary>
        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("volume")]
        public float? Volume { get; set; }

        [JsonProperty("emoji_id")]
        public ulong? EmojiId { get; set; }

        [JsonProperty("emoji_name")]
        public string EmojiName { get; set; }
    }
}
