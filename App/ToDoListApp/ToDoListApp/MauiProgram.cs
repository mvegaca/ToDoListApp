using ToDoListApp.Contracts.Services;
using ToDoListApp.Services;
using ToDoListApp.ViewModels;
using ToDoListApp.Views;

namespace ToDoListApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
        builder.Services.AddSingleton<IHttpsHandlerService, HttpsHandlerService>();
        builder.Services.AddSingleton<IDataService, HttpDataService>();

        builder.Services.AddSingleton<TodoListViewModel>();
        builder.Services.AddSingleton<TodoListView>();
        builder.Services.AddTransient<TodoCreateViewModel>();
        builder.Services.AddTransient<TodoCreateView>();

        return builder.Build();
	}
}
