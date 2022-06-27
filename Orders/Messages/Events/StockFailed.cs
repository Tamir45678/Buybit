using NServiceBus;

namespace Commands.Messages
{
    public class StockFailed:IEvent
    {
        public int Id { get; set; } 
        public int ProductId { get; set; }
    }
}
