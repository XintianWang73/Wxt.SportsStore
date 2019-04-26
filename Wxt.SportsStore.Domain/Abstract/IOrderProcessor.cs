namespace Wxt.SportsStore.Domain.Abstract
{
    using Wxt.SportsStore.Domain.Entities;

    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}
