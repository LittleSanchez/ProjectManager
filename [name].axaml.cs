using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace [namespace]
{
    public partial class _name_ : Window
    {
        public _name_()
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