using Commands.Messages;
using NServiceBus;
using Orders.Data;
using Orders.Entities;

namespace Orders.Messages
{
    public class RequestOrderHandler : IHandleMessages<OrderRequest>
    {
        static ILogger<RequestOrderHandler> _logger;
        private readonly DataContext _context;

        public RequestOrderHandler(DataContext context, ILogger<RequestOrderHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Handle(OrderRequest message, IMessageHandlerContext context)
        {
            _logger.LogInformation($"Order for User #{message.UserId} Arrive to Order Request Handler");
            //Adding Order to OrderDB
            _context.Orders.Add(
               new Order()
               {
                   UserId = message.UserId,
                   ProductId = message.ProductId,
                   Price = message.Price,
                   DeliveryDate = message.DeliveryDate
               });
            _context.SaveChanges();
            //Sending Command to Start Order Saga With getting OrderId
            int orderId = _context.Orders.Where(x => x.UserId == message.UserId).ToList().Last().Id;
            await context.SendLocal(new OrderCreated()
            {
                Id = orderId,
                UserId = message.UserId,
                ProductId = message.ProductId,
                Price = message.Price,
                DeliveryDate = message.DeliveryDate
            }).ConfigureAwait(false);
        }
    }
}
