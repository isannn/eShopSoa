using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using ShoppingCartAPI.Model;

namespace ShoppingCartAPI.API
{
    public class ProductCatalogClient : IProductCatalogClient
    {
        private readonly string productCatalogProductBase = "https://private-8d38cb-alekseiisaev.apiary-mock.com";
        private readonly string getProductPathTemplate = "/products?productIds=[{0}]";
        private readonly AsyncRetryPolicy ExponentialRetryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                3, attempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt)));

        private async Task<HttpResponseMessage> RequestProductFromProductCatalog(int[] productCatalogIds)
        {
            var productsResource = $"{getProductPathTemplate}{string.Join(",", productCatalogIds)}";
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(productCatalogProductBase);
                return await httpClient.GetAsync(productsResource).ConfigureAwait(false);
            }
        }

        private async Task<IEnumerable<ShoppingCartItem>> ConvertToShippingCartItems(
            HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            var products =
                JsonConvert.DeserializeObject<List<ProductCatalogProduct>>(await response.Content.ReadAsStringAsync()
                    .ConfigureAwait(false));
            return products.Select(p => new ShoppingCartItem(
                int.Parse((string) p.ProductId),
                p.ProductName,
                p.ProductDescription,
                p.Price
            ));
        }

        private async Task<IEnumerable<ShoppingCartItem>> GetItemsFromCatalogService(int[] productCatalogIds)
        {
            var response = await RequestProductFromProductCatalog(productCatalogIds).ConfigureAwait(false);
            return await ConvertToShippingCartItems(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<ShoppingCartItem>> 
            GetShoppingCartItems(int[] productCatalogIds)
        {
            return await ExponentialRetryPolicy
                .ExecuteAsync(async () =>
                    await GetItemsFromCatalogService(productCatalogIds)
                        .ConfigureAwait(false));
        }
    }
}