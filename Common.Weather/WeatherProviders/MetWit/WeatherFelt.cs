
namespace Gamoya.Common.Weather.WeatherProviders.Metwit
{
    public class WeatherFelt
    {
        [RestSharp.Deserializers.DeserializeAs(Name = "temperature")]
        [Newtonsoft.Json.JsonProperty("temperature")]
        public decimal Temperature { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "intensity")]
        [Newtonsoft.Json.JsonProperty("intensity")]
        public decimal? Intensity { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "visibility")]
        [Newtonsoft.Json.JsonProperty("visibility")]
        public decimal? Visibility { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "humidity")]
        [Newtonsoft.Json.JsonProperty("humidity")]
        public decimal? Humidity { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "wind_intensity")]
        [Newtonsoft.Json.JsonProperty("wind_intensity")]
        public decimal? WindIntensity { get; set; }
    }
}
