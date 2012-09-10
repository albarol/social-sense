namespace SocialSense.Spiders.Behaviors
{
    using System;
    using System.Threading;

    public class DelayBehavior : Behavior
    {
        private readonly TimeSpan interval;

        public DelayBehavior(TimeSpan interval)
        {
            this.interval = interval;
        }

        public override void Execute()
        {
            Thread.Sleep(this.interval);
        }
    }
}
