using Moq;
using NUnit.Framework;
using System;

namespace TDDMicroExercises.TirePressureMonitoringSystem.Tests
{
    [TestFixture]
    public class AlarmTests
    {
        [Test]
        public void Constructor_Throws_When_Low_Threshold_Is_Larger_Than_High()
        {
            var lowThreshold = 10;
            var highThreshold = 2;

            Assert.Throws<ArgumentException>(
                () => new Alarm(Mock.Of<ISensor>(), lowThreshold, highThreshold));
        }

        [TestCase(-1.0, 2.0)]
        [TestCase(1.0, -2.0)]
        [TestCase(-1.0, -2.0)]
        public void Constructor_Throws_When_At_Least_One_Threshold_Is_Negative(double lowThreshold, double highThreshold)
        {
            Assert.Throws<ArgumentException>(
                () => new Alarm(Mock.Of<ISensor>(), lowThreshold, highThreshold));
        }

        [TestCase(16.99, true)]
        [TestCase(17.0, false)]
        [TestCase(18.0, false)]
        [TestCase(21.0, false)]
        [TestCase(21.01, true)]
        public void Check_Sets_Alarm_For_Appropriate_Values(double psiValue, bool expectedAlarmValue)
        {
            var sensorMock = new Mock<ISensor>();
            sensorMock.Setup(m => m.PopNextPressurePsiValue()).Returns(psiValue);
            var target = new Alarm(sensorMock.Object);

            target.Check();

            Assert.AreEqual(expectedAlarmValue, target.AlarmOn);
        }

        [Test]
        public void Alarm_Is_Not_Canceled_By_Correct_Values()
        {
            var sensorMock = new Mock<ISensor>();
            sensorMock.SetupSequence(m => m.PopNextPressurePsiValue()).Returns(20).Returns(22).Returns(20);
            var target = new Alarm(sensorMock.Object);

            target.Check();
            Assert.AreEqual(false, target.AlarmOn);

            target.Check();
            Assert.AreEqual(true, target.AlarmOn);

            target.Check();
            Assert.AreEqual(true, target.AlarmOn);
        }

        [Test]
        public void Reset_Cancels_Alarm()
        {
            var sensorMock = new Mock<ISensor>();
            sensorMock.SetupSequence(m => m.PopNextPressurePsiValue()).Returns(20).Returns(22).Returns(20);
            var target = new Alarm(sensorMock.Object);

            target.Check();
            Assert.AreEqual(false, target.AlarmOn);

            target.Check();
            Assert.AreEqual(true, target.AlarmOn);
            target.Reset();

            target.Check();
            Assert.AreEqual(false, target.AlarmOn);
        }
    }
}
