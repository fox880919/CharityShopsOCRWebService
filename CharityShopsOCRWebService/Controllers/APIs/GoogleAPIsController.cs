using OnBarcode.Barcode.BarcodeScanner;
using CharityShopsOCRWebService.APIs;
using CharityShopsOCRWebService.Classes;
using CharityShopsOCRWebService.DTOs;
using CharityShopsOCRWebService.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using CharityShopsOCRWebService.Jsons;

namespace CharityShopsOCRWebService.Controllers
{
    [System.Web.Http.RoutePrefix("api/GoogleAPIs")]
    public class GoogleAPIsController : ApiController
    {

        [System.Web.Http.Route("ISBN")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(List<BookDTO>))]
        public IHttpActionResult GetBook(int userID,[FromBody] ImageInfo imageInfo)
        {
            string encryptedImage = imageInfo.encryptedImage;

            if (string.IsNullOrEmpty(encryptedImage))
            {
                return BadRequest("Null value");
            }

            Bitmap baseImage = (Bitmap)Base64Manager.Base64ToImage(encryptedImage);

            string[] datas = BarcodeScanner.Scan(baseImage, BarcodeType.ISBN);

            if (datas != null)
            {
                datas[0] = 9 + datas[0].Substring(1);

                if (datas[0].Length > 13)
                {
                    datas[0] = datas[0].Substring(0, 13);
                }

                Book book = APIsCall.getBookByISBN(datas[0]);

                List<BookDTO> listOfBooks = BookDTO.getListOfBooksDTO(userID, book);

                return Ok(listOfBooks);
            }
                return BadRequest();
        }

        [System.Web.Http.Route("Search")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(List<BookDTO>))]
        public IHttpActionResult GetBooks(int userID, [FromBody] BookSearchDetails bookSearchDetails)
        {
            string title = "";
            if (bookSearchDetails.Title != null)
                 title = bookSearchDetails.Title;

            string author = "";
            if(bookSearchDetails.Author != null)
                author = bookSearchDetails.Author;

            Book books = APIsCall.getBooksByResult(title, author);

            return Ok(BookDTO.getListOfBooksDTO(userID, books));
        }

        [System.Web.Http.Route("ISBN")]
        [ResponseType(typeof(List<BookDTO>))]
        public IHttpActionResult GetBook(int userID, string isbn)
        {
            Book book = APIsCall.getBookByISBN(isbn);

            List<BookDTO> listOfBooks = BookDTO.getListOfBooksDTO(userID, book);

            return Ok(listOfBooks);

        }

    }
}
