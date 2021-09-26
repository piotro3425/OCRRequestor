using System.Collections.Generic;

namespace OCRRequestor.Services
{
   public interface IFilesService
   {
      public IEnumerable<string> OpenImageFiles();
      public void SaveOcrResultToFileInOutput(string fileNameWithoutExtension, string content);
      public bool SearchForOcrReultFileInOutput(string fileNameWithoutExtension, out string content);
   }
}
