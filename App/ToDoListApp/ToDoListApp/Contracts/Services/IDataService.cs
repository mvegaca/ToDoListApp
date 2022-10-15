using ToDoListApp.Models;

namespace ToDoListApp.Contracts.Services
{
    public interface IDataService
    {
        Task<IEnumerable<TodoItem>> GetAllItemsAsync();

        Task SendItemAsync(TodoItem item, bool isNewItem);

        Task<bool> DeleteItemAsync(string key);
    }
}
