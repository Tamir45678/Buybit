using Marketplace.Data;
using Marketplace.DTOs;
using Marketplace.Models;
using Marketplace.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NServiceBus;

namespace Marketplace.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IMessageSession _messageSession;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(DataContext context,IMessageSession messageSession, ILogger<ProductsController> logger)
        {
            _context = context;
            _messageSession = messageSession;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() 
        {
            return await _context.Products
                .AsNoTracking().ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> PostOrder(OrderDto orderDto)
        {
            int productPrice = _context.Products.Where(product => product.Id == orderDto.ProductId).First().Price;
            _logger.LogInformation($"Order for User #{orderDto.UserId} Arrive to Controller");
            await _messageSession.Send(OrderService.CreateOrder(orderDto, productPrice));
            return Created("", orderDto);
        }

    }
}
