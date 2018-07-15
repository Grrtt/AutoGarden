namespace RaspPi.Tests
{
    using System;
    using System.IO;

    using NUnit.Framework;

    using RaspPi.Abstractions;
    using RaspPi.PinIO;

    [TestFixture]
    public class TestGpioPinVoltageReader
    {
        private const string GpioPin1Directory = "/sys/class/gpio/gpio1";

        private const string GpioPin1ValueFilePath = "/sys/class/gpio/gpio1/value";

        private const string TopLevelSysDirectory = "/sys/";

        private GpioPinVoltageReader systemUnderTest;

        [Test]
        public void GetPinVoltage_WhenInvoked_ReadsPinsValueFile()
        {
            WriteVoltageValue("1");
            GpioPinVoltage actual = InvokeGetPinVoltage(GpioPin.One);

            string[] lines = File.ReadAllLines(GpioPin1ValueFilePath);
            Enum.TryParse(lines[0], out GpioPinVoltage voltage);

            Assert.That(voltage, Is.EqualTo(actual));
        }

        [SetUp]
        public void SetUp()
        {
            CreateGpioPinDirectory();

            systemUnderTest = CreateSystemUnderTest();
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(TopLevelSysDirectory))
            {
                Directory.Delete(TopLevelSysDirectory, true);
            }
        }

        private void CreateGpioPinDirectory()
        {
            Directory.CreateDirectory(GpioPin1Directory);
        }

        private GpioPinVoltageReader CreateSystemUnderTest()
        {
            return new GpioPinVoltageReader();
        }

        private GpioPinVoltage InvokeGetPinVoltage(GpioPin pin)
        {
            return systemUnderTest.GetPinVoltage(pin);
        }

        private void WriteVoltageValue(string value)
        {
            File.WriteAllLines(GpioPin1ValueFilePath, new[] { value });
        }
    }
}