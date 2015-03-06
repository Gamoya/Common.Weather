using System;
using System.Collections.Generic;
using System.Linq;

namespace Gamoya.Common.Weather.WeatherProviders.Metwit {
    public class MetwitWeatherProvider : Common.Weather.WeatherProvider {
        private const string baseUrl = "https://api.metwit.com";

        private string clientId;
        private string clientKey;

        private RestSharp.RestClient restClient;

        public MetwitWeatherProvider() {
            Features = new Common.Weather.WeatherProviderFeatures() { CurrentWeather = true, ForecastWeatherPoints = true, SunAltitude = true, Condition = true, DewPoint = true, Temperature = true, Pressure = true, Precipitation = true, Visibility = true, Humidity = true, WindSpeed = true, WindDirection = true, WindChill = true, FeltWindIntensity = true, FeltVisibility = true, FeltTemperature = true, FeltIntensity = true, FeltHumidity = true, CloudCover = false, Ozone = false, DailyForecastWeather = true };
            this.restClient = new RestSharp.RestClient(baseUrl);
            this.restClient.AddHandler("application/json", new RestSharpJsonDeserializer());
            this.restClient.CookieContainer = new System.Net.CookieContainer();
        }

        public MetwitWeatherProvider(string clientId, string clientKey)
            : this() {
            this.clientId = clientId;
            this.clientKey = clientKey;
        }

        public List<WeatherPoint> GetMetwitWeather(decimal latitude, decimal longitude) {
            var request = new RestSharp.RestRequest("v2/weather", RestSharp.Method.GET);
            request.AddParameter("location_lat", latitude.ToString(System.Globalization.NumberFormatInfo.InvariantInfo), RestSharp.ParameterType.GetOrPost);
            request.AddParameter("location_lng", longitude.ToString(System.Globalization.NumberFormatInfo.InvariantInfo), RestSharp.ParameterType.GetOrPost);
            if (!string.IsNullOrEmpty(clientKey)) {
                request.AddHeader("Authorization", string.Format("Bearer {0}", clientKey));
            }

            var response = restClient.Execute<WeatherResponse>(request);
            if (response != null && response.Data != null) {
                return response.Data.Objects;
            } else {
                return null;
            }
        }

        private static Common.Weather.WeatherPoint ConvertToWeatherPoint(WeatherPoint source) {
            var destination = new Common.Weather.WeatherPoint() { SunAltitude = source.SunAltitude, Timestamp = source.Timestamp.ToLocalTime(), Location = new Common.Weather.Location() };

            ConvertWeatherLocation(destination.Location, source);
            if (source.Weather != null) {
                destination.Weather = new Common.Weather.WeatherPointData();
                ConvertWeather(destination.Weather, source.Weather);
            }

            return destination;
        }

        private static void ConvertWeather(Common.Weather.WeatherPointData destination, Weather source) {
            destination.Condition = (Common.Weather.WeatherCondition)Enum.Parse(typeof(Common.Weather.WeatherCondition), source.WeatherCondition.ToString());
            if (source.Measured != null) {
                ConvertMeasured(destination, source.Measured);
            }
            if (source.Felted != null) {
                ConvertFelt(destination, source.Felted);
            }
        }

        private static void ConvertMeasured(Common.Weather.WeatherPointData destination, WeatherMeasured source) {
            if (source.DewPoint.HasValue) {
                destination.DewPoint = new Common.Weather.Temperature() { Kelvin = source.DewPoint.Value };
            }
            destination.Humidity = source.Humidity;
            destination.Pressure = source.Pressure;
            if (source.Rainfall.GetValueOrDefault(0) > 0) {
                destination.PrecipitationType = Common.Weather.PrecipitationType.Rain;
                destination.Precipitation = source.Rainfall;
            } else if (source.Snowfall.GetValueOrDefault(0) > 0) {
                destination.PrecipitationType = Common.Weather.PrecipitationType.Snow;
                destination.Precipitation = source.Snowfall;
            }
            if (source.Temperature.HasValue) {
                destination.Temperature = new Common.Weather.Temperature() { Kelvin = source.Temperature.Value };
            }
            destination.Visibility = source.Visibility;
            destination.WindChill = source.WindChill;
            destination.WindDirection = source.WindDirection;
            destination.WindSpeed = source.WindSpeed;
        }

        private static void ConvertFelt(Common.Weather.WeatherPointData destination, WeatherFelt source) {
            destination.FeltHumidity = source.Humidity;
            destination.Temperature = new Common.Weather.Temperature() { Kelvin = source.Temperature };
            destination.FeltVisibility = source.Visibility;
            destination.FeltIntensity = source.Intensity;
            destination.FeltWindIntensity = source.WindIntensity;
        }

        private static void ConvertWeatherLocation(Common.Weather.Location destination, WeatherPoint source) {
            if (source.Location != null) {
                destination.Country = source.Location.Country;
                destination.Locality = source.Location.Locality;
            }
            if (source.Geo != null && source.Geo.Coordinates != null && source.Geo.Coordinates.Count == 2) {
                destination.Latitude = source.Geo.Coordinates[0];
                destination.Longitude = source.Geo.Coordinates[1];
            }
        }

