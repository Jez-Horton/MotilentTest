using Newtonsoft.Json;
using EntrolyticsNotifier.Models;

namespace EntrolyticsNotifier.Tests
{
    public class JsonParsingTests
    {
        private const string ValidJson = @"
        {
            ""notificationUrl"": ""https://webhook.site/test-url"",
            ""notificationContent"": {
                ""reportUID"": ""20fb8e02-9c55-410a-93a9-489c6f1d7598"",
                ""studyInstanceUID"": ""9998e02-9c55-410a-93a9-489c6f789798""
            }
        }";

        private const string InvalidJson = @"{""notificationUrl"": ""https://webhook.site/test-url""";

        [Test]
        public void ParseValidJson_ShouldReturnNotificationObject()
        {
            var notification = JsonConvert.DeserializeObject<Notification>(ValidJson);
            Assert.IsNotNull(notification);
            Assert.AreEqual("https://webhook.site/test-url", notification.NotificationUrl);
            Assert.AreEqual("20fb8e02-9c55-410a-93a9-489c6f1d7598", notification.NotificationContent.ReportUID);
        }

        [Test]
        public void ParseInvalidJson_ShouldThrowJsonException()
        {
            Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<Notification>(InvalidJson));
        }
    }
}