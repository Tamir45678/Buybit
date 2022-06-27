using NServiceBus;

namespace Commands.Messages
{
    public class CancelOrder : ICommand
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int Price { get; set; }

        public int ProductId { get; set; }

        public DateTime DeliveryDate { get; set; }
    }
}
