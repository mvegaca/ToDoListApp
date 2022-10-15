using ToDoListApp.Models;
using ToDoListApp.ViewModels;

namespace ToDoListApp.Views;

public partial class TodoListView : ContentPage, IDisposable
{
	private readonly TodoListViewModel _viewModel;

    public TodoListView(TodoListViewModel viewModel)
	{
		_viewModel = viewModel;		
        InitializeComponent();
        this.BindingContext = _viewModel;
        _viewModel.Initialize(this);
        Shell.Current.Navigated += OnNavigated;
    }

    private async void OnNavigated(object sender, ShellNavigatedEventArgs e)
    {
        if (e.Current.Location.ToString() == "//MainPage")
        {
            await _viewModel.LoadDataAsync();
        }
    }

    protected override void OnAppearing()
	{
		base.OnAppearing();        
    }

    public void Dispose()
    {
        Shell.Current.Navigated -= OnNavigated;
    }
}