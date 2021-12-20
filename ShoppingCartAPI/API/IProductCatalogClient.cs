using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingCartAPI.Model;

namespace ShoppingCartAPI.API
{
    public interface IProductCatalogClient
    {
        Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(int[] productCatalogIds);
    }
}