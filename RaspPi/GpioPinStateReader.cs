namespace RaspPi
{
    using RaspPi.Abstractions;

    public class GpioPinStateReader : IGpioPinStateReader
    {
        public GpioState GetPinState(GpioPin pin)
        {
            //TODO: To be implemented later.
            return GpioState.Open;
        }
    }
}