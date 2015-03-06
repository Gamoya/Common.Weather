using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamoya.Common.Weather.WeatherProviders.OpenWeatherMap {
    public class HourlyWeatherResponse {
        [RestSharp.Deserializers.DeserializeAs(Name = "city")]
        [Newtonsoft.Json.JsonProperty("city")]
        public City City { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "cnt")]
        [Newtonsoft.Json.JsonProperty("cnt")]
        public int Count { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "list")]
        [Newtonsoft.Json.JsonProperty("list")]
        public List<HourlyWeather> Weather { get; set; }
    }
}
