using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace Gamoya.Common.Weather.WeatherProviders.Yahoo {
    public class YahooWeatherProvider : Gamoya.Common.Weather.WeatherProvider {

        public YahooWeatherProvider() {
            Features = new Common.Weather.WeatherProviderFeatures() { CurrentWeather = true, ForecastWeatherPoints = false, DailyForecastWeather = true, SunAltitude = false, Condition = true, DewPoint = false, Temperature = true, Pressure = true, Precipitation = false, Visibility = true, Humidity = true, WindSpeed = true, WindDirection = true, WindChill = true, FeltWindIntensity = false, FeltVisibility = false, FeltTemperature = false, FeltIntensity = false, FeltHumidity = false, CloudCover = false, Ozone = false };
        }

        public WeatherPoint GetCurrentWeather(string woeId) {
            try {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(string.Format("http://weather.yahooapis.com/forecastrss?w={0}&u=c", woeId));

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {
                    var document = new System.Xml.XPath.XPathDocument(response.GetResponseStream());
                    var navigator = document.CreateNavigator();

                    XmlNamespaceManager manager = new XmlNamespaceManager(navigator.NameTable);
                    manager.AddNamespace("yweather", "http://xml.weather.yahoo.com/ns/rss/1.0");

                    var temperature = navigator.SelectSingleNode("rss/channel/item/yweather:condition/@temp", manager);
                    var conditionCode = navigator.SelectSingleNode("rss/channel/item/yweather:condition/@code", manager);

                    var windSpeed = navigator.SelectSingleNode("rss/channel/yweather:wind/@speed", manager);
                    var windChill = navigator.SelectSingleNode("rss/channel/yweather:wind/@chill", manager);
                    var windDirection = navigator.SelectSingleNode("rss/channel/yweather:wind/@direction", manager);
                    var atmosphereHumidity = navigator.SelectSingleNode("rss/channel/yweather:atmosphere/@humidity", manager);
                    var atmosphereVisibility = navigator.SelectSingleNode("rss/channel/yweather:atmosphere/@visibility", manager);
                    var atmospherePressure = navigator.SelectSingleNode("rss/channel/yweather:atmosphere/@pressure", manager);
                    //var atmosphereRising= navigator.SelectSingleNode("rss/channel/yweather:atmosphere/@rising", manager);
                    var astronomySunrise = navigator.SelectSingleNode("rss/channel/yweather:astronomy/@sunrise", manager);
                    var astronomySunset = navigator.SelectSingleNode("rss/channel/yweather:astronomy/@sunset", manager);

                    var weatherPoint = new Common.Weather.WeatherPoint() { Timestamp = DateTime.Now, Weather = new Common.Weather.WeatherPointData() };
                    weatherPoint.Weather.Temperature = new Common.Weather.Temperature() { Celsius = decimal.Parse(temperature.Value, System.Globalization.NumberFormatInfo.InvariantInfo) };
                    if (conditionCode != null && !string.IsNullOrEmpty(conditionCode.Value)) {
                        weatherPoint.Weather.Condition = GetWeatherCondition(conditionCode.Value);
                    }
                    if (windChill != null && !string.IsNullOrEmpty(windChill.Value)) {
                        weatherPoint.Weather.WindChill = decimal.Parse(windChill.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                    }
                    if (windSpeed != null && !string.IsNullOrEmpty(windSpeed.Value)) {
                        weatherPoint.Weather.WindSpeed = decimal.Parse(windSpeed.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                    }
                    if (windDirection != null && !string.IsNullOrEmpty(windDirection.Value)) {
                        weatherPoint.Weather.WindDirection = decimal.Parse(windDirection.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                    }
                    if (atmospherePressure != null && !string.IsNullOrEmpty(atmospherePressure.Value)) {
                        weatherPoint.Weather.Pressure = decimal.Parse(atmospherePressure.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                    }
                    if (atmosphereVisibility != null && !string.IsNullOrEmpty(atmosphereVisibility.Value)) {
                        weatherPoint.Weather.Visibility = decimal.Parse(atmosphereVisibility.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                    }
                    if (atmosphereHumidity != null && !string.IsNullOrEmpty(atmosphereHumidity.Value)) {
                        weatherPoint.Weather.Humidity = decimal.Parse(atmosphereHumidity.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                    }
                    //TODO: Sunrise, Sunset

                    return weatherPoint;
                }
            } catch (Exception ex) {
                return null;
            }
        }

        public List<WeatherPeriod> GetDailyForecastWeather(string woeId) {
            try {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(string.Format("http://weather.yahooapis.com/forecastrss?w={0}&u=c", woeId));

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {
                    var document = new System.Xml.XPath.XPathDocument(response.GetResponseStream());
                    var navigator = document.CreateNavigator();

                    XmlNamespaceManager manager = new XmlNamespaceManager(navigator.NameTable);
                    manager.AddNamespace("yweather", "http://xml.weather.yahoo.com/ns/rss/1.0");

                    var forecasts = navigator.Select("rss/channel/item/yweather:forecast", manager);

                    var list = new List<Common.Weather.WeatherPeriod>();

                    foreach (System.Xml.XPath.XPathNavigator forecast in forecasts) {
                        var weatherPeriod = new Common.Weather.WeatherPeriod() { Weather = new Common.Weather.WeatherPeriodData() };

                        var date = forecast.SelectSingleNode("@date", manager);
                        var day = forecast.SelectSingleNode("@day", manager);
                        var high = forecast.SelectSingleNode("@high", manager);
                        var low = forecast.SelectSingleNode("@low", manager);
                        var weatherCode = forecast.SelectSingleNode("@code", manager);

                        weatherPeriod.Weather.MaxTemperature = new Common.Weather.Temperature() { Celsius = decimal.Parse(high.Value, System.Globalization.NumberFormatInfo.InvariantInfo) };
                        weatherPeriod.Weather.MinTemperature = new Common.Weather.Temperature() { Celsius = decimal.Parse(low.Value, System.Globalization.NumberFormatInfo.InvariantInfo) };
                        weatherPeriod.Weather.Condition = GetWeatherCondition(weatherCode.Value);

                        //TODO: Tag bestimmen:
                        DateTime dateTime;
                        if (DateTime.TryParse(date.Value, System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.AssumeLocal, out dateTime)) {
                            weatherPeriod.TimeFrom = dateTime;
                            weatherPeriod.TimeTo = dateTime.AddDays(1);
                        }
                        //weatherInformation.DayOfWeek = day.Value;

                        list.Add(weatherPeriod);
                    }
                    return list;
                }
            } catch (Exception ex) {
                return null;
            }
        }

        private static Gamoya.Common.Weather.WeatherCondition GetWeatherCondition(string code) {
            Gamoya.Common.Weather.WeatherCondition condition;
            switch (code) {
                case "0":
                    condition = Gamoya.Common.Weather.WeatherCondition.Stormy;// "tornado";
                    break;
                case "1":
                    condition = Gamoya.Common.Weather.WeatherCondition.Stormy;//"tropical storm";
                    break;
                case "2":
                    condition = Gamoya.Common.Weather.WeatherCondition.Stormy;//"hurricane";
                    break;
                case "3":
                    condition = Gamoya.Common.Weather.WeatherCondition.Stormy;//"severe thunderstorms";
                    break;
                case "4":
                    condition = Gamoya.Common.Weather.WeatherCondition.Stormy;//"thunderstorms";
                    break;
                case "5":
                    condition = Gamoya.Common.Weather.WeatherCondition.SnowFlurries;//"mixed rain and snow";
                    break;
                case "6":
                    condition = Gamoya.Common.Weather.WeatherCondition.SnowFlurries;//"mixed rain and sleet";
                    break;
                case "7":
                    condition = Gamoya.Common.Weather.WeatherCondition.SnowFlurries;//"mixed snow and sleet";
                    break;
                case "8":
                    condition = Gamoya.Common.Weather.WeatherCondition.SnowFlurries;//"freezing drizzle";
                    break;
                case "9":
                    condition = Gamoya.Common.Weather.WeatherCondition.Rainy;//"drizzle";
                    break;
                case "10":
                    condition = Gamoya.Common.Weather.WeatherCondition.Rainy;//"freezing rain";
                    break;
                case "11":
                    condition = Gamoya.Common.Weather.WeatherCondition.Rainy;//"showers";
                    break;
                case "12":
                    condition = Gamoya.Common.Weather.WeatherCondition.Rainy;//"showers";
                    break;
                case "13":
                    condition = Gamoya.Common.Weather.WeatherCondition.SnowFlurries;//"snow flurries";
                    break;
                case "14":
                    condition = Gamoya.Common.Weather.WeatherCondition.Snowy;//"light snow showers";
                    break;
                case "15":
                    condition = Gamoya.Common.Weather.WeatherCondition.SnowFlurries;//"blowing snow";
                    break;
                case "16":
                    condition = Gamoya.Common.Weather.WeatherCondition.Snowy;//"snow";
                    break;
                case "17":
                    condition = Gamoya.Common.Weather.WeatherCondition.Hailing;//"hail";
                    break;
                case "18":
                    condition = Gamoya.Common.Weather.WeatherCondition.Hailing;//"sleet";
                    break;
                case "19":
                    condition = Gamoya.Common.Weather.WeatherCondition.Foggy;//"dust";
                    break;
                case "20":
                    condition = Gamoya.Common.Weather.WeatherCondition.Foggy;//"foggy";
                    break;
                case "21":
                    condition = Gamoya.Common.Weather.WeatherCondition.Foggy;//"haze";
                    break;
                case "22":
                    condition = Gamoya.Common.Weather.WeatherCondition.Foggy;//"smoky";
                    break;
                case "23":
                    condition = Gamoya.Common.Weather.WeatherCondition.Foggy;//"blustery";
                    break;
                case "24":
                    condition = Gamoya.Common.Weather.WeatherCondition.Windy;//"windy";
                    break;
                case "25":
                    condition = Gamoya.Common.Weather.WeatherCondition.Windy;//"cold";
                    break;
                case "26":
                    condition = Gamoya.Common.Weather.WeatherCondition.Cloudy;//"cloudy";
                    break;
                case "27":
                    condition = Gamoya.Common.Weather.WeatherCondition.Cloudy;//"mostly cloudy (night)";
                    break;
                case "28":
                    condition = Gamoya.Common.Weather.WeatherCondition.Cloudy;//"mostly cloudy (day)";
                    break;
                case "29":
                    condition = Gamoya.Common.Weather.WeatherCondition.PartlyCloudy;//"partly cloudy (night)";
                    break;
                case "30":
                    condition = Gamoya.Common.Weather.WeatherCondition.PartlyCloudy;//"partly cloudy (day)";
                    break;
                case "31":
                    condition = Gamoya.Common.Weather.WeatherCondition.Clear;//"clear (night)";
                    break;
                case "32":
                    condition = Gamoya.Common.Weather.WeatherCondition.Clear;//"sunny";
                    break;
                case "33":
                    condition = Gamoya.Common.Weather.WeatherCondition.Clear;//"fair (night)";
                    break;
                case "34":
                    condition = Gamoya.Common.Weather.WeatherCondition.Clear;//"fair (day)";
                    break;
                case "35":
                    condition = Gamoya.Common.Weather.WeatherCondition.Hailing;//"mixed rain and hail";
                    break;
                case "36":
                    condition = Gamoya.Common.Weather.WeatherCondition.Clear;//"hot";
                    break;
                case "37":
                    condition = Gamoya.Common.Weather.WeatherCondition.Stormy;//"isolated thunderstorms";
                    break;
                case "38":
                    condition = Gamoya.Common.Weather.WeatherCondition.Stormy;//"scattered thunderstorms";
                    break;
                case "39":
                    condition = Gamoya.Common.Weather.WeatherCondition.Stormy;//"scattered thunderstorms";
                    break;
                case "40":
                    condition = Gamoya.Common.Weather.WeatherCondition.Stormy;//"scattered showers";
                    break;
                case "41":
                    condition = Gamoya.Common.Weather.WeatherCondition.Snowy;//"heavy snow";
                    break;
                case "42":
                    condition = Gamoya.Common.Weather.WeatherCondition.SnowFlurries;//"scattered snow showers";
                    break;
                case "43":
                    condition = Gamoya.Common.Weather.WeatherCondition.SnowFlurries;//"heavy snow";
                    break;
                case "44":
                    condition = Gamoya.Common.Weather.WeatherCondition.PartlyCloudy;//"partly cloudy";
                    break;
                case "45":
                    condition = Gamoya.Common.Weather.WeatherCondition.Stormy;//"thundershowers";
                    break;
                case "46":
                    condition = Gamoya.Common.Weather.WeatherCondition.Snowy;//"snow showers";
                    break;
                case "47":
                    condition = Gamoya.Common.Weather.WeatherCondition.Stormy;//"isolated thundershowers";
                    break;
                default:
                    condition = Gamoya.Common.Weather.WeatherCondition.Unknown;
                    break;
            }

            return condition;
        }
    }
}
