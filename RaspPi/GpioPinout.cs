namespace RaspPi
{
    using RaspPi.Abstractions;
    using RaspPi.ErrorHandling;

    public class GpioPinout : IGpioPinout
    {
        private readonly IGpioErrorHandler gpioErrorHandler;

        private readonly IGpioPinStateReader gpioPinStateReader;

        private readonly IGpioPinStateWriter gpioPinStateWriter;

        private readonly IGpioPinVoltageReader gpioPinVoltageReader;

        private readonly IGpioPinVoltageWriter gpioPinVoltageWriter;

        public GpioPinout(
            IGpioPinStateReader gpioPinStateReader,
            IGpioPinStateWriter gpioPinStateWriter,
            IGpioPinVoltageReader gpioPinVoltageReader,
            IGpioPinVoltageWriter gpioPinVoltageWriter,
            IGpioErrorHandler gpioErrorHandler)
        {
            this.gpioPinStateReader = gpioPinStateReader;
            this.gpioPinStateWriter = gpioPinStateWriter;
            this.gpioPinVoltageReader = gpioPinVoltageReader;
            this.gpioPinVoltageWriter = gpioPinVoltageWriter;
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

        public void OutputHigh(GpioPin pin)
        {
            if (IsPinVoltageEqualTo(pin, GpioVoltage.Low))
            {
                gpioPinVoltageWriter.OutputHigh(pin);
            }
            else
            {
                HandleVoltageError(GpioVoltage.High);
            }
        }

        public void OutputLow(GpioPin pin)
        {
            if (IsPinVoltageEqualTo(pin, GpioVoltage.High))
            {
                gpioPinVoltageWriter.OutputLow(pin);
            }
            else
            {
                HandleVoltageError(GpioVoltage.Low);
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

        private GpioVoltage GetPinVoltage(GpioPin pin)
        {
            return gpioPinVoltageReader.GetPinVoltage(pin);
        }

        private void HandleStateError(GpioState state)
        {
            gpioErrorHandler.HandleStateError(state);
        }

        private void HandleVoltageError(GpioVoltage voltage)
        {
            gpioErrorHandler.HandleVoltageError(voltage);
        }

        private bool IsPinStateEqualTo(GpioPin pin, GpioState state)
        {
            GpioState actualState = GetPinState(pin);
            return actualState == state;
        }

        private bool IsPinVoltageEqualTo(GpioPin pin, GpioVoltage voltage)
        {
            GpioVoltage actualVoltage = GetPinVoltage(pin);
            return actualVoltage == voltage;
        }

        private void UnExportPin(GpioPin pin)
        {
            gpioPinStateWriter.UnExportPin(pin);
        }
    }
}