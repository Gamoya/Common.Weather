using System.Collections.Generic;

namespace Gamoya.Common.Weather.WeatherProviders.ForecastIO {
    public class WeatherResponse {
        [RestSharp.Deserializers.DeserializeAs(Name = "latitude")]
        [Newtonsoft.Json.JsonProperty("latitude")]
        public decimal Latitude { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "longitude")]
        [Newtonsoft.Json.JsonProperty("longitude")]
        public decimal Longitude { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "timezone")]
        [Newtonsoft.Json.JsonProperty("timezone")]
        public string Timezone { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "offset")]
        [Newtonsoft.Json.JsonProperty("offset")]
        public int Offset { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "currently")]
        [Newtonsoft.Json.JsonProperty("currently")]
        public Weather CurrentWeather { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "minutely")]
        [Newtonsoft.Json.JsonProperty("minutely")]
        public Forecast<MinutelyWeather> MinutelyForecast { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "hourly")]
        [Newtonsoft.Json.JsonProperty("hourly")]
        public Forecast<Weather> HourlyForecast { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "daily")]
        [Newtonsoft.Json.JsonProperty("daily")]
        public Forecast<DailyWeather> DailyForecast { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "alerts")]
        [Newtonsoft.Json.JsonProperty("alerts")]
        public List<Alert> Alerts { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "flags")]
        [Newtonsoft.Json.JsonProperty("flags")]
        public Flags Flags { get; set; }
    }
}
