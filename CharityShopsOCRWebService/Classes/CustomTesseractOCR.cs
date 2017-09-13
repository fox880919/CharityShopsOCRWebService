using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using Tesseract;

namespace CharityShopsOCRWebService.Classes
{
    public static class CustomTesseractOCR
    {
        public static string getPictureText(Bitmap image1)
        {
            var ENGLISH_LANGUAGE = @"eng";

            string location = Path.Combine(HttpRuntime.AppDomainAppPath, "tessdata");
            var text = "";

            //   using (var ocrEngine = new TesseractEngine(@".\tessdata", ENGLISH_LANGUAGE, EngineMode.Default))
            using (var ocrEngine = new TesseractEngine(location, ENGLISH_LANGUAGE, EngineMode.Default))

            {

                // using (var imageWithText = Pix.LoadFromFile(blogPostImage))
                using (var imageWithText = PixConverter.ToPix(image1))
                {
                    using (var page = ocrEngine.Process(imageWithText))
                    {
                         text = page.GetText();
                    }
                }
            }
            return text;
        }
    }
}