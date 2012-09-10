namespace SocialSense.Parsers.Mapping
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    internal class GooglePlusMap
    {
        public GooglePlusMap()
        {
            this.Items = new ItemResult[0];
        }

        [JsonProperty("nextPageToken")]
        public string NextPageToken { get; set; }
        [JsonProperty("items")]
        public ItemResult[] Items { get; set; }
    }

    internal class ItemResult
    {
        [JsonProperty("title")]
        private string title;
        [JsonProperty("published")]
        public DateTime Published { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("actor")]
        public Actor Actor { get; set; }
        [JsonProperty("object")]
        public ResultObject Object { get; set; }

        public string Title
        {
            get
            {
                if (string.IsNullOrEmpty(this.title))
                {
                    this.title = (this.Object.Content.Length > 50)
                                 ? this.Object.Content.Substring(0, 50)
                                 : this.Object.Content;
                }

                return this.title;
            }
        }
    }

    internal struct ResultObject
    {
        [JsonProperty("content")]
        private string content;
        [JsonProperty("objectType")]
        public string Type { get; set; }
        [JsonProperty("attachments")]
        public IList<Attachment> Attachments { get; set; }

        public string Content
        {
            get
            {
                if (!string.IsNullOrEmpty(this.content))
                {
                    return this.content;
                }

                string attachmentContent = string.Empty;

                foreach (var attachment in this.Attachments)
                {
                    if (!attachment.Type.Equals("photo") && !string.IsNullOrEmpty(attachment.Content)
                        && attachment.Content.Length > attachmentContent.Length)
                    {
                        attachmentContent = attachment.Content;
                    }
                }

                return attachmentContent;
            }
        }
    }

    internal struct Attachment
    {
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("objectType")]
        public string Type { get; set; }
    }

    internal struct Actor
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
    }
}
