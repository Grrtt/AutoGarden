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
            Enum.TryParse(voltageString, out GpioPinVoltage voltage);
            return voltage;
        }

        private string GetPinVoltageString(GpioPin pin)
        {
            string[] lines = File.ReadAllLines($"/sys/class/gpio/gpio{(int)pin}/value");
            return lines[0];
        }
    }
}