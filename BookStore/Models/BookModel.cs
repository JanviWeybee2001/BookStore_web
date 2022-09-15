using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BookStore.Helpers;
using Microsoft.AspNetCore.Http;

namespace BookStore.Models
{
    public class BookModel
    {
        [DataType(DataType.Date)]
        [Display(Name ="Choose the date")]
        public string myFeild { get; set; }
        public int id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public string Description { get; set; }

        public string category { get; set; }

        //[Display(Name = "Language of book")] // we can directly change the value of the text, Here browser'll display 'Language of book' insead of 'Language'.
        [Required(ErrorMessage = "Choose the language of your book")]
        public int LanguageId { get; set; }
        public string Language { get; set; }

        [Required(ErrorMessage ="Enter the NO. of TOTAL PAGES")]
        public int? TotalPages { get; set; }

        [Display(Name = "Choose the cover photo of your book")]
        [Required]
        public IFormFile CoverPhoto { get; set; }

        public string coverImageUrl { get; set; }

        [Display(Name ="Choose the gallery images of your book")]
        [Required]
        public IFormFileCollection GalleryFiles { get; set; }

        public List<GalleryModel> Gallery { get; set; }

        [Display(Name = "Upload your book in PDF format")]
        [Required]
        public IFormFile BookPdf { get; set; }

        public string BookPdfUrl { get; set; }
    }
}
