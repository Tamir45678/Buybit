using Balance.Data;
using Balance.Dto;
using Balance.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Balance.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ILogger<BudgetController> _logger;

        public BudgetController(DataContext context,ILogger<BudgetController> logger)
        {
            _context = context;
            _logger = logger;   
        }

        [HttpGet("{id}")]
        public ActionResult<BudgetDto> getBudget(int id)
        {
            _logger.LogInformation("Entered to Budget Controller");
            UserBalance userBalance = _context.UserBalances.Where(budget => budget.Id == id).First();
            _logger.LogInformation($"User Id: {userBalance.Id} and Balance is: {userBalance.Budget}");
            return new BudgetDto()
            {
                Id = userBalance.Id,
                Budget = userBalance.Budget
            };
        }
    }
}
