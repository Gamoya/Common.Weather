using System.Collections.Generic;
using System.Xml.Serialization;

namespace Gamoya.Common.Weather.WeatherProviders.WorldWeatherOnline {
    [XmlRoot("data")]
    public class WeatherData {
        [XmlElement("current_condition")]
        public WeatherCurrentCondition CurrentCondition { get; set; }
        [XmlElement("nearest_area")]
        public WeatherArea NearestArea { get; set; }
        [XmlElement("weather")]
        public List<WeatherForecast> Forecasts { get; set; }
        [XmlElement("request")]
        public WeatherRequest Request { get; set; }
    }
}
