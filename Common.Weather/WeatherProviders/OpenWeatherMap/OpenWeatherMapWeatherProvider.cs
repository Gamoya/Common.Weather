using System;
using System.Collections.Generic;
using System.Linq;

namespace Gamoya.Common.Weather.WeatherProviders.OpenWeatherMap {
    public class OpenWeatherMapWeatherProvider : Common.Weather.WeatherProvider {
        private const string baseUrl = "http://api.openweathermap.org/data/2.5";
        private string apiKey;
        private RestSharp.RestClient restClient;
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public OpenWeatherMapWeatherProvider() {
            Features = new Common.Weather.WeatherProviderFeatures() { };// CurrentWeather = true, ForecastWeatherPoints = true, SunAltitude = true, Condition = true, DewPoint = true, Temperature = true, Pressure = true, Precipitation = true, Visibility = true, Humidity = true, WindSpeed = true, WindDirection = true, WindChill = true, FeltWindIntensity = true, FeltVisibility = true, FeltTemperature = true, FeltIntensity = true, FeltHumidity = true, CloudCover = false, Ozone = false, DailyForecastWeather = true };
            this.restClient = new RestSharp.RestClient(baseUrl);
            this.restClient.AddHandler("application/json", new RestSharpJsonDeserializer());
            this.restClient.CookieContainer = new System.Net.CookieContainer();
        }

        public OpenWeatherMapWeatherProvider(string apiKey)
            : this() {
            this.apiKey = apiKey;
        }

        private static DateTime UnixToDateTime(long unixTime) {
            return UnixEpoch.AddSeconds(unixTime);
        }

        private static long DateTimeToUnix(DateTime time) {
            return System.Convert.ToInt64((time - UnixEpoch).TotalSeconds);
        }

        public CurrentWeatherResponse GetCurrentOpenWeatherMapWeather(decimal latitude, decimal longitude) {
            var request = new RestSharp.RestRequest("weather", RestSharp.Method.GET);
            request.AddParameter("lat", latitude.ToString(System.Globalization.NumberFormatInfo.InvariantInfo), RestSharp.ParameterType.GetOrPost);
            request.AddParameter("lon", longitude.ToString(System.Globalization.NumberFormatInfo.InvariantInfo), RestSharp.ParameterType.GetOrPost);
            if (!string.IsNullOrEmpty(apiKey)) {
                //request.AddHeader("Authorization", string.Format("Bearer {0}", clientKey));
            }

            var response = restClient.Execute<CurrentWeatherResponse>(request);
            if (response != null && response.Data != null) {
                return response.Data;
            } else {
                return null;
            }
        }

        public HourlyWeatherResponse GetHourlyOpenWeatherMapWeather(decimal latitude, decimal longitude) {
            var request = new RestSharp.RestRequest("forecast", RestSharp.Method.GET);
            request.AddParameter("lat", latitude.ToString(System.Globalization.NumberFormatInfo.InvariantInfo), RestSharp.ParameterType.GetOrPost);
            request.AddParameter("lon", longitude.ToString(System.Globalization.NumberFormatInfo.InvariantInfo), RestSharp.ParameterType.GetOrPost);
            if (!string.IsNullOrEmpty(apiKey)) {
                //request.AddHeader("Authorization", string.Format("Bearer {0}", clientKey));
            }

            var response = restClient.Execute<HourlyWeatherResponse>(request);
            if (response != null && response.Data != null) {
                return response.Data;
            } else {
                return null;
            }
        }

        public DailyWeatherResponse GetDailyOpenWeatherMapWeather(decimal latitude, decimal longitude, int count) {
            var request = new RestSharp.RestRequest("forecast/daily", RestSharp.Method.GET);
            request.AddParameter("lat", latitude.ToString(System.Globalization.NumberFormatInfo.InvariantInfo), RestSharp.ParameterType.GetOrPost);
            request.AddParameter("lon", longitude.ToString(System.Globalization.NumberFormatInfo.InvariantInfo), RestSharp.ParameterType.GetOrPost);
            request.AddParameter("cnt", count, RestSharp.ParameterType.GetOrPost);
            if (!string.IsNullOrEmpty(apiKey)) {
                //request.AddHeader("Authorization", string.Format("Bearer {0}", clientKey));
            }

            var response = restClient.Execute<DailyWeatherResponse>(request);
            if (response != null && response.Data != null) {
                return response.Data;
            } else {
                return null;
            }
        }

