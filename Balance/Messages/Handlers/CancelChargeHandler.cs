using Balance.Data;
using Balance.Entities;
using Commands.Messages;
using NServiceBus;

namespace Balance.Messages.Handlers
{
    public class CancelChargeHandler: IHandleMessages<CancelCharge>
    {
        private readonly ILogger<CancelChargeHandler> _logger;
        private readonly DataContext _dataContext;

        public CancelChargeHandler(ILogger<CancelChargeHandler> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        public async Task Handle(CancelCharge message,IMessageHandlerContext context)
        {
            _logger.LogInformation($"Cancel Order No. {message.Id} for User No. {message.UserId}");
            bool result = IncreaseBalance(message);
            if (result)
            {
                await context.Publish(new ChargeCancelled()
                {
                    Id = message.Id,
                    UserId = message.UserId,
                    Price = message.Price
                });
            }
        }


        private bool IncreaseBalance(CancelCharge message)
        {
            UserBalance user = _dataContext.UserBalances.Find(message.UserId);
            if (user == null) return false;
            user.Budget += message.Price;
            _dataContext.UserBalances.Update(user);
            _dataContext.SaveChanges();
            return true;
        }
    }
}
