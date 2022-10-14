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
        //private static readonly string TodoItemUrl = $"http://{LocalhostUrl}:8080/api/todo/{{0}}";
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

        public async Task<IEnumerable<TodoItem>> RefreshDataAsync()
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
    }
}
