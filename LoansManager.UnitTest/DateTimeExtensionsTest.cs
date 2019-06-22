using System;
using LoansManager.Common.Extensions;
using NUnit.Framework;

namespace LoansManager.UnitTest
{
    [TestFixture]
    public class DateTimeExtensionsTest
    {
        [Test]
        public void ToTimestampWhenCalledOnEpochTimestampShouldBeZero()
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
