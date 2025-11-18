using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace STranslate.Converters;

// 具体的枚举类型在使用时创建Converter并继承此泛型类

public class EnumToBoolConverter<T> : MarkupExtension, IValueConverter where T : struct, Enum
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not T currentValue || parameter is not string targetValueStr)
            return false;

        if (!Enum.TryParse<T>(targetValueStr, out var targetValue))
            return false;

        return currentValue.Equals(targetValue) ? true : false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => Binding.DoNothing;

    public override object ProvideValue(IServiceProvider serviceProvider) => this;
}
