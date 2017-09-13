using CharityShopsOCRWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharityShopsOCRWebService.DTOs
{
    public class UserDTO
    {
        public int ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public static UserDTO getUserDTO(User user)
        {
           
            return new UserDTO
            {
                ID = user.ID,
                Username = user.Username,
                Password = user.Password,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
        }

    }

}