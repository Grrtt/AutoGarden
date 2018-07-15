namespace RaspPi.ErrorHandling
{
    using RaspPi.Abstractions;

    public class GpioErrorHandling : IGpioErrorHandler
    {
        public void HandleStateError(GpioState state)
        {
            //TODO: To be implemented later.
        }

        public void HandleVoltageError(GpioVoltage voltage)
        {
            //TODO: To be implemented later.
        }
    }
}