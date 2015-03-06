using System.Collections.Generic;

namespace Gamoya.Common.Weather.WeatherProviders.Metwit {
    internal class WeatherResponse {
        [RestSharp.Deserializers.DeserializeAs(Name = "objects")]
        [Newtonsoft.Json.JsonProperty("objects")]
        public List<WeatherPoint> Objects { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "meta")]
        [Newtonsoft.Json.JsonProperty("meta")]
        public object Meta { get; set; }
    }
}
