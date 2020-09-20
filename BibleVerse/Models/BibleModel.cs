using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc.Routing;

namespace BibleVerse.Models
{
    public class BibleModel
    {
        [Required]
        public String Testament { get; set; }
        [Required]
        [Display(Description = "Book")]
        public String Book { get; set; }
        [Required]
        [Range(1,150)]
        public int Chapter { get; set; }
        [Required]
        [Range(1, 176)]
        public int Verse { get; set; }
        public String Text { get; set; }
        public BibleModel() { }
        public BibleModel(string testament, string book, int chapter, int verse, string text)
        {
            Testament = testament; Book = book; Chapter = chapter; Verse = verse; Text = text;
        }
        public BibleModel(string testament, string book, int chapter, int verse)
        {
            Testament = testament; Book = book; Chapter = chapter; Verse = verse; Text = "";
        }

    }
}