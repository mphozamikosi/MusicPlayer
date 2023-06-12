using System.Net.Http.Headers;

namespace MusicApp.RestCalls
{
    public class GlobalVariable
    {
        public static HttpClient httpClient = new HttpClient();

        static GlobalVariable()
        {
            string url = @"https://localhost:44333";
            httpClient.BaseAddress = new Uri(url);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}