namespace SocialSense.Parsers.Mapping
{
    using System;

    using Newtonsoft.Json;

    internal class DiggMap
    {
        public DiggMap()
        {
            this.Stories = new DiggStory[0];
        }

        [JsonProperty("stories")]
        public DiggStory[] Stories { get; set; }
    }

    internal class DiggStory
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("href")]
        public string Link { get; set; }

        [JsonIgnore]
        public DateTime CreatedTime
        {
            get
            {
                return DateTime.Now;
            }
        }

        [JsonProperty("user")]
        public DiggUser User { get; set; }
    }

    internal class DiggUser
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
