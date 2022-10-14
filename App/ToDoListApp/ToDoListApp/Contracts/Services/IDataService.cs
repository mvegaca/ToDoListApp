using ToDoListApp.Models;

namespace ToDoListApp.Contracts.Services
{
    public interface IDataService
    {
        Task<IEnumerable<TodoItem>> RefreshDataAsync();        
    }
}
