using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ExpBag.UI.Views
{
    public class AuthView : UserControl
    {
        public AuthView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
