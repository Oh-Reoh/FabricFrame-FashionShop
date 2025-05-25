using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FabricFrame.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required]
        public string City { get; set; }

        public string State { get; set; }

        [Required]
        public string Zip { get; set; }

        [Required]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
        public bool IsFragile { get; set; }

        public List<CartLine> CartItems { get; set; } = new List<CartLine>();

    }
}
