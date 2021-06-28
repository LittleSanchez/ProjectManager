using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ProjectManager.Domain.Constants;
using ProjectManager.Loader;
using ProjectManager.UI.ViewModels;
using ProjectManager.UI.Views;

namespace ProjectManager.UI
{
    public class App : Application
    {
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
                    DataContext = new MainWindowViewModel(new ProjectSelector(), new ProjectLoader(LoaderConfig.Instance))
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}