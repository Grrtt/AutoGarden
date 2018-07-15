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
        public void ClosePin_WhenClosedPinIsAskedToClose_GivesStateToErrorHandler()
        {
            ModifyPinStateProviderReturnValue(GpioPin.One, GpioState.Closed);

            InvokeClosePin(GpioPin.One);

            gpioErrorHandlerMock.Received(1).HandleStateError(GpioState.Closed);
        }

        [Test]
        public void ClosePin_WhenOpenPinIsAskedToClose_ClosesPin()
        {
            ModifyPinStateProviderReturnValue(GpioPin.One, GpioState.Open);

            InvokeClosePin(GpioPin.One);

            gpioPinStateWriterMock.Received(1).UnExportPin(GpioPin.One);
        }

        [Test]
        public void OpenPin_WhenClosedPinIsAskedToOpen_OpensPin()
        {
            ModifyPinStateProviderReturnValue(GpioPin.One, GpioState.Closed);

            InvokeOpenPin(GpioPin.One);

            gpioPinStateWriterMock.Received(1).ExportPin(GpioPin.One);
        }

        [Test]
        public void OpenPin_WhenOpenPinIsAskedToOpen_GivesStateToErrorHandler()
        {
            ModifyPinStateProviderReturnValue(GpioPin.One, GpioState.Open);

            InvokeOpenPin(GpioPin.One);

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

        private void InvokeClosePin(GpioPin pin)
        {
            systemUnderTest.ClosePin(pin);
        }

        private void InvokeOpenPin(GpioPin pin)
        {
            systemUnderTest.OpenPin(pin);
        }

        private void ModifyPinStateProviderReturnValue(GpioPin pin, GpioState state)
        {
            gpioPinStateReaderMock.GetPinState(pin).Returns(state);
        }
    }
}