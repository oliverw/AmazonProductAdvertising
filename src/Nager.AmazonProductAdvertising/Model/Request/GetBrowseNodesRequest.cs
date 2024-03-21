using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nager.AmazonProductAdvertising.Model.Request
{
    public class GetBrowseNodesRequest : AmazonRequest
    {
        public string[] BrowseNodeIds { get; set; }

        [JsonProperty(ItemConverterType = typeof(StringEnumConverter))]
        public LanguageCodes[] LanguagesOfPreference { get; set; }
        public GetBrowseNodesRequest(string[] browseNodeIds, string partnerTag, string partnerType, string marketplace) : base(partnerTag, partnerType, marketplace)
        {
            BrowseNodeIds = browseNodeIds;
        }

    }
}