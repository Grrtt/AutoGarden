namespace RaspPi.Abstractions
{
    public interface IGpioPinVoltageWriter
    {
        void OutputHigh(GpioPin pin);

        void OutputLow(GpioPin pin);
    }
}