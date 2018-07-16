namespace RaspPi.Tests.PinIO
{
    using System.IO;

    using NUnit.Framework;

    using RaspPi.Abstractions;
    using RaspPi.PinIO;

    [TestFixture]
    public class TestGpioPinDirectionReader
    {
        private const string GpioPinDirectory = "/sys/class/gpio/gpio1";

        private GpioPinDirectionReader systemUnderTest;

        [Test]
        public void GetPinDirection_WhenDirectionValueIsIn_ReturnsGpioPinDirectionIn()
        {
            WriteValueToGpioPinDirectionFile("in");

            GpioPinDirection actual = InvokeGetPinDirection(GpioPin.One);

            Assert.That(actual, Is.EqualTo(GpioPinDirection.In));
        }

        [Test]
        public void GetPinDirection_WhenDirectionValueIsOut_ReturnsGpioPinDirectionOut()
        {
            WriteValueToGpioPinDirectionFile("out");

            GpioPinDirection actual = InvokeGetPinDirection(GpioPin.One);

            Assert.That(actual, Is.EqualTo(GpioPinDirection.Out));
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
            if (Directory.Exists("/sys"))
            {
                Directory.Delete("/sys/", true);
            }
        }

        private void CreateGpioDirectory()
        {
            Directory.CreateDirectory(GpioPinDirectory);
        }

        private GpioPinDirectionReader CreateSystemUnderTest()
        {
            return new GpioPinDirectionReader();
        }

        private GpioPinDirection InvokeGetPinDirection(GpioPin pin)
        {
            return systemUnderTest.GetPinDirection(pin);
        }

        private void WriteValueToGpioPinDirectionFile(string value)
        {
            File.WriteAllLines($"{GpioPinDirectory}/direction", new[] { value });
        }
    }
}