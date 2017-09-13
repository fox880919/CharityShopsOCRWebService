using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharityShopsOCRWebService.Models
{
    public class User
    {
        public User()
        {
            Books = new List<BookTable>();
        }

        public int ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public virtual ICollection<BookTable> Books { get; set; }



    }
}