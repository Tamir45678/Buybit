using NServiceBus;

namespace Commands.Messages
{
    public class OrderRequest:ICommand
    {
        public int UserId { get; set; } 
        public int ProductId { get; set; }
        public int Price { get; set; }
        public DateTime DeliveryDate { get; set; }

    }
}
