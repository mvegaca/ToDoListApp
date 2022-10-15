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
        private readonly IPopUpService _popUpService;
        private ObservableCollection<TodoItem> _todoItems;
        public ObservableCollection<TodoItem> TodoItems
        {
            get => _todoItems;
            set => SetProperty(ref _todoItems, value);
        }

        private ICommand _addItemCommand;
        private ICommand _isCompleteCommand;
        private ICommand _deleteCommand;
        private ICommand _refreshCommand;
        public ICommand AddItemCommand => _addItemCommand ?? (_addItemCommand = new Command(OnAddItem, (obj) => !IsBusy));

        public ICommand IsCompleteCommand => _isCompleteCommand ?? (_isCompleteCommand = new Command(OnUpdateIsComplete, (obj) => !IsBusy));        

        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new Command(OnDelete, (obj) => !IsBusy));

        public ICommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new Command(OnRefresh, (obj) => !IsBusy));

        public TodoListViewModel(IDataService dataService, IPopUpService popUpService)
        {
            _dataService = dataService;
            _popUpService = popUpService;
        }

        public override async Task LoadDataAsync()
        {
            IsBusy = true;
            var data = await _dataService.GetAllItemsAsync();
            if (data == null)
            {
                _popUpService.DisplayAlert(Resources.Resources.TodoListViewTitle, Resources.Resources.PopUpMessageApiNoAvailable, Resources.Resources.PopUpOkButton);
            }
            else
            {
                TodoItems = new ObservableCollection<TodoItem>(data);
            }

            IsBusy = false;
        }

        public override void Initialize(Page page)
        {
            base.Initialize(page);
            _popUpService.Initialize(page);
        }

        protected override void RefreshBusyCommands()
        {
            base.RefreshBusyCommands();
            (AddItemCommand as Command).ChangeCanExecute();
            (IsCompleteCommand as Command).ChangeCanExecute();
            (DeleteCommand as Command).ChangeCanExecute();
            (RefreshCommand as Command).ChangeCanExecute();
        }

        private async void OnAddItem(object obj)
        {
            IsBusy = true;
            await Shell.Current.GoToAsync(nameof(TodoCreateView));
            IsBusy = false;
        }

        private void OnUpdateIsComplete(object obj)
        {
            if (obj is TodoItem todoItem)
            {
                IsBusy = true;
                todoItem.IsComplete = !todoItem.IsComplete;
                _dataService.SendItemAsync(todoItem, false);
                IsBusy = false;
            }
        }

        private async void OnDelete(object obj)
        {
            IsBusy = true;
            if (obj is TodoItem todoItem)
            {
                var success = await _dataService.DeleteItemAsync(todoItem.Key);
                if (success)
                {
                    var message = string.Format(Resources.Resources.PopUpMessageItemDeleted, todoItem.Name);
                    _popUpService.DisplayAlert(Resources.Resources.TodoListViewTitle, message, Resources.Resources.PopUpOkButton);
                    TodoItems.Remove(todoItem);
                }
            }
            IsBusy = false;
        }

        private async void OnRefresh(object obj)
        {
            IsBusy = true;
            var data = await _dataService.GetAllItemsAsync();
            TodoItems = new ObservableCollection<TodoItem>(data);
            IsBusy = false;
        }
    }
}
