using System.Diagnostics;
using ToDoListApp.Contracts.Services;
using ToDoListApp.Models;

namespace ToDoListApp.Services
{
    internal class LocalDataService : IDataService
    {
        private readonly List<TodoItem> _items = new List<TodoItem>()
        {
            new TodoItem() { Key = Guid.NewGuid().ToString(), Name = "Item 1", IsComplete = false },
            new TodoItem() { Key = Guid.NewGuid().ToString(), Name = "Item 2", IsComplete = false },
            new TodoItem() { Key = Guid.NewGuid().ToString(), Name = "Item 3", IsComplete = false },
            new TodoItem() { Key = Guid.NewGuid().ToString(), Name = "Item 4", IsComplete = false }
        };

        public async Task<bool> DeleteItemAsync(string key)
        {
            var item = _items.FirstOrDefault(i => i.Key == key);
            if (item != null)
            {
                Debug.WriteLine("######### TODO Item deleted!");
                return _items.Remove(item);
            }

            await Task.CompletedTask;
            return false;
        }

        public async Task<IEnumerable<TodoItem>> GetAllItemsAsync()
        {
            await Task.CompletedTask;
            Debug.WriteLine("######### TODO Items loaded!");
            return _items;
        }

        public async Task SendItemAsync(TodoItem item, bool isNewItem)
        {
            if (isNewItem)
            {
                _items.Add(item);
                Debug.WriteLine("######### TODO Item added!");
            }
            else
            {
                var existingItem = _items.FirstOrDefault(i => i.Key == item.Key);
                if (existingItem != null)
                {
                    existingItem.IsComplete = item.IsComplete;
                    Debug.WriteLine("######### TODO Item saved!");
                }
            }
            await Task.CompletedTask;
        }
    }
}
