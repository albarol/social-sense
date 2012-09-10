namespace SocialSense.Spiders.Behaviors
{
    using SocialSense.Spiders;

    public abstract class Behavior
    {
        internal Spider Spider { get; set; }
        public abstract void Execute();
    }
}
