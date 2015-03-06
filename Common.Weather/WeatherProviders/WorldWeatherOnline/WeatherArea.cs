using System.Xml.Serialization;

namespace Gamoya.Common.Weather.WeatherProviders.WorldWeatherOnline
{
    public class WeatherArea
    {
        [XmlElement("areaName")]
        public string AreaName { get; set; }
        [XmlElement("country")]
        public string Country { get; set; }
        [XmlElement("region")]
        public string Region { get; set; }
        [XmlElement("weatherUrl")]
        public string WeatherUrl { get; set; }
        [XmlElement("population")]
        public int? Population { get; set; }
        [XmlElement("latitude")]
        public decimal? Latitude { get; set; }
        [XmlElement("longitude")]
        public decimal? Longitude { get; set; }
    }
}
