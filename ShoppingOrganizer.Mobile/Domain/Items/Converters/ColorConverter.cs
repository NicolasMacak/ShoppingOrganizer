using System.Globalization;
using static ShoppingOrganizer.Models.Items.ItemAttachment;


namespace ShoppingOrganizer.Mobile.Domain.Items.Converters;
public class ColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (AttachmentState)value switch
        {
            AttachmentState.AlreadyAttached => Colors.Green,
            AttachmentState.Removed => Colors.PaleVioletRed,
            AttachmentState.New => Colors.LightGreen,
            _ => Colors.White
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
