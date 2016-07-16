﻿using Newtonsoft.Json;

namespace BitBucket.REST.API.Models
{
    public class Link
    {
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}