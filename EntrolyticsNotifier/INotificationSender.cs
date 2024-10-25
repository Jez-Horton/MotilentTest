using System.Net.Http;
using System.Threading.Tasks;
using EntrolyticsNotifier.Models;

namespace EntrolyticsNotifier
{
    public interface INotificationSender
    {
        /// <summary>
        /// Sends a notification to the specified URL with the given content.
        /// </summary>
        /// <param name="notification">The notification object containing URL and content.</param>
        /// <returns>The HTTP response from the server.</returns>
        Task<HttpResponseMessage> SendNotificationAsync(Notification notification);
    }
}
