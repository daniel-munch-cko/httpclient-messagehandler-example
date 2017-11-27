using System.Net.Http;
using System.Threading.Tasks;

namespace httpclient_messagehandler_example.Services
{
    public class FancyService
    {
        private HttpClient _client;
        public FancyService(HttpClient client) 
        {
            _client = client;
        }

        public async Task<string> GetSomethingFancy() 
        {
            using(var response = await _client.GetAsync("https://httpbin.org/anything"))
            {
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}