using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using NUnit.Framework;
using EntrolyticsNotifier.Models;
using EntrolyticsNotifier.Services;

namespace EntrolyticsNotifier.Tests
{
    public class HttpRequestTests
    {
        private Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        }

        [TearDown]
        public void Teardown()
        {
            _httpClient.Dispose();
        }

        [Test]
        public async Task SendNotification_ShouldReturnSuccessStatusCode()
        {
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Success")
            };
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(expectedResponse);

            var notification = new Notification
            {
                NotificationUrl = "https://webhook.site/test-url",
                NotificationContent = new NotificationContent
                {
                    ReportUID = "20fb8e02-9c55-410a-93a9-489c6f1d7598",
                    StudyInstanceUID = "9998e02-9c55-410a-93a9-489c6f789798"
                }
            };

            var notificationSender = new NotificationSender(_httpClient);
            var result = await notificationSender.SendNotificationAsync(notification);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual("Success", await result.Content.ReadAsStringAsync());
        }

        [Test]
        public async Task SendNotification_ShouldReturnFailStatusCode()
        {
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Failure")
            };
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(expectedResponse);

            var notification = new Notification
            {
                NotificationUrl = "https://webhook.site/test-url-invalid",
                NotificationContent = new NotificationContent
                {
                    ReportUID = "20fb8e02-9c55-410a-93a9-489c6f1d7598",
                    StudyInstanceUID = "9998e02-9c55-410a-93a9-489c6f789798"
                }
            };

            var notificationSender = new NotificationSender(_httpClient);

            var result = await notificationSender.SendNotificationAsync(notification);

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.AreEqual("Failure", await result.Content.ReadAsStringAsync());
        }

    }
}
