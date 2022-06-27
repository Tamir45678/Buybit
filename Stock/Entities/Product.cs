namespace Stock.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int Amount { get; set; }

        public Product(int id, int amount)
        {
            Id = id;
            Amount = amount;
        }

        public Product() { 
        }
    }
}
