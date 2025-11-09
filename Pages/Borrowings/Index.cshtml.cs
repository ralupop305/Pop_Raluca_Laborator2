using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pop_Raluca_Laborator2.Data;
using Pop_Raluca_Laborator2.Models;

namespace Pop_Raluca_Laborator2.Pages.Borrowings
{
    public class IndexModel : PageModel
    {
        private readonly Pop_Raluca_Laborator2.Data.Pop_Raluca_Laborator2Context _context;

        public IndexModel(Pop_Raluca_Laborator2.Data.Pop_Raluca_Laborator2Context context)
        {
            _context = context;
        }

        public IList<Borrowing> Borrowing { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Borrowing = await _context.Borrowing
                .Include(b => b.Book)
                .ThenInclude(b => b.Author)
                .Include(b => b.Member).ToListAsync();
        }
    }
}
