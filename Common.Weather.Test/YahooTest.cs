using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gamoya.Common.Weather.Tests {
    [TestClass]
    public class YahooTest {
        //[TestMethod]
        //public void TestRawWeather() {
        //    var weatherProvider = new Gamoya.Common.Weather.WeatherProviders.Yahoo.YahooWeatherProvider();
        //    var rawWeather = weatherProvider.GetYahooWeather(50.00060000m, 6.91390000m);
        //    Assert.IsNotNull(rawWeather);
        //}

        [TestMethod]
        public void TestCurrentWeather() {
            var weatherProvider = new Gamoya.Common.Weather.WeatherProviders.Yahoo.YahooWeatherProvider();
            var weatherData = weatherProvider.GetCurrentWeather(50.00060000m, 6.91390000m);
            Assert.IsNotNull(weatherData);
        }

        [TestMethod]
        public void TestForecastWeatherPoints() {
            var weatherProvider = new Gamoya.Common.Weather.WeatherProviders.Yahoo.YahooWeatherProvider();
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
            var weatherProvider = new Gamoya.Common.Weather.WeatherProviders.Yahoo.YahooWeatherProvider();
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
