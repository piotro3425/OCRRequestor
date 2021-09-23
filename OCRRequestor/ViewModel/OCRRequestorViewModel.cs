using System.ComponentModel;

namespace OCRRequestor.ViewModel
{
   class OCRRequestorViewModel : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;
      protected virtual void NotifyPropertyChanged(params string[] propertyNames)
      {
         if (PropertyChanged != null)
         {
            foreach (string propertyName in propertyNames)
               PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
         }
      }
   }
}
