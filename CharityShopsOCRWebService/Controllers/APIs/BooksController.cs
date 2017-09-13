using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CharityShopsOCRWebService.Models;
using CharityShopsOCRWebService.DTOs;

namespace CharityShopsOCRWebService.Controllers
{
    public class BooksController : ApiController
    {
        private BooksDbContext db = new BooksDbContext();

        // POST: api/Books
        [ResponseType(typeof(BookDTO))]
        public IHttpActionResult PostBookTable(int userID, [FromBody] BookDTO bookDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool bookExists = bookDTO.Exist;

            if (bookExists)
            {
                BookTable existedBook = db.books.FirstOrDefault(b => b.Isbn == bookDTO.Isbn && b.user.ID == userID);

                string authors = "";

                if (bookDTO.Authors.Count > 0)
                {
                    authors = bookDTO.Authors.ElementAt(0);

                    bookDTO.Authors.RemoveAt(0);
                }

                foreach (var author in bookDTO.Authors)
                {
                    authors = " / " + author;
                }

                string categories = "";

                if (bookDTO.Categories.Count > 0)
                {
                    categories = bookDTO.Categories.ElementAt(0);

                    bookDTO.Categories.RemoveAt(0);
                }

                foreach (var category in bookDTO.Categories)
                {
                    categories = " / " + category;
                }

                existedBook.Title = bookDTO.Title;
                existedBook.Isbn = bookDTO.Isbn;
                existedBook.publisher = bookDTO.Publisher;
                existedBook.DatePublished = bookDTO.DatePublished;
                existedBook.PageCount = bookDTO.PageCount;
                existedBook.ShelfLocation = bookDTO.ShelfLocation;
                existedBook.Quantity = existedBook.Quantity + bookDTO.Quantity;
                existedBook.Authors = authors;
                existedBook.Categories = categories;
                db.Entry(existedBook).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();

                    return Ok(bookDTO);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookTableExists(existedBook.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return BadRequest();
                        throw;

                    }
                }
            }

            else
            {
                User user = db.Users.Find(userID);

                if (user != null)
                {
                    string newAuthors = "";

                    if (bookDTO.Authors.Count > 0)
                    {
                        newAuthors = bookDTO.Authors.ElementAt(0);

                        bookDTO.Authors.RemoveAt(0);
                    }

                    foreach (var author in bookDTO.Authors)
                    {
                        newAuthors = " / " + author;
                    }

                    string newCategories = "";

                    if (bookDTO.Categories.Count > 0)
                    {
                        newCategories = bookDTO.Categories.ElementAt(0);

                        bookDTO.Categories.RemoveAt(0);
                    }

                    foreach (var category in bookDTO.Categories)
                    {
                        newCategories = " / " + category;
                    }

                    BookTable bookTable = new BookTable
                    {
                        user = user,
                        Isbn = bookDTO.Isbn,
                        Title = bookDTO.Title,
                        Authors = newAuthors,
                        publisher = bookDTO.Publisher,
                        DatePublished = bookDTO.DatePublished,
                        PageCount = bookDTO.PageCount,
                        Categories = newCategories,
                        ShelfLocation = bookDTO.ShelfLocation,
                        ThumbnailLink = bookDTO.Thumbnail,
                        SmallThumbnailLink = bookDTO.SmallThumbnail

                    };

                    db.books.Add(bookTable);
                    db.SaveChanges();

                    bookDTO.Exist = true;

                    return Ok(bookDTO);
                }

                else
                {
                    return BadRequest();
                }

            }


        }

        // GET: api/Books
        public IQueryable<BookTable> Getbooks()
        {
            return db.books;
        }

        // GET: api/Books/5
        [ResponseType(typeof(BookTable))]
        public IHttpActionResult GetBookTable(string isbn)
        {
            BookTable bookTable = db.books.FirstOrDefault(b => b.Isbn == isbn);

            if (bookTable == null)
            {
                return NotFound();
            }

            return Ok(bookTable);
        }

        // PUT: api/Books/5
        [ResponseType(typeof(BookDTO))]
        public IHttpActionResult PutBookTable(int id, BookTable bookTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bookTable.ID)
            {
                return BadRequest();
            }

            db.Entry(bookTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookTableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        // DELETE: api/Books/5
        [ResponseType(typeof(BookTable))]
        public IHttpActionResult DeleteBookTable(int id)
        {
            BookTable bookTable = db.books.Find(id);
            if (bookTable == null)
            {
                return NotFound();
            }

            db.books.Remove(bookTable);
            db.SaveChanges();

            return Ok(bookTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookTableExists(int id)
        {
            return db.books.Count(e => e.ID == id) > 0;
        }
    }
}