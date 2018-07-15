namespace RaspPi.Tests
{
    using System.IO;

    using NUnit.Framework;

    using RaspPi.Abstractions;

    [TestFixture]
    public class TestGpioPinStateWriter
    {
        private const string GpioDirectory = "/sys/class/gpio";

        private readonly string gpioExportFile = $"{GpioDirectory}/export";

        private readonly string gpioUnExportFile = $"{GpioDirectory}/unexport";

        private GpioPinStateWriter systemUnderTest;

        [Test]
        public void ExportPin_WhenInvoked_WritesPinToGpioExportFile()
        {
            ExportPin(GpioPin.One);

            string[] lines = File.ReadAllLines(gpioExportFile);

            Assert.That(lines[0], Is.EqualTo("1"));
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
            Directory.Delete("/sys", true);
        }

        [Test]
        public void UnExportPin_WhenInvoked_WritesPinToGpioUnExportFile()
        {
            UnExportPin(GpioPin.One);

            string[] lines = File.ReadAllLines(gpioUnExportFile);

            Assert.That(lines[0], Is.EqualTo("1"));
        }

        private void CreateGpioDirectory()
        {
            Directory.CreateDirectory(GpioDirectory);
        }

        private GpioPinStateWriter CreateSystemUnderTest()
        {
            return new GpioPinStateWriter();
        }

        private void ExportPin(GpioPin pin)
        {
            systemUnderTest.ExportPin(pin);
        }

        private void UnExportPin(GpioPin pin)
        {
            systemUnderTest.UnExportPin(pin);
        }
    }
}