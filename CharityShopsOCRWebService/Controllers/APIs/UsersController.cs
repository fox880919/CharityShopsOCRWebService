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
using CharityShopsOCRWebService.Jsons;
using CharityShopsOCRWebService.DTOs;

namespace CharityShopsOCRWebService.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private BooksDbContext db = new BooksDbContext();

        //GET: api/Users
        [Route("login")]
        [HttpPost]
        [ResponseType(typeof(User))]
        public IHttpActionResult Login([FromBody] UserLogin userLogin)
        {
            List<User> test = db.Users.ToList();

            User user = db.Users.FirstOrDefault(u => u.Username.Equals(userLogin.Username)
            && u.Password.Equals(userLogin.Password));

            if (user != null)
            {
                return Ok(UserDTO.getUserDTO(user));
            }
            else
                return Content(HttpStatusCode.Unauthorized, "Error: Check username or password");
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        [Route("register")]
        public IHttpActionResult Registeration([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (db.Users.FirstOrDefault(u => u.Username == user.Username) != null)
            {
                return Content(HttpStatusCode.Conflict, "Error: Username already exists");
            }

            else if (db.Users.FirstOrDefault(u => u.Name == user.Name) != null)
            {
                return Content(HttpStatusCode.Conflict, "Error: Shop name already exists");

            }

            else if (db.Users.FirstOrDefault(u => u.Email == user.Email) != null)
            {
                return Content(HttpStatusCode.Conflict, "Error: Email already exists");

            }

            else
            {
                db.Users.Add(user);
                db.SaveChanges();

                return Ok(UserDTO.getUserDTO(user));
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.ID == id) > 0;
        }
    }
}