
namespace Gamoya.Common.Weather {
    public class WeatherProviderFeatures {
        public bool CurrentWeather { get; set; }
        public bool ForecastWeatherPoints { get; set; }
        public bool DailyForecastWeather { get; set; }

        public bool SunAltitude { get; set; }
        public bool Temperature { get; set; }
        public bool Condition { get; set; }
        public bool WindSpeed { get; set; }
        public bool WindDirection { get; set; }
        public bool Humidity { get; set; }
        public bool WindChill { get; set; }
        public bool DewPoint { get; set; }
        public bool Visibility { get; set; }
        public bool Pressure { get; set; }
        public bool Precipitation { get; set; }
        public bool CloudCover { get; set; }
        public bool Ozone { get; set; }

        public bool FeltTemperature { get; set; }
        public bool FeltIntensity { get; set; }
        public bool FeltVisibility { get; set; }
        public bool FeltHumidity { get; set; }
        public bool FeltWindIntensity { get; set; }
    }
}
