using System.Xml.Serialization;

namespace Gamoya.Common.Weather.WeatherProviders.WorldWeatherOnline
{
    public class WeatherRequest
    {
        [XmlElement("query")]
        public string Query { get; set; }
        [XmlElement("type")]
        public string Type { get; set; }
    }
}
