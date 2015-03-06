
namespace Gamoya.Common.Weather.WeatherProviders.ForecastIO {
    public class DailyWeather {
        [RestSharp.Deserializers.DeserializeAs(Name = "time")]
        [Newtonsoft.Json.JsonProperty("time")]
        public long Time { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "summary")]
        [Newtonsoft.Json.JsonProperty("summary")]
        public string Summary { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "icon")]
        [Newtonsoft.Json.JsonProperty("icon")]
        public string Icon { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "sunriseTime")]
        [Newtonsoft.Json.JsonProperty("sunriseTime")]
        public long SunriseTime { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "sunsetTime")]
        [Newtonsoft.Json.JsonProperty("sunsetTime")]
        public long SunsetTime { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "precipIntensity")]
        [Newtonsoft.Json.JsonProperty("precipIntensity")]
        public decimal Precipitation { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "precipIntensityMax")]
        [Newtonsoft.Json.JsonProperty("precipIntensityMax")]
        public decimal MaxPrecipitation { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "precipProbability")]
        [Newtonsoft.Json.JsonProperty("precipProbability")]
        public decimal PrecipitationProbability { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "temperatureMin")]
        [Newtonsoft.Json.JsonProperty("temperatureMin")]
        public decimal MinTemperature { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "temperatureMinTime")]
        [Newtonsoft.Json.JsonProperty("temperatureMinTime")]
        public long MinTemperatureTime { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "temperatureMax")]
        [Newtonsoft.Json.JsonProperty("temperatureMax")]
        public decimal MaxTemperature { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "temperatureMaxTime")]
        [Newtonsoft.Json.JsonProperty("temperatureMaxTime")]
        public long MaxTemperatureTime { get; set; }
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
