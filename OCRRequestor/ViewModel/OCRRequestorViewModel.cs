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

      private IFilesService filesService;

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

      public OCRRequestorViewModel(IFilesService filesService)
      {
         this.filesService = filesService;

         ExitCommand = new Command(p => Application.Current.Shutdown(), p => true);
         OpenFilesCommand = new Command(OpenFilesHandler, p => true);
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

      private void UpdatePreview(OcrElemData ocrElemData) => SelectedOcrElemImageUrl = ocrElemData?.FileFullPath;
   }
}
