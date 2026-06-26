using Newtonsoft.Json;

namespace CyberChatbotPOE
{
    public class CyberTask
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Reminder")]
        public string Reminder { get; set; }

        [JsonProperty("IsComplete")]
        public bool IsComplete { get; set; }

        [JsonProperty("CreatedAt")]
        public string CreatedAt { get; set; }
    }
}