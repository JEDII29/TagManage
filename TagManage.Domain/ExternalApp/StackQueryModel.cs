using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TagManage.Domain.ExternalApp
{
    public class StackQueryModel
    {
        [JsonPropertyName("items")]
        public List<StackTagModel> Items { get; set; }
    }

    public class StackTagModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("count")]
        public int Count { get; set; }
        [JsonPropertyName("is_required")]
        public bool IsRequired { get; set; }
        [JsonPropertyName("is_moderator_only")]
        public bool IsModeratorOnly { get; set; }
        [JsonPropertyName("has_synonyms")]
        public bool HasSynonyms { get; set; }
    }
}
