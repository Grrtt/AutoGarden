namespace RaspPi.Tests.PinIO
{
    using System.IO;

    using NUnit.Framework;

    using RaspPi.Abstractions;
    using RaspPi.PinIO;

    [TestFixture]
    public class TestGpioPinVoltageWriter
    {
        private const string GpioTopLevelDirectory = "/sys/";

        private GpioPinVoltageWriter systemUnderTest;

        [Test]
        public void SetHigh_WhenInvoked_Writes1ToGpioPinValueFile()
        {
            InvokeSetHigh(GpioPin.One);

            string[] lines = File.ReadAllLines("/sys/class/gpio/gpio1/value");

            Assert.That(lines[0], Is.EqualTo("1"));
        }

        [Test]
        public void SetLow_WhenInvoked_Writes0ToGpioPinValueFile()
        {
            InvokeSetLow(GpioPin.One);

            string[] lines = File.ReadAllLines("/sys/class/gpio/gpio1/value");

            Assert.That(lines[0], Is.EqualTo("0"));
        }

        [SetUp]
        public void SetUp()
        {
            CreateGpioDirectory();

            systemUnderTest = CreateSystemUnderTest();
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(GpioTopLevelDirectory))
            {
                Directory.Delete(GpioTopLevelDirectory, true);
            }
        }

        private void CreateGpioDirectory()
        {
            Directory.CreateDirectory("/sys/class/gpio/gpio1");
        }

        private GpioPinVoltageWriter CreateSystemUnderTest()
        {
            return new GpioPinVoltageWriter();
        }

        private void InvokeSetHigh(GpioPin pin)
        {
            systemUnderTest.SetHigh(pin);
        }

        private void InvokeSetLow(GpioPin pin)
        {
            systemUnderTest.SetLow(pin);
        }
    }
}