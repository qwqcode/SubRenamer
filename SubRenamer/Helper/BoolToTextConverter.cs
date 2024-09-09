using System.Linq;

namespace SubRenamer.Helper;

using Avalonia.Data.Converters;
using System;
using System.Globalization;

public class BoolToTextConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var args = parameter?.ToString()?.Split('|') ?? [];
        var trueText = args.ElementAtOrDefault(0) ?? "True";
        var falseText = args.ElementAtOrDefault(1) ?? "False";

        return value is bool boolValue ? (boolValue ? trueText : falseText) : falseText;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
