
namespace Gamoya.Common.Weather.WeatherProviders.ForecastIO
{
    public class MinutelyWeather
    {
        [RestSharp.Deserializers.DeserializeAs(Name = "time")]
        [Newtonsoft.Json.JsonProperty("time")]
        public long Time { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "precipIntensity")]
        [Newtonsoft.Json.JsonProperty("precipIntensity")]
        public decimal Precipitation { get; set; }
    }
}
