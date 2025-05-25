using System.ComponentModel.DataAnnotations;

namespace FabricFrame.Models
{
    public class CartItems
    {
        [Key]
        public int DesignId { get; set; }
        public string DesignName { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
