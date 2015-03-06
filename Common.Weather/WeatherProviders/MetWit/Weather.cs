using System.ComponentModel;

namespace Gamoya.Common.Weather.WeatherProviders.Metwit
{
    public class Weather
    {
        private static TypeConverter statusConverter = TypeDescriptor.GetConverter(typeof(WeatherStatus));

        [RestSharp.Deserializers.DeserializeAs(Name = "status")]
        [Newtonsoft.Json.JsonProperty("status")]
        public string Status { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "measured")]
        [Newtonsoft.Json.JsonProperty("measured")]
        public WeatherMeasured Measured { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "felted")]
        [Newtonsoft.Json.JsonProperty("felted")]
        public WeatherFelt Felted { get; set; }

        public WeatherStatus WeatherCondition
        {
            get
            {
                return (WeatherStatus)statusConverter.ConvertFrom(Status);
            }
            set
            {
                Status = statusConverter.ConvertToString(value);
            }
        }
    }
}
