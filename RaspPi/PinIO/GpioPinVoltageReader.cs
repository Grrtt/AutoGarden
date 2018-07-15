namespace RaspPi.PinIO
{
    using System;
    using System.IO;

    using RaspPi.Abstractions;

    public class GpioPinVoltageReader : IGpioPinVoltageReader
    {
        public GpioPinVoltage GetPinVoltage(GpioPin pin)
        {
            string voltageString = GetPinVoltageString(pin);
            return TryParse<GpioPinVoltage>(voltageString);
        }

        private string GetPinVoltageString(GpioPin pin)
        {
            string[] lines = File.ReadAllLines($"/sys/class/gpio/gpio{(int)pin}/value");
            return lines[0];
        }

        private T TryParse<T>(string valueString)
            where T : struct, IConvertible
        {
            Enum.TryParse(valueString, out T value);
            return value;
        }
    }
}