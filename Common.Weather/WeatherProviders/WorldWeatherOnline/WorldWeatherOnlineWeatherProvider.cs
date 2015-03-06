using RestSharp.Deserializers;
using System.Collections.Generic;

namespace Gamoya.Common.Weather.WeatherProviders.WorldWeatherOnline {
    public class WorldWeatherOnlineWeatherProvider : Gamoya.Common.Weather.WeatherProvider {
        private const string baseUrl = "http://api.worldweatheronline.com";
        private RestSharp.RestClient restClient;

        private string clientKey;

        public WorldWeatherOnlineWeatherProvider(string clientKey) {
            Features = new Common.Weather.WeatherProviderFeatures() { CurrentWeather = true, DailyForecastWeather = true, Temperature = true, Condition = true, Humidity = true, Pressure = true, Visibility = true, Precipitation = true, WindDirection = true, WindSpeed = true, CloudCover = true, SunAltitude = false, FeltHumidity = false, FeltIntensity = false, FeltTemperature = false, FeltVisibility = false, FeltWindIntensity = false, DewPoint = false, WindChill = false, ForecastWeatherPoints = false, Ozone = false };
            this.clientKey = clientKey;
            restClient = new RestSharp.RestClient(baseUrl);
            restClient.AddHandler("text/xml", new DotNetXmlDeserializer());
        }

        public WeatherData GetWorldWeatherOnlineWeather(decimal latitude, decimal longitude, int numberOfDays) {
            var request = new RestSharp.RestRequest("free/v1/weather.ashx", RestSharp.Method.GET);
            request.AddParameter("q", string.Format("{0},{1}", latitude.ToString(System.Globalization.NumberFormatInfo.InvariantInfo), longitude.ToString(System.Globalization.NumberFormatInfo.InvariantInfo)), RestSharp.ParameterType.GetOrPost);
            request.AddParameter("format", "xml", RestSharp.ParameterType.GetOrPost);
            request.AddParameter("num_of_days", numberOfDays, RestSharp.ParameterType.GetOrPost);
            if (numberOfDays <= 0) {
                request.AddParameter("fx", "no", RestSharp.ParameterType.GetOrPost);
            }
            request.AddParameter("includeLocation", "yes", RestSharp.ParameterType.GetOrPost);
            request.AddParameter("key", clientKey, RestSharp.ParameterType.GetOrPost);

            var response = restClient.Execute<WeatherData>(request);
            if (response != null && response.Data != null) {
                return response.Data;
            } else {
                return null;
            }
        }

        private static Common.Weather.WeatherPoint ConvertToWeatherPoint(WeatherCurrentCondition source, WeatherArea area) {
            var destination = new Common.Weather.WeatherPoint();

            if (area != null) {
                destination.Location = new Common.Weather.Location();
                destination.Location.Country = area.Country;
                destination.Location.Locality = area.AreaName;
                destination.Location.Latitude = area.Latitude;
                destination.Location.Longitude = area.Longitude;
            }

            destination.Weather = new Common.Weather.WeatherPointData();
            destination.Weather.Condition = ConvertToWeatherCondition(source.WeatherCode);
            if (source.TemperatureCelsius.HasValue) {
                destination.Weather.Temperature = new Common.Weather.Temperature() { Celsius = source.TemperatureCelsius.Value };
            } else if (source.TemperatureFahrenheit.HasValue) {
                destination.Weather.Temperature = new Common.Weather.Temperature() { Fahrenheit = source.TemperatureFahrenheit.Value };
            }
            destination.Weather.Humidity = source.Humidity;
            destination.Weather.Pressure = source.Pressure;
            destination.Weather.WindDirection = source.WindDirection;
            destination.Weather.WindSpeed = source.WindspeedKmph;
            if (source.Precipitation.GetValueOrDefault(0) > 0) {
                destination.Weather.PrecipitationType = Common.Weather.PrecipitationType.Rain;
                destination.Weather.Precipitation = source.Precipitation;
            }
            destination.Weather.Visibility = source.Visibility;
            destination.Weather.CloudCover = source.CloudCover;

            return destination;
        }

