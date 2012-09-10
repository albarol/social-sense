namespace SocialSense.Spiders.Behaviors
{
    using System;

    public class RandomUserAgentBehavior : Behavior
    {
        private readonly string[] agents = 
        {
            "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0; SLCC1; .NET CLR 2.0.50727; .NET CLR 3.0.30729; OfficeLiveConnector.1.4; OfficeLivePath.1.3; .NET4.0C; .NET 4.0E; .NET CLR 3.5.21022; .NET CLR 3.5.30729",
            "Mozilla/5.0 (Windows; U; Windows NT 5.2; pt-BR) AppleWebKit/534.4 (KHTML, like Gecko) Chrome/6.0.481.0 Safari/534.4",
            "Mozilla/5.0 (Windows; U; Windows NT 5.1; pt-BR; rv:1.9.2.7) Gecko/20100713 Firefox/3.6.7 (.NET CLR 3.5.30729)",
            "Mozilla/5.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; InfoPath.2; SLCC1; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET CLR 2.0.50727; pt-BR)",
        };

        public override void Execute()
        {
            string randomAgent = this.agents[new Random().Next(this.agents.Length - 1)];
            this.Spider.HttpWebRequest.UserAgent = randomAgent;
        }
    }
}
