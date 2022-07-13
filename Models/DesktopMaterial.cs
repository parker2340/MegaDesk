using System.ComponentModel.DataAnnotations;

namespace MegaDesk.Models
{
    public class DesktopMaterial
    {
        public int DesktopMaterialId { get; set; }
        public string material { get; set; } = string.Empty;
        public decimal Cost { get; set; }

    }
}