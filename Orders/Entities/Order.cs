namespace Orders.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int ProductId { get; set; }

        public int Price { get; set; }

        public DateTime DeliveryDate { get; set; }
    }
}
