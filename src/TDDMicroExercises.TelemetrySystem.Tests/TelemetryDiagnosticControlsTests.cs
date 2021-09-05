using Moq;
using NUnit.Framework;
using System;

namespace TDDMicroExercises.TelemetrySystem.Tests
{
    [TestFixture]
    public class TelemetryDiagnosticControlsTests
    {
        [Test]
        public void CheckTransmission_Sends_Diagnostic_Message_And_Receives_Diagnostic_Info()
        {
            var telemetryClientMock = new Mock<ITelemetryClient>();
            telemetryClientMock.Setup(m => m.OnlineStatus).Returns(true);
            telemetryClientMock.Setup(m => m.Receive()).Returns(Consts.TelemetryClientDiagnosticMessage);
            var target = new TelemetryDiagnosticControls(telemetryClientMock.Object);

            target.CheckTransmission();

            Assert.AreEqual(Consts.TelemetryClientDiagnosticMessage, target.DiagnosticInfo);
            telemetryClientMock.Verify(m => m.Send(Consts.TelemetryClientDiagnosticMessage), Times.Once);
        }

        [Test]
        public void CheckTransmission_Connects_With_Client()
        {
            var onlineStatus = false;
            var telemetryClientMock = new Mock<ITelemetryClient>();
            telemetryClientMock.Setup(m => m.OnlineStatus).Returns(() => onlineStatus);
            telemetryClientMock.Setup(m => m.Connect(It.IsAny<string>())).Callback(() => onlineStatus = true);
            telemetryClientMock.Setup(m => m.Receive()).Returns(Consts.TelemetryClientDiagnosticMessage);
            var target = new TelemetryDiagnosticControls(telemetryClientMock.Object);

            target.CheckTransmission();

            telemetryClientMock.Verify(m => m.Connect(Consts.TelemetryClientDiagnosticConnectionString), Times.Once);
        }

        [Test]
        public void CheckTransmission_Retries_Connecting_After_Fail()
        {
            var onlineStatus = false;
            var connectCallCount = 0;
            var telemetryClientMock = new Mock<ITelemetryClient>();
            telemetryClientMock.Setup(m => m.OnlineStatus).Returns(() => onlineStatus);
            telemetryClientMock.Setup(m => m.Connect(It.IsAny<string>())).Callback(() =>
            {
                onlineStatus = connectCallCount == 2 ? true : false;
                connectCallCount++;
            });
            telemetryClientMock.Setup(m => m.Receive()).Returns(Consts.TelemetryClientDiagnosticMessage);
            var target = new TelemetryDiagnosticControls(telemetryClientMock.Object);

            target.CheckTransmission();

            telemetryClientMock.Verify(m => m.Connect(Consts.TelemetryClientDiagnosticConnectionString), Times.Exactly(3));
        }

        [Test]
        public void CheckTransmission_Throws_When_Connecting_Consistently_Fails()
        {
            var telemetryClientMock = new Mock<ITelemetryClient>();
            telemetryClientMock.Setup(m => m.OnlineStatus).Returns(false);
            telemetryClientMock.Setup(m => m.Receive()).Returns(Consts.TelemetryClientDiagnosticMessage);
            var target = new TelemetryDiagnosticControls(telemetryClientMock.Object);

            Assert.Throws<Exception>(() => target.CheckTransmission());

            Assert.IsEmpty(target.DiagnosticInfo);
            telemetryClientMock.Verify(m => m.Connect(It.IsAny<string>()), Times.Exactly(3));
        }

    }
}
