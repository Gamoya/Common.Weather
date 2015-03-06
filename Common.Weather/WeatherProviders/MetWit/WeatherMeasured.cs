
namespace Gamoya.Common.Weather.WeatherProviders.Metwit {
    public class WeatherMeasured {
        [RestSharp.Deserializers.DeserializeAs(Name = "temperature")]
        [Newtonsoft.Json.JsonProperty("temperature")]
        public decimal? Temperature { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "wind_chill")]
        [Newtonsoft.Json.JsonProperty("wind_chill")]
        public decimal? WindChill { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "humidity")]
        [Newtonsoft.Json.JsonProperty("humidity")]
        public decimal? Humidity { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "dew_point")]
        [Newtonsoft.Json.JsonProperty("dew_point")]
        public decimal? DewPoint { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "visibility")]
        [Newtonsoft.Json.JsonProperty("visibility")]
        public decimal? Visibility { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "wind_speed")]
        [Newtonsoft.Json.JsonProperty("wind_speed")]
        public decimal? WindSpeed { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "wind_direction")]
        [Newtonsoft.Json.JsonProperty("wind_direction")]
        public decimal? WindDirection { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "pressure")]
        [Newtonsoft.Json.JsonProperty("pressure")]
        public decimal? Pressure { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "rainfall")]
        [Newtonsoft.Json.JsonProperty("rainfall")]
        public decimal? Rainfall { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "snowfall")]
        [Newtonsoft.Json.JsonProperty("snowfall")]
        public decimal? Snowfall { get; set; }
    }
}
