using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ExpBag.Loader.Constants;
using ExpBag.Infrastructure.Environment;
using ExpBag.Loader;
using ExpBag.Loader.Abstractions;
using ExpBag.UI.ViewModels;
using ExpBag.UI.Views;
using System;
using ExpBag.UI.Startup;
using ExpBag.Application.Interfaces;
using ExpBag.UI.Abstractions;

namespace ExpBag.UI
{
    public class App : Avalonia.Application
    {

        private readonly IServiceProvider serviceProvider = ServiceProviderFactory.ServiceProvider;
        private IAppViewService viewService;

        public override void Initialize()
        {
            viewService = serviceProvider.GetService(typeof(IAppViewService)) as IAppViewService;
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = serviceProvider.GetService(typeof(MainWindowViewModel))
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}