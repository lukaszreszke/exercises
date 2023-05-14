namespace XML1
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public class VeterinaryCalendarHttpClient
    {
        private readonly HttpClient _httpClient;

        public VeterinaryCalendarHttpClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetCalendarData1()
        {
            string url = "https://example.com/veterinary/calendar1.xml";
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string xmlString = await response.Content.ReadAsStringAsync();

            return xmlString;
        }

        public async Task<string> GetCalendarData2()
        {
            string url = "https://example.com/veterinary/calendar2.xml";
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string xmlString = await response.Content.ReadAsStringAsync();

            return xmlString;
        }
    }
}