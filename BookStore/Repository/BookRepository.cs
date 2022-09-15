using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context = null;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<int> AddNewbook(BookModel model)
        {
            var newBook = new Books()
            {
                Author = model.Author,
                CreateOn = DateTime.UtcNow,
                Description = model.Description,
                Title = model.Title,
                LanguageId = model.LanguageId,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                UpdateOn = DateTime.UtcNow,
                coverImageUrl = model.coverImageUrl,
                BookPdfUrl = model.BookPdfUrl
            };
            newBook.BookGallery = new List<BookGallery>();
            foreach (var file in model.Gallery)
            {
                newBook.BookGallery.Add(new BookGallery()
                {
                    Name = file.Name,
                    URL = file.URL
                });
            }

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            return newBook.id;
        }

        public async Task<List<BookModel>> GetAllBooks()
        {
            var books = new List<BookModel>();
            var allbooks = await _context.Books.ToListAsync();
            if (allbooks?.Any() == true)
            {
                foreach (var book in allbooks)
                {
                    books.Add(new BookModel()
                    {
                        Author = book.Author,
                        category = book.category,
                        Description = book.Description,
                        id = book.id,
                        LanguageId = book.LanguageId,
                        Title = book.Title,
                        TotalPages = book.TotalPages,
                        coverImageUrl = book.coverImageUrl
                    });
                }
            }
            return books;
        }

        public async Task<List<BookModel>> GetTopBooksAsync(int count)
        {
            return await _context.Books.Select(book => new BookModel()
            {
                Author = book.Author,
                category = book.category,
                Description = book.Description,
                id = book.id,
                LanguageId = book.LanguageId,
                Language = book.Language.Name,
                Title = book.Title,
                TotalPages = book.TotalPages,
                coverImageUrl = book.coverImageUrl
            }).Take(count).ToListAsync();
        }

        public async Task<BookModel> GetBookById(int id)
        {
            return await _context.Books.Where(x => x.id == id)
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    category = book.category,
                    Description = book.Description,
                    id = book.id,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    coverImageUrl = book.coverImageUrl,
                    Gallery = book.BookGallery.Select(g => new GalleryModel()
                    {
                        Id = g.Id,
                        Name = g.Name,
                        URL = g.URL
                    }).ToList(),
                    BookPdfUrl = book.BookPdfUrl
                }).FirstOrDefaultAsync();
            //return _context.Books.Where(x => x.id == id).FirstOrDefaultAsync(); 

        }

        public List<BookModel> SearchBook(string title, string authorName)
        {
            return null;
        }


        //private List<BookModel> DataSource()
        //{
        //    return new List<BookModel>()
        //    {
        //        new BookModel(){id =1, Title="MVC", Author="Nitish", Description="It is a description of MVC book.", category="Programming", Language="English", TotalPages=134},
        //        new BookModel(){id =2, Title="Dot net core", Author="Nitish", Description="It is a description of Dot net core book.", category="Framework", Language="French", TotalPages=567},
        //        new BookModel(){id =3, Title="C#", Author="Monika", Description="It is a description of C# book.", category="Developer", Language="Chiense", TotalPages=427},
        //        new BookModel(){id =4, Title="Java", Author="Webgentle", Description="It is a description of Java book.", category="Programming", Language="English", TotalPages=752},
        //        new BookModel(){id =5, Title="php", Author="Webgentle", Description="It is a description of php book.", category="Programming", Language="Korean", TotalPages=472},
        //        new BookModel(){id =6, Title="Azure DecOps", Author="Janvi", Description="It is a description of Azure DecOps book.", category="DevOps", Language="Hindi", TotalPages=457},
        //    };
        //}
    }
}
