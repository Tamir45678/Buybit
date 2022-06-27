using Commands.Messages;
using Marketplace.DTOs;
namespace Marketplace.Services
{
    public static class OrderService
    {
        public static OrderRequest CreateOrder(OrderDto orderDto, int price)
        {
            return new OrderRequest()
            {
                UserId = orderDto.UserId,
                ProductId = orderDto.ProductId,
                Price = price,
                DeliveryDate = orderDto.DeliveryDate
            };
        }
    }
}
