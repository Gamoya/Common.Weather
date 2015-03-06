using System.Xml.Serialization;

namespace Gamoya.Common.Weather.WeatherProviders.WorldWeatherOnline
{
    public class WeatherCurrentCondition
    {
        [XmlElement("cloudcover")]
        public decimal? CloudCover { get; set; }
        [XmlElement("humidity")]
        public decimal? Humidity { get; set; }
        [XmlElement("precipMM")]
        public decimal? Precipitation { get; set; }
        [XmlElement("pressure")]
        public decimal? Pressure { get; set; }
        [XmlElement("temp_C")]
        public decimal? TemperatureCelsius { get; set; }
        [XmlElement("temp_F")]
        public decimal? TemperatureFahrenheit { get; set; }
        [XmlElement("visibility")]
        public decimal? Visibility { get; set; }
        [XmlElement("weatherCode")]
        public string WeatherCode { get; set; }
        [XmlElement("weatherDesc")]
        public string WeatherDescription { get; set; }
        [XmlElement("weatherIconUrl")]
        public string WeatherIconUrl { get; set; }
        [XmlElement("winddirDegree")]
        public decimal? WindDirection { get; set; }
        [XmlElement("winddir16Point")]
        public string WindDirection16Point { get; set; }
        [XmlElement("windspeedKmph")]
        public decimal? WindspeedKmph { get; set; }
        [XmlElement("windspeedMiles")]
        public decimal? WindspeedMiles { get; set; }
    }
}
