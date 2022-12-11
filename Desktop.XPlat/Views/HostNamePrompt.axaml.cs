using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Rimot.Desktop.XPlat.ViewModels;
using Rimot.Desktop.XPlat.Views;

namespace Rimot.Desktop.XPlat.Views
{
    public class HostNamePrompt : Window
    {
        public HostNamePrompt()
        {
            Owner = MainWindow.Current;
            InitializeComponent();
        }

        public HostNamePromptViewModel ViewModel => DataContext as HostNamePromptViewModel;

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
