using NServiceBus;

namespace Commands.Messages
{
    public class CancelOrder : ICommand
    {
        public int Id { get; set; }

    }
}
