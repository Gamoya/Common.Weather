using System;

namespace Gamoya.Common.Weather {
    public class WeatherPoint {
        public DateTime Timestamp { get; set; }
        public Location Location { get; set; }
        public WeatherPointData Weather { get; set; }
        public decimal? SunAltitude { get; set; }
    }
}
