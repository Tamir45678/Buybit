using Balance.Data;
using Balance.Entities;
using NServiceBus;

namespace Commands.Messages
{
    public class RefundUserHandler : IHandleMessages<RefundUser>
    {
        private readonly ILogger<RefundUserHandler> _logger;
        private readonly DataContext _dataContext;

        public RefundUserHandler(DataContext dataContext,ILogger<RefundUserHandler> logger)
        {
            _dataContext = dataContext;
            _logger=logger; 
        }

        public async Task Handle(RefundUser message, IMessageHandlerContext context)
        {
            _logger.LogInformation($"Refund User No. {message.UserId} for Order No. {message.Id}");
            bool result = IncreaseBalance(message);
            if (result)
            {
                await context.Publish(new UserRefunded()
                {
                    Id = message.Id,
                    UserId = message.UserId,
                    Price = message.Price
                });
            }

        }

        private bool IncreaseBalance(RefundUser message)
        {
            UserBalance user = _dataContext.UserBalances.Find(message.UserId);
            if(user == null) return false;
            user.Budget += message.Price;
            _dataContext.UserBalances.Update(user);
            _dataContext.SaveChanges();
            return true;
        }

    }
}
