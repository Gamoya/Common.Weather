using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamoya.Common.Weather.WeatherProviders.OpenWeatherMap {
    public class City {
        [RestSharp.Deserializers.DeserializeAs(Name = "id")]
        [Newtonsoft.Json.JsonProperty("id")]
        public string Id { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "name")]
        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "country")]
        [Newtonsoft.Json.JsonProperty("country")]
        public string CountryCode { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "coord")]
        [Newtonsoft.Json.JsonProperty("coord")]
        public Coordinates Coordinates { get; set; }
    }
}
