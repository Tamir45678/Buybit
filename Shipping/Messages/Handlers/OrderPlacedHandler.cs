using Commands.Messages;
using NServiceBus;
using Shipping.Data;
using Shipping.Entities;

namespace Shipping.Messages.Handlers
{
    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        private readonly ILogger<OrderPlacedHandler> _logger;
        private readonly DataContext _dataContext;

        public OrderPlacedHandler(ILogger<OrderPlacedHandler> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        public async Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            _logger.LogInformation($"Adding Order no. {message.Id} To Shippment");
            _dataContext.Orders.Add(
                new Order
                {
                    Id=message.Id,
                    DeliveryDate=message.DeliveryDate
                });
            await _dataContext.SaveChangesAsync();
        }

    }
}