        public override Common.Weather.WeatherPoint GetCurrentWeather(decimal latitude, decimal longitude) {
            var weatherPoints = GetMetwitWeather(latitude, longitude);
            if (weatherPoints != null && weatherPoints.Count > 0) {
                return ConvertToWeatherPoint(weatherPoints[0]);
            } else {
                return null;
            }
        }

        public override List<Common.Weather.WeatherPoint> GetForecastWeatherPoints(decimal latitude, decimal longitude) {
            var weatherPoints = GetMetwitWeather(latitude, longitude);
            if (weatherPoints != null) {
                return weatherPoints.Skip(1).ToList().ConvertAll(ConvertToWeatherPoint);
            } else {
                return null;
            }
        }

        public override List<Common.Weather.WeatherPeriod> GetDailyForecastWeather(decimal latitude, decimal longitude) {
            var weatherPoints = GetMetwitWeather(latitude, longitude);
            var list = new List<Common.Weather.WeatherPeriod>();

            if (weatherPoints.Count > 0) {
                Common.Weather.WeatherPeriod weatherPeriod = null;

                foreach (var weatherPoint in weatherPoints) {
                    if (weatherPeriod == null || weatherPoint.Timestamp >= weatherPeriod.TimeTo) {
                        weatherPeriod = new Common.Weather.WeatherPeriod() { TimeFrom = weatherPoint.Timestamp.Date, TimeTo = weatherPoint.Timestamp.Date.AddDays(1), Location = new Common.Weather.Location(), Weather = new WeatherPeriodData() };
                        ConvertWeatherLocation(weatherPeriod.Location, weatherPoint);
                        list.Add(weatherPeriod);
                    }

                    if (weatherPoint.Weather.Measured.Temperature.HasValue) {
                        if (weatherPeriod.Weather.MaxTemperature == null || weatherPeriod.Weather.MaxTemperature.Kelvin < weatherPoint.Weather.Measured.Temperature) {
                            weatherPeriod.Weather.MaxTemperature = new Temperature() { Kelvin = weatherPoint.Weather.Measured.Temperature.Value };
                            weatherPeriod.Weather.MaxTemperatureTime = weatherPoint.Timestamp;
                        }
                        if (weatherPeriod.Weather.MinTemperature == null || weatherPeriod.Weather.MaxTemperature.Kelvin > weatherPoint.Weather.Measured.Temperature) {
                            weatherPeriod.Weather.MinTemperature = new Temperature() { Kelvin = weatherPoint.Weather.Measured.Temperature.Value };
                            weatherPeriod.Weather.MinTemperatureTime = weatherPoint.Timestamp;
                        }
                    }
                }
            }
            return list;
        }

        //public string GetMetags()
        //{
        //    var client = new RestSharp.RestClient(baseUrl);
        //    var request = new RestSharp.RestRequest("v2/metags", RestSharp.Method.GET);
        //    if (!string.IsNullOrEmpty(clientKey))
        //    {
        //        request.AddHeader("Authorization", string.Format("Bearer {0}", clientKey));
        //    }

        //    var response = client.Execute(request);
        //    if (response != null && response.Content != null)
        //    {
        //        return response.Content;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //public string Authenticate()
        //{
        //    var client = new RestSharp.RestClient(baseUrl);
        //    var request = new RestSharp.RestRequest("v2/metags", RestSharp.Method.GET);
        //    if (!string.IsNullOrEmpty(clientKey))
        //    {
        //        request.AddHeader("Authorization", string.Format("Bearer {0}", clientKey));
        //    }

        //    var response = client.Execute(request);
        //    if (response != null && response.Content != null)
        //    {
        //        return response.Content;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //public string Authorize()
        //{
        //    var request = new RestSharp.RestRequest(string.Format("metwit_authorize#state=123456&access_token={0}&expires_in=3600", clientKey), RestSharp.Method.GET);
        //    //request.AddParameter("state", "123456", RestSharp.ParameterType.GetOrPost);
        //    //request.AddParameter("access_token", clientKey, RestSharp.ParameterType.GetOrPost);
        //    //request.AddParameter("expires_in", "3600", RestSharp.ParameterType.GetOrPost);

        //    var response = restClient.Execute(request);
        //    if (response != null && response.Content != null)
        //    {
        //        return response.Content;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //public string GetAccessToken()
        //{
        //    var request = new RestSharp.RestRequest("token", RestSharp.Method.POST);

        //    request.AddHeader("Authorization", string.Format("Basic {0})", clientKey));
        //    request.AddHeader("Content-type", "application/www-form-urlencoded");

        //    request.AddParameter("grant_type", "client_credentials", RestSharp.ParameterType.GetOrPost);
        //    //request.AddParameter("access_token", clientKey, RestSharp.ParameterType.GetOrPost);
        //    //request.AddParameter("expires_in", "3600", RestSharp.ParameterType.GetOrPost);

        //    var response = restClient.Execute(request);
        //    if (response != null && response.Content != null)
        //    {
        //        return response.Content;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

    }
}
