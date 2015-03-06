using System.Collections.Generic;

namespace Gamoya.Common.Weather.WeatherProviders.ForecastIO {
    public class Flags {
        [RestSharp.Deserializers.DeserializeAs(Name = "sources")]
        [Newtonsoft.Json.JsonProperty("sources")]
        public List<string> Sources { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "isd-stations")]
        [Newtonsoft.Json.JsonProperty("isd-stations")]
        public List<string> IsdStations { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "lamp-stations")]
        [Newtonsoft.Json.JsonProperty("lamp-stations")]
        public List<string> LampStations { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "metar-stations")]
        [Newtonsoft.Json.JsonProperty("metar-stations")]
        public List<string> MetarStations { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "darksky-stations")]
        [Newtonsoft.Json.JsonProperty("darksky-stations")]
        public List<string> DarkskyStations { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "units")]
        [Newtonsoft.Json.JsonProperty("units")]
        public string Units { get; set; }
    }
}
