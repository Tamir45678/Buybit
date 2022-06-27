using NServiceBus;

namespace Commands.Messages
{
    public class StockCancelled:IMessage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
    }
}
