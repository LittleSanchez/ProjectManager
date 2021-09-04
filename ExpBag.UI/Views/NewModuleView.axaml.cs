using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ExpBag.UI.Views
{
    public partial class NewModuleView : UserControl
    {
        public NewModuleView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
