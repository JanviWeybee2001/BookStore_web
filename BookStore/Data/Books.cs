using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public class Books
    {    
        public int id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public string category { get; set; }

        public int LanguageId { get; set; }

        public int TotalPages { get; set; }

        public string coverImageUrl { get; set; }

        public string BookPdfUrl { get; set; }

        public DateTime? CreateOn { get; set; }

        public DateTime? UpdateOn { get; set; }

        public Language Language { get; set; }

        public ICollection<BookGallery> BookGallery { get; set; }
    }
}
