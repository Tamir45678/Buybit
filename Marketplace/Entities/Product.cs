namespace Marketplace.Models
{
    public class Product
    {
        public int Id { get; set; } 
        public string ProductName { get; set; }    
        
        public int Price { get; set; }

        public string ProductType { get; set; }
        
        public string ImageUrl { get; set; }

        public bool isInStock { get; set; }

        public Product(string productName, int price, string productType,string imageUrl)
        {
            ProductName = productName;
            Price = price;
            ProductType = productType;
            ImageUrl = imageUrl;
        }
    }
}
