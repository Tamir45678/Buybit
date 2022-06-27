using Marketplace.Models;

namespace Marketplace.DTOs
{

        public class OrderDto
        {
            public int UserId { get; set; }

            public int ProductId { get; set; }

            public DateTime DeliveryDate { get; set; }
        }
    
}
