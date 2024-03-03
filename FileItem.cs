using System.Collections.ObjectModel;
using Avalonia.Media.Imaging;

namespace Lab4Visual;

public class FileItem
{
    public string Name { get; set; }
    public string Type { get; set; }
    public Bitmap Icon { get; set; }
    public string Path { get; set; }
    //public ObservableCollection<Bitmap> Images { get; } = new ObservableCollection<Bitmap>();
}