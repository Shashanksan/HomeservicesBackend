using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Final_Project.Models
{
    public class ResendEmailService
    {
        private readonly HttpClient _httpClient;

        public ResendEmailService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", "re_iD6uwRtq_NPBGF42e9xbanYmYNqC7WfQJ`"); // Replace this
        }

        public async Task SendEmailAsync(EmailRequest email)
        {
            var json = JsonSerializer.Serialize(email);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://api.resend.com/emails", content);

            if (!response.IsSuccessStatusCode)
            {
                var details = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to send email. Status: {response.StatusCode}. Details: {details}");
            }
        }
    }

   
}
