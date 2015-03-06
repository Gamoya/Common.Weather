using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamoya.Common.Weather.WeatherProviders.OpenWeatherMap {
    public class Clouds {
        [RestSharp.Deserializers.DeserializeAs(Name = "all")]
        [Newtonsoft.Json.JsonProperty("all")]
        public decimal? CloudCover { get; set; }
    }
}
