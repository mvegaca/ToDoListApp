using ToDoListApp.Views;

namespace ToDoListApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(TodoCreateView), typeof(TodoCreateView));
    }
}
