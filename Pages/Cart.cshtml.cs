using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FabricFrame.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace FabricFrame.Pages
{
    public class CartModel : PageModel
    {
        private readonly Cart _cart;

        public CartModel(Cart cart) => _cart = cart;

        public IEnumerable<CartLine> CartItems => _cart.Items;
        public decimal CartTotal => _cart.ComputeTotalValue();

        [TempData]
        public string SuccessMessage { get; set; }

        public Cart Cart => _cart;

        public void OnGet() { }

        public IActionResult OnPostAdd(int designId, [FromServices] StoreDBContext context)
        {
            var design = context.Designs.FirstOrDefault(d => d.DesignId == designId);
            if (design != null)
            {
                _cart.AddItem(design, 1);
                SuccessMessage = "Item added to cart!";
            }

            return RedirectToPage();
        }

        public IActionResult OnPostAddCustom(string designName, string price, string imageUrl, string brand, [FromServices] StoreDBContext context)
        {
            if (decimal.TryParse(price.Replace("₱", "").Replace(",", ""), out var priceValue))
            {
                var design = new Design
                {
                    DesignName = designName,
                    ImageUrl = imageUrl,
                    Price = priceValue,
                    Brand = brand,
                    OriginalPrice = priceValue
                };

                context.Designs.Add(design);
                context.SaveChanges(); // Save it to get a valid auto-generated DesignId

                _cart.AddItem(design, 1);
                SuccessMessage = "Item added to cart!";
            }

            return RedirectToPage();
        }


        public IActionResult OnPostRemove(int designId)
        {
            _cart.RemoveItem(designId);
            return RedirectToPage();
        }

        public IActionResult OnPostClear()
        {
            _cart.Clear();
            return RedirectToPage();
        }
    }
}
