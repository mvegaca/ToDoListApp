using ToDoListApp.ViewModels;

namespace ToDoListApp.Views;

public partial class TodoListView : ContentPage
{
	private readonly TodoListViewModel _viewModel;

    public TodoListView(TodoListViewModel viewModel)
	{
		_viewModel = viewModel;		
        InitializeComponent();
        this.BindingContext = _viewModel;
    }

	protected override async void OnAppearing()
	{
		base.OnAppearing();
        await _viewModel.LoadDataAsync();
    }
}