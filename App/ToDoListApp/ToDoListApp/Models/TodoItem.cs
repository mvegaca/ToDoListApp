using ToDoListApp.ViewModels;

namespace ToDoListApp.Models
{
    public class TodoItem : Observable
    {
        private string _key;
        private string _name;
        private bool _isComplete;

        public string Key
        {
            get => _key;
            set => SetProperty(ref _key, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public bool IsComplete
        {
            get => _isComplete;
            set => SetProperty(ref _isComplete, value);
        }
    }
}
