using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using ProjectManager.Domain.Models;
using ProjectManager.Loader.Abstractions;
using ProjectManager.UI.ViewModels;
using System.IO;
using System.Linq;

namespace ProjectManager.UI.Views
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            
#if DEBUG
            this.AttachDevTools();
#endif
        }

       

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}