using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Data;
using Shipping.Entities;

namespace Shipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingsController : ControllerBase
    {
        private readonly ILogger<ShippingsController> _logger;
        private readonly DataContext _dataContext;

        public ShippingsController(ILogger<ShippingsController> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        [HttpGet("{id}")]
        public ActionResult<Order> getOrdersShipping(int id)
        {
            _logger.LogInformation($"Request for order no. {id}");
            Order orderShipping = _dataContext.Orders.Where(order => order.Id == id).FirstOrDefault();
            return Ok(orderShipping);
        }
    }
}
