using System.Collections.Generic;

namespace Gamoya.Common.Weather.WeatherProviders.OpenWeatherMap {
    public class CurrentWeatherResponse {
        [RestSharp.Deserializers.DeserializeAs(Name = "id")]
        [Newtonsoft.Json.JsonProperty("id")]
        public string CityId { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "name")]
        [Newtonsoft.Json.JsonProperty("name")]
        public string CityName { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "dt")]
        [Newtonsoft.Json.JsonProperty("dt")]
        public long? Timestamp { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "coord")]
        [Newtonsoft.Json.JsonProperty("coord")]
        public Coordinates Coordinates { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "sys")]
        [Newtonsoft.Json.JsonProperty("sys")]
        public WeatherLocation WeatherLocation { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "main")]
        [Newtonsoft.Json.JsonProperty("main")]
        public Main Main { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "wind")]
        [Newtonsoft.Json.JsonProperty("wind")]
        public Wind Wind { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "rain")]
        [Newtonsoft.Json.JsonProperty("rain")]
        public Precipitation Rain { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "snow")]
        [Newtonsoft.Json.JsonProperty("snow")]
        public Precipitation Snow { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "clouds")]
        [Newtonsoft.Json.JsonProperty("clouds")]
        public Clouds Clouds { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "weather")]
        [Newtonsoft.Json.JsonProperty("weather")]
        public List<Weather> WeatherConditions { get; set; }

    }
}
