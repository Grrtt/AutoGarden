namespace RaspPi.Abstractions
{
    public interface IGpioPinVoltageReader
    {
        GpioVoltage GetPinVoltage(GpioPin pin);
    }
}