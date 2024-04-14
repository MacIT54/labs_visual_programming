using System;
using System.ComponentModel.DataAnnotations;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Threading;
using lab11Visual.Entities;
using static Avalonia.AvaloniaProperty;

namespace lab11Visual.Components;

public class ObjectVisualizer : TemplatedControl
{
    public static readonly StyledProperty<object> TargetObjectProperty =
        Register<ObjectVisualizer,object>(
            nameof(TargetObject),
            null,
            defaultBindingMode: BindingMode.TwoWay
            );
    
    public object TargetObject
    {
        get => GetValue(TargetObjectProperty);
        set => SetValue(TargetObjectProperty, value);
    }
    
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        ObjectVisualizerViewModel visualizer = new ObjectVisualizerViewModel();
        visualizer.ParseObject(TargetObject);
        visualizer.FormContent(null, visualizer.Nodes);
        DataContext = visualizer;
    }
}