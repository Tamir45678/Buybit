using NServiceBus;

namespace Commands.Messages
{
    public class CheckQuantity : ICommand
    {
        public int Id { get; set; }
        public int ProductId { get; set; }   
    }
}
