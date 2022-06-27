using Commands.Message;
using Commands.Messages;
using NServiceBus;
using NServiceBus.Logging;
using Orders.Data;
using Orders.Entities;
using Orders.Saga;

namespace Commands.Messages
{
    public class OrderCancelledPolicy :
        Saga<OrderCancelledPolicyData>,
        IAmStartedByMessages<CancelOrder>,
        IHandleMessages<UserRefunded>,
        IHandleMessages<ProductRetrieved>,
        IHandleMessages<ShippmentCanceled>,
        IHandleMessages<CancelShippmentFailed>

    {
        private readonly ILogger<OrderCancelledPolicy> _logger;
        private readonly DataContext _context;

        public OrderCancelledPolicy(ILogger<OrderCancelledPolicy> logger, DataContext context)
        {
            _logger = logger;
            _context = context; 
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderCancelledPolicyData> mapper)
        {
            mapper.MapSaga(sagaData => sagaData.OrderId)
                .ToMessage<CancelOrder>(message => message.Id)
                .ToMessage<UserRefunded>(message => message.Id)
                .ToMessage<ProductRetrieved>(message => message.Id)
                .ToMessage<ShippmentCanceled>(message => message.Id)
                .ToMessage<CancelShippmentFailed>(message => message.Id);
        }


        public async Task Handle(CancelOrder message, IMessageHandlerContext context)
        {
            _logger.LogInformation($"Starting Canceling Order {message.Id}");
            Data.isStartedCancelOrder = true;   
            Data.UserId = message.UserId;   
            Data.Price = message.Price;
            Data.ProductId = message.ProductId;
            Data.DeliveryDate = message.DeliveryDate;

            await context.Send(new CancelShippment()
            {
                Id = message.Id,
                DeliveryDate = message.DeliveryDate
            });
        }

        public async Task Handle(ShippmentCanceled message, IMessageHandlerContext context)
        {
            Data.isShippmentCancelled = true;
            _logger.LogInformation($"Order #{message.Id} Shippment Cancelled = {Data.isShippmentCancelled}");
            await context.Send(new RefundUser()
            {
                Id = message.Id,
                UserId = Data.UserId,
                Price = Data.Price
            });
        }

        public Task Handle(CancelShippmentFailed message,IMessageHandlerContext context)
        {
            _logger.LogInformation($"Order #{message.Id} already shipped");
            return Task.CompletedTask;
        }
        public async Task Handle(UserRefunded message, IMessageHandlerContext context)
        {
            Data.isUserRefunded = true;
            _logger.LogInformation($"User #{message.UserId} Refunded = {Data.isUserRefunded}");
            await context.Send(new RetrieveProduct()
            {
                Id = message.Id,
                ProductId = Data.ProductId
            }).ConfigureAwait(false); 
        }

        public Task Handle(ProductRetrieved message, IMessageHandlerContext context)
        {
            Data.isProductRetrieved = true;
            _logger.LogInformation($"Product #{message.ProductId} Retrieved = {Data.isProductRetrieved}");
            Order order = _context.Orders.Find(message.Id);
            return RemoveOrder(order,context);
        }


        private async Task RemoveOrder(Order order,IMessageHandlerContext context)
        {
            _logger.LogInformation("Processing Data For RemoveOrder");
            if (Data.isUserRefunded && Data.isProductRetrieved && Data.isShippmentCancelled)
            {
                _logger.LogInformation($"Removing Order No.{order.Id}");
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                MarkAsComplete();    
            }
        }



    }

}
