using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Inf_kiosk_2.Utils
{
    public class Utils
    {
         public static ImageBrush GetBrushFromBitmap(Bitmap sourceBitmap)
         {
             var bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(sourceBitmap.GetHbitmap(),
             IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
             var ib = new ImageBrush {ImageSource = bs};
             return ib;
         }
    }
}
