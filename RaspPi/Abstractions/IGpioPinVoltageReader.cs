namespace RaspPi.Abstractions
{
    public interface IGpioPinVoltageReader
    {
        GpioPinVoltage GetPinVoltage(GpioPin pin);
    }
}