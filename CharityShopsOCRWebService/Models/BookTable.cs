using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharityShopsOCRWebService.Models
{
    public class BookTable
    {
        public int ID { get; set; }

        public virtual User user { get; set; }

        public string Isbn { get; set; }

        public string Title { get; set; }

        public string Authors { get; set; }

        public string publisher { get; set; }

        public string DatePublished { get; set; }

        public double PageCount { get; set; }

        public string Categories { get; set; }

        public int Quantity { get; set; }

        public string ShelfLocation { get; set; }

        public string ThumbnailLink { get; set; }

        public string SmallThumbnailLink { get; set; }





    }
}