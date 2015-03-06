using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamoya.Common.Weather.WeatherProviders.OpenWeatherMap {
    public class DailyTemperature {
        [RestSharp.Deserializers.DeserializeAs(Name = "day")]
        [Newtonsoft.Json.JsonProperty("day")]
        public decimal? Day { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "min")]
        [Newtonsoft.Json.JsonProperty("min")]
        public decimal? Minimum { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "max")]
        [Newtonsoft.Json.JsonProperty("max")]
        public decimal? Maximum { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "night")]
        [Newtonsoft.Json.JsonProperty("night")]
        public decimal? Night { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "eve")]
        [Newtonsoft.Json.JsonProperty("eve")]
        public decimal? Evening { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "morn")]
        [Newtonsoft.Json.JsonProperty("morn")]
        public decimal? Morning { get; set; }
    }
}
