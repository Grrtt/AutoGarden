namespace AppRunner
{
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    using RaspPi.Abstractions;

    class Program
    {
        static void Main(string[] args)
        {
            WindsorContainer container = new WindsorContainer();
            container.Register(
                Classes.FromAssemblyNamed("RaspPi").Pick().WithServiceAllInterfaces().LifestyleTransient());

            IGpioPinout pinout = container.Resolve<IGpioPinout>();

            pinout.OpenPin(GpioPin.Two);
        }
    }
}