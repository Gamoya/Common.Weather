using System.Collections.Generic;

namespace Gamoya.Common.Weather.WeatherProviders.Metwit.GeoJson
{
    public class Point
    {
        [RestSharp.Deserializers.DeserializeAs(Name = "type")]
        public string Type { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "coordinates")]
        public List<decimal> Coordinates { get; set; }
        //public List<Position> Coordinates { get; set; }
    }
}
