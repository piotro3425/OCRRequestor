﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;

namespace OCRRequestor.Services
{
   class FilesService : IFilesService
   {
      public IEnumerable<string> OpenImageFiles()
      {
         try
         {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image Files|*.bmp;*.png;*.jpg;*.jpeg";

            if (openFileDialog.ShowDialog() == true)
            {
               return openFileDialog.FileNames;
            }
         }
         catch(Exception ex)
         {
            MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
         }

         return new string[0];
      }
   }
}
