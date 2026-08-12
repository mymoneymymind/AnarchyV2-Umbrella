namespace Discord.Gateway
{
    public class ScheduledEventEventArgs
    {
        public DiscordScheduledEvent Event { get; private set; }

        internal ScheduledEventEventArgs(DiscordScheduledEvent ev)
        {
            Event = ev;
        }

        public override string ToString()
        {
            return Event.ToString();
        }
    }

    public class ScheduledEventUserEventArgs
    {
        public ulong GuildId { get; private set; }
        public DiscordScheduledEvent Event { get; private set; }
        public GuildMember User { get; private set; }

        internal ScheduledEventUserEventArgs(ulong guildId, DiscordScheduledEvent ev, GuildMember user)
        {
            GuildId = guildId;
            Event = ev;
            User = user;
        }
    }
}
