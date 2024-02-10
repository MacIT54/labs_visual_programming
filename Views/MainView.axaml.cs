using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Lab2.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    private void ClickHandler(object sender, RoutedEventArgs args)
    {
        if (sender is Button button)
        {
            rectangle.Fill = button.Background;
        }
    }
}
