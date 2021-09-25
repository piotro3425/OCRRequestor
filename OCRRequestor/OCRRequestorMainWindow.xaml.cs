using OCRRequestor.Services;
using OCRRequestor.ViewModel;
using System.Windows;

namespace OCRRequestor
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class OCRRequestorMainWindow : Window
   {
      public OCRRequestorMainWindow()
      {
         InitializeComponent();
         DataContext = new OCRRequestorViewModel(new FilesService(), new ImageProcessorService());
      }
   }
}
