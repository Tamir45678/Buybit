using Commands.Message;
using NServiceBus;
using Stock.Data;
using Stock.Entities;

namespace Commands.Messages
{
    public class RetrieveProductHandler : IHandleMessages<RetrieveProduct>
    {
        private readonly ILogger<RetrieveProductHandler> _logger;
        private readonly DataContext _context;



        public RetrieveProductHandler(ILogger<RetrieveProductHandler> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Handle(RetrieveProduct message, IMessageHandlerContext context)
        {
            _logger.LogInformation($"Retrieving Product No. {message.ProductId} for Order No. {message.Id}");
            Product product = _context.Products.Where(product=>product.Id==message.ProductId).First();
            if (product != null)
            {
                product.Amount += 1;
                _context.Products.Update(product);
                _context.SaveChanges();
            }

            await context.Publish(new ProductRetrieved()
            {
                Id = message.Id,
                ProductId = message.ProductId,
            }).ConfigureAwait(false);

        }

    }
}
