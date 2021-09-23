using OCRRequestor.Commands;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace OCRRequestor.ViewModel
{
   class OCRRequestorViewModel : INotifyPropertyChanged
   {
      public ICommand ExitCommand { get; set; }

      public event PropertyChangedEventHandler PropertyChanged;
      protected virtual void NotifyPropertyChanged(params string[] propertyNames)
      {
         if (PropertyChanged != null)
         {
            foreach (string propertyName in propertyNames)
               PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
         }
      }

      public OCRRequestorViewModel()
      {
         ExitCommand = new Command(p => Application.Current.Shutdown(), p => true);
      }
   }
}
