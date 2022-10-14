using System;

namespace ShopSecondHand.Data.RequestModels.OrderDetailRequest
{
    public class CreateOrderDetailRequest
    {
        public Guid? ProductId { get; set; }
        public Guid? OrderId { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
    }
}
