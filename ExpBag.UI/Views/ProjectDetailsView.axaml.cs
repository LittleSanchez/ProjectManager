using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ExpBag.UI.Views
{
    public partial class ProjectDetailsView : UserControl
    {
        public ProjectDetailsView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
