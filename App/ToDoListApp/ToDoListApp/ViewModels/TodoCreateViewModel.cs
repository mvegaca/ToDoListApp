using System.Windows.Input;
using ToDoListApp.Contracts.Services;
using ToDoListApp.Models;

namespace ToDoListApp.ViewModels
{
    public class TodoCreateViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly IPopUpService _popUpService;
        private string _name;
        private ICommand _submmitCommand;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public ICommand SubmmitCommand => _submmitCommand ?? (_submmitCommand = new Command(OnSubmmit, (obj) => !IsBusy));        

        public TodoCreateViewModel(IDataService dataService, IPopUpService popUpService)
        {
            _dataService = dataService;
            _popUpService = popUpService;
        }

        public override void Initialize(Page page)
        {
            base.Initialize(page);
            _popUpService.Initialize(page);
        }

        protected override void RefreshBusyCommands()
        {
            base.RefreshBusyCommands();
            (SubmmitCommand as Command).ChangeCanExecute();
        }

        private async void OnSubmmit(object obj)
        {
            IsBusy = true;
            if (string.IsNullOrEmpty(Name))
            {
                _popUpService.DisplayAlert(Resources.Resources.TodoCreateViewTitle, Resources.Resources.PopUpMessageNameEmpty, Resources.Resources.PopUpOkButton);
            }
            else
            {
                var todoItem = new TodoItem()
                {
                    Key = Guid.NewGuid().ToString(),
                    Name = Name,
                    IsComplete = false
                };
                await _dataService.SendItemAsync(todoItem, true);
                await Shell.Current.GoToAsync("..");
            }
            IsBusy = false;
        }
    }
}
