namespace SocialSense.Parsers.Mapping
{
    using System;

    using Newtonsoft.Json;

    internal class TwitterMap
    {
        public TwitterMap()
        {
            this.Results = new TwittResult[0];
        }

        public TwittResult[] Results { get; set; }

        [JsonProperty("since_id")]
        public long SinceId { get; set; }
        [JsonProperty("max_id")]
        public long MaxId { get; set; }
        [JsonProperty("refresh_url")]
        public string RefreshUrl { get; set; }
        [JsonProperty("results_per_page")]
        public string ResultsPerPage { get; set; }
        [JsonProperty("next_page")]
        public string NextPage { get; set; }
        [JsonProperty("completed_in")]
        public double CompletedIn { get; set; }
        public int Page { get; set; }
        public string Query { get; set; }
        public string PreviousPage { get; set; }
        public int Total { get; set; }
        public string Warning { get; set; }
    }

    internal class TwittResult
    {
        public long Id { get; set; }
        public string Text { get; set; }

        public string Title
        {
            get { return this.ExtractTitle(this.Text); }
        }

        [JsonProperty("to_user")]
        public string ToUser { get; set; }
        [JsonProperty("to_user_id")]
        public string ToUserId { get; set; }
        [JsonProperty("from_user")]
        public string FromUser { get; set; }
        [JsonProperty("from_user_id")]
        public long FromUserId { get; set; }
        [JsonProperty("iso_language_code")]
        public string IsoLanguageCode { get; set; }
        public string Source { get; set; }
        [JsonProperty("profile_image_url")]
        public string ProfileImageUrl { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        private string ExtractTitle(string text)
        {
            return (text.Length > 20 && text.IndexOf(" ", 20, StringComparison.Ordinal) > 0)
                        ? text.Substring(0, text.IndexOf(" ", 20, StringComparison.CurrentCultureIgnoreCase))
                        : text;
        }
    }
}
