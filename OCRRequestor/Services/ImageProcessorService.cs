using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace OCRRequestor.Services
{
   class ImageProcessorService : IImageProcessorService
   {
      public Bitmap LoadImage(string FilePath) => new Bitmap(FilePath);

      public Bitmap ResizeImage(Bitmap bitmap, Size size)
      {
         Bitmap resizedBitmap = new Bitmap(size.Width, size.Height);

         try
         {
            using Graphics graphics = Graphics.FromImage((Image)resizedBitmap);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
            graphics.DrawImage(bitmap, 0, 0, size.Width, size.Height);
         }
         catch(Exception)
         {
            return null;
         }

         return resizedBitmap;
      }

      public Bitmap ResizeImageToWidth(Bitmap bitmap, int width)
      {
         Bitmap resultBitmap = null;

         try
         {
            double prescaler = (double)bitmap.Width / width;
            Size newSize = new Size((int)(bitmap.Width / prescaler), (int)(bitmap.Height / prescaler));
            resultBitmap = ResizeImage(bitmap, newSize);
         }
         catch(Exception)
         {
            return null;
         }

         return resultBitmap;
      }

      public byte[] GetBitmapAsJpgData(Bitmap bitmap)
      {
         using MemoryStream stream = new MemoryStream();

         try
         {
            bitmap.Save(stream, ImageFormat.Jpeg);
         }
         catch(Exception)
         {
            return new byte[0];
         }

         return stream.ToArray();
      }

      private ImageCodecInfo GetEncoder(ImageFormat format) => ImageCodecInfo.GetImageDecoders().Where(e => e.FormatID == format.Guid).FirstOrDefault();
   }
}
