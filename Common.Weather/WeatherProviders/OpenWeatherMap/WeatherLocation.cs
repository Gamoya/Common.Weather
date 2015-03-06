using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamoya.Common.Weather.WeatherProviders.OpenWeatherMap {
    public class WeatherLocation {
        [RestSharp.Deserializers.DeserializeAs(Name = "country")]
        [Newtonsoft.Json.JsonProperty("country")]
        public string CountryCode{ get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "sunrise")]
        [Newtonsoft.Json.JsonProperty("sunrise")]
        public long? Sunrise { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "sunset")]
        [Newtonsoft.Json.JsonProperty("sunset")]
        public long? Sunset { get; set; }
    }
}
