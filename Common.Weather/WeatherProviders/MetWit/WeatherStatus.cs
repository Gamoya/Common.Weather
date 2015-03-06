using System.ComponentModel;

namespace Gamoya.Common.Weather.WeatherProviders.Metwit {
    [TypeConverter(typeof(EnumDescriptionTypeConverter<WeatherStatus>))]
    public enum WeatherStatus {
        [Description("unknown")]
        Unknown,
        [Description("clear")]
        Clear,
        [Description("rainy")]
        Rainy,
        [Description("stormy")]
        Stormy,
        [Description("snowy")]
        Snowy,
        [Description("partly cloudy")]
        PartlyCloudy,
        [Description("cloudy")]
        Cloudy,
        [Description("hailing")]
        Hailing,
        [Description("heavy seas")]
        HeavySeas,
        [Description("calm seas")]
        CalmSeas,
        [Description("foggy")]
        Foggy,
        [Description("snow flurries")]
        SnowFlurries,
        [Description("windy")]
        Windy
    }
}
