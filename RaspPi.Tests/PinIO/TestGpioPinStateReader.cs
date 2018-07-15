namespace RaspPi.Tests.PinIO
{
    using System.IO;

    using NUnit.Framework;

    using RaspPi.Abstractions;
    using RaspPi.PinIO;

    [TestFixture]
    public class TestGpioPinStateReader
    {
        private const string GpioDirectory = "/sys/class/gpio";

        private const string GpioTopLevelDirectory = "/sys";

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
            if (Directory.Exists($"{GpioTopLevelDirectory}"))
            {
                Directory.Delete($"{GpioTopLevelDirectory}", true);
            }
        }

        private void CreateGpioFolder(string folder)
        {
            Directory.CreateDirectory($"{GpioDirectory}/{folder}");
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