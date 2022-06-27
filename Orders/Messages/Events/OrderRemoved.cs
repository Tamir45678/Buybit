using NServiceBus;

namespace Commands.Messages
{
    public class OrderRemoved:IEvent
    {
        public int Id { get; set; } 
        public int UserId { get; set; }
        
        public int Price { get; set; }

        public int ProductId { get; set; }
    }
}
