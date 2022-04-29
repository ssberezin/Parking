using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Parking.Helpes
{
    public class ImagePathConverter : IValueConverter
    {
        //for convert bytedata array to image
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value != null && value is byte[])
            {
                byte[] imgData = value as byte[];
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.StreamSource = new MemoryStream(imgData);
                img.EndInit();
                return img;
            }
            return null;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
