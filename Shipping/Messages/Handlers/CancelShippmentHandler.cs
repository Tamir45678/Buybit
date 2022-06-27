using Commands.Messages;
using NServiceBus;
using Shipping.Data;
using Shipping.Entities;

namespace Shipping.Messages.Handlers
{
    public class CancelShippmentHandler : IHandleMessages<CancelShippment>
    {
        private readonly ILogger<CancelShippment> _logger;
        private readonly DataContext _dataContext;

        public CancelShippmentHandler(ILogger<CancelShippment> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        public async Task Handle(CancelShippment message,IMessageHandlerContext context)
        {
            _logger.LogInformation($"Cancelling Order no. {message} Shipping");
            Order order = _dataContext.Orders.Where(x => x.Id == message.Id).FirstOrDefault();
            if (order == null || order.DeliveryDate < DateTime.Now)
                await context.Publish(new CancelShippmentFailed()
                {
                    Id = message.Id
                });
            else
            {
                _dataContext.Orders.Remove(order);
                await _dataContext.SaveChangesAsync();
                await context.Publish(new ShippmentCanceled()
                {
                    Id=message.Id
                });
            }
        }


    }
}
