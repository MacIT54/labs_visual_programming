using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Homework_LogicalApp.Controls;

namespace Homework_LogicalApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        StackPanel sp = this.FindControl<StackPanel>("panel");
        var input = new InputControler();
        var invert = new InverterConrol();
        var output = new OutputControler();

        input.output_el = invert;

        invert.input_el = input;
        invert.output_el = output;

        output.input_el = invert;

        input.Connect();
        invert.Connect();

        sp.Children.Add(input);
        sp.Children.Add(invert);
        sp.Children.Add(output);
    }
}