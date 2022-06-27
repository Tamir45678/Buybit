using Marketplace.Data;
using Marketplace.Models;
using NServiceBus;

namespace Commands.Messages
{
    public class StockUpdatedHandler : IHandleMessages<StockUpdated>
    {
        private readonly ILogger<StockUpdatedHandler> _logger;
        private readonly DataContext _dataContext;

        public StockUpdatedHandler(DataContext dataContext, ILogger<StockUpdatedHandler> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        public Task Handle(StockUpdated message, IMessageHandlerContext context)
        {
            _logger.LogInformation($"Handling Product {message.ProductId} stock updated");
            Product product = _dataContext.Products.Where(product => product.Id == message.ProductId).First();
            if (message.Amount == 0) {
                product.isInStock = false;
                _dataContext.Products.Update(product);
                _dataContext.SaveChanges();
            }
            return Task.CompletedTask;

        }
    }
}
