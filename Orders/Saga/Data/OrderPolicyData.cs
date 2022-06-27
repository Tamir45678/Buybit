using NServiceBus;

namespace Orders.Saga
{
    public class OrderPolicyData : ContainSagaData
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }

        public int Price { get; set; }

        public int ProductId { get; set; }

        public DateTime DeliveryDate { get; set; }

        public bool isOrderStarted { get; set; }
        public bool IsOrderBilled { get; set; }
        public bool IsStockUpdated { get; set; }
        
        public bool isStockFailed { get; set; }

        public bool isBalanceFailed { get; set; }

    }
}
