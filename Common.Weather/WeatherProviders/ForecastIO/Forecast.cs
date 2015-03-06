using System.Collections.Generic;

namespace Gamoya.Common.Weather.WeatherProviders.ForecastIO {
    public class Forecast<T> {
        [RestSharp.Deserializers.DeserializeAs(Name = "summary")]
        [Newtonsoft.Json.JsonProperty("summary")]
        public string Summary { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "icon")]
        [Newtonsoft.Json.JsonProperty("icon")]
        public string Icon { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "data")]
        [Newtonsoft.Json.JsonProperty("data")]
        public List<T> Data { get; set; }
    }
}
