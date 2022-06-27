using NServiceBus;

namespace Commands.Messages
{
    public class ProductRetrieved:IEvent
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
    }
}
