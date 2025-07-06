using System.Text.Json.Serialization;

namespace Final_Project.Models
{
    public class EmailRequest
    {
        [System.Text.Json.Serialization.JsonPropertyName("from")]
        public string From { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("to")]
        public string To { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("subject")]
        public string Subject { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("html")]
        public string Html { get; set; }
    }
}
