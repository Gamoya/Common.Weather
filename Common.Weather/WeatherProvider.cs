using System;
using System.Collections.Generic;

namespace Gamoya.Common.Weather {
    public abstract class WeatherProvider {
        public WeatherProviderFeatures Features { get; protected set; }

        public virtual WeatherPoint GetCurrentWeather(decimal latitude, decimal longitude) {
            throw new NotImplementedException();
        }

        public virtual List<WeatherPoint> GetForecastWeatherPoints(decimal latitude, decimal longitude) {
            throw new NotImplementedException();
        }

        public virtual List<WeatherPeriod> GetDailyForecastWeather(decimal latitude, decimal longitude) {
            throw new NotImplementedException();
        }
    }
}
