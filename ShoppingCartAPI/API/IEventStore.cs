using ShoppingCartAPI.Model;

namespace ShoppingCartAPI.API
{
    public interface IEventStore
    {
        void AddEvent(string addproductitem, ShoppingCartItem shoppingCartItem);
    }
}