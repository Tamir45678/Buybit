using NServiceBus;

namespace Commands.Messages
{
    public class ShippmentCanceled:IEvent
    {
        public int Id { get; set; }
        
    }
}
