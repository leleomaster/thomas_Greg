using System.Net.Http.Headers;

namespace ThomasGreg.Web.Sevices
{
    public static class HttpClientSingleton
    {
        private static HttpClient HttpClient;
        private static MediaTypeHeaderValue contentType = new MediaTypeHeaderValue("application/json");

        public static HttpClient GetHttpClient()
        {
            if(HttpClient != null)
            {
                return HttpClient;
            }
            else
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                return httpClient;
            }
        }
    }
}
