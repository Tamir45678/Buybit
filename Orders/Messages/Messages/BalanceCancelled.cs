using NServiceBus;

namespace Commands.Messages
{
    public class BalanceCancelled:IMessage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Price { get; set; }
    }
}
