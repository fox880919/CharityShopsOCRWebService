using CharityShopsOCRWebService.Classes;
using CharityShopsOCRWebService.Jsons;
using CharityShopsOCRWebService.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace CharityShopsOCRWebService.Controllers
{
    public class CoverOCRController : ApiController
    {
        [ResponseType(typeof(OcrResult))]
        [HttpPost]
        public IHttpActionResult Index([FromBody] ImageInfo imageInfo)
        {
            string encryptedImage = imageInfo.encryptedImage;

            Bitmap img = (Bitmap)Base64Manager.Base64ToImage(encryptedImage);

            string text = CustomTesseractOCR.getPictureText(img);

            OcrResult ocrResult = new OcrResult();

            ocrResult.Result = text;

            return Ok(ocrResult);
        }

        public string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        public Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
    }
}

