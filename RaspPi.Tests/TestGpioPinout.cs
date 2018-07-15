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

        private GpioPinout systemUnderTest;

        [Test]
        public void SetPinState_WhenClosedPinIsAskedToClose_GivesStateToErrorHandler()
        {
            ModifyPinStateProviderReturnValue(GpioPin.One, GpioState.Closed);

            InvokeSetPinState(GpioPin.One, GpioState.Closed);

            gpioErrorHandlerMock.Received(1).HandleStateError(GpioState.Closed);
        }

        [Test]
        public void SetPinState_WhenClosedPinIsAskedToOpen_OpensPin()
        {
            ModifyPinStateProviderReturnValue(GpioPin.One, GpioState.Closed);

            InvokeSetPinState(GpioPin.One, GpioState.Open);

            gpioPinStateWriterMock.Received(1).ExportPin(GpioPin.One);
        }

        [Test]
        public void SetPinState_WhenOpenPinIsAskedToClose_ClosesPin()
        {
            ModifyPinStateProviderReturnValue(GpioPin.One, GpioState.Open);

            InvokeSetPinState(GpioPin.One, GpioState.Closed);

            gpioPinStateWriterMock.Received(1).UnExportPin(GpioPin.One);
        }

        [Test]
        public void SetPinState_WhenOpenPinIsAskedToOpen_GivesStateToErrorHandler()
        {
            ModifyPinStateProviderReturnValue(GpioPin.One, GpioState.Open);

            InvokeSetPinState(GpioPin.One, GpioState.Open);

            gpioErrorHandlerMock.Received(1).HandleStateError(GpioState.Open);
        }

        [SetUp]
        public void SetUp()
        {
            gpioPinStateReaderMock = CreatePinStateReaderMock();
            gpioPinStateWriterMock = CreatePinStateWriterMock();
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

        private GpioPinout CreateSystemUnderTest()
        {
            return new GpioPinout(gpioPinStateReaderMock, gpioPinStateWriterMock, gpioErrorHandlerMock);
        }

        private void InvokeSetPinState(GpioPin pin, GpioState state)
        {
            systemUnderTest.SetPinState(pin, state);
        }

        private void ModifyPinStateProviderReturnValue(GpioPin pin, GpioState state)
        {
            gpioPinStateReaderMock.GetPinState(pin).Returns(state);
        }
    }
}