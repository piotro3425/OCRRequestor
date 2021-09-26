using OCRRequestor.Commands;
using OCRRequestor.Model;
using OCRRequestor.Services;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OCRRequestor.ViewModel
{
   class OCRRequestorViewModel : ViewModelBase
   {
      public ICommand ExitCommand { get; set; }
      public ICommand OpenFilesCommand { get; set; }
      public ICommand OcrElemMouseDoubleClickCommand { get; set; }
      public ICommand LoadedCommand { get; set; }

      private const string ApiKeyFilePath = "apikey.txt";

      private readonly IFilesService filesService;
      private readonly IImageProcessorService imageProcessorService;
      private readonly IOcrService ocrService;

      private OcrElemData selectedOcrElem;
      private string selectedOcrElemImageUrl;
      private string ocrResultText;

      public OcrElemData SelectedOcrElem
      {
         get => selectedOcrElem;
         set
         {
            SetProperty(ref selectedOcrElem, value);
            UpdatePreview(value);
            UpdateOcrResult(value);
         }
      }

      private void UpdatePreview(OcrElemData ocrElemData) => SelectedOcrElemImageUrl = ocrElemData?.FileFullPath;

      private void UpdateOcrResult(OcrElemData value)
      {
         if (value is not null && value.IsProcessed)
         {
            OcrResultText = value.OcrResult;
         }
      }

      public string SelectedOcrElemImageUrl
      {
         get => selectedOcrElemImageUrl;
         set => SetProperty(ref selectedOcrElemImageUrl, value);
      }

      public string OcrResultText
      {
         get => ocrResultText;
         set => SetProperty(ref ocrResultText, value);
      }

      public ObservableCollection<OcrElemData> ocrElemsData { get; set; } = new ObservableCollection<OcrElemData>();

      public OCRRequestorViewModel(IFilesService filesService, IImageProcessorService imageProcessorService, IOcrService ocrService)
      {
         this.filesService = filesService;
         this.imageProcessorService = imageProcessorService;
         this.ocrService = ocrService;

         ExitCommand = new Command(p => Application.Current.Shutdown(), p => true);
         OpenFilesCommand = new Command(OpenFilesHandler, p => true);
         OcrElemMouseDoubleClickCommand = new Command(OceElemMouseDoubleClickHandler, p => true);
         LoadedCommand = new Command(LoadedHandler, p => true);
      }

      private void LoadedHandler(object obj)
      {
         if(ocrService != null && File.Exists(ApiKeyFilePath))
         {
            ocrService.SetApiKey(File.ReadAllText(ApiKeyFilePath));
         }
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

      private async void OceElemMouseDoubleClickHandler(object parameters)
      {
         await OcrSingleElement(SelectedOcrElem);
      }

      private async Task OcrSingleElement(OcrElemData ocrElemData)
      {
         if (ocrElemData != null)
         {
            using System.Drawing.Bitmap bitmap = imageProcessorService.LoadImage(ocrElemData.FileFullPath);
            using System.Drawing.Bitmap scaledBitmap = imageProcessorService.ResizeImageToWidth(bitmap, 1000);
            var data = imageProcessorService.GetBitmapAsJpgData(scaledBitmap);
            var ocrResult = await ocrService.ExecuteOcrProcess(data);

            if (ocrResult != null)
            {
               ocrElemData.IsProcessed = true;
               ocrElemData.OcrResult = ocrResult;
               OcrResultText = ocrResult;
            }
         }
      }

   }
}
