using CharityShopsOCRWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CharityShopsOCRWebService.DTOs
{
    public class BookDTO
    {
        public string Isbn { get; set; }

        public string Title { get; set; }

        public List<string> Authors { get; set; }

        public string Publisher { get; set; }

        public string DatePublished { get; set; }

        public double PageCount { get; set; }

        public List<string> Categories { get; set; }

        public string ShelfLocation { get; set; }

        public int Quantity { get; set; }

        public string SmallThumbnail { get; set; }

        public string Thumbnail { get; set; }

        public bool Exist { get; set; }



        public static BookDTO getBookDTO(int userID, Book book)
        {
            BookDTO bookDTO = new BookDTO();

            string isbn = "";

            foreach (var item in book.items)
            {
                VolumeInfo volumeInfo = item.volumeInfo;

                string tempThumbnail = volumeInfo.imageLinks.thumbnail;

                string tempSmallThumbnail = volumeInfo.imageLinks.smallThumbnail;


                if (volumeInfo.industryIdentifiers != null)
                {
                    foreach (var industryIdentifier in volumeInfo.industryIdentifiers)
                    {
                        if (industryIdentifier.type.Equals("ISBN_13"))
                            isbn = industryIdentifier.identifier;
                    }

                    if (isbn == "")
                    {
                        isbn = volumeInfo.industryIdentifiers.ElementAt(0).identifier;
                    }
                }


                if (isbn != "" && doesBookExist(userID, isbn, volumeInfo.title))
                {
                    using (var db = new BooksDbContext())
                    {
                        var existedBook = db.books.FirstOrDefault(b => b.Isbn == isbn && b.user.ID == userID);

                        string existedLocation = existedBook.ShelfLocation;

                        bookDTO = createDTO(volumeInfo);

                        bookDTO.Quantity = 1;

                        bookDTO.Thumbnail = tempThumbnail;

                        bookDTO.SmallThumbnail = tempSmallThumbnail;

                        bookDTO.Exist = true;

                        bookDTO.ShelfLocation = existedLocation;

                        bookDTO.Isbn = isbn;

                        return (bookDTO);
                    }
                }

                else
                {
                    bookDTO = createDTO(volumeInfo);

                    bookDTO.Quantity = 1;

                    bookDTO.Thumbnail = tempThumbnail;

                    bookDTO.SmallThumbnail = tempSmallThumbnail;

                    bookDTO.Isbn = isbn;

                    return (bookDTO);
                }

            }

            return (bookDTO);

        }


        public static List<BookDTO> getListOfBooksDTO(int userID, Book book)
        {
            string isbn = "";

            List<BookDTO> listOfBooksDTO = new List<BookDTO>();

            foreach (var item in book.items)
            {
                VolumeInfo volumeInfo = item.volumeInfo;

                string tempThumbnail = "";
                string tempSmallThumbnail = "";

                if(volumeInfo.imageLinks != null)
                {
                    if (volumeInfo.imageLinks.thumbnail != null)
                        tempThumbnail = volumeInfo.imageLinks.thumbnail;

                    if (volumeInfo.imageLinks.smallThumbnail != null)
                        tempSmallThumbnail = volumeInfo.imageLinks.smallThumbnail;
                }
               
                    if (volumeInfo.industryIdentifiers != null)
                    {
                        foreach (var industryIdentifier in volumeInfo.industryIdentifiers)
                        {
                            if (industryIdentifier.type.Equals("ISBN_13"))
                                isbn = industryIdentifier.identifier;
                        }

                        if (isbn == "")
                        {
                            isbn = volumeInfo.industryIdentifiers.ElementAt(0).identifier;
                        }
                    }
               

                    if (isbn != "" && doesBookExist(userID, isbn, volumeInfo.title))
                    {
                        using (var db = new BooksDbContext())
                        {

                            var existedBook = db.books.FirstOrDefault(b => b.Isbn == isbn && b.user.ID == userID);

                            string existedLocation = existedBook.ShelfLocation;

                            BookDTO bookDTO = createDTO(volumeInfo);

                            bookDTO.Quantity = 1;

                            bookDTO.Thumbnail = tempThumbnail;

                            bookDTO.SmallThumbnail = tempSmallThumbnail;

                            bookDTO.Exist = true;

                            bookDTO.ShelfLocation = existedLocation;

                            bookDTO.Isbn = isbn;

                            listOfBooksDTO.Add(bookDTO);
                        }
                    }

                else
                {
                    BookDTO bookDTO =createDTO(volumeInfo);


                    bookDTO.Quantity = 1;

                    bookDTO.Thumbnail = tempThumbnail;

                    bookDTO.SmallThumbnail = tempSmallThumbnail;

                    bookDTO.Isbn = isbn;

                    listOfBooksDTO.Add(bookDTO);
                }
            }

            
            return listOfBooksDTO;

        }

        public static BookDTO createDTO(VolumeInfo volumeInfo)
        {
            BookDTO bookDTO = new BookDTO();

            if (volumeInfo.title != null)
            {
                bookDTO.Title = volumeInfo.title;
            }

            if (volumeInfo.authors != null)
            {
                bookDTO.Authors = volumeInfo.authors.ToList();
            }

            if (volumeInfo.publisher != null)
            {
                bookDTO.Publisher = volumeInfo.publisher;
            }

            if (volumeInfo.publishedDate != null)
            {
                bookDTO.DatePublished = volumeInfo.publishedDate;
            }

            bookDTO.PageCount = volumeInfo.pageCount;

            if (volumeInfo.categories != null)
            {
                bookDTO.Categories = volumeInfo.categories.ToList();
            }



            return bookDTO;
        }

        public static BookDTO getBookDTO(BookTable book)
        {
          
            List<string> newAuthors = Regex.Split(book.Authors, " / ").ToList();

            List<string> newCategories = Regex.Split(book.Categories, " / ").ToList();

            return new BookDTO
            {
                Isbn = book.Isbn,
                Title = book.Title,
                Authors = newAuthors,
                Publisher = book.publisher,
                DatePublished = book.DatePublished,
                PageCount = book.PageCount,
                Categories = newCategories,
                Quantity = book.Quantity,
                Exist = true,
                Thumbnail = book.ThumbnailLink,
                SmallThumbnail = book.SmallThumbnailLink
            };
        }


        public static bool doesBookExist(int userID, string isbn, string title)
        {

                using (var db = new BooksDbContext())
                {
                    BookTable book = db.books.FirstOrDefault(b => b.Isbn.Equals(isbn) && b.user.ID == userID && b.Title == title);

                    if (book != null)
                    {
                        return true;
                    }

                    else
                    {
                        return false;
                    }

                }
            }
    }
}