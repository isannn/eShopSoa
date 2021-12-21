using System.Collections.Generic;
using System.Linq;
using ShoppingCartAPI.API;

namespace ShoppingCartAPI.Model
{
    public class ShoppingCart
    {
        private int _userId;
        private readonly List<ShoppingCartItem> _items;


        public ShoppingCart(int userId)
        {
            _userId = userId;
            _items = new List<ShoppingCartItem>();
        }


        public void AddItems(IEnumerable<ShoppingCartItem> shoppingCartItems, IEventStore eventStore)
        {
            foreach (var item in shoppingCartItems)
            {
                _items.Add(item);
                eventStore.Raise("ShoppingCartItemAdded", new { _userId, item });
            }
        }

        public void RemoveItems(int[] catalogProductIds, IEventStore eventStore)
        {
            foreach (var productId in catalogProductIds)
            {
                var shoppingCartItem = _items.FirstOrDefault(item => item.GetProductId() == productId);
                if (shoppingCartItem != null)
                {
                    _items.Remove(shoppingCartItem);
                    eventStore.Raise("RemoveProductItem", new { _userId, shoppingCartItem });
                }
            }
        }

        public int GetUserId()
        {
            return _userId;
        }
    }
}