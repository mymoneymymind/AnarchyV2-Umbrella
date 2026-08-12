using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Discord.Commands;
using Newtonsoft.Json;

namespace Discord
{
    public class DiscordRole : Controllable, IMentionable
    {
        [JsonProperty("id")]
        public ulong Id { get; private set; }

        internal ulong GuildId { get; set; }

        public MinimalGuild Guild
        {
            get { return new MinimalGuild(GuildId).SetClient(Client); }
        }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("color")]
        private uint _color;
        public Color Color
        {
            get { return Color.FromArgb((int) _color); }
            private set { _color = (uint) Color.FromArgb(0, value.R, value.G, value.B).ToArgb(); }
        }

        /// <summary>
        /// Gradient role colors (API vX "colors" field). Null for non-gradient roles.
        /// When present, Discord renders the role as a gradient instead of a solid <see cref="Color"/>.
        /// </summary>
        [JsonProperty("colors")]
        private uint[] _colors;
        public IReadOnlyList<Color> Colors
        {
            get
            {
                if (_colors == null)
                    return null;

                var list = new List<Color>(_colors.Length);
                foreach (var c in _colors)
                    list.Add(Color.FromArgb((int) c));

                return list;
            }
        }

        /// <summary>
        /// True when this role is a gradient role (has the <see cref="Colors"/> array).
        /// </summary>
        public bool IsGradientRole
        {
            get { return _colors != null && _colors.Length > 0; }
        }

        /// <summary>
        /// Tags on the role (bot id, integration id, subscription/premium info).
        /// </summary>
        [JsonProperty("tags")]
        public RoleTags Tags { get; private set; }

        [JsonProperty("position")]
        public int Position { get; private set; }

        [JsonProperty("hoist")]
        public bool Seperated { get; private set; }

        [JsonProperty("mentionable")]
        public bool Mentionable { get; private set; }

        [JsonProperty("permissions")]
        public DiscordPermission Permissions { get; private set; }

        public async Task<DiscordRole> ModifyAsync(RoleProperties properties)
        {
            return await Client.ModifyRoleAsync(GuildId, Id, properties);
        }

        /// <summary>
        /// Modifies the role
        /// </summary>
        /// <param name="properties">Options for modifying the role</param>
        public DiscordRole Modify(RoleProperties properties)
        {
            return ModifyAsync(properties).GetAwaiter().GetResult();
        }

        public async Task DeleteAsync()
        {
            await Client.DeleteRoleAsync(GuildId, Id);
        }

        /// <summary>
        /// Deletes the role
        /// </summary>
        public void Delete()
        {
            DeleteAsync().GetAwaiter().GetResult();
        }

        public string AsMessagable()
        {
            return $"<@&{Id}>";
        }

        public override string ToString()
        {
            return Name;
        }

        public static implicit operator ulong(DiscordRole instance)
        {
            return instance.Id;
        }
    }

    /// <summary>
    /// Tags associated with a <see cref="DiscordRole"/>.
    /// </summary>
    public class RoleTags
    {
        [JsonProperty("bot_id")]
        public ulong? BotId { get; private set; }

        [JsonProperty("integration_id")]
        public ulong? IntegrationId { get; private set; }

        [JsonProperty("premium_subscriber")]
        public bool? PremiumSubscriber { get; private set; }

        [JsonProperty("subscription_listing_id")]
        public ulong? SubscriptionListingId { get; private set; }

        [JsonProperty("available_for_purchase")]
        public bool? AvailableForPurchase { get; private set; }

        [JsonProperty("guild_connections")]
        public bool? GuildConnections { get; private set; }
    }
}
