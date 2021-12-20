namespace ShoppingCartAPI.Model
{
    public class ShoppingCartItem
    {
        private int _productId;
        private string _productName;
        private string _productDescription;
        private Money _price;

        public ShoppingCartItem(int productId, string productName, string productDescription, Money price)
        {
            _productId = productId;
            _productName = productName;
            _productDescription = productDescription;
            _price = price;
        }

        public int GetProductId()
        {
            return _productId;
        }
    }
}