using Avalonia.Controls;
using lab9Vis.ViewModels;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Avalonia;

namespace lab9Vis.Views
{
    public partial class MyUserControl : UserControl
    {
        public static readonly StyledProperty<int> WidthProperty =
            AvaloniaProperty.Register<MyUserControl, int>(nameof(Width), defaultValue: 50);

        public int Width
        {
            get => GetValue(WidthProperty);
            set => SetValue(WidthProperty, value);
        }

        public static readonly StyledProperty<int> HeightProperty =
            AvaloniaProperty.Register<MyUserControl, int>(nameof(Height), defaultValue: 40);

        public static readonly StyledProperty<bool> IsExpandedProperty =
    AvaloniaProperty.Register<MyUserControl, bool>(nameof(IsExpanded), defaultValue: false);

        public bool IsExpanded
        {
            get => GetValue(IsExpandedProperty);
            set => SetValue(IsExpandedProperty, value);
        }
        public int Height
        {
            get => GetValue(HeightProperty);
            set => SetValue(HeightProperty, value);
        }

        public MyUserControl()
        {
            InitializeComponent();
        }



        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}