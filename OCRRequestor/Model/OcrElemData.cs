using OCRRequestor.ViewModel;

namespace OCRRequestor.Model
{
   class OcrElemData : ViewModelBase
   {
      private string fileName;
      private string fileFullPath;
      private bool isProcessed;
      private string ocrResult;

      public string FileName
      {
         get => fileName;
         set => SetProperty(ref fileName, value);
      }
      public string FileFullPath
      {
         get => fileFullPath;
         set => SetProperty(ref fileFullPath, value);
      }
      public bool IsProcessed
      {
         get => isProcessed;
         set => SetProperty(ref isProcessed, value);
      }
      public string OcrResult
      {
         get => ocrResult;
         set => SetProperty(ref ocrResult, value);
      }
   }
}