        private static Common.Weather.WeatherPoint ConvertToWeatherPoint(CurrentWeatherResponse source) {
            var destination = new Common.Weather.WeatherPoint();

            destination.Location = ConvertToLocation(source);
            destination.Weather = ConvertToWeatherPointData(source);
            if (source.Timestamp.HasValue) {
                destination.Timestamp = UnixToDateTime(source.Timestamp.Value);
            }
            destination.SunAltitude = null;

            return destination;
        }

        private static List<Common.Weather.WeatherPoint> ConvertToWeatherPoints(HourlyWeatherResponse source) {
            var destination = new List<Common.Weather.WeatherPoint>();

            foreach (var item in source.Weather) {
                var destinationItem = new Common.Weather.WeatherPoint();

                if (item.Timestamp.HasValue) {
                    destinationItem.Timestamp = UnixToDateTime(item.Timestamp.Value);
                }
                destinationItem.Weather = ConvertToWeatherPointData(item);

                destinationItem.SunAltitude = null;

                destination.Add(destinationItem);
            }

            return destination;
        }

        private static Common.Weather.Location ConvertToLocation(CurrentWeatherResponse source) {
            var destination = new Common.Weather.Location();

            destination.Country = source.WeatherLocation.CountryCode;
            destination.Latitude = source.Coordinates.Latitude;
            destination.Longitude = source.Coordinates.Longitude;
            destination.Locality = null;

            return destination;
        }

        private static Common.Weather.WeatherPointData ConvertToWeatherPointData(HourlyWeather source) {
            var destination = new Common.Weather.WeatherPointData();

            if (source.Main != null) {
                destination.Pressure = source.Main.Pressure;
                if (source.Main.Temperature.HasValue) {
                    destination.Temperature = new Temperature() { Kelvin = source.Main.Temperature.Value };
                }
            }
            destination.Humidity = source.Main.Humidity;

            if (source.WeatherConditions != null && source.WeatherConditions.Count > 0) {
                destination.Condition = ConvertToWeatherCondition(source.WeatherConditions[0].Id);
            } else {
                destination.Condition = WeatherCondition.Unknown;
            }

            destination.DewPoint = null;
            destination.FeltHumidity = null;
            destination.FeltIntensity = null;
            destination.FeltTemperature = null;
            destination.FeltVisibility = null;
            destination.FeltWindIntensity = null;
            destination.Ozone = null;
            destination.Visibility = null;
            destination.WindChill = null;

            return destination;
        }

        private static Common.Weather.WeatherPointData ConvertToWeatherPointData(CurrentWeatherResponse source) {
            var destination = new Common.Weather.WeatherPointData();

            if (source.Main != null) {
                destination.Pressure = source.Main.Pressure;
                if (source.Main.Temperature.HasValue) {
                    destination.Temperature = new Temperature() { Kelvin = source.Main.Temperature.Value };
                }
                destination.Humidity = source.Main.Humidity;
                if (source.Clouds != null) {
                    destination.CloudCover = source.Clouds.CloudCover;
                }
                if (source.WeatherConditions != null && source.WeatherConditions.Count > 0) {
                    destination.Condition = ConvertToWeatherCondition(source.WeatherConditions[0].Id);
                } else {
                    destination.Condition = WeatherCondition.Unknown;
                }

                if (source.Wind != null) {
                    destination.WindDirection = source.Wind.Direction;
                    destination.WindSpeed = source.Wind.Speed;
                }

                if (source.Snow != null) {
                    destination.PrecipitationType = PrecipitationType.Snow;
                    destination.Precipitation = source.Snow.Last3Hours;
                } else if (source.Rain != null) {
                    destination.PrecipitationType = PrecipitationType.Rain;
                    destination.Precipitation = source.Rain.Last3Hours;
                }

                destination.DewPoint = null;
                destination.FeltHumidity = null;
                destination.FeltIntensity = null;
                destination.FeltTemperature = null;
                destination.FeltVisibility = null;
                destination.FeltWindIntensity = null;
                destination.Ozone = null;
                destination.Visibility = null;
                destination.WindChill = null;
            }

            return destination;
        }

