using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MegaDesk.Models;

namespace MegaDesk.Pages_Quotes
{
    public class CreateModel : PageModel
    {
        private readonly RazorDeskQuoteContext _context;

        public CreateModel(RazorDeskQuoteContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["DesktopMaterialId"] = new SelectList(_context.DesktopMaterial, "DesktopMaterialId", "material");
            ViewData["DeliveryTypeId"] = new SelectList(_context.DeliveryType, "ID", "DeliveryName");
            return Page();

        }

        [BindProperty]
        public DeskQuote DeskQuote { get; set; } = default!;
        
        [BindProperty]
        public Desk Desk { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return RedirectToPage("./Index");
            }

            _context.Desk.Add(Desk);
            await _context.SaveChangesAsync();

            DeskQuote.Desk = Desk;

            DeskQuote.OrderDate = DateTime.Now;

            DeskQuote.Price = DeskQuote.GetQuotePrice(_context);

            _context.DeskQuote.Add(DeskQuote);
            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }
    }
}
