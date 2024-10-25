using System;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;

namespace EntrolyticsNotifier.Tests
{
    public class ErrorHandlingTests
    {
        private const string ValidJsonPath = "validNotification.json";
        private const string InvalidJsonPath = "invalidNotification.json";
        NotificationLoader _notificationLoader;


        [SetUp]
        public void Setup()
        {
            File.WriteAllText(ValidJsonPath, @"
            {
                ""notificationUrl"": ""https://webhook.site/test-url"",
                ""notificationContent"": {
                    ""reportUID"": ""20fb8e02-9c55-410a-93a9-489c6f1d7598"",
                    ""studyInstanceUID"": ""9998e02-9c55-410a-93a9-489c6f789798""
                }
            }");

            _notificationLoader = new NotificationLoader();

            File.WriteAllText(InvalidJsonPath, @"{""notificationUrl"": ""https://webhook.site/test-url""");
        }

        [TearDown]
        public void Teardown()
        {
            File.Delete(ValidJsonPath);
            File.Delete(InvalidJsonPath);
        }

        [Test]
        public void LoadValidJson_ShouldReturnNotificationObject()
        {
            var notificaitonLoader = new NotificationLoader();
            var result = notificaitonLoader.LoadNotification(ValidJsonPath);

            Assert.IsNotNull(result);
            Assert.AreEqual("https://webhook.site/test-url", result.NotificationUrl);
            Assert.AreEqual("20fb8e02-9c55-410a-93a9-489c6f1d7598", result.NotificationContent.ReportUID);
        }

        [Test]
        public void LoadInvalidJson_ShouldThrowJsonException()
        {
            Assert.Throws<JsonException>(() => _notificationLoader.LoadNotification(InvalidJsonPath));
        }
    }
}
