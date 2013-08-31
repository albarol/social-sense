namespace SocialSense.Spiders
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;

    using SocialSense.Spiders.Behaviors;

    public class Spider
    {
        private readonly List<Behavior> behaviors = new List<Behavior>();

        public Spider()
        {
            this.TimeOut = 15;
        }

        public int TimeOut { get; set; }
        internal HttpWebRequest HttpWebRequest { get; set; }

        public virtual string DownloadContent(string url)
        {
            this.HttpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            foreach (var behavior in this.behaviors)
            {
                behavior.Execute();
            }
            this.HttpWebRequest.Timeout = (int)TimeSpan.FromSeconds(this.TimeOut).TotalMilliseconds;

            var webResponse = (HttpWebResponse)this.HttpWebRequest.GetResponse();

            StreamReader reader = string.IsNullOrEmpty(webResponse.CharacterSet) ?
                                  new StreamReader(webResponse.GetResponseStream()) :
                                  new StreamReader(webResponse.GetResponseStream(), Encoding.GetEncoding(webResponse.CharacterSet));
            return reader.ReadToEnd();
        }

        public void AddBehavior(params Behavior[] behaviors)
        {
            foreach (var behavior in behaviors)
            {
                behavior.Spider = this;
                this.behaviors.Add(behavior);
            }
        }
    }
}
