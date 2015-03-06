using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gamoya.Common.Weather.Tests {
    [TestClass]
    public class MetwitTest {
        [TestMethod]
        public void TestRawWeather() {
            var weatherProvider = new Gamoya.Common.Weather.WeatherProviders.Metwit.MetwitWeatherProvider();
            var rawWeather = weatherProvider.GetMetwitWeather(50.00060000m, 6.91390000m);
            Assert.IsNotNull(rawWeather);
        }

        [TestMethod]
        public void TestCurrentWeather() {
            var weatherProvider = new Gamoya.Common.Weather.WeatherProviders.Metwit.MetwitWeatherProvider();
            var weatherData = weatherProvider.GetCurrentWeather(50.00060000m, 6.91390000m);
            Assert.IsNotNull(weatherData);
        }

        [TestMethod]
        public void TestForecastWeatherPoints() {
            var weatherProvider = new Gamoya.Common.Weather.WeatherProviders.Metwit.MetwitWeatherProvider();
            var forecasts = weatherProvider.GetForecastWeatherPoints(50.00060000m, 6.91390000m);
            Assert.IsNotNull(forecasts);
        }

        [TestMethod]
        public void TestDailyForecastWeather() {
            var weatherProvider = new Gamoya.Common.Weather.WeatherProviders.Metwit.MetwitWeatherProvider();
            var forecasts = weatherProvider.GetDailyForecastWeather(50.00060000m, 6.91390000m);
            Assert.IsNotNull(forecasts);
        }
    }
}
