using System.Collections.Generic;
using System.Threading.Tasks;

namespace Discord
{
    public static class SoundboardExtensions
    {
        /// <summary>
        /// Gets all soundboard sounds for a guild.
        /// </summary>
        public static async Task<IReadOnlyList<DiscordSoundboardSound>> GetSoundboardSoundsAsync(this DiscordClient client, ulong guildId)
            => (await client.HttpClient.GetAsync($"/guilds/{guildId}/soundboard-sounds")).Deserialize<List<DiscordSoundboardSound>>().SetClientsInList(client);

        public static IReadOnlyList<DiscordSoundboardSound> GetSoundboardSounds(this DiscordClient client, ulong guildId)
            => client.GetSoundboardSoundsAsync(guildId).GetAwaiter().GetResult();

        /// <summary>
        /// Gets a single soundboard sound by id.
        /// </summary>
        public static async Task<DiscordSoundboardSound> GetSoundboardSoundAsync(this DiscordClient client, ulong soundId)
            => (await client.HttpClient.GetAsync($"/soundboard-sounds/{soundId}")).Deserialize<DiscordSoundboardSound>().SetClient(client);

        public static DiscordSoundboardSound GetSoundboardSound(this DiscordClient client, ulong soundId)
            => client.GetSoundboardSoundAsync(soundId).GetAwaiter().GetResult();

        /// <summary>
        /// Creates a soundboard sound in a guild.
        /// </summary>
        public static async Task<DiscordSoundboardSound> CreateSoundboardSoundAsync(this DiscordClient client, ulong guildId, SoundboardSoundProperties properties)
            => (await client.HttpClient.PostAsync($"/guilds/{guildId}/soundboard-sounds", properties)).Deserialize<DiscordSoundboardSound>().SetClient(client);

        public static DiscordSoundboardSound CreateSoundboardSound(this DiscordClient client, ulong guildId, SoundboardSoundProperties properties)
            => client.CreateSoundboardSoundAsync(guildId, properties).GetAwaiter().GetResult();

        /// <summary>
        /// Modifies a soundboard sound.
        /// </summary>
        public static async Task<DiscordSoundboardSound> ModifySoundboardSoundAsync(this DiscordClient client, ulong guildId, ulong soundId, SoundboardSoundProperties properties)
            => (await client.HttpClient.PatchAsync($"/guilds/{guildId}/soundboard-sounds/{soundId}", properties)).Deserialize<DiscordSoundboardSound>().SetClient(client);

        public static DiscordSoundboardSound ModifySoundboardSound(this DiscordClient client, ulong guildId, ulong soundId, SoundboardSoundProperties properties)
            => client.ModifySoundboardSoundAsync(guildId, soundId, properties).GetAwaiter().GetResult();

        /// <summary>
        /// Deletes a soundboard sound.
        /// </summary>
        public static async Task DeleteSoundboardSoundAsync(this DiscordClient client, ulong guildId, ulong soundId)
            => await client.HttpClient.DeleteAsync($"/guilds/{guildId}/soundboard-sounds/{soundId}");

        public static void DeleteSoundboardSound(this DiscordClient client, ulong guildId, ulong soundId)
            => client.DeleteSoundboardSoundAsync(guildId, soundId).GetAwaiter().GetResult();

        /// <summary>
        /// Sends a soundboard sound to a channel (plays it).
        /// </summary>
        public static async Task SendSoundboardSoundAsync(this DiscordClient client, ulong channelId, ulong soundId, bool? force = null)
        {
            string qs = force.HasValue ? $"?force={(force.Value ? "true" : "false")}" : "";
            await client.HttpClient.PostAsync($"/channels/{channelId}/send-soundboard-sound{qs}", new { sound_id = soundId });
        }

        public static void SendSoundboardSound(this DiscordClient client, ulong channelId, ulong soundId, bool? force = null)
            => client.SendSoundboardSoundAsync(channelId, soundId, force).GetAwaiter().GetResult();
    }
}
