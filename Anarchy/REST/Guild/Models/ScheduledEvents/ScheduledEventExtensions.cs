using System.Collections.Generic;
using System.Threading.Tasks;

namespace Discord
{
    public static class ScheduledEventExtensions
    {
        /// <summary>
        /// Gets a guild's scheduled events. Optionally includes the number of users subscribed to each event.
        /// </summary>
        public static async Task<IReadOnlyList<DiscordScheduledEvent>> GetScheduledEventsAsync(this DiscordClient client, ulong guildId, bool withUserCount = false)
        {
            string qs = withUserCount ? "?with_user_count=true" : "";
            return (await client.HttpClient.GetAsync($"/guilds/{guildId}/scheduled-events{qs}")).Deserialize<List<DiscordScheduledEvent>>().SetClientsInList(client);
        }

        public static IReadOnlyList<DiscordScheduledEvent> GetScheduledEvents(this DiscordClient client, ulong guildId, bool withUserCount = false)
            => client.GetScheduledEventsAsync(guildId, withUserCount).GetAwaiter().GetResult();

        /// <summary>
        /// Gets a single scheduled event.
        /// </summary>
        public static async Task<DiscordScheduledEvent> GetScheduledEventAsync(this DiscordClient client, ulong guildId, ulong eventId, bool withUserCount = false)
        {
            string qs = withUserCount ? "?with_user_count=true" : "";
            return (await client.HttpClient.GetAsync($"/guilds/{guildId}/scheduled-events/{eventId}{qs}")).Deserialize<DiscordScheduledEvent>().SetClient(client);
        }

        public static DiscordScheduledEvent GetScheduledEvent(this DiscordClient client, ulong guildId, ulong eventId, bool withUserCount = false)
            => client.GetScheduledEventAsync(guildId, eventId, withUserCount).GetAwaiter().GetResult();

        /// <summary>
        /// Creates a scheduled event in a guild.
        /// </summary>
        public static async Task<DiscordScheduledEvent> CreateScheduledEventAsync(this DiscordClient client, ulong guildId, ScheduledEventProperties properties)
            => (await client.HttpClient.PostAsync($"/guilds/{guildId}/scheduled-events", properties)).Deserialize<DiscordScheduledEvent>().SetClient(client);

        public static DiscordScheduledEvent CreateScheduledEvent(this DiscordClient client, ulong guildId, ScheduledEventProperties properties)
            => client.CreateScheduledEventAsync(guildId, properties).GetAwaiter().GetResult();

        /// <summary>
        /// Modifies a scheduled event.
        /// </summary>
        public static async Task<DiscordScheduledEvent> ModifyScheduledEventAsync(this DiscordClient client, ulong guildId, ulong eventId, ScheduledEventProperties properties)
            => (await client.HttpClient.PatchAsync($"/guilds/{guildId}/scheduled-events/{eventId}", properties)).Deserialize<DiscordScheduledEvent>().SetClient(client);

        public static DiscordScheduledEvent ModifyScheduledEvent(this DiscordClient client, ulong guildId, ulong eventId, ScheduledEventProperties properties)
            => client.ModifyScheduledEventAsync(guildId, eventId, properties).GetAwaiter().GetResult();

        /// <summary>
        /// Deletes a scheduled event.
        /// </summary>
        public static async Task DeleteScheduledEventAsync(this DiscordClient client, ulong guildId, ulong eventId)
            => await client.HttpClient.DeleteAsync($"/guilds/{guildId}/scheduled-events/{eventId}");

        public static void DeleteScheduledEvent(this DiscordClient client, ulong guildId, ulong eventId)
            => client.DeleteScheduledEventAsync(guildId, eventId).GetAwaiter().GetResult();

        /// <summary>
        /// Gets the list of users subscribed to a scheduled event.
        /// </summary>
        public static async Task<IReadOnlyList<GuildMember>> GetScheduledEventUsersAsync(this DiscordClient client, ulong guildId, ulong eventId, int limit = 100)
            => (await client.HttpClient.GetAsync($"/guilds/{guildId}/scheduled-events/{eventId}/users?limit={limit}")).Deserialize<List<GuildMember>>().SetClientsInList(client);

        public static IReadOnlyList<GuildMember> GetScheduledEventUsers(this DiscordClient client, ulong guildId, ulong eventId, int limit = 100)
            => client.GetScheduledEventUsersAsync(guildId, eventId, limit).GetAwaiter().GetResult();
    }
}
