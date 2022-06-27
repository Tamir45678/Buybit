using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Commands.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NServiceBus;
using Orders.Data;
using Newtonsoft;
using Newtonsoft.Json;
using Orders.Entities;
using Microsoft.EntityFrameworkCore;

namespace Orders.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMessageSession _messageSession;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(DataContext context, IMessageSession messageSession,ILogger<OrdersController> logger)
        {
            _context = context;
            _messageSession = messageSession;
            _logger = logger;
        }



        //Looking for OrderId + Starting Cancel Order Saga
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> CancelOrder(int id)
        {
            
            Order order = _context.Orders.Where(x => x.Id == id).FirstOrDefault();
            _logger.LogInformation($"Starting Canceling Order #{order.Id}");
            if (order.DeliveryDate < DateTime.Now) return BadRequest("Order already shipped");
            await _messageSession.SendLocal(new CancelOrder()
            {
                Id = id,
                UserId = order.UserId,  
                Price = order.Price,
                ProductId=order.ProductId,
                DeliveryDate= order.DeliveryDate

            }).ConfigureAwait(false);
            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Order>>> getOrders(int id)
        {
            var ordersByUserId = await _context.Orders.Where(order => order.UserId == id).ToListAsync();
            _logger.LogInformation(JsonConvert.SerializeObject(ordersByUserId));
            if (!ordersByUserId.Any()) return NoContent();
            return Ok(ordersByUserId);
        }
    }
}