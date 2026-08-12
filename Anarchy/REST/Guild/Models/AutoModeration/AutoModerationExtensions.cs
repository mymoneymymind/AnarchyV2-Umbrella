using System.Collections.Generic;
using System.Threading.Tasks;

namespace Discord
{
    public static class AutoModerationExtensions
    {
        /// <summary>
        /// Lists a guild's Auto Moderation rules.
        /// </summary>
        public static async Task<IReadOnlyList<DiscordAutoModRule>> GetAutoModRulesAsync(this DiscordClient client, ulong guildId)
            => (await client.HttpClient.GetAsync($"/guilds/{guildId}/auto-moderation/rules")).Deserialize<List<DiscordAutoModRule>>().SetClientsInList(client);

        public static IReadOnlyList<DiscordAutoModRule> GetAutoModRules(this DiscordClient client, ulong guildId)
            => client.GetAutoModRulesAsync(guildId).GetAwaiter().GetResult();

        /// <summary>
        /// Gets a single Auto Moderation rule.
        /// </summary>
        public static async Task<DiscordAutoModRule> GetAutoModRuleAsync(this DiscordClient client, ulong guildId, ulong ruleId)
            => (await client.HttpClient.GetAsync($"/guilds/{guildId}/auto-moderation/rules/{ruleId}")).Deserialize<DiscordAutoModRule>().SetClient(client);

        public static DiscordAutoModRule GetAutoModRule(this DiscordClient client, ulong guildId, ulong ruleId)
            => client.GetAutoModRuleAsync(guildId, ruleId).GetAwaiter().GetResult();

        /// <summary>
        /// Creates an Auto Moderation rule.
        /// </summary>
        public static async Task<DiscordAutoModRule> CreateAutoModRuleAsync(this DiscordClient client, ulong guildId, AutoModRuleProperties properties)
            => (await client.HttpClient.PostAsync($"/guilds/{guildId}/auto-moderation/rules", properties)).Deserialize<DiscordAutoModRule>().SetClient(client);

        public static DiscordAutoModRule CreateAutoModRule(this DiscordClient client, ulong guildId, AutoModRuleProperties properties)
            => client.CreateAutoModRuleAsync(guildId, properties).GetAwaiter().GetResult();

        /// <summary>
        /// Modifies an Auto Moderation rule.
        /// </summary>
        public static async Task<DiscordAutoModRule> ModifyAutoModRuleAsync(this DiscordClient client, ulong guildId, ulong ruleId, AutoModRuleProperties properties)
            => (await client.HttpClient.PatchAsync($"/guilds/{guildId}/auto-moderation/rules/{ruleId}", properties)).Deserialize<DiscordAutoModRule>().SetClient(client);

        public static DiscordAutoModRule ModifyAutoModRule(this DiscordClient client, ulong guildId, ulong ruleId, AutoModRuleProperties properties)
            => client.ModifyAutoModRuleAsync(guildId, ruleId, properties).GetAwaiter().GetResult();

        /// <summary>
        /// Deletes an Auto Moderation rule.
        /// </summary>
        public static async Task DeleteAutoModRuleAsync(this DiscordClient client, ulong guildId, ulong ruleId)
            => await client.HttpClient.DeleteAsync($"/guilds/{guildId}/auto-moderation/rules/{ruleId}");

        public static void DeleteAutoModRule(this DiscordClient client, ulong guildId, ulong ruleId)
            => client.DeleteAutoModRuleAsync(guildId, ruleId).GetAwaiter().GetResult();
    }
}
