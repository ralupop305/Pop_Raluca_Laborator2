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
    public class DetailsModel : PageModel
    {
        private readonly Pop_Raluca_Laborator2.Data.Pop_Raluca_Laborator2Context _context;

        public DetailsModel(Pop_Raluca_Laborator2.Data.Pop_Raluca_Laborator2Context context)
        {
            _context = context;
        }

        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Book
              // Se presupune că Author și Publisher sunt deja incluse
                .Include(b => b.Author) // Includerea autorului (dacă e necesar)
                .Include(b => b.Publisher) // Includerea editurii
                .Include(b => b.BookCategories) // Includerea entităților intermediare
                    .ThenInclude(bc => bc.Category) // Includerea detaliilor categoriei
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
