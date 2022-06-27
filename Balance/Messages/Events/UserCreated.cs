using NServiceBus;

namespace Commands.Messages
{
    public class UserCreated : IEvent
    {
        public int Id { get; set; }
    }
}
