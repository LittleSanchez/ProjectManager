using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using ExpBag.Domain.Models;
using ExpBag.Loader.Abstractions;
using ExpBag.UI.ViewModels;
using System.IO;
using System.Linq;

namespace ExpBag.UI.Views
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