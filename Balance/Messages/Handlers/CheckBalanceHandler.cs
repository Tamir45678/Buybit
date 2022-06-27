using Balance.Data;
using Balance.Entities;
using NServiceBus;
using NServiceBus.Logging;

namespace Commands.Messages
{
    public class CheckBalanceHandler : IHandleMessages<CheckBalance>
    {
        private readonly ILogger<CheckBalanceHandler> _logger;
        private readonly DataContext _dataContext;

        public CheckBalanceHandler(DataContext dataContext,ILogger<CheckBalanceHandler> logger)
        {
            _dataContext = dataContext;
            _logger = logger;   
        }   

        public Task Handle(CheckBalance message, IMessageHandlerContext context)
        {
            bool setBalance = DecreaseBalance(message);
            _logger.LogInformation($"Balance is set = {setBalance}"); 
            if (setBalance)
            {
                _logger.LogInformation($"Order {message.Id} is billed");
                OrderBilled orderBilled = new OrderBilled()
                {
                    Id = message.Id,
                    UserId = message.UserId,
                    Price = message.Price,
                };

                return context.Publish(orderBilled);
            }
            //If there is not enough money, Event published to retrieved data
            _logger.LogError($"Order {message.Id} failed, Starting rollback");
            return context.Publish(new BalanceFailed()
            {
                Id=message.Id,
                UserId=message.UserId,  
                Price=message.Price
            });    

        }

        private bool DecreaseBalance(CheckBalance message)
        {
            UserBalance user = _dataContext.UserBalances.Find(message.UserId);
            if (user.Budget < message.Price || user==null)
                return false;
            user.Budget -= message.Price;
            _dataContext.UserBalances.Update(user);
            _dataContext.SaveChanges();
            return true;
        }
    }
}
