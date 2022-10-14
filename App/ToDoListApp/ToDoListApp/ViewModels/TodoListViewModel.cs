using System.Collections.ObjectModel;
using System.Windows.Input;
using ToDoListApp.Contracts.Services;
using ToDoListApp.Models;
using ToDoListApp.Views;

namespace ToDoListApp.ViewModels
{
    public class TodoListViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private ObservableCollection<TodoItem> _todoItems;
        public ObservableCollection<TodoItem> TodoItems
        {
            get => _todoItems;
            set => SetProperty(ref _todoItems, value);
        }

        private ICommand _addItemCommand;
        private ICommand _markAsCompletedCommand;
        private ICommand _deleteCommand;
        public ICommand AddItemCommand => _addItemCommand ?? (_addItemCommand = new Command(OnAddItem, (obj) => !IsBusy));

        public ICommand MarkAsCompletedCommand => _markAsCompletedCommand ?? (_markAsCompletedCommand = new Command(OnMarkAsCompleted, (obj) => !IsBusy));

        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new Command(OnDelete, (obj) => !IsBusy));        

        public TodoListViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task LoadDataAsync()
        {
            IsBusy = true;
            var data = await _dataService.RefreshDataAsync();
            if (data != null)
            {
                TodoItems = new ObservableCollection<TodoItem>(data);
            }

            IsBusy = false;
        }

        protected override void RefreshBusyCommands()
        {
            base.RefreshBusyCommands();
            (AddItemCommand as Command).ChangeCanExecute();
            (MarkAsCompletedCommand as Command).ChangeCanExecute();
            (DeleteCommand as Command).ChangeCanExecute();
        }

        private async void OnAddItem(object obj)
        {
            IsBusy = true;
            await Shell.Current.GoToAsync(nameof(TodoCreateView));
            IsBusy = false;
        }

        private void OnMarkAsCompleted(object obj)
        {
            if (obj is TodoItem todoItem)
            {

            }
        }

        private void OnDelete(object obj)
        {
            if (obj is TodoItem todoItem)
            {

            }
        }
    }
}
