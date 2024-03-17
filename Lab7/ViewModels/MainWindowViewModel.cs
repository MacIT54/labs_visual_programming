using System.IO;
using System;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;


namespace Lab7.ViewModels;

public class FileLogger
{
    private readonly string _filePath;

    public FileLogger(string filePath)
    {
        _filePath = filePath;
    }

    public void Log(string message)
    {
        using (StreamWriter writer = new StreamWriter(_filePath, true))
        {
            writer.WriteLine($"{DateTime.Now}: {message}");
        }
    }
}

public class MainWindowViewModel : ViewModelBase
{
    private readonly ObservableCollection<object> _collection = new ObservableCollection<object>();
    private readonly FileLogger _logger = new FileLogger("C:\\Users\\macit\\Desktop\\Lab7\\log.txt");

    public ObservableCollection<object> Collection => _collection;

    public ReactiveCommand<Unit, Unit> AddItemCommand { get; }

    public MainWindowViewModel()
    {
        AddItemCommand = ReactiveCommand.Create(AddItem);
    }

    private void AddItem()
    {
        _collection.Add($"Item {_collection.Count}");
        _logger.Log("Item added to collection.");
    }
}
