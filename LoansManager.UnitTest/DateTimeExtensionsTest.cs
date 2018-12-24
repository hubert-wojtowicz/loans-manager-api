using LoansManager.Util;
using NUnit.Framework;
using System;

namespace LoansManager.UnitTest
{
    [TestFixture]
    public class DateTimeExtensionsTest
    {
        [Test]
        public void ToTimestamp_WhenCalledOnEpoch_TimestampShouldBeZero()
        {
            // Arrange
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Act
            var timestamp = epoch.ToTimestamp();

            // Assert
            Assert.That(timestamp, Is.EqualTo(0));
        }
    }
}
