namespace RaspPi
{
    using System.IO;

    using RaspPi.Abstractions;

    public class GpioPinStateWriter : IGpioPinStateWriter
    {
        private const string GpioExportFile = "/sys/class/gpio/export";

        private const string GpioUnExportFile = "/sys/class/gpio/unexport";

        public void ExportPin(GpioPin pin)
        {
            File.WriteAllLines(GpioExportFile, new[] { $"{(int)pin}" });
        }

        public void UnExportPin(GpioPin pin)
        {
            File.WriteAllLines(GpioUnExportFile, new[] { $"{(int)pin}" });
        }
    }
}