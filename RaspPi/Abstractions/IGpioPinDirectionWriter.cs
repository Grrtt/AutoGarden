namespace RaspPi.Abstractions
{
    public interface IGpioPinDirectionWriter
    {
        void SetRead(GpioPin pin);

        void SetWrite(GpioPin pin);
    }
}