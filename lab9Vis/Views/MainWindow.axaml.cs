using Avalonia.Controls;
using lab9Vis.ViewModels;
using Avalonia.Markup.Xaml;
using ReactiveUI;

namespace lab9Vis.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}