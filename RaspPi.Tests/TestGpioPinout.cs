namespace RaspPi.Tests
{
    using NSubstitute;

    using NUnit.Framework;

    using RaspPi.Abstractions;
    using RaspPi.ErrorHandling;

    [TestFixture]
    public class TestGpioPinout
    {
        private IGpioErrorHandler gpioErrorHandlerMock;

        private IGpioPinStateReader gpioPinStateReaderMock;

        private IGpioPinStateWriter gpioPinStateWriterMock;

        private IGpioPinVoltageReader gpioPinVoltageReaderMock;

        private IGpioPinVoltageWriter gpioPinVoltageWriterMock;

        private GpioPinout systemUnderTest;

        [Test]
        public void ClosePin_WhenClosedPinIsAskedToClose_GivesStateToErrorHandler()
        {
            ModifyPinStateReaderMockReturnValue(GpioPin.One, GpioState.Closed);

            InvokeClosePin(GpioPin.One);

            gpioErrorHandlerMock.Received(1).HandleStateError(GpioState.Closed);
        }

        [Test]
        public void ClosePin_WhenOpenPinIsAskedToClose_ClosesPin()
        {
            ModifyPinStateReaderMockReturnValue(GpioPin.One, GpioState.Open);

            InvokeClosePin(GpioPin.One);

            gpioPinStateWriterMock.Received(1).UnExportPin(GpioPin.One);
        }

        [Test]
        public void OpenPin_WhenClosedPinIsAskedToOpen_OpensPin()
        {
            ModifyPinStateReaderMockReturnValue(GpioPin.One, GpioState.Closed);

            InvokeOpenPin(GpioPin.One);

            gpioPinStateWriterMock.Received(1).ExportPin(GpioPin.One);
        }

        [Test]
        public void OpenPin_WhenOpenPinIsAskedToOpen_GivesStateToErrorHandler()
        {
            ModifyPinStateReaderMockReturnValue(GpioPin.One, GpioState.Open);

            InvokeOpenPin(GpioPin.One);

            gpioErrorHandlerMock.Received(1).HandleStateError(GpioState.Open);
        }

        [Test]
        public void OutputHigh_WhenPinVoltageIsHigh_GivesVoltageToErrorHandler()
        {
            ModifyPinVoltageReaderMockReturnValue(GpioPin.One, GpioVoltage.High);

            InvokeOutputHigh(GpioPin.One);

            gpioErrorHandlerMock.Received(1).HandleVoltageError(GpioVoltage.High);
        }

        [Test]
        public void OutputHigh_WhenVoltageIsLow_ChangesPinOutputToHighVoltage()
        {
            ModifyPinVoltageReaderMockReturnValue(GpioPin.One, GpioVoltage.Low);

            InvokeOutputHigh(GpioPin.One);

            gpioPinVoltageWriterMock.Received(1).OutputHigh(GpioPin.One);
        }

        [Test]
        public void OutputLow_WhenVoltageIsHigh_ChangesPinOutputToLowVoltage()
        {
            ModifyPinVoltageReaderMockReturnValue(GpioPin.One, GpioVoltage.High);

            InvokeOutputLow(GpioPin.One);

            gpioPinVoltageWriterMock.Received(1).OutputLow(GpioPin.One);
        }

        [Test]
        public void OutputLow_WhenVoltageIsLow_GivesVoltageToErrorHandler()
        {
            ModifyPinVoltageReaderMockReturnValue(GpioPin.One, GpioVoltage.Low);

            InvokeOutputLow(GpioPin.One);

            gpioErrorHandlerMock.Received(1).HandleVoltageError(GpioVoltage.Low);
        }

        [SetUp]
        public void SetUp()
        {
            gpioPinStateReaderMock = CreatePinStateReaderMock();
            gpioPinStateWriterMock = CreatePinStateWriterMock();
            gpioPinVoltageReaderMock = CreatePinVoltageReaderMock();
            gpioPinVoltageWriterMock = CreatePinVoltageWriterMock();
            gpioErrorHandlerMock = CreateGpioErrorHandlerMock();

            systemUnderTest = CreateSystemUnderTest();
        }

        private IGpioErrorHandler CreateGpioErrorHandlerMock()
        {
            return Substitute.For<IGpioErrorHandler>();
        }

        private IGpioPinStateReader CreatePinStateReaderMock()
        {
            return Substitute.For<IGpioPinStateReader>();
        }

        private IGpioPinStateWriter CreatePinStateWriterMock()
        {
            return Substitute.For<IGpioPinStateWriter>();
        }

        private IGpioPinVoltageReader CreatePinVoltageReaderMock()
        {
            return Substitute.For<IGpioPinVoltageReader>();
        }

        private IGpioPinVoltageWriter CreatePinVoltageWriterMock()
        {
            return Substitute.For<IGpioPinVoltageWriter>();
        }

        private GpioPinout CreateSystemUnderTest()
        {
            return new GpioPinout(
                gpioPinStateReaderMock,
                gpioPinStateWriterMock,
                gpioPinVoltageReaderMock,
                gpioPinVoltageWriterMock,
                gpioErrorHandlerMock);
        }

        private void InvokeClosePin(GpioPin pin)
        {
            systemUnderTest.ClosePin(pin);
        }

        private void InvokeOpenPin(GpioPin pin)
        {
            systemUnderTest.OpenPin(pin);
        }

        private void InvokeOutputHigh(GpioPin pin)
        {
            systemUnderTest.OutputHigh(pin);
        }

        private void InvokeOutputLow(GpioPin pin)
        {
            systemUnderTest.OutputLow(pin);
        }

        private void ModifyPinStateReaderMockReturnValue(GpioPin pin, GpioState state)
        {
            gpioPinStateReaderMock.GetPinState(pin).Returns(state);
        }

        private void ModifyPinVoltageReaderMockReturnValue(GpioPin pin, GpioVoltage voltage)
        {
            gpioPinVoltageReaderMock.GetPinVoltage(pin).Returns(voltage);
        }
    }
}