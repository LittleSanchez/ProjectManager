using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ExpBag.UI.Views
{
    public partial class ModulesListView : UserControl
    {
        public ModulesListView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
