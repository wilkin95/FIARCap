using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FIARCap.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(10, ErrorMessage = "ISBNs are limited to 10 digits")]
        public string ISBN { get; set; }
        [Required(ErrorMessage = "The Book Title is required")]
        [Display(Name ="Book Title")]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public string Illustrator { get; set; }
        [Range(0, 2050)]
        public int Copyright { get; set; }
        public BookCategory Category { get; set; }
        public string Summary { get; set; }
        public string Topics { get; set; }
        public string ImagePath { get; set; }
        public ICollection<Review> Reviews { get; set; }

    }
}