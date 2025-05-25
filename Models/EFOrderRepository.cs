using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FabricFrame.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private readonly StoreDBContext context;

        public EFOrderRepository(StoreDBContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Order> Orders => context.Orders
            .Include(o => o.CartItems);

        public void SaveOrder(Order order)
        {
            // Prevent EF from trying to insert existing Designs
            foreach (var line in order.CartItems)
            {
                context.Attach(line.Design);
            }

            if (order.OrderId == 0)
            {
                context.Orders.Add(order);
            }

            context.SaveChanges();
        }

    }
}
