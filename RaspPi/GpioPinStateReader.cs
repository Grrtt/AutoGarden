namespace RaspPi
{
    using System.IO;

    using RaspPi.Abstractions;

    public class GpioPinStateReader : IGpioPinStateReader
    {
        public GpioState GetPinState(GpioPin pin)
        {
            if (Directory.Exists($"/sys/class/gpio/gpio{(int)pin}"))
            {
                return GpioState.Open;
            }

            return GpioState.Closed;
        }
    }
}