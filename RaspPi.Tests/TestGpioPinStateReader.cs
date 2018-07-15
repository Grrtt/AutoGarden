namespace RaspPi.Tests
{
    using System.IO;

    using NUnit.Framework;

    using RaspPi.Abstractions;

    [TestFixture]
    public class TestGpioPinStateReader
    {
        private const string gpioTopLevelDirectory = "/sys/class/gpio/";

        private GpioPinStateReader systemUnderTest;

        [Test]
        public void GetPinState_WhenGpioPinFolderDoesNotExist_ReturnsGpioStateClosed()
        {
            GpioState actual = InvokeGetPinState(GpioPin.One);

            Assert.That(actual, Is.EqualTo(GpioState.Closed));
        }

        [Test]
        public void GetPinState_WhenGpioPinFolderExists_ReturnsGpioStateOpen()
        {
            CreateGpioFolder("gpio1");

            GpioState actual = InvokeGetPinState(GpioPin.One);

            Assert.That(actual, Is.EqualTo(GpioState.Open));
        }

        [SetUp]
        public void SetUp()
        {
            systemUnderTest = CreateSystemUnderTest();
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists($"{gpioTopLevelDirectory}"))
            {
                Directory.Delete($"{gpioTopLevelDirectory}", true);
            }
        }

        private void CreateGpioFolder(string folder)
        {
            Directory.CreateDirectory($"{gpioTopLevelDirectory}/{folder}");
        }

        private GpioPinStateReader CreateSystemUnderTest()
        {
            return new GpioPinStateReader();
        }

        private GpioState InvokeGetPinState(GpioPin pin)
        {
            return systemUnderTest.GetPinState(pin);
        }
    }
}