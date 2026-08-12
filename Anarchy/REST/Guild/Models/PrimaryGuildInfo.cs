using Newtonsoft.Json;

namespace Discord
{
    /// <summary>
    /// Information about a guild's "primary guild" (home/student-hub) association.
    /// </summary>
    public class PrimaryGuildInfo
    {
        [JsonProperty("id")]
        public ulong? Id { get; private set; }

        [JsonProperty("identity_guild_id")]
        public ulong? IdentityGuildId { get; private set; }

        [JsonProperty("identity_enabled")]
        public bool? IdentityEnabled { get; private set; }
    }
}
