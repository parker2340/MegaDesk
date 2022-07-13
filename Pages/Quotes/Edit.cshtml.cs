using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MegaDesk.Models;

namespace MegaDesk.Pages_Quotes
{
    public class EditModel : PageModel
    {
        private readonly RazorDeskQuoteContext _context;

        public EditModel(RazorDeskQuoteContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Desk Desk { get; set; }
        [BindProperty]
        public DeskQuote DeskQuote { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DeskQuote == null)
            {
                return NotFound();
            }

            var deskquote =  await _context.DeskQuote.FirstOrDefaultAsync(m => m.DeskQuoteId == id);
            if (deskquote == null)
            {
                return NotFound();
            }
            DeskQuote = deskquote;
           ViewData["DeskId"] = new SelectList(_context.Desk, "DeskQuoteId", "DeskQuoteId");
           ViewData["DeliveryTypeId"] = new SelectList(_context.DeliveryType, "ID", "ID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DeskQuote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeskQuoteExists(DeskQuote.DeskQuoteId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DeskQuoteExists(int id)
        {
          return (_context.DeskQuote?.Any(e => e.DeskQuoteId == id)).GetValueOrDefault();
        }
    }
}
