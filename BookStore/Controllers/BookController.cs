using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : Controller   // when you write url then , you have to write only book/getallbooks .. because the class name is only book ,.. Controller is a postfix.!!
    {
        private readonly IBookRepository _bookRepository = null;
       
        private readonly ILanguageRepository _languageRepository = null;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(IBookRepository bookRepository, ILanguageRepository languageRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("~/AllBooks")]
        public async Task<ViewResult> GetAllBooks()
        {
            var data = await _bookRepository.GetAllBooks();

            return View(data);
        }

        //public ViewResult GetBook(int id,string bookName) for some extra anchor tag paramenter

        //[Route("book-details/{id}")] // by this, we can change the url by book-details/[id]
        [Route("book-details/{id:int:min(1)}", Name = "bookDetailsRoute")] // by this, we can pass this route to getAllBook.cshtml
        public async Task<ViewResult> GetBook(int id)
        {
            //dynamic data = new ExpandoObject(); // we create a ExpandoObject for giving the different proprties
            //data.book = _bookRepository.GetBookById(id); // by this we can acces this book property by written Model.book.id ;)
            //data.name = "JAnvi"; // also we can give any data.[propertName]

            var data = await _bookRepository.GetBookById(id);

            return View(data);

            //return "book-" + id.ToString();
            //// also you can write ... return $"book-{id}";
            ////http://localhost:63122/book/getbook/2
        }

        public List<BookModel> SearchBooks(string bookName, string authorName)
        {

            return _bookRepository.SearchBook(bookName, authorName);

            //return $"Book with name = {bookName} & Author = {authorName}";
            ////http://localhost:63122/book/searchbooks?bookname=theDream&authorname=janvi
        }
        public ViewResult AddNewBook(bool isSuccess = false, int Bookid = 0)
        {
            var model = new BookModel();
            ViewBag.IsSuccess = isSuccess;
            ViewBag.IsBookId = Bookid;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if(ModelState.IsValid)
            {
                if (bookModel.CoverPhoto != null)
                {
                    string folder = "books/cover/";
                    bookModel.coverImageUrl = await UploadImage(folder, bookModel.CoverPhoto);
                }
                if (bookModel.GalleryFiles != null)
                {
                    string folder = "books/gallery/";

                    bookModel.Gallery = new List<GalleryModel>();

                    foreach(var file in bookModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            URL = await UploadImage(folder, file)
                        };
                        bookModel.Gallery.Add(gallery);
                    }
                }
                if (bookModel.BookPdf != null)
                {
                    string folder = "books/pdf/";
                    bookModel.BookPdfUrl = await UploadImage(folder, bookModel.BookPdf);
                }

                int id = await _bookRepository.AddNewbook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookid = id });
                }
            }

            return View();
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }
    }
}


//ViewBag.Language = new SelectList(GetLanguage(), "Id", "Text");
//ViewBag.Language = new List<String>() { "Hindi", "English", "Dutch" };

//ViewBag.Language = GetLanguage().Select(x => new SelectListItem()
//{
//    Text = x.Text
//}).ToList();

//ViewBag.Language = new List<SelectListItem>()
//{
//    new SelectListItem(){Text="Hindi", Value="Hindi"},
//    new SelectListItem(){Text="English", Value="English", Selected = true},
//    new SelectListItem(){Text="Dutch", Value="Dutch"},
//    new SelectListItem(){Text="Tamil", Value="Tamil", Disabled = true},
//};

//var group1 = new SelectListGroup() { Name = "Group 1" };
//var group2 = new SelectListGroup() { Name = "Group 2" };
//var group3 = new SelectListGroup() { Name = "Group 3" };

//ViewBag.Language = new List<SelectListItem>()
//{
//    new SelectListItem(){Text="Hindi", Value="Hindi"},
//    new SelectListItem(){Text="English", Value="English"},
//    new SelectListItem(){Text="Dutch", Value="Dutch"},
//    new SelectListItem(){Text="Tamil", Value="Tamil"},
//    new SelectListItem(){Text="Urdu", Value="Urdu"},
//    new SelectListItem(){Text="Japanese", Value="Japanese"},
//};