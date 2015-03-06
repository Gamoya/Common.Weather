using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamoya.Common.Weather.WeatherProviders.OpenWeatherMap {
    public class DailyWeather {
        [RestSharp.Deserializers.DeserializeAs(Name = "dt")]
        [Newtonsoft.Json.JsonProperty("dt")]
        public long? Timestamp { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "temp")]
        [Newtonsoft.Json.JsonProperty("temp")]
        public DailyTemperature DailyTemperature { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "pressure")]
        [Newtonsoft.Json.JsonProperty("pressure")]
        public decimal? Pressure { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "humidity")]
        [Newtonsoft.Json.JsonProperty("humidity")]
        public decimal? Humidity { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "weather")]
        [Newtonsoft.Json.JsonProperty("weather")]
        public List<Weather> WeatherConditions { get; set; }
    }
}
