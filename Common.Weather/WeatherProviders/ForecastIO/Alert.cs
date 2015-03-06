
namespace Gamoya.Common.Weather.WeatherProviders.ForecastIO {
    public class Alert {
        [RestSharp.Deserializers.DeserializeAs(Name = "title")]
        [Newtonsoft.Json.JsonProperty("title")]
        public string Title { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "expires")]
        [Newtonsoft.Json.JsonProperty("expires")]
        public long Expires { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "uri")]
        [Newtonsoft.Json.JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
