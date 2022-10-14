using ToDoListApp.ViewModels;

namespace ToDoListApp.Views;

public partial class TodoCreateView : ContentPage
{
    private readonly TodoCreateViewModel _viewModel;

    public TodoCreateView(TodoCreateViewModel viewModel)
    {
        _viewModel = viewModel;
        this.BindingContext = _viewModel;
        InitializeComponent();
	}
}