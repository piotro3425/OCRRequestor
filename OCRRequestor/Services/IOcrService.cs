using System.Threading.Tasks;

namespace OCRRequestor.Services
{
   public interface IOcrService
   {
      public void SetApiKey(string apiKey);
      public Task<string> ExecuteOcrProcess(byte[] imageData);
   }
}