        private static Common.Weather.WeatherPeriod ConvertToWeatherPeriod(WeatherForecast source, WeatherArea area) {
            var destination = new Common.Weather.WeatherPeriod();
            destination.TimeFrom = source.Date.ToUniversalTime().ToLocalTime();
            destination.TimeTo = destination.TimeFrom.AddDays(1);

            if (area != null) {
                destination.Location = new Common.Weather.Location();
                destination.Location.Country = area.Country;
                destination.Location.Locality = area.AreaName;
                destination.Location.Latitude = area.Latitude;
                destination.Location.Longitude = area.Longitude;
            }

            destination.Weather = new Common.Weather.WeatherPeriodData();
            destination.Weather.Condition = ConvertToWeatherCondition(source.WeatherCode);
            if (source.MinTemperatureCelsius.HasValue) {
                destination.Weather.MinTemperature = new Common.Weather.Temperature() { Celsius = source.MinTemperatureCelsius.Value };
            } else if (source.MinTemperatureFahrenheit.HasValue) {
                destination.Weather.MinTemperature = new Common.Weather.Temperature() { Fahrenheit = source.MinTemperatureFahrenheit.Value };
            }
            if (source.MaxTemperatureCelsius.HasValue) {
                destination.Weather.MaxTemperature = new Common.Weather.Temperature() { Celsius = source.MaxTemperatureCelsius.Value };
            } else if (source.MaxTemperatureFahrenheit.HasValue) {
                destination.Weather.MaxTemperature = new Common.Weather.Temperature() { Fahrenheit = source.MaxTemperatureFahrenheit.Value };
            }
            destination.Weather.WindDirection = source.WindDirection;
            destination.Weather.WindSpeed = source.WindspeedKmph;
            destination.Weather.Precipitation = source.Precipitation;

            return destination;
        }

