namespace RaspPi.PinIO
{
    using System.IO;

    using RaspPi.Abstractions;

    public class GpioPinVoltageWriter : IGpioPinVoltageWriter
    {
        private readonly string gpioFilePath = "/sys/class/gpio/gpio{0}/value";

        public void SetHigh(GpioPin pin)
        {
            WriteVoltageToPinValueFile(pin, 1);
        }

        public void SetLow(GpioPin pin)
        {
            WriteVoltageToPinValueFile(pin, 0);
        }

        private void WriteVoltageToPinValueFile(GpioPin pin, int value)
        {
            string filePath = string.Format(gpioFilePath, (int)pin);
            File.WriteAllLines(filePath, new[] { $"{value}" });
        }
    }
}