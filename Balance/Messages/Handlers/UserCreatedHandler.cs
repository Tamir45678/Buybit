using Balance.Data;
using Balance.Entities;
using Commands.Messages;
using NServiceBus;

namespace Users
{
    public class UserCreatedHandler: IHandleMessages<UserCreated>
    {
        private readonly ILogger<UserCreatedHandler> _logger;
        private readonly DataContext _dataContext;

        public UserCreatedHandler(ILogger<UserCreatedHandler> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        public async Task Handle(UserCreated message, IMessageHandlerContext context) {
            _dataContext.UserBalances.Add(new UserBalance()
            {
                Id = message.Id,
                Budget = 1000
            });
            await _dataContext.SaveChangesAsync();
        }
    }
}
