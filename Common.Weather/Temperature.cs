
namespace Gamoya.Common.Weather {
    public class Temperature {
        private const decimal CelsiusOffset = 273.15m;
        private const decimal FahrenheitMultiplier = 1.8m;
        private const decimal FahrenheitOffset = 459.67m;

        public decimal Kelvin { get; set; }
        public decimal Celsius {
            get {
                return Kelvin - CelsiusOffset;
            }
            set {
                Kelvin = value + CelsiusOffset;
            }
        }
        public decimal Fahrenheit {
            get {
                return Kelvin * FahrenheitMultiplier - FahrenheitOffset;
            }
            set {
                Kelvin = (value + FahrenheitOffset) / FahrenheitMultiplier;
            }
        }
    }
}
