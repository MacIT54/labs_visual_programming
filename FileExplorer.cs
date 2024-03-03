using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;

namespace Lab4Visual;

public class FileExplorer : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Bitmap? _selectedItemImage;

        public Bitmap? SelectedItemImage
        {
            get => _selectedItemImage;
            set
            {
                if (_selectedItemImage != value)
                {
                    _selectedItemImage = value;
                    OnPropertyChanged(nameof(SelectedItemImage));
                }
            }
        }

        private int _count;
        private string _curPath;
        FileItem _chItem;

        public ObservableCollection<FileItem> Items 
        {
            get;
        } = new ObservableCollection<FileItem>();

        public FileItem ChItem
        {
            get => _chItem;

            set
            {
                _chItem = value;
                SingleClick(_chItem);
                DoubleClick(_chItem);
                
            }
        }

        public string CurPath
        {
            get => _curPath;
            set
            {
                if(_curPath != value)
                {
                    _curPath = value;
                    LoadSystemItemsAsync(_curPath);
                    OnPropertyChanged(nameof(CurPath));
                }
            }
        }


        public FileExplorer()
        {
            _count = 1;
            CurPath = "C:\\";
            SelectedItemImage = null;
        }

        private async Task LoadSystemItemsAsync(string path)
        {
            Items.Clear();

            if (_count == 0)
            {
                Items.Add(new FileItem
                {
                    Name = "..",
                    Type = "parent_directory",
                    Path = path,
                    Icon = new Bitmap("~/../../../../Assets/FolderUp.png")
                });

                await LoadDirectoriesAsync(path);
                await LoadFilesAsync(path);
            }
            else if (_count == 1)
            {
                await LoadLogicalDrivesAsync();
                _count = 0;
            }
        }

        private async Task LoadDirectoriesAsync(string path)
        {
            foreach (string directory in await Task.Run(() => Directory.GetDirectories(path)))
            {
                Items.Add(new FileItem
                {
                    Name = System.IO.Path.GetFileName(directory),
                    Type = "directory",
                    Path = path,
                    Icon = new Bitmap("~/../../../../Assets/folder.png")
                });
            }
        }

        private async Task LoadFilesAsync(string path)
        {
            foreach (string file in await Task.Run(() => Directory.GetFiles(path)))
            {
                string extension = Path.GetExtension(file).ToLower();
                if (extension == ".png" || extension == ".jpg" || extension == ".jpeg" || extension == ".gif")
                {
                    Items.Add(new FileItem
                    {
                        Name = System.IO.Path.GetFileName(file),
                        Type = "image",
                        Path = path,
                        Icon = new Bitmap("~/../../../../Assets/gallery.png")
                    });
                }
                else
                {
                    Items.Add(new FileItem
                    {
                        Name = System.IO.Path.GetFileName(file),
                        Type = "file",
                        Path = path,
                        Icon = new Bitmap("~/../../../../Assets/File.png")
                    });
                }
            }
        }

        private async Task LoadLogicalDrivesAsync()
        {
            foreach (string drive in await Task.Run(() => Directory.GetLogicalDrives()))
            {
                Items.Add(new FileItem
                {
                    Name = drive,
                    Type = "logical_drive",
                    Path = drive,
                    Icon = new Bitmap("~/../../../../Assets/hard-disk.png")
                });
            }
        }


        public void DoubleClick(FileItem item)
        {
            if(item != null)
            {
                if(item.Name == ".." && Directory.GetParent(_curPath) != null)
                {
                    CurPath = Directory.GetParent(CurPath)?.FullName;
                }

                else if(item.Name == ".." && Directory.GetParent(_curPath) == null)
                {
                    _count = 1;
                    LoadSystemItemsAsync(_curPath);
                }

                else
                {
                    if(item.Type == "directory")
                    {
                        CurPath = Path.Combine(CurPath, item.Name);
                    }
                    else if (item.Type == "logical_drive")
                    {
                        LoadSystemItemsAsync(_curPath);
                    }
                    
                }
            }
        }

        public void SingleClick(FileItem item)
        {
            if (item != null)
            {
                if (item.Type == "image")
                {
                    string extension = Path.GetExtension(item.Name).ToLower();
                    if (extension == ".png" || extension == ".jpg" || extension == ".jpeg" || extension == ".gif")
                    {
                        SelectedItemImage = new Bitmap(Path.Combine(CurPath, item.Name));
                    }
                }
            }
        }

        public void ClearSelectedImage()
        {   
            SelectedItemImage = null;
        }

}
