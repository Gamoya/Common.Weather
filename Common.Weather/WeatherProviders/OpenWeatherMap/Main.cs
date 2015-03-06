using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamoya.Common.Weather.WeatherProviders.OpenWeatherMap {
    public class Main {
        [RestSharp.Deserializers.DeserializeAs(Name = "temp")]
        [Newtonsoft.Json.JsonProperty("temp")]
        public decimal? Temperature { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "humidity")]
        [Newtonsoft.Json.JsonProperty("humidity")]
        public decimal? Humidity { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "pressure")]
        [Newtonsoft.Json.JsonProperty("pressure")]
        public decimal? Pressure { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "sea_level")]
        [Newtonsoft.Json.JsonProperty("sea_level")]
        public decimal? SeaLevel { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "grnd_level")]
        [Newtonsoft.Json.JsonProperty("grnd_level")]
        public decimal? GroundLevel { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "temp_min")]
        [Newtonsoft.Json.JsonProperty("temp_min")]
        public decimal? MinimumTemperature { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "temp_max")]
        [Newtonsoft.Json.JsonProperty("temp_max")]
        public decimal? MaximumTemperature { get; set; }

    }
}
