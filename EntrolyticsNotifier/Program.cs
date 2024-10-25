using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using EntrolyticsNotifier.Models;

static async Task Main(string[] args)
{
    if (args.Length != 1)
    {
        Console.WriteLine("Usage: EntrolyticsNotifier <path_to_json_file>");
        return;
    }

    string filePath = args[0];

    if (!File.Exists(filePath))
    {
        Console.WriteLine("Error: JSON file not found.");
        return;
    }

    try
    {
        string jsonData = File.ReadAllText(filePath);
        var notification = JsonConvert.DeserializeObject<Notification>(jsonData);

        if (notification == null || string.IsNullOrEmpty(notification.NotificationUrl))
        {
            Console.WriteLine("Error: Invalid JSON content.");
            return;
        }

        var content = JsonConvert.SerializeObject(notification.NotificationContent);
        using (var client = new HttpClient())
        {
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var stopwatch = Stopwatch.StartNew();

            HttpResponseMessage response = await client.PostAsync(notification.NotificationUrl, httpContent);
            stopwatch.Stop();

            string responseContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Notification URL: {notification.NotificationUrl}");
            Console.WriteLine($"Content Sent: {content}");
            Console.WriteLine($"Content Received: {responseContent}");
            Console.WriteLine($"HTTP Response Code: {response.StatusCode}");
            Console.WriteLine($"Response Time: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
    catch (JsonException)
    {
        Console.WriteLine("Error: Failed to parse JSON.");
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine($"HTTP Error: {e.Message}");
    }
    catch (Exception e)
    {
        Console.WriteLine($"Unexpected Error: {e.Message}");
    }
}