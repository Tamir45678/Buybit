using NServiceBus;

namespace Orders.Saga
{
    public class OrderCancelledPolicyData: ContainSagaData
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public int Price { get; set; }

        public int ProductId { get; set; }
        
        public DateTime DeliveryDate { get; set; }

        public bool isStartedCancelOrder { get; set; }
        public bool isShippmentCancelled { get; set; }
        public bool isUserRefunded { get; set; }

        public bool isProductRetrieved { get; set; }

    }
}
