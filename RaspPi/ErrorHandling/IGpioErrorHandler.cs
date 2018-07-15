namespace RaspPi.ErrorHandling
{
    using RaspPi.Abstractions;

    public interface IGpioErrorHandler
    {
        void HandleDirectionError(GpioPinDirection direction);

        void HandleStateError(GpioState state);

        void HandleVoltageError(GpioPinVoltage pinVoltage);
    }
}