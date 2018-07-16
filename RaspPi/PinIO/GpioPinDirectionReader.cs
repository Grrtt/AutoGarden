namespace RaspPi.PinIO
{
    using System.Collections.Generic;
    using System.IO;

    using RaspPi.Abstractions;

    public class GpioPinDirectionReader : IGpioPinDirectionReader
    {
        private readonly IDictionary<string, GpioPinDirection> directions;

        public GpioPinDirectionReader()
        {
            directions = CreateDirectionsDictionary();
        }

        public GpioPinDirection GetPinDirection(GpioPin pin)
        {
            string[] lines = File.ReadAllLines($"/sys/class/gpio/gpio{(int)pin}/direction");
            return GetDirectionStringEnumValue(lines[0]);
        }

        private IDictionary<string, GpioPinDirection> CreateDirectionsDictionary()
        {
            Dictionary<string, GpioPinDirection> dictionary = new Dictionary<string, GpioPinDirection>();

            dictionary.Add("in", GpioPinDirection.In);
            dictionary.Add("out", GpioPinDirection.Out);

            return dictionary;
        }

        private GpioPinDirection GetDirectionStringEnumValue(string value)
        {
            return directions[value];
        }
    }
}