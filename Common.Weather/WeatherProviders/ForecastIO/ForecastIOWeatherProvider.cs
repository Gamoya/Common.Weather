using System;
using System.Collections.Generic;

namespace Gamoya.Common.Weather.WeatherProviders.ForecastIO {
    public class ForecastIOWeatherProvider : Gamoya.Common.Weather.WeatherProvider {
        private const string baseUrl = "https://api.forecast.io";
        private string _apiKey;
        private RestSharp.RestClient _restClient;

        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public ForecastIOWeatherProvider(string apiKey) {
            _apiKey = apiKey;
            _restClient = new RestSharp.RestClient(baseUrl);
            _restClient.AddHandler("application/json", new RestSharpJsonDeserializer());
            Features = new Gamoya.Common.Weather.WeatherProviderFeatures() { CurrentWeather = true, CloudCover = true, DewPoint = true, Humidity = true, Ozone = true, Precipitation = true, WindSpeed = true, WindDirection = true, Pressure = true, Temperature = true, Visibility = true, SunAltitude = false, FeltHumidity = false, FeltIntensity = false, FeltTemperature = false, FeltVisibility = false, FeltWindIntensity = false, ForecastWeatherPoints = true, DailyForecastWeather = true, WindChill = false };
        }

        public WeatherResponse GetForecastIOWeather(decimal latitude, decimal longitude, DateTime? time, string units, string[] excludes) {
            RestSharp.RestRequest request;
            if (time.HasValue) {
                request = new RestSharp.RestRequest("forecast/{apikey}/{latitude},{longitude},{time}", RestSharp.Method.GET);
                request.AddParameter("time", DateTimeToUnix(time.Value), RestSharp.ParameterType.UrlSegment);
            } else {
                request = new RestSharp.RestRequest("forecast/{apikey}/{latitude},{longitude}", RestSharp.Method.GET);
            }
            request.AddParameter("latitude", latitude.ToString(System.Globalization.NumberFormatInfo.InvariantInfo), RestSharp.ParameterType.UrlSegment);
            request.AddParameter("longitude", longitude.ToString(System.Globalization.NumberFormatInfo.InvariantInfo), RestSharp.ParameterType.UrlSegment);
            request.AddParameter("apikey", _apiKey, RestSharp.ParameterType.UrlSegment);
            if (!string.IsNullOrEmpty(units)) {
                request.AddParameter("units", units, RestSharp.ParameterType.GetOrPost);
            }
            if (excludes != null && excludes.Length > 0) {
                request.AddParameter("exclude", string.Join(",", excludes), RestSharp.ParameterType.GetOrPost);
            }

            var response = _restClient.Execute<WeatherResponse>(request);
            return response.Data;
        }

        private static Common.Weather.WeatherPoint ConvertToWeatherPoint(WeatherResponse source) {
            var destination = new Common.Weather.WeatherPoint();

            destination.Location = new Location();
            destination.Location.Latitude = source.Latitude;
            destination.Location.Longitude = source.Longitude;

            return destination;
        }

        private static Common.Weather.WeatherPeriod ConvertToWeatherPeriod(WeatherResponse source) {
            var destination = new Common.Weather.WeatherPeriod();

            destination.Location = new Common.Weather.Location();
            destination.Location.Latitude = source.Latitude;
            destination.Location.Longitude = source.Longitude;

            return destination;
        }

        private static DateTime UnixToDateTime(long unixTime) {
            return UnixEpoch.AddSeconds(unixTime);
        }

        private static long DateTimeToUnix(DateTime time) {
            return System.Convert.ToInt64((time - UnixEpoch).TotalSeconds);
        }

        private static void ConvertToWeatherPoint(Common.Weather.WeatherPoint destination, Weather source) {
            destination.Timestamp = UnixToDateTime(source.Time);
            destination.Weather = ConvertToWeatherPointData(source);
        }

        private static void ConvertToWeatherPeriod(Common.Weather.WeatherPeriod destination, DailyWeather source) {
            destination.TimeFrom = UnixToDateTime(source.Time);
            destination.TimeTo = destination.TimeFrom.AddDays(1);
            destination.SunriseTime = UnixToDateTime(source.SunriseTime);
            destination.SunsetTime = UnixToDateTime(source.SunsetTime);
            destination.Weather = ConvertToWeatherPeriodData(source);
        }

