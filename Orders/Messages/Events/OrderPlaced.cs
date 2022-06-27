using NServiceBus;

namespace Commands.Messages
{
    public class OrderPlaced:IEvent
    {
        public int Id { get; set; }

        public DateTime DeliveryDate { get; set; }
  
    }
}
