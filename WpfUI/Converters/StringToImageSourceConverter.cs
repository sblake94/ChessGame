using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfUI.Converters
{
    public class StringToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string imagePath)
            {
                if (File.Exists(imagePath))
                {
                    try
                    {
                        var bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(imagePath);
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();
                        return bitmap;
                    }
                    catch (Exception)
                    {
                        // Handle any exception that may occur during image loading
                        // Return a default image source or handle the error as desired
                    }
                }
                // Return a default image source if the file doesn't exist
                // or handle the missing file scenario as desired
            }
            // Return null or handle the invalid value scenario as desired
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
