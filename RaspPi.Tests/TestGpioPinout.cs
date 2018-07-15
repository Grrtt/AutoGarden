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

        private IGpioPinDirectionReader gpioPinDirectionReaderMock;

        private IGpioPinDirectionWriter gpioPinDirectionWriterMock;

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
        public void SetHigh_WhenPinVoltageIsHigh_GivesVoltageToErrorHandler()
        {
            ModifyPinVoltageReaderMockReturnValue(GpioPin.One, GpioPinVoltage.High);

            InvokeSetHigh(GpioPin.One);

            gpioErrorHandlerMock.Received(1).HandleVoltageError(GpioPinVoltage.High);
        }

        [Test]
        public void SetHigh_WhenVoltageIsLow_ChangesPinOutputToHighVoltage()
        {
            ModifyPinVoltageReaderMockReturnValue(GpioPin.One, GpioPinVoltage.Low);

            InvokeSetHigh(GpioPin.One);

            gpioPinVoltageWriterMock.Received(1).SetHigh(GpioPin.One);
        }

        [Test]
        public void SetLow_WhenVoltageIsHigh_ChangesPinOutputToLowVoltage()
        {
            ModifyPinVoltageReaderMockReturnValue(GpioPin.One, GpioPinVoltage.High);

            InvokeSetLow(GpioPin.One);

            gpioPinVoltageWriterMock.Received(1).SetLow(GpioPin.One);
        }

        [Test]
        public void SetLow_WhenVoltageIsLow_GivesVoltageToErrorHandler()
        {
            ModifyPinVoltageReaderMockReturnValue(GpioPin.One, GpioPinVoltage.Low);

            InvokeSetLow(GpioPin.One);

            gpioErrorHandlerMock.Received(1).HandleVoltageError(GpioPinVoltage.Low);
        }

        [Test]
        public void SetRead_WhenReadPinIsAskedToRead_GivesDirectionToErrorHandler()
        {
            ModifyPinDirectionReaderReturnValue(GpioPin.One, GpioPinDirection.In);

            InvokeSetRead(GpioPin.One);

            gpioErrorHandlerMock.Received(1).HandleDirectionError(GpioPinDirection.In);
        }

        [Test]
        public void SetRead_WhenReadPinIsAskedToWrite_SetsPinToRead()
        {
            ModifyPinDirectionReaderReturnValue(GpioPin.One, GpioPinDirection.In);

            InvokeSetWrite(GpioPin.One);

            gpioPinDirectionWriterMock.Received(1).SetWrite(GpioPin.One);
        }

        [Test]
        public void SetRead_WhenWritePinIsAskedToRead_SetsPinToRead()
        {
            ModifyPinDirectionReaderReturnValue(GpioPin.One, GpioPinDirection.Out);

            InvokeSetRead(GpioPin.One);

            gpioPinDirectionWriterMock.Received(1).SetRead(GpioPin.One);
        }

        [SetUp]
        public void SetUp()
        {
            gpioPinStateReaderMock = CreatePinStateReaderMock();
            gpioPinStateWriterMock = CreatePinStateWriterMock();
            gpioPinDirectionReaderMock = CreatePinDirectionReaderMock();
            gpioPinDirectionWriterMock = CreatePinDirectionWriterMock();
            gpioPinVoltageReaderMock = CreatePinVoltageReaderMock();
            gpioPinVoltageWriterMock = CreatePinVoltageWriterMock();
            gpioErrorHandlerMock = CreateGpioErrorHandlerMock();

            systemUnderTest = CreateSystemUnderTest();
        }

        [Test]
        public void SetWrite_WhenWritePinIsAskedToWrite_GivesDirectionToErrorHandler()
        {
            ModifyPinDirectionReaderReturnValue(GpioPin.One, GpioPinDirection.Out);

            InvokeSetWrite(GpioPin.One);

            gpioErrorHandlerMock.Received(1).HandleDirectionError(GpioPinDirection.Out);
        }

        private IGpioErrorHandler CreateGpioErrorHandlerMock()
        {
            return Substitute.For<IGpioErrorHandler>();
        }

        private IGpioPinDirectionReader CreatePinDirectionReaderMock()
        {
            return Substitute.For<IGpioPinDirectionReader>();
        }

        private IGpioPinDirectionWriter CreatePinDirectionWriterMock()
        {
            return Substitute.For<IGpioPinDirectionWriter>();
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
                gpioPinDirectionReaderMock,
                gpioPinDirectionWriterMock,
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

        private void InvokeSetHigh(GpioPin pin)
        {
            systemUnderTest.SetHigh(pin);
        }

        private void InvokeSetLow(GpioPin pin)
        {
            systemUnderTest.SetLow(pin);
        }

        private void InvokeSetRead(GpioPin pin)
        {
            systemUnderTest.SetRead(pin);
        }

        private void InvokeSetWrite(GpioPin pin)
        {
            systemUnderTest.SetWrite(pin);
        }

        private void ModifyPinDirectionReaderReturnValue(GpioPin pin, GpioPinDirection direction)
        {
            gpioPinDirectionReaderMock.GetPinDirection(pin).Returns(direction);
        }

        private void ModifyPinStateReaderMockReturnValue(GpioPin pin, GpioState state)
        {
            gpioPinStateReaderMock.GetPinState(pin).Returns(state);
        }

        private void ModifyPinVoltageReaderMockReturnValue(GpioPin pin, GpioPinVoltage pinVoltage)
        {
            gpioPinVoltageReaderMock.GetPinVoltage(pin).Returns(pinVoltage);
        }
    }
}