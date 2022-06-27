using NServiceBus;

namespace Commands.Message
{
    public class RetrieveProduct : ICommand
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
    }
}
