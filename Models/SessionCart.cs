using FabricFrame.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json.Serialization;

namespace FabricFrame.Models
{
    public class SessionCart : Cart
    {
        public static Cart GetCart(IServiceProvider services)
        {
            var session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
            var cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        [JsonIgnore]
        public ISession? Session { get; set; }

        public override void AddItem(Design design, int quantity)
        {
            base.AddItem(design, quantity);
            SaveCart();
        }

        public override void RemoveItem(int designId)
        {
            base.RemoveItem(designId);
            SaveCart();
        }

        public override void Clear()
        {
            base.Clear();
            SaveCart();
        }

        private void SaveCart()
        {
            Session?.SetJson("Cart", this);
        }
    }
}