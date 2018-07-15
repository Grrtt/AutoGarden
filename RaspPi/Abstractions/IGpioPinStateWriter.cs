namespace RaspPi.Abstractions
{
    public interface IGpioPinStateWriter
    {
        void ExportPin(GpioPin pin);

        void UnExportPin(GpioPin pin);
    }
}