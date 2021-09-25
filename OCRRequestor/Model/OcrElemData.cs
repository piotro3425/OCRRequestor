using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OCRRequestor.Model
{
   class OcrElemData : INotifyPropertyChanged
   {
      private string fileName;
      private string fileFullPath;
      private bool isProcessed;
      private string ocrResult;

      public string FileName
      {
         get => fileName;
         set
         {
            fileName = value;
            NotifyPropertyChanged();
         }
      }
      public string FileFullPath
      {
         get => fileFullPath;
         set
         {
            fileFullPath = value;
            NotifyPropertyChanged();
         }
      }
      public bool IsProcessed
      {
         get => isProcessed;
         set
         {
            isProcessed = value;
            NotifyPropertyChanged();
         }
      }
      public string OcrResult
      {
         get => ocrResult;
         set
         {
            ocrResult = value;
            NotifyPropertyChanged();
         }
      }

      public event PropertyChangedEventHandler PropertyChanged;
      protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
      {
         if (PropertyChanged != null)
         {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
         }
      }
   }
}
