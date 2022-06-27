using NServiceBus;
using NServiceBus.Logging;
using Stock.Data;
using Stock.Entities;

namespace Commands.Messages
{
    public class CheckQuantityHandler : IHandleMessages<CheckQuantity>
    {
        static ILogger<CheckQuantityHandler> _logger;
        private readonly DataContext _context;

        public CheckQuantityHandler(DataContext context,ILogger<CheckQuantityHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Handle(CheckQuantity message, IMessageHandlerContext context)
        {
            _logger.LogInformation($"(Stock Handler - Check Quantity of Order ID: {message.Id})");
            Product product = _context.Products.Where(product=>message.ProductId==product.Id).First();
            _logger.LogInformation($"(Stock Handler - Product ID is : {product.Id}) and his amount is {product.Amount}");

            if (DecreaseStock(product))
            {
                var stockUpdated = new StockUpdated()
                {
                    Id = message.Id,
                    ProductId = message.ProductId,
                    Amount=product.Amount
                };
                await context.Publish(stockUpdated);
                _logger.LogInformation($"(Stock Handler - Quantity of Product {message.ProductId} is {product.Amount})");
            }
            else
            {
                _logger.LogError($"Order {message.Id} failed, Starting rollback");
                await context.Publish(new StockFailed()
                {
                    Id = message.Id,
                    ProductId = message.ProductId
                });
            }
        }

        private bool DecreaseStock(Product product)
        {
            _logger.LogInformation($"Product #{product.Id} Is checked");
            if (product.Amount == 0 || product == null)
                return false;
            product.Amount--;
            _context.Products.Update(product);
            _context.SaveChanges();
            _logger.LogInformation($"Product #{product.Id} Reduced to {product.Amount}");
            return true;
        }
    }
}
