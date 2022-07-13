using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MegaDesk.Models;

    public class RazorDeskQuoteContext : DbContext
    {
        public RazorDeskQuoteContext (DbContextOptions<RazorDeskQuoteContext> options)
            : base(options)
        {
        }

        public DbSet<MegaDesk.Models.DeskQuote> DeskQuote { get; set; }
        public DbSet<MegaDesk.Models.Desk> Desk { get; set; }
        public DbSet<MegaDesk.Models.DesktopMaterial> DesktopMaterial { get; set; }
        public DbSet<MegaDesk.Models.DeliveryType> DeliveryType { get; set; }



    }
