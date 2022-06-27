using NServiceBus;

namespace Commands.Messages
{
    public class StockUpdated : IEvent
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        public int Amount { get; set; }
    }
}
