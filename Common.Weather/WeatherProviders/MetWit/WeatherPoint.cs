using System;
using System.Collections.Generic;

namespace Gamoya.Common.Weather.WeatherProviders.Metwit {
    public class WeatherPoint {
        [RestSharp.Deserializers.DeserializeAs(Name = "timestamp")]
        [Newtonsoft.Json.JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "weather")]
        [Newtonsoft.Json.JsonProperty("weather")]
        public Weather Weather { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "geo")]
        [Newtonsoft.Json.JsonProperty("geo")]
        public GeoJson.Point Geo { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "icon")]
        [Newtonsoft.Json.JsonProperty("icon")]
        public string Icon { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "location")]
        [Newtonsoft.Json.JsonProperty("location")]
        public WeatherLocation Location { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "sun_altitude")]
        [Newtonsoft.Json.JsonProperty("sun_altitude")]
        public decimal SunAltitude { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "sources")]
        [Newtonsoft.Json.JsonProperty("sources")]
        public List<string> Sources { get; set; }
    }
}
