namespace RaspPi.Abstractions
{
    public interface IGpioPinout
    {
        void SetPinState(GpioPin pin, GpioState state);
    }
}