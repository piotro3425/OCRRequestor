using System;
using System.Windows.Input;

namespace OCRRequestor.Commands
{
   class Command : ICommand
   {
      Action<object> executeMethod;
      Func<object, bool> canExecuteMethod;

      public Command(Action<object> executeMethod, Func<object, bool> canExecuteMethod)
      {
         this.executeMethod = executeMethod;
         this.canExecuteMethod = canExecuteMethod;
      }

      public event EventHandler CanExecuteChanged;

      public bool CanExecute(object parameter)
      {
         if (canExecuteMethod != null)
            return canExecuteMethod(parameter);
         else
            return true;
      }

      public void Execute(object parameter)
      {
         if (executeMethod != null)
            executeMethod(parameter);
      }
   }
}
