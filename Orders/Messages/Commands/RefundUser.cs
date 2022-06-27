using NServiceBus;

namespace Commands.Messages
{
    public class RefundUser :ICommand
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Price { get; set; }
    }
}