        private static Common.Weather.WeatherCondition ConvertToWeatherCondition(string code) {
            //http://openweathermap.org/weather-conditions
            Common.Weather.WeatherCondition condition;

            switch (code) {
                //case "???":
                //    condition = WeatherCondition.CalmSeas;
                //    break;
                case "800":
                case "904":
                    condition = WeatherCondition.Clear;
                    break;
                case "802":
                case "803":
                case "804":
                    condition = WeatherCondition.Cloudy;
                    break;
                case "701":
                case "711":
                case "721":
                case "731":
                case "741":
                case "751":
                case "761":
                case "762":
                case "771":
                case "781":
                    condition = WeatherCondition.Foggy;
                    break;
                case "906":
                    condition = WeatherCondition.Hailing;
                    break;
                //case "???":
                //    condition = WeatherCondition.HeavySeas;
                //    break;
                case "801":
                    condition = WeatherCondition.PartlyCloudy;
                    break;
                case "300":
                case "301":
                case "302":
                case "310":
                case "311":
                case "312":
                case "313":
                case "314":
                case "321":
                case "500":
                case "501":
                case "502":
                case "503":
                case "504":
                case "511":
                case "520":
                case "521":
                case "522":
                case "531":
                    condition = WeatherCondition.Rainy;
                    break;
                //case "???":
                //    condition = WeatherCondition.SnowFlurries;
                //    break;
                case "600":
                case "601":
                case "602":
                case "611":
                case "612":
                case "615":
                case "616":
                case "620":
                case "621":
                case "622":
                    condition = WeatherCondition.Snowy;
                    break;
                case "200":
                case "201":
                case "202":
                case "210":
                case "211":
                case "212":
                case "221":
                case "230":
                case "231":
                case "232":
                case "900":
                case "901":
                case "902":
                case "958":
                case "959":
                case "960":
                case "961":
                case "962":
                    condition = WeatherCondition.Stormy;
                    break;
                case "952":
                case "953":
                case "954":
                case "955":
                case "956":
                case "957":
                case "905":
                    condition = WeatherCondition.Windy;
                    break;
                default:
                    condition = WeatherCondition.Unknown;
                    break;
            }

            return condition;
        }

        private static List<Common.Weather.WeatherPeriod> ConvertToWeatherPeriods(DailyWeatherResponse source) {
            var destination = new List<Common.Weather.WeatherPeriod>();

            foreach (var period in source.Weather) {
                destination.Add(ConvertToWeatherPeriod(source, period));
            }

            return destination;
        }

        private static Common.Weather.WeatherPeriod ConvertToWeatherPeriod(DailyWeatherResponse source, DailyWeather day) {
            var destination = new Common.Weather.WeatherPeriod();

            destination.TimeFrom = UnixToDateTime(day.Timestamp.Value);
            destination.TimeTo = destination.TimeFrom.AddDays(1);
            destination.Weather = ConvertToWeatherPeriodData(day);

            return destination;
        }

        private static Common.Weather.WeatherPeriodData ConvertToWeatherPeriodData(DailyWeather source) {
            var destination = new Common.Weather.WeatherPeriodData();

            if (source.WeatherConditions != null && source.WeatherConditions.Count > 0) {
                destination.Condition = ConvertToWeatherCondition(source.WeatherConditions[0].Id);
            }
            destination.Humidity = source.Humidity;
            destination.Pressure = source.Pressure;
            if (source.DailyTemperature != null) {
                if (source.DailyTemperature.Maximum.HasValue) {
                    destination.MaxTemperature = new Temperature() { Kelvin = source.DailyTemperature.Maximum.Value };
                }
                if (source.DailyTemperature.Minimum.HasValue) {
                    destination.MinTemperature = new Temperature() { Kelvin = source.DailyTemperature.Minimum.Value };
                }
            }

            //destination.MaxPrecipitation = ;
            //destination.PrecipitationType = ;

            destination.CloudCover = null;
            destination.DewPoint = null;
            destination.MaxTemperatureTime = null;
            destination.MinTemperatureTime = null;
            destination.Ozone = null;
            destination.PrecipitationProbability = null;
            destination.Visibility = null;
            destination.WindChill = null;
            destination.WindDirection = null;
            destination.WindSpeed = null;

            return destination;
        }


        public override Common.Weather.WeatherPoint GetCurrentWeather(decimal latitude, decimal longitude) {
            var rawWeather = GetCurrentOpenWeatherMapWeather(latitude, longitude);
            return ConvertToWeatherPoint(rawWeather);
        }

        public override List<Common.Weather.WeatherPoint> GetForecastWeatherPoints(decimal latitude, decimal longitude) {
            var rawWeather = GetHourlyOpenWeatherMapWeather(latitude, longitude);
            return ConvertToWeatherPoints(rawWeather);
        }

        public override List<Common.Weather.WeatherPeriod> GetDailyForecastWeather(decimal latitude, decimal longitude) {
            var rawWeather = GetDailyOpenWeatherMapWeather(latitude, longitude, 16);
            return ConvertToWeatherPeriods(rawWeather);
        }
    }
}
