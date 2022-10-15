using ToDoListApp.Contracts.Services;
using ToDoListApp.Services;
using ToDoListApp.ViewModels;
using ToDoListApp.Views;

namespace ToDoListApp;

public static class MauiProgram
{
    // TODO: Set StorageMode to API to get the data from the API
    // Api project url
    // https://github.com/fluendo/ToDoAPI
    // Don't forget to deploy the API before run the TODO List App
    //
    public static StorageMode StorageMode = StorageMode.Local;

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
        builder.Services.AddTransient<IPopUpService, PopUpService>();
        switch (StorageMode)
		{
			case StorageMode.Api:
                builder.Services.AddSingleton<IDataService, HttpDataService>();
                break;
			case StorageMode.Local:
                builder.Services.AddSingleton<IDataService, LocalDataService>();
                break;		
		}        

        builder.Services.AddSingleton<TodoListViewModel>();
        builder.Services.AddSingleton<TodoListView>();

        builder.Services.AddTransient<TodoCreateViewModel>();
        builder.Services.AddTransient<TodoCreateView>();

        return builder.Build();
	}
}
