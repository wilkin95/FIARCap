using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace FIARCap.Models
{
    public class Review
    {
        public int ID { get; set; }

        [Display(Name = "Date Created")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        [Required]
        public string Content { get; set; }

        public int BookID { get; set; }

        [Display(Name = "Book")]
        public string BookTitle { get; set; }
    }
}