using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FabricFrame.Models;
using System.Linq;

namespace FabricFrame.Pages
{
    public class CheckoutModel : PageModel
    {
        private IOrderRepository orderRepo;
        private Cart cart;

        public CheckoutModel(IOrderRepository repo, Cart cartService)
        {
            orderRepo = repo;
            cart = cartService;
        }

        [BindProperty]
        public Order Order { get; set; }

        public IActionResult OnPost()
        {
            if (!cart.Items.Any())
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                Order.CartItems = cart.Items.ToList();
                orderRepo.SaveOrder(Order);
                cart.Clear();
                return RedirectToPage("/OrderCompleted");
            }

            return Page();
        }
    }
}
