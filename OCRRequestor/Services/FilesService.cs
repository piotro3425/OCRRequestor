using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace OCRRequestor.Services
{
   class FilesService : IFilesService
   {
      private const string Dir = "out";
      private const string Extension = "txt";

      public IEnumerable<string> OpenImageFiles()
      {
         string[] fileNames = new string[0];

         try
         {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image Files|*.bmp;*.png;*.jpg;*.jpeg";

            if (openFileDialog.ShowDialog() == true)
            {
               fileNames = openFileDialog.FileNames;
            }
         }
         catch(Exception ex)
         {
            MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
         }

         return fileNames;
      }

      public void SaveOcrResultToFileInOutput(string fileNameWithoutExtension, string content)
      {
         string filePath = GetFullFilePath(fileNameWithoutExtension);
         File.WriteAllText(filePath, content);
      }

      private string GetFullFilePath(string fileNameWithoutExtension)
         => $"{Dir}\\{GetFileNameWithExtension(fileNameWithoutExtension)}";

      private string GetFileNameWithExtension(string fileNameWithoutExtension)
         => $"{fileNameWithoutExtension}.{Extension}";

      public bool SearchForOcrReultFileInOutput(string fileNameWithoutExtension, out string content)
      {
         content = null;
         string filePath = GetFullFilePath(fileNameWithoutExtension);

         if (File.Exists(filePath))
         {
            content = File.ReadAllText(filePath);
            return true;
         }

         return false;
      }
   }
}
