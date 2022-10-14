using System;

namespace ShopSecondHand.Data.ResponseModels.OrderDetailResponse
{
    public class CreateOrderDetailResponse
    {
        public Guid? ProductId { get; set; }
        public Guid? OrderId { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }

    }
}
