using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using EntrolyticsNotifier.Models;
using EntrolyticsNotifier;
using System.Net.Http;

namespace EntrolyticsNotifier
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var loader = new NotificationLoader();


            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nSelect an action:");
                Console.WriteLine("1. Send Notification");
                Console.WriteLine("2. View Notification Content");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice (1-3): ");
                string choice = Console.ReadLine();
                string? path = "";

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter the path to the Notification: ");

                        path = Console.ReadLine();

                        await SendNotification(ViewNotificationContent(path));
                        break;
                    case "2":
                        Console.WriteLine("Enter the path to the Notification: ");
                        path = Console.ReadLine();

                        ViewNotificationContent(path);
                        break;
                    case "3":
                        exit = true;
                        Console.WriteLine("Exiting the application.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select an option from 1 to 3.");
                        break;
                }
            }
        }

        private static async Task SendNotification(Notification notification)
        {
            using (var httpClient = new HttpClient())
            {
                var sender = new NotificationSender(httpClient);
                try
                {
                    var stopwatch = Stopwatch.StartNew();

                    var response = await sender.SendNotificationAsync(notification);

                    stopwatch.Stop();

                    string responseContent = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("\n--- Notification Sent ---");
                    Console.WriteLine($"Notification URL: {notification.NotificationUrl}");
                    Console.WriteLine($"Content Sent: {JsonConvert.SerializeObject(notification.NotificationContent)}");
                    Console.WriteLine($"Content Received: {responseContent}");
                    Console.WriteLine($"HTTP Response Code: {response.StatusCode}");
                    Console.WriteLine($"Response Time: {stopwatch.ElapsedMilliseconds} ms");
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"HTTP Error: {ex.Message}");
                }
            }
        }


        private static Notification? ViewNotificationContent(string filePath)
        {
            try
            {
                var sender = new NotificationLoader();

                Notification notification = sender.LoadNotification(filePath);

                Console.WriteLine("\n--- Notification Content ---");
                Console.WriteLine($"Notification URL: {notification.NotificationUrl}");
                Console.WriteLine($"Report UID: {notification.NotificationContent.ReportUID}");
                Console.WriteLine($"Study Instance UID: {notification.NotificationContent.StudyInstanceUID}");

                return notification;
            } catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
