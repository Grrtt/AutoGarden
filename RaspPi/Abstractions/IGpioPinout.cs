﻿namespace RaspPi.Abstractions
{
    public interface IGpioPinout
    {
        void ClosePin(GpioPin pin);

        void OpenPin(GpioPin pin);

        void SetHigh(GpioPin pin);

        void SetLow(GpioPin pin);
    }
}