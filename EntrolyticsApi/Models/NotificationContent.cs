using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace EntrolyticsNotifier.Api.Models
{
    public class NotificationContent
    {
        [JsonProperty("reportUID")]
        public string ReportUID { get; set; }

        [JsonProperty("studyInstanceUID")]
        public string StudyInstanceUID { get; set; }
    }
}
