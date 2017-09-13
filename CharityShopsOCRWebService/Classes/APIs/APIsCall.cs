using CharityShopsOCRWebService.Classes;
using CharityShopsOCRWebService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace CharityShopsOCRWebService.APIs
{
    public class APIsCall
    {
        public static Book getBookByISBN(string isbn)
        {
            var task = getBookByISBNAsync(isbn);
        
            task.Wait();

            var booksList = task.Result;

            return booksList;
        }

        public static async Task<Book> getBookByISBNAsync(string isbn)
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri(Constants.GoogleIsbnURL + isbn + Constants.MaxResults);

                var response = await client.GetAsync(uri).ConfigureAwait(continueOnCapturedContext: false);

                string json = await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);

                var book = JsonConvert.DeserializeObject<Book>(json);

                return book;

            }
        }


        public static Book getBooksByResult( string title, string author)
        {
            var task = getBooksByResultAsync(title, author);

            task.Wait();

            var booksList = task.Result;

            return booksList;
        }
        public static async Task<Book> getBooksByResultAsync( string title, string author)
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri(Constants.GoogleResultURL + title + "&author:" + author + Constants.MaxResults);

                if(title == "")
                uri = new Uri(Constants.GoogleResultURL +"author:" + author + Constants.MaxResults);

                if(author == "")
                uri = new Uri(Constants.GoogleResultURL + title + Constants.MaxResults);

                var response = await client.GetAsync(uri).ConfigureAwait(continueOnCapturedContext: false);

                string json = await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);


                var book = JsonConvert.DeserializeObject<Book>(json);

                return book;


            }
        }

    }
}