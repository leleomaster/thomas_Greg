using System.Net.Http.Headers;
using ThomasGreg.Web.Sevices.Interfaces;
using ThomasGreg.Web.Utils;

namespace ThomasGreg.Web.Sevices.Implementation
{
    public abstract class ServiceBase<T> : IServiceBase<T>
        where T : class
    {
        private readonly HttpClient _httpClient = HttpClientSingleton.GetHttpClient();
        public string basePath = "";

        public ServiceBase(string _basePath, IConfiguration configuration)
        {
            basePath = configuration["thomasGregAPI:baseURL"] + _basePath;
        }

        public async Task<IEnumerable<T>> ObterTodos(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(basePath);

            return await response.RedContentAsync<List<T>>();
        }

        public async Task<T> ObterPorId(long id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{basePath}/{id}");

            return await response.RedContentAsync<T>();
        }

        public async Task<bool> Adicionar(T model, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJson($"{basePath}", model);

            if (response.IsSuccessStatusCode) return true;
            else throw new Exception("Something went worng when calling API");
        }

        public async Task<bool> Atualizar(T model, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PutAsJson($"{basePath}", model);

            if (response.IsSuccessStatusCode) return true;
            else throw new Exception("Something went worng when calling API");
        }

        public async Task<bool> Remover(long id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.DeleteAsync($"{basePath}/{id}");

            if (response.IsSuccessStatusCode) return true;
            else throw new Exception("Something went worng when calling API");
        }
    }
}
