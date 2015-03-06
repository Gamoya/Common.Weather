using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamoya.Common.Weather.WeatherProviders.OpenWeatherMap {
    public class Weather {
        [RestSharp.Deserializers.DeserializeAs(Name = "id")]
        [Newtonsoft.Json.JsonProperty("id")]
        public string Id { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "main")]
        [Newtonsoft.Json.JsonProperty("main")]
        public string Main { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "description")]
        [Newtonsoft.Json.JsonProperty("description")]
        public string Description { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "icon")]
        [Newtonsoft.Json.JsonProperty("icon")]
        public string Icon { get; set; }

    }
}
