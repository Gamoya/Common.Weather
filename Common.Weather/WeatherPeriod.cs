using System;

namespace Gamoya.Common.Weather {
    public class WeatherPeriod {
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public DateTime? SunriseTime { get; set; }
        public DateTime? SunsetTime { get; set; }
        public Location Location { get; set; }
        public WeatherPeriodData Weather { get; set; }
    }
}
