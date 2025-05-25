using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
namespace FabricFrame.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new();

        public virtual void AddItem(Design design, int quantity)
        {
            var line = Lines.FirstOrDefault(d => d.Design.DesignId == design.DesignId);

            if (line == null)
                Lines.Add(new CartLine { Design = design, Quantity = quantity });
            else
                line.Quantity += quantity;
        }

        public virtual void RemoveItem(int designId) =>
            Lines.RemoveAll(l => l.Design.DesignId == designId);

        public decimal ComputeTotalValue() =>
            Lines.Sum(e => e.Design.Price * e.Quantity);

        public virtual void Clear() => Lines.Clear();

        public IEnumerable<CartLine> Items => Lines;
    }

    public class CartLine
    {
        [Key]
        public int CartItemId { get; set; }
        public int DesignId { get; set; }


        public Design Design { get; set; }
        public int Quantity { get; set; }
    }
}