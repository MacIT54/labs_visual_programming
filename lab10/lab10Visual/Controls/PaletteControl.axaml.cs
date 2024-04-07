using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Media;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace lab10Visual.Controls
{
    public partial class PalletteControl : TemplatedControl, INotifyPropertyChanged
    {
        public static readonly StyledProperty<Color> CurrentColorProperty =
        AvaloniaProperty.Register<PalletteControl, Color>(nameof(CurrentColor), Colors.Red, defaultBindingMode: BindingMode.TwoWay);
        public Color CurrentColor
        {
            get => GetValue(CurrentColorProperty);
            set => SetValue(CurrentColorProperty, value);
        }

        public static StyledProperty<HsvColor> CurrentHsvColorProperty =
        AvaloniaProperty.Register<PalletteControl, HsvColor>("CurrentHsvColor", defaultValue: Colors.Red.ToHsv());

        public HsvColor CurrentHsvColor
        {
            get => GetValue(CurrentHsvColorProperty);
            set => SetValue(CurrentHsvColorProperty, value);
        }

        public static readonly StyledProperty<List<Color>> ColorDictionaryProperty =
            AvaloniaProperty.Register<PalletteControl, List<Color>>(nameof(ColorDictionary));
        public List<Color> ColorDictionary
        {
            get => GetValue(ColorDictionaryProperty);
            set => SetValue(ColorDictionaryProperty, value);
        }

        public static readonly StyledProperty<ObservableCollection<Color>> NewColorDictionaryProperty =
    AvaloniaProperty.Register<PalletteControl, ObservableCollection<Color>>(nameof(NewColorDictionary));
        public ObservableCollection<Color> NewColorDictionary
        {
            get => GetValue(NewColorDictionaryProperty);
            set => SetValue(NewColorDictionaryProperty, value);
        }

        public PalletteControl()
        {
            InitializeColorDictionary();
            InitializeNewColorDictionary();
        }

        private void InitializeColorDictionary()
        {
            ColorDictionary = new List<Color>(new[] {
                "#FF8080", "#FFFF80", "#80FF80", "#00FF80", "#80FFFF", "#0080FF", "#FF80C0", "#FF80FF", "#FF0000", "#FFFF00",
                "#80FF00", "#00FF40", "#00FFFF", "#0080C0", "#8080C0", "#FF00FF", "#804040", "#FF8040", "#00FF00", "#008080",
                "#004080", "#8080FF", "#800040", "#FF0080", "#800000", "#FF8000", "#008000", "#008040", "#0000FF", "#0000A0",
                "#800080", "#8000FF", "#400000", "#804000", "#008000", "#004000", "#000080", "#000040", "#400040", "#400080",
                "#000000", "#808000", "#808040", "#808080", "#408080", "#C0C0C0", "#400040", "#FFFFFF"
            }.Select(Color.Parse));
        }

        private void InitializeNewColorDictionary()
        {
            NewColorDictionary = new ObservableCollection<Color>(Enumerable.Repeat(Colors.White, 32));
        }

        private void UpdateColor(Color color)
        {
            CurrentColor = color;
            CurrentHsvColor = color.ToHsv();
        }

        private void UpdateColorFromRgb(byte alpha, byte red, byte green, byte blue)
        {
            UpdateColor(new Color(alpha, red, green, blue));
        }

        private void UpdateColorFromHsv(double alpha, double shade, double contrast, double value)
        {
            UpdateColor(HsvColor.FromAhsv(alpha, shade, contrast, value).ToRgb());
        }

        private Button? selected;
        public void HandleColorSelect(object? btn)
        {
            if (btn is not Button button || !(button.DataContext is Color clr))
                return;

            CurrentColor = clr;
            UpdateColor(clr);

            selected?.Classes.Remove("selected");
            selected = button;
            selected.Classes.Add("selected");
        }


        public void AddColorTolab10VisualHandler()
        {
            Color currentColor = CurrentColor;
            int indexOfWhite = NewColorDictionary.IndexOf(Colors.White);
            if (indexOfWhite != -1)
            {
                NewColorDictionary[indexOfWhite] = currentColor;
                RaisePropertyChanged(nameof(NewColorDictionary)); 
            }
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            DataContext = this;

            var spectrum = e.NameScope.Find<ColorSpectrum>("Spectrum");
            var alphaTb = e.NameScope.Find<TextBox>("Alpha");
            var shadeTb = e.NameScope.Find<TextBox>("Shade");
            var contrastTb = e.NameScope.Find<TextBox>("Contrast");
            var redTb = e.NameScope.Find<TextBox>("Red");
            var greenTb = e.NameScope.Find<TextBox>("Green");
            var blueTb = e.NameScope.Find<TextBox>("Blue");
            

            void BindTextChangedEvent(TextBox textBox, Action<string> handler)
            {
                textBox.TextChanged += (_, args) =>
                {
                    if (byte.TryParse(textBox.Text, out var value))
                        handler(textBox.Text);
                };
            }

            redTb.TextChanged += (sender, args) =>
            {
                if (byte.TryParse(redTb.Text, out byte red) && red >= 0 && red <= 255)
                {
                    UpdateColorFromRgb(CurrentColor.A, red, CurrentColor.G, CurrentColor.B);
                }
            };

            greenTb.TextChanged += (sender, args) =>
            {
                if (byte.TryParse(greenTb.Text, out byte green) && green >= 0 && green <= 255)
                {
                    UpdateColorFromRgb(CurrentColor.A, CurrentColor.R, green, CurrentColor.B);
                }
            };

            blueTb.TextChanged += (sender, args) =>
            {
                if (byte.TryParse(blueTb.Text, out byte blue) && blue >= 0 && blue <= 255)
                {
                    UpdateColorFromRgb(CurrentColor.A, CurrentColor.R, CurrentColor.G, blue);
                }
            };


            alphaTb.TextChanged += (sender, args) =>
            {
                if (byte.TryParse(alphaTb.Text, out var alpha) == false) return;
                UpdateColorFromRgb(alpha, CurrentColor.R, CurrentColor.G, CurrentColor.B);
            };


            contrastTb.TextChanged += (sender, args) =>
            {
                if (double.TryParse(contrastTb.Text, out double contrast) && (contrast < 0 || contrast > 1))
                {
                    UpdateColorFromHsv(CurrentHsvColor.A, CurrentHsvColor.H, contrast, CurrentHsvColor.V);
                }
            };

            shadeTb.TextChanged += (sender, args) =>
            {
                if (double.TryParse(shadeTb.Text, out double shade) && (shade > 0 || shade <= 360))
                {
                    UpdateColorFromHsv(CurrentHsvColor.A, shade, CurrentHsvColor.S, CurrentHsvColor.V);
                }
            };

            spectrum.ColorChanged += (_, args) =>
            {
                UpdateColor(args.NewColor);
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
