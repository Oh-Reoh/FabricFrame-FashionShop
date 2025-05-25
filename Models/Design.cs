using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

namespace FabricFrame.Models
{
    public class Design
    {
        public int DesignId { get; set; }

        public string DesignName { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? OriginalPrice { get; set; }
        public string? Brand { get; set; }
    }
}
