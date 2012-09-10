namespace SocialSense.Spiders.Behaviors
{
    using System;

    public class GoogleUserAgentBehavior : Behavior
    {
        private readonly string[] agents = 
        {
            "( Robots.txt Validator http://www.searchengineworld.com/cgi-bin/robotcheck.cgi )"
        };

        public override void Execute()
        {
            string randomAgent = this.agents[new Random().Next(this.agents.Length - 1)];
            this.Spider.HttpWebRequest.UserAgent = randomAgent;
        }
    }
}