        private static Common.Weather.WeatherCondition ConvertToWeatherCondition(string weatherCode) {
            switch (weatherCode) {
                case "395": // Moderate or heavy snow in area with thunder	wsymbol_0012_heavy_snow_showers	wsymbol_0028_heavy_snow_showers_night
                    return Common.Weather.WeatherCondition.Snowy; // eventuell Stormy
                case "392": // Patchy light snow in area with thunder	wsymbol_0016_thundery_showers	wsymbol_0032_thundery_showers_night
                    return Common.Weather.WeatherCondition.Snowy;
                case "389": //	Moderate or heavy rain in area with thunder	wsymbol_0024_thunderstorms	wsymbol_0040_thunderstorms_night
                    return Common.Weather.WeatherCondition.Rainy;
                case "386": //	Patchy light rain in area with thunder	wsymbol_0016_thundery_showers	wsymbol_0032_thundery_showers_night
                    return Common.Weather.WeatherCondition.Rainy;
                case "377": //	Moderate or heavy showers of ice pellets	wsymbol_0021_cloudy_with_sleet	wsymbol_0037_cloudy_with_sleet_night
                    return Common.Weather.WeatherCondition.Hailing;
                case "374": //	Light showers of ice pellets	wsymbol_0013_sleet_showers	wsymbol_0029_sleet_showers_night
                    return Common.Weather.WeatherCondition.Hailing;
                case "371": //	Moderate or heavy snow showers	wsymbol_0012_heavy_snow_showers	wsymbol_0028_heavy_snow_showers_night
                    return Common.Weather.WeatherCondition.SnowFlurries;
                case "368": //	Light snow showers	wsymbol_0011_light_snow_showers	wsymbol_0027_light_snow_showers_night
                    return Common.Weather.WeatherCondition.Snowy;
                case "365": //	Moderate or heavy sleet showers	wsymbol_0013_sleet_showers	wsymbol_0029_sleet_showers_night
                    return Common.Weather.WeatherCondition.Rainy;
                case "362": //	Light sleet showers	wsymbol_0013_sleet_showers	wsymbol_0029_sleet_showers_night
                    return Common.Weather.WeatherCondition.Rainy;
                case "359": //	Torrential rain shower	wsymbol_0018_cloudy_with_heavy_rain	wsymbol_0034_cloudy_with_heavy_rain_night
                    return Common.Weather.WeatherCondition.Rainy;
                case "356": //	Moderate or heavy rain shower	wsymbol_0010_heavy_rain_showers	wsymbol_0026_heavy_rain_showers_night
                    return Common.Weather.WeatherCondition.Rainy;
                case "353": //	Light rain shower	wsymbol_0009_light_rain_showers	wsymbol_0025_light_rain_showers_night
                    return Common.Weather.WeatherCondition.Rainy;
                case "350": //	Ice pellets	wsymbol_0021_cloudy_with_sleet	wsymbol_0037_cloudy_with_sleet_night
                    return Common.Weather.WeatherCondition.Hailing;
                case "338": //	Heavy snow	wsymbol_0020_cloudy_with_heavy_snow	wsymbol_0036_cloudy_with_heavy_snow_night
                    return Common.Weather.WeatherCondition.Snowy;
                case "335": //	Patchy heavy snow	wsymbol_0012_heavy_snow_showers	wsymbol_0028_heavy_snow_showers_night
                    return Common.Weather.WeatherCondition.Snowy;
                case "332": //	Moderate snow	wsymbol_0020_cloudy_with_heavy_snow	wsymbol_0036_cloudy_with_heavy_snow_night
                    return Common.Weather.WeatherCondition.Snowy;
                case "329": //	Patchy moderate snow	wsymbol_0020_cloudy_with_heavy_snow	wsymbol_0036_cloudy_with_heavy_snow_night
                    return Common.Weather.WeatherCondition.Snowy;
                case "326": //	Light snow	wsymbol_0011_light_snow_showers	wsymbol_0027_light_snow_showers_night
                    return Common.Weather.WeatherCondition.Snowy;
                case "323": //	Patchy light snow	wsymbol_0011_light_snow_showers	wsymbol_0027_light_snow_showers_night
                    return Common.Weather.WeatherCondition.Snowy;
                case "320": //	Moderate or heavy sleet	wsymbol_0019_cloudy_with_light_snow	wsymbol_0035_cloudy_with_light_snow_night
                    return Common.Weather.WeatherCondition.Rainy;
                case "317": //	Light sleet	wsymbol_0021_cloudy_with_sleet	wsymbol_0037_cloudy_with_sleet_night
                    return Common.Weather.WeatherCondition.Rainy;
                case "314": //	Moderate or Heavy freezing rain	wsymbol_0021_cloudy_with_sleet	wsymbol_0037_cloudy_with_sleet_night
                    return Common.Weather.WeatherCondition.Rainy;
                case "311": //	Light freezing rain	wsymbol_0021_cloudy_with_sleet	wsymbol_0037_cloudy_with_sleet_night
                    return Common.Weather.WeatherCondition.Rainy;
                case "308": //	Heavy rain	wsymbol_0018_cloudy_with_heavy_rain	wsymbol_0034_cloudy_with_heavy_rain_night
                    return Common.Weather.WeatherCondition.Rainy;
                case "305": //	Heavy rain at times	wsymbol_0010_heavy_rain_showers	wsymbol_0026_heavy_rain_showers_night
                    return Common.Weather.WeatherCondition.Rainy;
                case "302": //	Moderate rain	wsymbol_0018_cloudy_with_heavy_rain	wsymbol_0034_cloudy_with_heavy_rain_night
                    return Common.Weather.WeatherCondition.Rainy;
                case "299": //	Moderate rain at times	wsymbol_0010_heavy_rain_showers	wsymbol_0026_heavy_rain_showers_night
                    return Common.Weather.WeatherCondition.Rainy;
                case "296": //	Light rain	wsymbol_0017_cloudy_with_light_rain	wsymbol_0025_light_rain_showers_night
                    return Common.Weather.WeatherCondition.Rainy;
                case "293": //	Patchy light rain	wsymbol_0017_cloudy_with_light_rain	wsymbol_0033_cloudy_with_light_rain_night
                    return Common.Weather.WeatherCondition.Rainy;
                case "284": //	Heavy freezing drizzle	wsymbol_0021_cloudy_with_sleet	wsymbol_0037_cloudy_with_sleet_night
                    return Common.Weather.WeatherCondition.Rainy;
                case "281": //	Freezing drizzle	wsymbol_0021_cloudy_with_sleet	wsymbol_0037_cloudy_with_sleet_night
                    return Common.Weather.WeatherCondition.Rainy;
                case "266": //	Light drizzle	wsymbol_0017_cloudy_with_light_rain	wsymbol_0033_cloudy_with_light_rain_night
                    return Common.Weather.WeatherCondition.Rainy;
                case "263": //	Patchy light drizzle	wsymbol_0009_light_rain_showers	wsymbol_0025_light_rain_showers_night
                    return Common.Weather.WeatherCondition.Rainy;
                case "260": //	Freezing fog	wsymbol_0007_fog	wsymbol_0007_fog
                    return Common.Weather.WeatherCondition.Foggy;
                case "248": //	Fog	wsymbol_0007_fog	wsymbol_0007_fog
                    return Common.Weather.WeatherCondition.Foggy;
                case "230": //	Blizzard	wsymbol_0020_cloudy_with_heavy_snow	wsymbol_0036_cloudy_with_heavy_snow_night
                    return Common.Weather.WeatherCondition.Stormy;
                case "227": //	Blowing snow	wsymbol_0019_cloudy_with_light_snow	wsymbol_0035_cloudy_with_light_snow_night
                    return Common.Weather.WeatherCondition.SnowFlurries;
                case "200": //	Thundery outbreaks in nearby	wsymbol_0016_thundery_showers	wsymbol_0032_thundery_showers_night
                    return Common.Weather.WeatherCondition.Stormy;
                case "185": //	Patchy freezing drizzle nearby	wsymbol_0021_cloudy_with_sleet	wsymbol_0037_cloudy_with_sleet_night
                    return Common.Weather.WeatherCondition.Rainy;
                case "182": //	Patchy sleet nearby	wsymbol_0021_cloudy_with_sleet	wsymbol_0037_cloudy_with_sleet_night
                    return Common.Weather.WeatherCondition.Rainy;
                case "179": //	Patchy snow nearby	wsymbol_0013_sleet_showers	wsymbol_0029_sleet_showers_night
                    return Common.Weather.WeatherCondition.Snowy;
                case "176": //	Patchy rain nearby	wsymbol_0009_light_rain_showers	wsymbol_0025_light_rain_showers_night
                    return Common.Weather.WeatherCondition.Rainy;
                case "143": //	Mist	wsymbol_0006_mist	wsymbol_0006_mist
                    return Common.Weather.WeatherCondition.Foggy;
                case "122": //	Overcast	wsymbol_0004_black_low_cloud	wsymbol_0004_black_low_cloud
                    return Common.Weather.WeatherCondition.Cloudy;
                case "119": //	Cloudy	wsymbol_0003_white_cloud	wsymbol_0004_black_low_cloud
                    return Common.Weather.WeatherCondition.Cloudy;
                case "116": //	Partly Cloudy	wsymbol_0002_sunny_intervals	wsymbol_0008_clear_sky_night
                    return Common.Weather.WeatherCondition.PartlyCloudy;
                case "113": //	Clear/Sunny	wsymbol_0001_sunny	wsymbol_0008_clear_sky_night
                    return Common.Weather.WeatherCondition.Clear;
                default:
                    return Common.Weather.WeatherCondition.Unknown;
            }
        }

        public override Common.Weather.WeatherPoint GetCurrentWeather(decimal latitude, decimal longitude) {
            var weatherData = GetWorldWeatherOnlineWeather(latitude, longitude, 0);
            if (weatherData != null && weatherData.CurrentCondition != null) // && weatherData.CurrentCondition.Count > 0)
            {
                return ConvertToWeatherPoint(weatherData.CurrentCondition, weatherData.NearestArea);
            } else {
                return null;
            }
        }

        public override List<Common.Weather.WeatherPeriod> GetDailyForecastWeather(decimal latitude, decimal longitude) {
            var weatherData = GetWorldWeatherOnlineWeather(latitude, longitude, 5);
            if (weatherData != null && weatherData.Forecasts != null) {
                var list = new List<Common.Weather.WeatherPeriod>();
                foreach (var item in weatherData.Forecasts) {
                    list.Add(ConvertToWeatherPeriod(item, weatherData.NearestArea));
                }
                return list;
            } else {
                return null;
            }
        }
    }
}
