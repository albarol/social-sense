namespace SocialSense.Parsers.Mapping
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    internal class FacebookMap
    {
        public FacebookMap()
        {
            this.Posts = new List<FacebookPost>();
        }

        [JsonProperty("data")]
        public IList<FacebookPost> Posts { get; set; }
    }

    internal class FacebookPost
    {
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("from")]
        public FacebookUser Author { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("caption")]
        public string Caption { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("created_time")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime CreatedTime { get; set; }
    }

    internal class FacebookUser
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
