using System.Diagnostics;
using System.Text;
using System.Text.Json;
using ToDoListApp.Contracts.Services;
using ToDoListApp.Models;

namespace ToDoListApp.Services
{
    public enum Scheme
    {
        Http,
        Https
    }

    public class HttpDataService : IDataService
    {
        private const Scheme SCHEME = Scheme.Https;

        public static readonly string LocalhostUrl = DeviceInfo.Platform == DevicePlatform.Android ? "10.0.2.2" : "localhost";
        private static readonly string TodoItemUrl = $"http://{LocalhostUrl}:8080/api/todo/{{0}}";
        private static readonly string GetAllUrl = $"http://{LocalhostUrl}:8080/api/todo/";

        private readonly HttpClient _client;
        private readonly IHttpsHandlerService _handlerService;

        public HttpDataService(IHttpsHandlerService handlerService)
        {
#if DEBUG
            _handlerService = handlerService;
            var handler = _handlerService.GetPlatformMessageHandler();
            if (handler != null)
                _client = new HttpClient(handler);
            else
                _client = new HttpClient();
#else
            _client = new HttpClient();
#endif
        }

        public async Task<IEnumerable<TodoItem>> GetAllItemsAsync()
        {
            var uri = new Uri(GetAllUrl);
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<IEnumerable<TodoItem>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"######### ERROR: {ex}");
            }

            return null;
        }

        public async Task SendItemAsync(TodoItem item, bool isNewItem)
        {
            Uri uri = new Uri(string.Format(TodoItemUrl, item.Key));
            try
            {
                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(item, options);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                    response = await _client.PostAsync(uri, content);
                else
                    response = await _client.PutAsync(uri, content);

                if (response.IsSuccessStatusCode)
                    Debug.WriteLine("######### TODO Item saved!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"######### ERROR: {ex}");
            }
        }

        public async Task<bool> DeleteItemAsync(string key)
        {
            Uri uri = new Uri(string.Format(TodoItemUrl, key));
            try
            {
                var response = await _client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("######### TODO Item deleted!");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"######### ERROR: {ex}");
            }

            return false;
        }
    }
}
