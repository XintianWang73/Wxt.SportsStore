namespace Wxt.SportsStore.WebApp.Models
{
    using Wxt.SportsStore.Domain.Entities;

    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}