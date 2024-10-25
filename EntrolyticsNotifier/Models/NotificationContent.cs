using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EntrolyticsNotifier.Models
{
    public class Notification
    {
        [JsonProperty("notificationUrl")]
        public string NotificationUrl { get; set; }

        [JsonProperty("notificationContent")]
        public NotificationContent NotificationContent { get; set; }
    }

    public class NotificationContent
    {
        [JsonProperty("reportUID")]
        public string ReportUID { get; set; }

        [JsonProperty("studyInstanceUID")]
        public string StudyInstanceUID { get; set; }
    }
}
