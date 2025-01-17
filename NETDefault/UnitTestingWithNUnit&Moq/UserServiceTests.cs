﻿using Moq;
using NUnit.Framework;

namespace UnitTestingExample
{
    internal class UserServiceTests : UserServiceTestsBase
    {
        [Test]
        public void GetDashboard_WhenCustomerNameNotFound_ItShouldThrow()
        {
            // Arrange
            SetupGetUserIdByUserName(_unknownUsername);

            // Assert
            Assert.Throws<NotFoundException>(() => _subject.GetDashboardByUserName(_unknownUsername));

            _gateway.Verify(x => x.GetUserIdByUserName(_unknownUsername), Times.Once());
            _gateway.VerifyNoOtherCalls();
        }

        [Test]
        public void GetDashboard_WhenCustomerNameFound_ItShouldBeReturned()
        {
            // Arrange
            SetupGetUserIdByUserName(_knownUsername);

            var expected = _userId.ToString();

            // Act
            var result = _subject.GetDashboardByUserName(_knownUsername);

            // Assert
            Assert.That(result.Contains(expected));

            _gateway.Verify(x => x.GetUserIdByUserName(_knownUsername), Times.Once());
            _gateway.Verify(x => x.GetHobbiesByUserId(_userId), Times.Once());
        }

        [Test]
        public void GetDashboard_WhenUserFound_ItShouldGetHobbies()
        {
            // Arrange
            SetupGetUserIdByUserName(_knownUsername);
            SetupGetHobbiesByUserId(new string[0] { });

            // Act
            _ = _subject.GetDashboardByUserName(_knownUsername);

            // Assert
            _gateway.Verify(x => x.GetUserIdByUserName(_knownUsername), Times.Once());
            _gateway.Verify(x => x.GetHobbiesByUserId(_userId), Times.Once());
            _gateway.Verify(x => x.GetNotificationsByUserId(_userId), Times.Once());
            _gateway.VerifyNoOtherCalls();
        }

        [TestCaseSource(typeof(HobbiesTestCases))]
        public void GetDashboard_WhenHobbiesFound_ItShouldBeReturned(string[] hobbies, string expectedHobbiesAsString)
        {
            // Arrange
            SetupGetUserIdByUserName(_knownUsername);
            SetupGetHobbiesByUserId(hobbies);

            // Act
            var result = _subject.GetDashboardByUserName(_knownUsername);

            // Assert
            Assert.That(result.Contains(expectedHobbiesAsString));

            _gateway.Verify(x => x.GetUserIdByUserName(_knownUsername), Times.Once());
            _gateway.Verify(x => x.GetHobbiesByUserId(_userId), Times.Once());
        }

        [TestCase(null, "notification error")]
        [TestCase(0, "0 notifications")]
        [TestCase(1, "1 notification")]
        public void GetDashboard_WhenNotificationsFound_ItShouldBeReturned(int? count, string expectedNotificationString)
        {
            // Arrange
            SetupGetUserIdByUserName(_knownUsername);
            SetupGetHobbiesByUserId(new string[0] { });
            SetupGetNotificationsByUserId(count);

            // Act
            var result = (string)_subject.GetDashboardByUserName(_knownUsername);

            // Assert
            Assert.That(result.Contains(expectedNotificationString));

            _gateway.Verify(x => x.GetUserIdByUserName(_knownUsername), Times.Once());
            _gateway.Verify(x => x.GetHobbiesByUserId(_userId), Times.Once());
            _gateway.Verify(x => x.GetNotificationsByUserId(_userId), Times.Once());
        }
    }
}
