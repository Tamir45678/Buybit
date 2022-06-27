using NServiceBus;

namespace Commands.Messages
{
    public class BalanceFailed:IEvent
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int Price { get; set; }
    }
}
