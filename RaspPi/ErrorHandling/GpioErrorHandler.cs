namespace RaspPi.ErrorHandling
{
    using RaspPi.Abstractions;

    public class GpioErrorHandler : IGpioErrorHandler
    {
        public void HandleDirectionError(GpioPinDirection direction)
        {
            //TODO: To be implemented later.
        }

        public void HandleStateError(GpioState state)
        {
            //TODO: To be implemented later.
        }

        public void HandleVoltageError(GpioPinVoltage pinVoltage)
        {
            //TODO: To be implemented later.
        }
    }
}