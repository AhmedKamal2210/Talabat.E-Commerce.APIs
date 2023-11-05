namespace Talabat.Core.Entities.OrderEntities
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
            
        }

        public ProductItemOrdered(
            int productItemId,
            string productName,
            string productPictureUrl)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            ProductPictureUrl = productPictureUrl;
        }

        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string ProductPictureUrl { get; set; }
    }
}
