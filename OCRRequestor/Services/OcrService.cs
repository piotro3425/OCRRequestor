using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static OCRRequestor.Model.OCRResult;

namespace OCRRequestor.Services
{
   class OcrService : IOcrService
   {
      private const string RequestUri = "https://api.ocr.space/Parse/Image";

      public async Task<string> ExecuteOcrProcess(byte[] imageData)
      {
         string parsedText = string.Empty;
         HttpClient httpClient = new HttpClient();
         httpClient.Timeout = new TimeSpan(1, 1, 1);

         try
         {
            MultipartFormDataContent form = PrepareFormData(imageData);
            HttpResponseMessage response = await httpClient.PostAsync(RequestUri, form);
            string strContent = await response.Content.ReadAsStringAsync();
            Rootobject ocrResult = JsonConvert.DeserializeObject<Rootobject>(strContent);

            if (ocrResult.OCRExitCode == 1)
            {
               parsedText = ExtractText(ocrResult);
            }
            else
            {
               return null;
            }
         }
         catch (Exception ex)
         {
            System.Windows.MessageBox.Show(ex.ToString(), "Error");
            return null;
         }

         return parsedText;
      }

      private static string ExtractText(Rootobject ocrResult)
      {
         string text = string.Empty;

         for (int i = 0; i < ocrResult.ParsedResults.Count(); i++)
         {
            text += ocrResult.ParsedResults[i].ParsedText;
         }

         return text;
      }

      private static MultipartFormDataContent PrepareFormData(byte[] imageData)
      {
         MultipartFormDataContent form = new MultipartFormDataContent();
         form.Add(new StringContent("helloworld"), "apikey");
         form.Add(new StringContent("pol"), "language");
         form.Add(new StringContent("2"), "ocrengine");
         form.Add(new StringContent("true"), "scale");
         form.Add(new StringContent("true"), "istable");
         form.Add(new ByteArrayContent(imageData, 0, imageData.Length), "image", "image.jpg");

         return form;
      }
   }
}
