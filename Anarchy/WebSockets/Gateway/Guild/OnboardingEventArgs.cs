namespace Discord.Gateway
{
    public class OnboardingEventArgs
    {
        public DiscordOnboarding Onboarding { get; private set; }

        internal OnboardingEventArgs(DiscordOnboarding onboarding)
        {
            Onboarding = onboarding;
        }

        public override string ToString()
        {
            return Onboarding.ToString();
        }
    }
}
