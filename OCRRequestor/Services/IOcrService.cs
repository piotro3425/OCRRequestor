using System.Threading.Tasks;

namespace OCRRequestor.Services
{
   public interface IOcrService
   {
      public void SetApiKey(string apiKey);
      public void SetUseOfAlternativeEngine(bool useAlternativeEngine);
      public Task<string> ExecuteOcrProcess(byte[] imageData);
   }
}
