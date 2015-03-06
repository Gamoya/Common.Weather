using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamoya.Common.Weather.WeatherProviders.OpenWeatherMap {
    public class Precipitation {
        [RestSharp.Deserializers.DeserializeAs(Name = "3h")]
        [Newtonsoft.Json.JsonProperty("3h")]
        public decimal? Last3Hours { get; set; }
    }
}
