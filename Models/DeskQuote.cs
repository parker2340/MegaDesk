using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MegaDesk.Models
{
    public class DeskQuote
    {
        //constraints
        private const decimal BASE_DESK_PRICE = 200.00M;
        private const decimal SURFACE_AREA_COST = 1.00M;
        private const decimal DRAWER_COST = 50.00M;


        [Key]
        public int DeskQuoteId { get; set; }
        [Display(Name ="Customer Name")]
        public string CustomerName { get; set; } = string.Empty;
        [Display(Name ="Price")]

        public decimal Price { get; set; }

        [Display(Name ="Order Date")]
        public DateTime OrderDate { get; set; }
        public int DeskId {get; set; }
        [Display(Name="Delivery Type")]
        public int DeliveryTypeId { get; set; }

//navigation
        public Desk Desk { get; set; }
        public DeliveryType deliveryType { get; set; }



//methods
        public decimal GetQuotePrice(RazorDeskQuoteContext _context){
            decimal quotePrice = BASE_DESK_PRICE;
            decimal surfaceArea = this.Desk.Depth * this.Desk.Width;
            decimal surfacePrice = 0.00M;
            if(surfaceArea > 1000)
            {
                surfacePrice = (surfaceArea - 1000) * SURFACE_AREA_COST;
            }

            decimal drawerPrice = this.Desk.NumberOfDrawers * DRAWER_COST;

            decimal surfaceMaterialPrice = 0.00M;
            var surfaceMaterialPrices = _context.DesktopMaterial
            .Where(d=> d.DesktopMaterialId == this.Desk.DesktopMaterialId)
            .FirstOrDefault();
            surfaceMaterialPrice = surfaceMaterialPrices.Cost;

            decimal DeliveryCost = 0.00M;
            var DeliveryCosts = _context.DeliveryType
            .Where(d=> d.ID == this.DeliveryTypeId).FirstOrDefault();
            if(surfaceArea < 1000)
            {
                DeliveryCost = DeliveryCosts.PriceUnder1000;
            }
            else if (surfaceArea <=2000)
            {
                DeliveryCost = DeliveryCosts.PriceBetween1000And2000;
            }
            else if (surfaceArea > 2000)
            {
                DeliveryCost = DeliveryCosts.PriceOver2000;
            }

            return quotePrice + surfacePrice + surfaceMaterialPrice + drawerPrice + DeliveryCost;
        }
    }

}