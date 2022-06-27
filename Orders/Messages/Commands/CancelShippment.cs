using NServiceBus;

namespace Commands.Messages
{
    public class CancelShippment : ICommand
    {
        public int Id { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
