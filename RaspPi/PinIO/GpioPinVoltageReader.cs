namespace RaspPi.PinIO
{
    using RaspPi.Abstractions;
    public class GpioPinVoltageReader : IGpioPinVoltageReader
    {
        public GpioVoltage GetPinVoltage(GpioPin pin)
        {
            //TODO: To be implemented later.
            return GpioVoltage.High;
        }
    }
}