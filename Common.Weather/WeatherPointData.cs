
namespace Gamoya.Common.Weather {
    public class WeatherPointData {
        public WeatherCondition Condition { get; set; }
        public Temperature Temperature { get; set; }
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

        public Temperature FeltTemperature { get; set; }
        public decimal? FeltIntensity { get; set; }
        public decimal? FeltVisibility { get; set; }
        public decimal? FeltHumidity { get; set; }
        public decimal? FeltWindIntensity { get; set; }
    }
}
