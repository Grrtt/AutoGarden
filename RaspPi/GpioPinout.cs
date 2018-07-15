namespace RaspPi
{
    using RaspPi.Abstractions;
    using RaspPi.ErrorHandling;

    public class GpioPinout : IGpioPinout
    {
        private readonly IGpioErrorHandler gpioErrorHandler;

        private readonly IGpioPinStateReader gpioPinStateReader;

        private readonly IGpioPinStateWriter gpioPinStateWriter;

        public GpioPinout(
            IGpioPinStateReader gpioPinStateReader,
            IGpioPinStateWriter gpioPinStateWriter,
            IGpioErrorHandler gpioErrorHandler)
        {
            this.gpioPinStateReader = gpioPinStateReader;
            this.gpioPinStateWriter = gpioPinStateWriter;
            this.gpioErrorHandler = gpioErrorHandler;
        }

        public void ClosePin(GpioPin pin)
        {
            if (IsPinStateEqualTo(pin, GpioState.Open))
            {
                UnExportPin(pin);
            }
            else
            {
                HandleStateError(GpioState.Closed);
            }
        }

        public void OpenPin(GpioPin pin)
        {
            if (IsPinStateEqualTo(pin, GpioState.Closed))
            {
                ExportPin(pin);
            }
            else
            {
                HandleStateError(GpioState.Open);
            }
        }

        private void ExportPin(GpioPin pin)
        {
            gpioPinStateWriter.ExportPin(pin);
        }

        private GpioState GetPinState(GpioPin pin)
        {
            return gpioPinStateReader.GetPinState(pin);
        }

        private void HandleStateError(GpioState state)
        {
            gpioErrorHandler.HandleStateError(state);
        }

        private bool IsPinStateEqualTo(GpioPin pin, GpioState state)
        {
            GpioState actualState = GetPinState(pin);
            return actualState == state;
        }

        private void UnExportPin(GpioPin pin)
        {
            gpioPinStateWriter.UnExportPin(pin);
        }
    }
}