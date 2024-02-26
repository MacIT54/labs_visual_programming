using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using Avalonia.Media.Imaging;

namespace Lab4Visual;

public class FileExplorer : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _mode;
        private string _currentPath;
        FileItem _choosenItem;
        public ObservableCollection<FileItem> Items { get; } = new ObservableCollection<FileItem>();

        public FileItem ChoosenItem
        {
            get => _choosenItem;
            set
            {
                _choosenItem = value;
                DoubleMouseClick(_choosenItem);
            }
        }

        public string CurrentPath
        {
            get => _currentPath;
            set
            {
                if (_currentPath != value)
                {
                    _currentPath = value;
                    LoadFileSystemItems(_currentPath);
                    OnPropertyChanged(nameof(CurrentPath));
                }
            }
        }

        public FileExplorer()
        {
            _mode = 0;
            CurrentPath = "C:\\";
        }

        private void LoadFileSystemItems(string path)
        {
            Items.Clear();
            if (_mode == 0)
            {
                Items.Add(new FileItem { Name = "..", Type = "parent_directory", Path = path, Icon = new Bitmap("~/../../../../Assets/FolderUp.png") });

                foreach (string directory in Directory.GetDirectories(path))
                {
                    Items.Add(new FileItem { Name = System.IO.Path.GetFileName(directory), Type = "directory", Path = path, Icon = new Bitmap("~/../../../../Assets/Folder.png") });
                }

                foreach (string file in Directory.GetFiles(path))
                {
                    Items.Add(new FileItem { Name = System.IO.Path.GetFileName(file), Type = "file", Path = path, Icon = new Bitmap("~/../../../../Assets/File.png") });
                }
            }
            else if(_mode == 1)
            {
                foreach (string drive in Directory.GetLogicalDrives())
                {
                    Items.Add(new FileItem { Name = drive, Type = "logical_drive", Path = path, Icon = new Bitmap("~/../../../../Assets/Disk.png") });
                }
                _mode = 0;
            }
        }

        public void DoubleMouseClick(FileItem item)
        {
            if (item != null)
            {
                if (item.Name == ".." && Directory.GetParent(_currentPath) != null)
                {
                    CurrentPath = Directory.GetParent(CurrentPath)?.FullName;
                }
                else if(item.Name == ".." && Directory.GetParent(_currentPath) == null)
                {
                    _mode = 1;
                    LoadFileSystemItems(_currentPath);
                }
                else
                {
                    if (item.Type == "directory")
                    {
                        CurrentPath = Path.Combine(CurrentPath, item.Name);
                    }
                    else if( item.Type == "logical_drive")
                    {
                        LoadFileSystemItems(_currentPath);
                    }
                }
            }
        }
    }
