using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gamoya.Common.Weather.Tests {
    [TestClass]
    public class ForecastIOTest {
        private readonly string ApiKey = System.Configuration.ConfigurationManager.AppSettings["ForecastIOApiKey"];

        [TestMethod]
        public void TestRawWeather() {
            var weatherProvider = new Gamoya.Common.Weather.WeatherProviders.ForecastIO.ForecastIOWeatherProvider(ApiKey);
            var rawWeather = weatherProvider.GetForecastIOWeather(50.00060000m, 6.91390000m, null, "ca", null);
            Assert.IsNotNull(rawWeather);
        }

        [TestMethod]
        public void TestCurrentWeather() {
            var weatherProvider = new Gamoya.Common.Weather.WeatherProviders.ForecastIO.ForecastIOWeatherProvider(ApiKey);
            var weatherData = weatherProvider.GetCurrentWeather(50.00060000m, 6.91390000m);
            Assert.IsNotNull(weatherData);
        }

        [TestMethod]
        public void TestForecastWeatherPoints() {
            var weatherProvider = new Gamoya.Common.Weather.WeatherProviders.ForecastIO.ForecastIOWeatherProvider(ApiKey);
            if (weatherProvider.Features.ForecastWeatherPoints) {
                var forecasts = weatherProvider.GetForecastWeatherPoints(50.00060000m, 6.91390000m);
                Assert.IsNotNull(forecasts);
            } else {
                try {
                    var forecasts = weatherProvider.GetForecastWeatherPoints(50.00060000m, 6.91390000m);
                    Assert.Fail();
                } catch { }
            }
        }

        [TestMethod]
        public void TestDailyForecastWeather() {
            var weatherProvider = new Gamoya.Common.Weather.WeatherProviders.ForecastIO.ForecastIOWeatherProvider(ApiKey);
            if (weatherProvider.Features.DailyForecastWeather) {
                var forecasts = weatherProvider.GetDailyForecastWeather(50.00060000m, 6.91390000m);
                Assert.IsNotNull(forecasts);
            } else {
                try {
                    var forecasts = weatherProvider.GetDailyForecastWeather(50.00060000m, 6.91390000m);
                    Assert.Fail();
                } catch { }
            }
        }
    }
}
