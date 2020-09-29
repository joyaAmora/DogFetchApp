using Newtonsoft.Json;
using System.Collections.Generic;

namespace ApiHelper.Models
{
    public class BreedModel
    {
        [JsonProperty("Message")]
        public Dictionary<string, List<string>> Breeds { get; set; }
    }
}
