namespace Discord.Gateway
{
    public class AutoModRuleEventArgs
    {
        public DiscordAutoModRule Rule { get; private set; }

        internal AutoModRuleEventArgs(DiscordAutoModRule rule)
        {
            Rule = rule;
        }

        public override string ToString()
        {
            return Rule.ToString();
        }
    }

    public class AutoModActionExecutionEventArgs
    {
        public AutoModActionExecution Execution { get; private set; }

        internal AutoModActionExecutionEventArgs(AutoModActionExecution execution)
        {
            Execution = execution;
        }
    }
}
