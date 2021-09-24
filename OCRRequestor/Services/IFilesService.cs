using System.Collections.Generic;

namespace OCRRequestor.Services
{
   public interface IFilesService
   {
      public IEnumerable<string> OpenImageFiles();
   }
}
