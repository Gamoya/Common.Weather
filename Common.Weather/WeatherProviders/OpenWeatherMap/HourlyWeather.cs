using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamoya.Common.Weather.WeatherProviders.OpenWeatherMap {
    public class HourlyWeather {
        [RestSharp.Deserializers.DeserializeAs(Name = "dt")]
        [Newtonsoft.Json.JsonProperty("dt")]
        public long? Timestamp { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "main")]
        [Newtonsoft.Json.JsonProperty("main")]
        public Main Main { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "weather")]
        [Newtonsoft.Json.JsonProperty("weather")]
        public List<Weather> WeatherConditions { get; set; }
    }
}
