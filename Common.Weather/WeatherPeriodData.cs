using System;

namespace Gamoya.Common.Weather {
    public class WeatherPeriodData {
        public WeatherCondition Condition { get; set; }
        public Temperature MinTemperature { get; set; }
        public Temperature MaxTemperature { get; set; }
        public DateTime? MaxTemperatureTime { get; set; }
        public DateTime? MinTemperatureTime { get; set; }
        public decimal? MaxPrecipitation { get; set; }
        public decimal? PrecipitationProbability { get; set; }
        public decimal? WindChill { get; set; }
        public decimal? Humidity { get; set; }
        public Temperature DewPoint { get; set; }
        public decimal? Visibility { get; set; }
        public decimal? WindSpeed { get; set; }
        public decimal? WindDirection { get; set; }
        public decimal? Pressure { get; set; }
        public PrecipitationType PrecipitationType { get; set; }
        public decimal? Precipitation { get; set; }
        public decimal? CloudCover { get; set; }
        public decimal? Ozone { get; set; }
    }
}
