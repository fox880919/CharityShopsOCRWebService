using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using CharityShopsOCRWebService.Classes;
using System.Web.Http;
using System.IO;

namespace CharityShopsOCRWebService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index([FromBody] string encryptedImage)
        {


            //var imagePath = @"\\Mac\Home\Desktop\Charity Project\Books Samples\test3.jpg";

            //Bitmap baseImage = (Bitmap)Bitmap.FromFile(imagePath);

            //string imageString = Base64Manager.ImageToBase64(baseImage, baseImage.RawFormat);


            //Bitmap img = (Bitmap)Base64Manager.Base64ToImage(imageString);

            //string text = CustomTesseractOCR.getPictureText(img);

            //ViewBag.Title = "Home Page";

            //return Content(text);

            return RedirectToAction("Index", "Admins");

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
