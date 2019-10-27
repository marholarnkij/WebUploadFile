using Newtonsoft.Json;
namespace Common.Lib.Entities.ReturnModels
{
    public class JournalReturnModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "Payment")]
        public string Payment { get; set; }
        [JsonProperty(PropertyName = "Status")]
        public string Status { get; set; }
    }
}
