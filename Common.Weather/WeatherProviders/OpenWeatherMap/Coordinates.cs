using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamoya.Common.Weather.WeatherProviders.OpenWeatherMap {
    public class Coordinates {
        [RestSharp.Deserializers.DeserializeAs(Name = "lat")]
        [Newtonsoft.Json.JsonProperty("lat")]
        public decimal Latitude { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "lon")]
        [Newtonsoft.Json.JsonProperty("lon")]
        public decimal Longitude { get; set; }
    }
}
