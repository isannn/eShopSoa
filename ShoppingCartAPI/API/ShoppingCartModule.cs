using Nancy;
using Nancy.ModelBinding;
using ShoppingCartAPI.Model;

namespace ShoppingCartAPI.API
{
    public class ShoppingCartModule: NancyModule
    {
        public ShoppingCartModule(IShoppingCartStore shoppingCartStore, IProductCatalogClient productCatalogClient,
            IEventStore eventStore): base("/shoppingcart")
        {
            Get("/{userid:int}", parameters =>
            {
                var userId = (int) parameters.userid;
                return shoppingCartStore.Get(userId);
            });

            Post("/{userid:int}/items",
                async (parameters, _) =>
                {
                    var productCatalogIds = this.Bind<int[]>();
                    var userId = (int) parameters.userid;

                    var shoppingCart = shoppingCartStore.Get(userId);
                    var shoppingCartItems = await productCatalogClient
                        .GetShoppingCartItems(productCatalogIds)
                        .ConfigureAwait(false);
                    shoppingCart.AddItems(shoppingCartItems, eventStore);
                    shoppingCartStore.Save(shoppingCart);

                    return shoppingCart;
                });
            Delete("/{userid:int}/items",
                async (parameters, _) =>
                {
                    var productCatalogIds = this.Bind<int[]>();
                    var userId = (int) parameters.userid;

                    var shoppingCart = shoppingCartStore.Get(userId);
                    shoppingCart.RemoveItems(productCatalogIds, eventStore);
                    shoppingCartStore.Save(shoppingCart);

                    return shoppingCart;
                });
        }
    }
}
