using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;

namespace Lab4Visual;

public partial class MainWindow : Window
{
    private readonly FileExplorer explorer;

    public MainWindow()
    {
        InitializeComponent();
        explorer = new FileExplorer();
        DataContext = explorer;
    }

    private void TappedListBox(object? sender, TappedEventArgs args)
    {
        if (sender is ListBox listBox && listBox.SelectedItem is FileItem selectedItem)
        {
            explorer.ClearSelectedImage();
            explorer.ChItem = selectedItem;
        }
    }

    private void TappedImage(object sender, TappedEventArgs e)
    {
        if (sender is Image image && image.DataContext is FileItem item && DataContext is FileExplorer explorer)
        {
            explorer.SingleClick(item);
        }
    }
}