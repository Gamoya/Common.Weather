using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamoya.Common.Weather.WeatherProviders.OpenWeatherMap {
    public class Wind {
        [RestSharp.Deserializers.DeserializeAs(Name = "speed")]
        [Newtonsoft.Json.JsonProperty("speed")]
        public decimal? Speed { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "deg")]
        [Newtonsoft.Json.JsonProperty("deg")]
        public decimal? Direction { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "gust")]
        [Newtonsoft.Json.JsonProperty("gust")]
        public decimal? Gust { get; set; }

    }
}
