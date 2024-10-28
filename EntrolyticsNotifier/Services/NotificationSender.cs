using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using EntrolyticsNotifier.Models;

namespace EntrolyticsNotifier.Services
{
    public class NotificationSender : INotificationSender
    {
        private readonly HttpClient _httpClient;

        public NotificationSender(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> SendNotificationAsync(Notification notification)
        {
            if (notification == null) throw new ArgumentNullException(nameof(notification));
            if (string.IsNullOrEmpty(notification.NotificationUrl)) throw new ArgumentException("Notification URL cannot be null or empty.", nameof(notification.NotificationUrl));

            var jsonContent = JsonConvert.SerializeObject(notification.NotificationContent);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(notification.NotificationUrl, httpContent);

            return response;
        }
    }
}
