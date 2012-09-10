namespace SocialSense.Spiders.Behaviors
{
    using System;
    using System.Threading;

    public class RandomDelayBehavior : Behavior
    {
        private readonly TimeSpan initialTime, finalTime;

        public RandomDelayBehavior(int initialSeconds, int finalSeconds)
        {
            if (finalSeconds < initialSeconds)
            {
                throw new ArgumentException("initialInterval should be less than finalInterval");
            }

            this.initialTime = TimeSpan.FromSeconds(initialSeconds);
            this.finalTime = TimeSpan.FromSeconds(finalSeconds);
        }

        public RandomDelayBehavior(TimeSpan initialInvertal, TimeSpan finalInterval)
        {
            if (finalInterval < initialInvertal)
            {
                throw new ArgumentException("initialInterval should be less than finalInterval");
            }

            this.initialTime = initialInvertal;
            this.finalTime = finalInterval;
        }

        public override void Execute()
        {
            int initialMilliseconds = Convert.ToInt32(this.initialTime.TotalMilliseconds);
            int finalMilliseconds = Convert.ToInt32(this.finalTime.TotalMilliseconds);
            Thread.Sleep(new Random().Next(initialMilliseconds, finalMilliseconds));
        }
    }
}
