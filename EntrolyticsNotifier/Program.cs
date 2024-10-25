using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using EntrolyticsNotifier.Models;
using EntrolyticsNotifier;


NotificationLoader notificationLoader;
NotificationSender notificationSender;

static async Task Main(string[] args)
{
    if (args.Length != 1)
    {
        Console.WriteLine("Usage: EntrolyticsNotifier <path_to_json_file>");
        return;
    }

    string filePath = args[0];
    var loader = new NotificationLoader();

    // Load the notification using NotificationLoader
    Notification notification;
    try
    {
        notification = loader.LoadNotification(filePath);
    }
    catch (Exception ex) when (ex is ArgumentException || ex is FileNotFoundException || ex is JsonException)
    {
        Console.WriteLine($"Error loading notification: {ex.Message}");
        return;
    }

    // Display menu options
    bool exit = false;
    while (!exit)
    {
        Console.WriteLine("\nSelect an action:");
        Console.WriteLine("1. Send Notification");
        Console.WriteLine("2. View Notification Content");
        Console.WriteLine("3. Exit");
        Console.Write("Enter your choice (1-3): ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                await SendNotification(notification);
                break;
            case "2":
                ViewNotificationContent(notification);
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

static async Task SendNotification(Notification notification)
{
    using (var httpClient = new HttpClient())
    {
        var sender = new NotificationSender(httpClient);
        try
        {
            var response = await sender.SendNotificationAsync(notification);
            string responseContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine("\n--- Notification Sent ---");
            Console.WriteLine($"Notification URL: {notification.NotificationUrl}");
            Console.WriteLine($"Content Sent: {JsonConvert.SerializeObject(notification.NotificationContent)}");
            Console.WriteLine($"Content Received: {responseContent}");
            Console.WriteLine($"HTTP Response Code: {response.StatusCode}");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP Error: {ex.Message}");
        }
    }
}

static void ViewNotificationContent(Notification notification)
{
    Console.WriteLine("\n--- Notification Content ---");
    Console.WriteLine($"Notification URL: {notification.NotificationUrl}");
    Console.WriteLine($"Report UID: {notification.NotificationContent.ReportUID}");
    Console.WriteLine($"Study Instance UID: {notification.NotificationContent.StudyInstanceUID}");
}