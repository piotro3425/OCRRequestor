using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OCRRequestor.ViewModel
{
   class ViewModelBase : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;

      protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
      {
         if (PropertyChanged != null)
         {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
         }
      }

      protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
      {
         if (EqualityComparer<T>.Default.Equals(storage, value))
            return false;
         storage = value;
         this.NotifyPropertyChanged(propertyName);
         return true;
      }
   }
}
