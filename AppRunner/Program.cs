namespace AppRunner
{
    using System;
    using System.Linq;
    using System.Threading;

    using Unosquare.RaspberryIO;
    using Unosquare.RaspberryIO.Gpio;

    class Program
    {
        static void Main(string[] args)
        {
            foreach (GpioPin pin in Pi.Gpio.OrderBy(x => x.BcmPinNumber))
            {
                Console.WriteLine(pin.Name);
                Console.WriteLine(pin.BcmPinNumber);
                Console.WriteLine(pin.PinMode);
                Console.WriteLine(pin.ReadValue());
            }

            GpioPin gpioPin = Pi.Gpio.GetGpioPinByBcmPinNumber(2);
            gpioPin.PinMode = GpioPinDriveMode.Output;

            while(true)
            {
                gpioPin.Write(GpioPinValue.Low);
                Thread.Sleep(500);
                gpioPin.Write(GpioPinValue.High);
                Thread.Sleep(500);
            }
        }
    }
}