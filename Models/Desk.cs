using System.ComponentModel.DataAnnotations;

namespace MegaDesk.Models
{
    public class Desk
    {
        public int DeskId { get; set; }

        public int Width { get; set; }
        public int Depth { get; set; }
        [Display(Name="Number of Drawers")]
        public int NumberOfDrawers { get; set; }
        [Display(Name="Desktop Material")]
        //Navigation
        public int DesktopMaterialId { get; set; }
        public DesktopMaterial DesktopMaterial { get; set; }
        [Key]
         public int DeskQuoteId { get; set; }
        public DeskQuote DeskQuote { get; set; }

    }
}