        private static Common.Weather.WeatherPointData ConvertToWeatherPointData(Weather source) {
            var destination = new Common.Weather.WeatherPointData();

            destination.Temperature = new Common.Weather.Temperature { Celsius = source.Temperature };
            destination.DewPoint = new Common.Weather.Temperature { Celsius = source.DewPoint };
            destination.Pressure = source.Pressure;
            destination.WindDirection = source.WindDirection;
            destination.WindSpeed = source.WindSpeed;
            destination.Visibility = source.Visibility;
            if (source.Precipitation > 0) {
                destination.Precipitation = source.Precipitation;
            }
            destination.Ozone = source.Ozone;
            destination.Humidity = source.Humidity;
            destination.CloudCover = source.CloudCover;

            return destination;
        }

        private static Common.Weather.WeatherPeriodData ConvertToWeatherPeriodData(DailyWeather source) {
            var destination = new Common.Weather.WeatherPeriodData();

            destination.MaxTemperature = new Common.Weather.Temperature() { Celsius = source.MaxTemperature };
            destination.MaxTemperatureTime = UnixToDateTime(source.MaxTemperatureTime);
            destination.MinTemperature = new Common.Weather.Temperature() { Celsius = source.MinTemperature };
            destination.MinTemperatureTime = UnixToDateTime(source.MinTemperatureTime);
            destination.MaxPrecipitation = source.MaxPrecipitation;
            destination.PrecipitationProbability = source.PrecipitationProbability;
            destination.DewPoint = new Common.Weather.Temperature() { Celsius = source.DewPoint };
            destination.Pressure = source.Pressure;
            destination.WindDirection = source.WindDirection;
            destination.WindSpeed = source.WindSpeed;
            destination.Visibility = source.Visibility;
            if (source.Precipitation > 0) {
                destination.Precipitation = source.Precipitation;
            }
            destination.Ozone = source.Ozone;
            destination.Humidity = source.Humidity;
            destination.CloudCover = source.CloudCover;

            return destination;
        }

        public override Common.Weather.WeatherPoint GetCurrentWeather(decimal latitude, decimal longitude) {
            var weather = GetForecastIOWeather(latitude, longitude, null, Units.CA, new string[] { Excludes.Daily, Excludes.Hourly, Excludes.Minutely, Excludes.Alerts, Excludes.Flags });

            if (weather != null) {
                var weatherPoint = ConvertToWeatherPoint(weather);
                ConvertToWeatherPoint(weatherPoint, weather.CurrentWeather);
                return weatherPoint;
            } else {
                return null;
            }
        }

        public override List<Common.Weather.WeatherPoint> GetForecastWeatherPoints(decimal latitude, decimal longitude) {
            var weather = GetForecastIOWeather(latitude, longitude, null, Units.CA, new string[] { Excludes.Daily, Excludes.Currently, Excludes.Minutely, Excludes.Alerts, Excludes.Flags });

            if (weather != null) {
                var list = new List<Common.Weather.WeatherPoint>();
                if (weather.HourlyForecast != null && weather.HourlyForecast.Data != null) {
                    foreach (var forecast in weather.HourlyForecast.Data) {
                        var weatherPoint = ConvertToWeatherPoint(weather);
                        ConvertToWeatherPoint(weatherPoint, forecast);
                        list.Add(weatherPoint);
                    }
                }
                return list;
            } else {
                return null;
            }
        }

        public override List<Common.Weather.WeatherPeriod> GetDailyForecastWeather(decimal latitude, decimal longitude) {
            var weather = GetForecastIOWeather(latitude, longitude, null, Units.CA, new string[] { Excludes.Currently, Excludes.Hourly, Excludes.Minutely, Excludes.Alerts, Excludes.Flags });

            if (weather != null) {
                var list = new List<Common.Weather.WeatherPeriod>();
                if (weather.DailyForecast != null && weather.DailyForecast.Data != null) {
                    foreach (var forecast in weather.DailyForecast.Data) {
                        var weatherPeriod = ConvertToWeatherPeriod(weather);
                        ConvertToWeatherPeriod(weatherPeriod, forecast);
                        list.Add(weatherPeriod);
                    }
                }
                return list;
            } else {
                return null;
            }
        }
    }
}
