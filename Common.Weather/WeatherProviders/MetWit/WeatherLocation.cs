
namespace Gamoya.Common.Weather.WeatherProviders.Metwit
{
    public class WeatherLocation
    {
        [RestSharp.Deserializers.DeserializeAs(Name = "country")]
        [Newtonsoft.Json.JsonProperty("country")]
        public string Country { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "locality")]
        [Newtonsoft.Json.JsonProperty("locality")]
        public string Locality { get; set; }
    }
}
