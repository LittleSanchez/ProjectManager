using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ExpBag.Domain.Constants;
using ExpBag.Infrastructure.Environment;
using ExpBag.Loader;
using ExpBag.Loader.Abstractions;
using ExpBag.UI.ViewModels;
using ExpBag.UI.Views;
using System;

namespace ExpBag.UI
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
                desktop.MainWindow = new ExpBag.UI.Views.MainWindow()
                {
                    DataContext = new MainWindowViewModel()
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}