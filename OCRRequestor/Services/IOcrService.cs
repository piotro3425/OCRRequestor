using System.Threading.Tasks;

namespace OCRRequestor.Services
{
   public interface IOcrService
   {
      public Task<string> ExecuteOcrProcess(byte[] imageData);
   }
}
