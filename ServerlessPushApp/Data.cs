using Newtonsoft.Json;

namespace ServerlessPushApp
{
    public class Data
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("amount")]
        public int Amount { get; set; }
        [JsonProperty("sensorId")]
        public string SensorId { get; set; }
    }
}
