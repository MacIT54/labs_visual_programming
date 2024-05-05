using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Avalonia.VisualTree;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_LogicalApp.Controls
{
    public partial class Connectable : Control
    {
        public List<bool>? BoolArrayIn { get; set; }
        public List<bool>? BoolArrayOut { get; set; }

        public Connectable? output_el = null;
        public Connectable? input_el = null;

        Point? pos;

        public void Draw_connection(DrawingContext context)
        {
            if (output_el == null || input_el == null) return;

            // Convert the origin point of each control to coordinates relative to the container
            var aa = (IClassicDesktopStyleApplicationLifetime)(Application.Current.ApplicationLifetime);

            var input_pos1 = input_el.TranslatePoint(new Point(0, 0), (StackPanel)input_el.GetLogicalParent());
            var output_pos1 = output_el.TranslatePoint(new Point(0, 0), (StackPanel)output_el.GetLogicalParent());
            if(input_pos1 is Point input_pos && output_pos1 is Point output_pos)
            {
                Console.WriteLine(input_pos.ToString());
                Console.WriteLine(output_pos.ToString());
                
                context.DrawLine(new Pen(Brushes.Red, 2), input_pos, output_pos);
            }

            // Draw the line between these relative points

        }

        public bool Connect()
        {
            if (output_el == null || input_el == null) return false;
            
            output_el.BoolArrayIn = BoolArrayOut;

            return true;
        }
    }
}
