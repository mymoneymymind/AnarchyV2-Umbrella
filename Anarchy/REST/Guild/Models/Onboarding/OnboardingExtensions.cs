using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Discord
{
    public static class OnboardingExtensions
    {
        /// <summary>
        /// Gets the guild's onboarding configuration.
        /// </summary>
        public static async Task<DiscordOnboarding> GetOnboardingAsync(this DiscordClient client, ulong guildId)
            => (await client.HttpClient.GetAsync($"/guilds/{guildId}/onboarding")).Deserialize<DiscordOnboarding>().SetClient(client);

        public static DiscordOnboarding GetOnboarding(this DiscordClient client, ulong guildId)
            => client.GetOnboardingAsync(guildId).GetAwaiter().GetResult();

        /// <summary>
        /// Modifies the guild's onboarding configuration.
        /// </summary>
        public static async Task<DiscordOnboarding> ModifyOnboardingAsync(this DiscordClient client, ulong guildId, OnboardingProperties properties)
            => (await client.HttpClient.PutAsync($"/guilds/{guildId}/onboarding", properties)).Deserialize<DiscordOnboarding>().SetClient(client);

        public static DiscordOnboarding ModifyOnboarding(this DiscordClient client, ulong guildId, OnboardingProperties properties)
            => client.ModifyOnboardingAsync(guildId, properties).GetAwaiter().GetResult();
    }
}
