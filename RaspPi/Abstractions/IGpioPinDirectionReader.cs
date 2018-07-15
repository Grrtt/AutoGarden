namespace RaspPi.Abstractions
{
    public interface IGpioPinDirectionReader
    {
        GpioPinDirection GetPinDirection(GpioPin pin);
    }
}