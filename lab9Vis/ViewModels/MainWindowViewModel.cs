using ReactiveUI;
using System;
using System.Reactive;
using Avalonia;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using System;
using Avalonia.Threading;

namespace lab9Vis.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private bool _isExpanded;
        private int _width;
        private int _height;
        private double _sliderValue;

        private String _isOpen = "Open";

        public MainWindowViewModel()
        {
            IsExpanded = false;
            Width = 50;
            Height = 40;
            SliderValue = 50;
            ToggleWindowSizeCommand = ReactiveCommand.Create(ToggleWindowSize);
        }

        public String IsOpen
        {
            get => _isOpen;
            set => this.RaiseAndSetIfChanged(ref _isOpen, value);
        }

        public bool IsExpanded
        {
            get => _isExpanded;
            set => this.RaiseAndSetIfChanged(ref _isExpanded, value);
        }

        public int Width
        {
            get => _width;
            set => this.RaiseAndSetIfChanged(ref _width, value);
        }

        public int Height
        {
            get => _height;
            set => this.RaiseAndSetIfChanged(ref _height, value);
        }

        public double SliderValue
        {
            get => _sliderValue;
            set => this.RaiseAndSetIfChanged(ref _sliderValue, value);
        }

        public ReactiveCommand<Unit, Unit> ToggleWindowSizeCommand { get; }


        private void ToggleWindowSize()
        {
            if (IsExpanded)
            {
                Width = 50;
                Height = 40;
                IsOpen = "Open";
            }
            else
            {
                Width = 500;
                Height = 500;
                IsOpen = "Close";
            }

            IsExpanded = !IsExpanded;
        }
    }
}
