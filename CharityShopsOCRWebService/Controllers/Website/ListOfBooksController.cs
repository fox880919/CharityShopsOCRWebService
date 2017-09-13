using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CharityShopsOCRWebService.Models;

namespace CharityShopsOCRWebService.Controllers
{
    public class ListOfBooksController : Controller
    {
        private BooksDbContext db = new BooksDbContext();

        // GET: ListOfBooks
        public ActionResult Index()
        {
            if(Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Admins");

            }

            int id = Convert.ToInt32(Session["UserID"].ToString());

            return View(db.books.Where(b => b.user.ID == id).ToList());
        }

        // GET: ListOfBooks/Details/5
        public ActionResult Details(int? id)
        {

            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Admins");

            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTable bookTable = db.books.Find(id);
            if (bookTable == null)
            {
                return HttpNotFound();
            }
            return View(bookTable);
        }

        // GET: ListOfBooks/Create
        public ActionResult Create()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Admins");

            }

            return View();
        }

        // POST: ListOfBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Isbn,Title,Authors,publisher,DatePublished,PageCount,Categories,Quantity,ShelfLocation,ThumbnailLink,SmallThumbnailLink")] BookTable bookTable)
        {

            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Admins");

            }

            if (ModelState.IsValid)
            {
                db.books.Add(bookTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookTable);
        }

        // GET: ListOfBooks/Edit/5
        public ActionResult Edit(int? id)
        {

            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Admins");

            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTable bookTable = db.books.Find(id);
            if (bookTable == null)
            {
                return HttpNotFound();
            }
            return View(bookTable);
        }

        // POST: ListOfBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Isbn,Title,Authors,publisher,DatePublished,PageCount,Categories,Quantity,ShelfLocation,ThumbnailLink,SmallThumbnailLink")] BookTable bookTable)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Admins");

            }
            
            if (ModelState.IsValid)
            {
                db.Entry(bookTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookTable);
        }

        // GET: ListOfBooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Admins");

            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTable bookTable = db.books.Find(id);
            if (bookTable == null)
            {
                return HttpNotFound();
            }
            return View(bookTable);
        }

        // POST: ListOfBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Admins");

            }

            BookTable bookTable = db.books.Find(id);
            db.books.Remove(bookTable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
