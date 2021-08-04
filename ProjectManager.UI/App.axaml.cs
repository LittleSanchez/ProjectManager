using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ProjectManager.Domain.Constants;
using ProjectManager.Infrastructure.Environment;
using ProjectManager.Loader;
using ProjectManager.Loader.Abstractions;
using ProjectManager.UI.ViewModels;
using ProjectManager.UI.Views;
using System;

namespace ProjectManager.UI
{
    public class App : Avalonia.Application
    {

        private readonly IServiceProvider serviceProvider = ServiceProviderFactory.ServiceProvider;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new ProjectManager.UI.Views.MainWindow()
                {
                    DataContext = new MainWindowViewModel()
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}