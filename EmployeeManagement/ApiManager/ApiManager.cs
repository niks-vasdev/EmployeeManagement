using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeManagement.ApiManager
{
    public class ApiManager : IApiManager
    {
        private readonly HttpClient _httpClient;

        public ApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            return await CallAPI<T>(HttpMethod.Get, uri, null);
        }

        public async Task<T> GetAsync<T>(string uri, object data)
        {
            return await CallAPI<T>(HttpMethod.Get, uri, data);
        }

        public async Task<T> PostAsync<T>(string uri, T data)
        {
            return await CallAPI<T>(HttpMethod.Post, uri, data);
        }

        public async Task<TR> PostAsync<T, TR>(string uri, T data)
        {
            return await CallAPI<TR>(HttpMethod.Post, uri, data);
        }

        public async Task<T> PostAsync<T>(string uri, object data)
        {
            return await CallAPI<T>(HttpMethod.Post, uri, data);
        }

        public async Task<T> PutAsync<T>(string uri, object data)
        {
            return await CallAPI<T>(HttpMethod.Put, uri, data);
        }

        public async Task<R> PutAsync<T, R>(string uri, T data)
        {
            return await CallAPI<R>(HttpMethod.Put, uri, data);
        }

        public async Task<T> DeleteAsync<T>(string uri)
        {
            return await CallAPI<T>(HttpMethod.Delete, uri, null);
        }

        public async Task<R> DeleteAsync<T, R>(string uri, T data)
        {
            return await CallAPI<R>(HttpMethod.Delete, uri, data);
        }

        private async Task<T> CallAPI<T>(HttpMethod method, string uri, object data)
        {
            try
            {
                string jsonResult = string.Empty;
                var req = new HttpRequestMessage(method, uri);
                req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (data != null)
                {
                    req.Content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                }

                var response = await _httpClient.SendAsync(req);

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };

                    jsonResult = await response.Content.ReadAsStringAsync();
                    var json = JsonSerializer.Deserialize<T>(jsonResult, options);
                    return json;
                }

                throw new HttpRequestException($"API request failed with status code {response.StatusCode}");
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
