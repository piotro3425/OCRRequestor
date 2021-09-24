using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;

namespace OCRRequestor.Services
{
   class FilesService : IFilesService
   {
      public IEnumerable<string> OpenImageFiles()
      {
         OpenFileDialog openFileDialog = new OpenFileDialog();
         openFileDialog.Multiselect = true;
         openFileDialog.Filter = "Image Files|*.bmp;*.png;*.jpg;*.jpeg";

         if (openFileDialog.ShowDialog() == true)
         {
            return openFileDialog.FileNames;
         }

         return new string[0];
      }
   }
}
