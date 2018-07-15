namespace RaspPi
{
    using RaspPi.Abstractions;
    using RaspPi.ErrorHandling;

    public class GpioPinout : IGpioPinout
    {
        private readonly IGpioErrorHandler gpioErrorHandler;

        private readonly IGpioPinDirectionReader gpioPinDirectionReader;

        private readonly IGpioPinDirectionWriter gpioPinDirectionWriter;

        private readonly IGpioPinStateReader gpioPinStateReader;

        private readonly IGpioPinStateWriter gpioPinStateWriter;

        private readonly IGpioPinVoltageReader gpioPinVoltageReader;

        private readonly IGpioPinVoltageWriter gpioPinVoltageWriter;

        public GpioPinout(
            IGpioPinStateReader gpioPinStateReader,
            IGpioPinStateWriter gpioPinStateWriter,
            IGpioPinDirectionReader gpioPinDirectionReader,
            IGpioPinDirectionWriter gpioPinDirectionWriter,
            IGpioPinVoltageReader gpioPinVoltageReader,
            IGpioPinVoltageWriter gpioPinVoltageWriter,
            IGpioErrorHandler gpioErrorHandler)
        {
            this.gpioPinStateReader = gpioPinStateReader;
            this.gpioPinStateWriter = gpioPinStateWriter;
            this.gpioPinDirectionReader = gpioPinDirectionReader;
            this.gpioPinDirectionWriter = gpioPinDirectionWriter;
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

        public void SetHigh(GpioPin pin)
        {
            if (IsPinVoltageEqualTo(pin, GpioPinVoltage.Low))
            {
                gpioPinVoltageWriter.SetHigh(pin);
            }
            else
            {
                HandleVoltageError(GpioPinVoltage.High);
            }
        }

        public void SetLow(GpioPin pin)
        {
            if (IsPinVoltageEqualTo(pin, GpioPinVoltage.High))
            {
                gpioPinVoltageWriter.SetLow(pin);
            }
            else
            {
                HandleVoltageError(GpioPinVoltage.Low);
            }
        }

        public void SetRead(GpioPin pin)
        {
            if (IsPinDirectionEqualTo(pin, GpioPinDirection.Out))
            {
                gpioPinDirectionWriter.SetRead(pin);
            }
            else
            {
                HandleDirectionError(GpioPinDirection.In);
            }
        }

        public void SetWrite(GpioPin pin)
        {
            if (IsPinDirectionEqualTo(pin, GpioPinDirection.In))
            {
                gpioPinDirectionWriter.SetWrite(pin);
            }
            else
            {
                HandleDirectionError(GpioPinDirection.Out);
            }
        }

        private void ExportPin(GpioPin pin)
        {
            gpioPinStateWriter.ExportPin(pin);
        }

        private GpioPinDirection GetPinDirection(GpioPin pin)
        {
            return gpioPinDirectionReader.GetPinDirection(pin);
        }

        private GpioState GetPinState(GpioPin pin)
        {
            return gpioPinStateReader.GetPinState(pin);
        }

        private GpioPinVoltage GetPinVoltage(GpioPin pin)
        {
            return gpioPinVoltageReader.GetPinVoltage(pin);
        }

        private void HandleDirectionError(GpioPinDirection direction)
        {
            gpioErrorHandler.HandleDirectionError(direction);
        }

        private void HandleStateError(GpioState state)
        {
            gpioErrorHandler.HandleStateError(state);
        }

        private void HandleVoltageError(GpioPinVoltage pinVoltage)
        {
            gpioErrorHandler.HandleVoltageError(pinVoltage);
        }

        private bool IsPinDirectionEqualTo(GpioPin pin, GpioPinDirection direction)
        {
            GpioPinDirection actualDirection = GetPinDirection(pin);
            return actualDirection == direction;
        }

        private bool IsPinStateEqualTo(GpioPin pin, GpioState state)
        {
            GpioState actualState = GetPinState(pin);
            return actualState == state;
        }

        private bool IsPinVoltageEqualTo(GpioPin pin, GpioPinVoltage pinVoltage)
        {
            GpioPinVoltage actualPinVoltage = GetPinVoltage(pin);
            return actualPinVoltage == pinVoltage;
        }

        private void UnExportPin(GpioPin pin)
        {
            gpioPinStateWriter.UnExportPin(pin);
        }
    }
}