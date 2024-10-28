using System;
using System.IO;
using Newtonsoft.Json;
using EntrolyticsNotifier.Models;

namespace EntrolyticsNotifier.Services
{
    public class NotificationLoader : INotificationLoader
    {
        public Notification LoadNotification(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException("JSON file not found.", filePath);

            string jsonData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<Notification>(jsonData);
        }
    }
}
