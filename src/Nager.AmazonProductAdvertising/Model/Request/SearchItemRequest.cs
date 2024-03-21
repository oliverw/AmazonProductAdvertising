using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nager.AmazonProductAdvertising.Model.Request
{
    public class SearchItemRequest : AmazonRequest
    {
        public string? Keywords { get; set; }
        public string? Actor { get; set; }
        public string? Artist { get; set; }
        public string? Author { get; set; }
        public string? Brand { get; set; }
        public string? Title { get; set; }
        public int? ItemPage { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public SortBy? SortBy { get; set; }
        public string? BrowseNodeId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public SearchIndex? SearchIndex { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Condition? Condition { get; set; }
        public SearchItemRequest(string partnerTag, string partnerType, string marketplace, string? keywords = null, string? actor = null, string? artist = null, string? author = null, string? brand = null, string? title = null, SearchIndex? searchIndex = null, string? browseNodeId = null) : base(partnerTag, partnerType, marketplace)
        {
            Keywords = keywords;
            Actor = actor;
            Artist = artist;
            Author = author;
            Brand = brand;
            Title = title;
            SearchIndex = searchIndex;
            BrowseNodeId = browseNodeId;
            if(keywords is null && brand is null && title is null && searchIndex is null && actor is null && artist is null && author is null && browseNodeId is null)
            {
                throw new ArgumentNullException("At least one of Actor, Artist, Author, Brand, BrowseNodeId, Keywords, SearchIndex, Title should be provided.");
            }
        }
    }
}
