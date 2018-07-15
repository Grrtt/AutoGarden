namespace RaspPi.Abstractions
{
    public interface IGpioPinStateReader
    {
        GpioState GetPinState(GpioPin pin);
    }
}