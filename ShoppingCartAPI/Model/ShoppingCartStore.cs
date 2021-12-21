using System.Collections.Generic;

namespace ShoppingCartAPI.Model
{
    public class ShoppingCartStore : IShoppingCartStore
    {
        private static readonly Dictionary<int, ShoppingCart> database = new Dictionary<int, ShoppingCart>();

        public ShoppingCart Get(int userId)
        {
            if (!database.ContainsKey(userId))
                database[userId] = new ShoppingCart(userId);
            return database[userId];
        }

        public void Save(ShoppingCart shoppingCart)
        {
            int userId = shoppingCart.GetUserId();
            if (database.ContainsKey(userId))
            {
                database[userId] = shoppingCart;
            }
        }
    }
}