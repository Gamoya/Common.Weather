
namespace Gamoya.Common.Weather.WeatherProviders.ForecastIO
{
    public class Weather
    {
        [RestSharp.Deserializers.DeserializeAs(Name = "time")]
        [Newtonsoft.Json.JsonProperty("time")]
        public long Time { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "summary")]
        [Newtonsoft.Json.JsonProperty("summary")]
        public string Summary { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "icon")]
        [Newtonsoft.Json.JsonProperty("icon")]
        public string Icon { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "precipIntensity")]
        [Newtonsoft.Json.JsonProperty("precipIntensity")]
        public decimal Precipitation { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "temperature")]
        [Newtonsoft.Json.JsonProperty("temperature")]
        public decimal Temperature { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "dewPoint")]
        [Newtonsoft.Json.JsonProperty("dewPoint")]
        public decimal DewPoint { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "windSpeed")]
        [Newtonsoft.Json.JsonProperty("windSpeed")]
        public decimal WindSpeed { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "windBearing")]
        [Newtonsoft.Json.JsonProperty("windBearing")]
        public decimal WindDirection { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "cloudCover")]
        [Newtonsoft.Json.JsonProperty("cloudCover")]
        public decimal CloudCover { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "humidity")]
        [Newtonsoft.Json.JsonProperty("humidity")]
        public decimal Humidity { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "pressure")]
        [Newtonsoft.Json.JsonProperty("pressure")]
        public decimal Pressure { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "visibility")]
        [Newtonsoft.Json.JsonProperty("visibility")]
        public decimal Visibility { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "ozone")]
        [Newtonsoft.Json.JsonProperty("ozone")]
        public decimal Ozone { get; set; }
    }
}
