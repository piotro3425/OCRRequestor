using System.Drawing;

namespace OCRRequestor.Services
{
   public interface IImageProcessorService
   {
      public Bitmap LoadImage(string FilePath);
      public Bitmap ResizeImage(Bitmap bitmap, Size size);
      public Bitmap ResizeImageToWidth(Bitmap bitmap, int width);
      public byte[] GetBitmapAsJpgData(Bitmap bitmap);
   }
}
