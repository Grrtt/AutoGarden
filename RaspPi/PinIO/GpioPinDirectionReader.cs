namespace RaspPi.PinIO
{
    using RaspPi.Abstractions;

    public class GpioPinDirectionReader : IGpioPinDirectionReader
    {
        public GpioPinDirection GetPinDirection(GpioPin pin)
        {
            //TODO: To be implemented later.
            return GpioPinDirection.In;
        }
    }
}