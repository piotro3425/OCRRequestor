using OCRRequestor.Commands;
using OCRRequestor.Model;
using OCRRequestor.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace OCRRequestor.ViewModel
{
   class OCRRequestorViewModel : INotifyPropertyChanged
   {
      public ICommand ExitCommand { get; set; }
      public ICommand OpenFilesCommand { get; set; }
      public ICommand OcrElemMouseDoubleClickCommand { get; set; }

      private readonly IFilesService filesService;
      private readonly IImageProcessorService imageProcessorService;

      private OcrElemData selectedOcrElem;
      private string selectedOcrElemImageUrl;

      public OcrElemData SelectedOcrElem
      {
         get => selectedOcrElem;
         set
         {
            selectedOcrElem = value;
            NotifyPropertyChanged();
            UpdatePreview(value);
         }
      }

      public string SelectedOcrElemImageUrl
      {
         get => selectedOcrElemImageUrl;
         set
         {
            selectedOcrElemImageUrl = value;
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


      public ObservableCollection<OcrElemData> ocrElemsData { get; set; } = new ObservableCollection<OcrElemData>();

      public OCRRequestorViewModel(IFilesService filesService, IImageProcessorService imageProcessorService)
      {
         this.filesService = filesService;
         this.imageProcessorService = imageProcessorService;

         ExitCommand = new Command(p => Application.Current.Shutdown(), p => true);
         OpenFilesCommand = new Command(OpenFilesHandler, p => true);
         OcrElemMouseDoubleClickCommand = new Command(OceElemMouseDoubleClickHandler, p => true);
      }
      private void OpenFilesHandler(object parameter)
      {
         ocrElemsData.Clear();

         filesService.OpenImageFiles().Select(e => new OcrElemData
         {
            FileName = Path.GetFileName(e),
            FileFullPath = e,
            IsProcessed = false
         }).ToList().ForEach(e => ocrElemsData.Add(e));
      }

      private void OceElemMouseDoubleClickHandler(object parameters)
      {
         if (selectedOcrElem != null)
         {
            using System.Drawing.Bitmap bitmap = imageProcessorService.LoadImage(selectedOcrElem.FileFullPath);
            using System.Drawing.Bitmap scaledBitmap = imageProcessorService.ResizeImageToWidth(bitmap, 1000);
            MessageBox.Show($"Width: {bitmap.Width}, Height: {bitmap.Height}", "Original Image");
            MessageBox.Show($"Width: {scaledBitmap.Width}, Height: {scaledBitmap.Height}", "Scaled Image");

            var data = imageProcessorService.GetBitmapAsJpgData(scaledBitmap);
            File.WriteAllBytes("image.jpg", data);
         }
      }

      private void UpdatePreview(OcrElemData ocrElemData) => SelectedOcrElemImageUrl = ocrElemData?.FileFullPath;
   }
}
