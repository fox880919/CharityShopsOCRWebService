using CharityShopsOCRWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CharityShopsOCRWebService.Controllers
{
    public class AdminsController : Controller
    {

        private BooksDbContext myDbContext = new BooksDbContext();

        // GET: Admins
        public ActionResult Index()
        {
            if(Session["UserID"] != null)
            {
                return RedirectToAction("Index", "ListOfBooks");
            }
            return View();
        }

        public ActionResult Logout()
        {
            if (Session["UserID"] != null)
            {
                Session["UserID"] = null;
                return RedirectToAction("Index", "Admins");
            }
            return View();
        }

        public ActionResult CheckLogin()
        {

            string username = Request.Form["username"];
            string password = Request.Form["Password"];

            var usernameEncoded = Encoding.UTF8.GetBytes(username);
            var passwordEncoded = Encoding.UTF8.GetBytes(password);

            string usernameEncodedText = Convert.ToBase64String(usernameEncoded);
            string passwordeEncodedText = Convert.ToBase64String(passwordEncoded);

            var usr = myDbContext.Users.
                Where(c => c.Username == usernameEncodedText && c.Password == passwordeEncodedText).
                SingleOrDefault();

            if (usr != null)
            {
                var usernameDecoded = Convert.FromBase64String(usr.Username.ToString());

                string usernameDecodedText = Encoding.UTF8.GetString(usernameDecoded);

                Session["UserID"] = usr.ID.ToString();
                Session["UserName"] = usernameDecodedText;
                Session["Name"] = usr.Name.ToString();

                return RedirectToAction("Index", "ListOfBooks");
            }
            else
            {
                ModelState.AddModelError("Credentials", "Invalid Credentials");
                ViewBag.ErrorMessage = "USername or Password is wrong";

                return View("Index");
            }
        }





        public ActionResult BooksList()
        {
            if(Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Admins");
            }

            int id = Convert.ToInt32(Session["UserID"].ToString());


            List<BookTable> books = myDbContext.books.Where(b => b.user.ID == id).ToList();

            BooksList booksClass = new BooksList();

            booksClass.booksList = books;

            return View(books);
        }
    }
}