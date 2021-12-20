using System.Collections.Generic;
using System.Linq;
using ShoppingCartAPI.API;

namespace ShoppingCartAPI.Model
{
    public class ShoppingCart
    {
        private int _userId;
        public List<ShoppingCartItem> ShoppingCartItems { get; }

        public ShoppingCart(int userId)
        {
            _userId = userId;
            ShoppingCartItems = new List<ShoppingCartItem>();
        }


        public void AddItems(IEnumerable<ShoppingCartItem> shoppingCartItems, IEventStore eventStore)
        {
            foreach (var shoppingCartItem in shoppingCartItems)
            {
                ShoppingCartItems.Add(shoppingCartItem);
                eventStore.AddEvent("AddProductItem", shoppingCartItem);
            }
        }

        public void RemoveItems(int[] shoppingCartItems, IEventStore eventStore)
        {
            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var productItem = ShoppingCartItems.FirstOrDefault(item => item.GetProductId() == shoppingCartItem);
                if (productItem != null)
                {
                    ShoppingCartItems.Remove(productItem);
                    eventStore.AddEvent("RemoveProductItem", productItem);
                }
            }
        }
    }
}