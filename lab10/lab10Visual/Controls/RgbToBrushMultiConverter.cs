using Avalonia.Data;
using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;

namespace lab10Visual.Controls
{
    public class RgbToBrushMultiConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {

            if (values?.Count != 4 || !targetType.IsAssignableFrom(typeof(Color)))
                throw new NotSupportedException();

            if (!values.All(x => x is string or UnsetValueType or null))
                return BindingOperations.DoNothing;

            if (!byte.TryParse((string?)values[0], out var r) ||
                !byte.TryParse((string?)values[1], out var g) ||
                !byte.TryParse((string?)values[2], out var b) ||
                !byte.TryParse((string?)values[3], out var a))

                return BindingOperations.DoNothing;

            var color = new Color(a, r, g, b);
            return color;
        }
    }
}
