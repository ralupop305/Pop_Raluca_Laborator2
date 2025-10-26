using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pop_Raluca_Laborator2.Data;
using Pop_Raluca_Laborator2.Models;

namespace Pop_Raluca_Laborator2.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly Pop_Raluca_Laborator2.Data.Pop_Raluca_Laborator2Context _context;

        public CreateModel(Pop_Raluca_Laborator2.Data.Pop_Raluca_Laborator2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID","PublisherName");
            ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID", "LastName");
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Book.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
