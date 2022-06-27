using NServiceBus;
using NServiceBus.Logging;
using Orders.Data;
using Orders.Entities;
using Orders.Saga;

namespace Commands.Messages
{
    public class OrderPolicy :
        Saga<OrderPolicyData>,
        IAmStartedByMessages<OrderCreated>,
        IHandleMessages<StockUpdated>,
        IHandleMessages<OrderBilled>,
        IHandleMessages<BalanceFailed>,
        IHandleMessages<StockFailed>,
        IHandleMessages<ChargeCancelled>
    {
        private readonly ILogger<OrderPolicy> _logger;
        private readonly DataContext _dataContext;

        public OrderPolicy(ILogger<OrderPolicy> logger,DataContext context)
        {
            _logger = logger;
            _dataContext = context;
            
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderPolicyData> mapper)
        {
            mapper.MapSaga(sagaData => sagaData.OrderId)
                .ToMessage<OrderCreated>(message => message.Id)
                .ToMessage<OrderBilled>(message => message.Id)
                .ToMessage<StockUpdated>(message => message.Id)
                .ToMessage<ChargeCancelled>(message=>message.Id)
                .ToMessage<BalanceFailed>(message => message.Id)
                .ToMessage<StockFailed>(message => message.Id);
        }

       
        public async Task Handle(OrderCreated message, IMessageHandlerContext context)
        {
            Data.isOrderStarted = true;
            Data.UserId = message.UserId;
            Data.ProductId = message.ProductId;
            Data.Price = message.Price;
            Data.DeliveryDate = message.DeliveryDate;
            _logger.LogInformation($"Order no. {message.Id} Create Handle Successfully");
            await context.Send(new CheckBalance()
            {
                Id = message.Id,
                UserId = message.UserId,
                Price = message.Price
            });
        }

        public async Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            _logger.LogInformation($"Order #{message.Id} Billed = {Data.IsOrderBilled}");
            Data.IsOrderBilled = true;
            await context.Send(new CheckQuantity()
            {
                Id=message.Id,  
                ProductId=Data.ProductId
            });
        }
        public Task Handle(StockUpdated message, IMessageHandlerContext context)
        {
            Data.IsStockUpdated = true;
            _logger.LogInformation($"Stock #{message.Id} Updated = {Data.IsStockUpdated}");
            return ProcessOrder(context);
        }

        public Task Handle(BalanceFailed message,IMessageHandlerContext context)
        {
            _logger.LogInformation($"Order is too Expensive to User no. {message.UserId}, Rollback from Balance");
            Data.isBalanceFailed = true;
            return RemoveOrder();
        }

        public async Task Handle (StockFailed message,IMessageHandlerContext context)
        {
            _logger.LogInformation($"No Stock for Product no. {message.ProductId}, Rollback from Stock");
            Data.isStockFailed = true;
            await context.Send(new CancelCharge()
            {
                Id=message.Id,
                UserId=Data.UserId,
                Price=Data.Price
            });
        }

        public Task Handle(ChargeCancelled message,IMessageHandlerContext context)
        {
            _logger.LogInformation($"Charge Cancelled for Order no. {message.Id},Pass to remove Order");
            return RemoveOrder();
        }


        public async Task RemoveOrder()
        {
            _logger.LogInformation($"Deleting Order no.{Data.OrderId}");
            Order order = _dataContext.Orders.Where(x=>x.Id==Data.OrderId).First();
            _dataContext.Orders.Remove(order);
            await _dataContext.SaveChangesAsync();
            MarkAsComplete();
        }

        private async Task ProcessOrder(IMessageHandlerContext context)
        {
            _logger.LogInformation("Sending Order To Shipment");
            await context.Publish(new OrderPlaced()
            {
                Id=Data.OrderId,
                DeliveryDate=Data.DeliveryDate 
            });
            MarkAsComplete();
               
        }
    }

}
