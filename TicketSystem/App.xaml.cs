using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using TicketSystem.Core;
using TicketSystem.Models.Entities;
using TicketSystem.MVVM.ViewModels;
using TicketSystem.Services;

namespace TicketSystem;

public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddSingleton<MainWindow>(provider => new MainWindow
        {
            DataContext = provider.GetRequiredService<MainViewModel>()
        });
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<TicketsViewModel>();
        services.AddSingleton<CreateTicketViewModel>();
        services.AddSingleton<CreateUserViewModel>();
        services.AddSingleton<ListUsersViewModel>();
        services.AddSingleton<TicketEntity>();
        services.AddSingleton<CommentViewModel>();
        services.AddSingleton<INavigationService, NavigationService>();

        services.AddSingleton<Func<Type, ViewModel>>(serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));

        _serviceProvider = services.BuildServiceProvider();
    }


    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
        base.OnStartup(e);
    }
}
