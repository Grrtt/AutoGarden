namespace RaspPi.ErrorHandling
{
    using RaspPi.Abstractions;

    public interface IGpioErrorHandler
    {
        void HandleStateError(GpioState state);

        void HandleVoltageError(GpioPinVoltage pinVoltage);
    }
}