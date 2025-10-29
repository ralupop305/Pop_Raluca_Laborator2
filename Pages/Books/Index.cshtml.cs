using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pop_Raluca_Laborator2.Data;
using Pop_Raluca_Laborator2.Models;

namespace Pop_Raluca_Laborator2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Pop_Raluca_Laborator2.Data.Pop_Raluca_Laborator2Context _context;

        public IndexModel(Pop_Raluca_Laborator2.Data.Pop_Raluca_Laborator2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; } = default!;
        public BookData BookD { get; set; }
        public int BookID { get; set; }
        public int CategoryID { get; set; }
        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }
        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
        {
            BookD = new BookData();
            TitleSort = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            AuthorSort = sortOrder == "author" ? "author_desc" : "author";
            CurrentFilter = searchString;

            // start query
            var booksQuery = _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .Include(b => b.BookCategories).ThenInclude(bc => bc.Category)
                .AsNoTracking();

            // apply search filter if provided
            if (!string.IsNullOrEmpty(searchString))
            {
                booksQuery = booksQuery.Where(s =>
                    s.Author.FirstName.Contains(searchString) ||
                    s.Author.LastName.Contains(searchString) ||
                    s.Title.Contains(searchString));
            }

            // apply sorting
            switch (sortOrder)
            {
                case "title_desc":
                    booksQuery = booksQuery.OrderByDescending(b => b.Title);
                    break;
                case "author_desc":
                    booksQuery = booksQuery.OrderByDescending(b => b.Author.FullName);
                    break;
                case "author":
                    booksQuery = booksQuery.OrderBy(b => b.Author.FullName);
                    break;
                default:
                    booksQuery = booksQuery.OrderBy(b => b.Title);
                    break;
            }
            // materialize once
            var booksList = await booksQuery.ToListAsync();
            BookD.Books = booksList;
            Book = booksList; // ensures Model.Book[0] usage in the view is safe when list non-empty

            if (id != null)
            {
                BookID = id.Value;
                var book = BookD.Books.Where(i => i.ID == id.Value).SingleOrDefault();
                if (book != null)
                    BookD.Categories = book.BookCategories.Select(s => s.Category);
            }
        }

    }
}
