using Commands.Messages;
using Marketplace.Data;
using Marketplace.Models;
using NServiceBus;

namespace Marketplace.Messages.Handlers
{
    public class ProductRetrievedHandler : IHandleMessages<ProductRetrieved>
    {
        private readonly ILogger<ProductRetrievedHandler> _logger;
        private readonly DataContext _dataContext;

        public ProductRetrievedHandler(ILogger<ProductRetrievedHandler> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }
    
        public Task Handle(ProductRetrieved message, IMessageHandlerContext context)
        {
            _logger.LogInformation($"Handling Product #{message.ProductId} Retrieved. Checking if Status update needed ");
            Product product = _dataContext.Products.Where(product => product.Id == message.ProductId).First();
            if (!product.isInStock)
            {
                product.isInStock = true;
                _dataContext.Products.Update(product);
                _dataContext.SaveChanges();
            }
            return Task.CompletedTask;
        }
    }
}
