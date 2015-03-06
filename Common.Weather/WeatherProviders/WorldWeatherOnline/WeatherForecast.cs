using System;
using System.Xml.Serialization;

namespace Gamoya.Common.Weather.WeatherProviders.WorldWeatherOnline
{
    public class WeatherForecast
    {
        [XmlElement("date")]
        public DateTime Date { get; set; }
        [XmlElement("tempMaxC")]
        public decimal? MaxTemperatureCelsius { get; set; }
        [XmlElement("tempMaxF")]
        public decimal? MaxTemperatureFahrenheit { get; set; }
        [XmlElement("tempMinC")]
        public decimal? MinTemperatureCelsius { get; set; }
        [XmlElement("tempMinF")]
        public decimal? MinTemperatureFahrenheit { get; set; }
        [XmlElement("windspeedMiles")]
        public decimal? WindspeedMiles { get; set; }
        [XmlElement("windspeedKmph")]
        public decimal? WindspeedKmph { get; set; }
        [XmlElement("winddirDegree")]
        public decimal? WindDirection { get; set; }
        [XmlElement("winddir16Point")]
        public string WindDirection16Point { get; set; }
        [XmlElement("precipMM")]
        public decimal? Precipitation { get; set; }
        [XmlElement("weatherCode")]
        public string WeatherCode { get; set; }
        [XmlElement("weatherDesc")]
        public string WeatherDescription { get; set; }
        [XmlElement("weatherIconUrl")]
        public string WeatherIconUrl { get; set; }
    }
}
