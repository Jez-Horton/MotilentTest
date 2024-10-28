using System.IO;
using EntrolyticsNotifier.Models;

namespace EntrolyticsNotifier.Services
{
    public interface INotificationLoader
    {
        /// <summary>
        /// Loads a notification from a JSON file.
        /// </summary>
        /// <param name="filePath">The path to the JSON file.</param>
        /// <returns>The deserialized Notification object.</returns>
        /// <exception cref="FileNotFoundException">Thrown if the file does not exist.</exception>
        /// <exception cref="JsonException">Thrown if the JSON is invalid.</exception>
        Notification LoadNotification(string filePath);
    }
}
