using NServiceBus;

namespace Commands.Messages
{
    public class CancelShippmentFailed:IEvent
    {  
        public int Id { get; set; }
    }
}
