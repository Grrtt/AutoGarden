namespace RaspPi.Abstractions
{
    public interface IGpioPinVoltageWriter
    {
        void SetHigh(GpioPin pin);

        void SetLow(GpioPin pin);
    }
}