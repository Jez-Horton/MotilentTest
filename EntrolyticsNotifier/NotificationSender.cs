using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using EntrolyticsNotifier.Models;

namespace EntrolyticsNotifier
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
            throw new NotImplementedException();
        }
    }
}
