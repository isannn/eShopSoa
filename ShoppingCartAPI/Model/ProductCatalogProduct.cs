namespace ShoppingCartAPI.Model
{
    public class ProductCatalogProduct
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public Money Price { get; set; }
    }
}