namespace ToDoListApp.ViewModels
{
    public abstract class ViewModelBase : Observable
    {
        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                SetProperty(ref _isBusy, value);
                RefreshBusyCommands();
            }
        }

        public virtual void Initialize(Page page) { }

        public virtual async Task LoadDataAsync() => await Task.CompletedTask;

        protected virtual void RefreshBusyCommands() { }
    }
}
