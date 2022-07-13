using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MegaDesk.Models;

namespace MegaDesk.Pages_Quotes
{
    public class IndexModel : PageModel
    {
        private readonly RazorDeskQuoteContext _context;

        public IndexModel(RazorDeskQuoteContext context)
        {
            _context = context;
        }

        public IList<DeskQuote> DeskQuote { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.DeskQuote != null)
            {
                DeskQuote = await _context.DeskQuote
                .Include(d => d.Desk)
                .Include(d => d.deliveryType).ToListAsync();
            }
        }
    }
}
