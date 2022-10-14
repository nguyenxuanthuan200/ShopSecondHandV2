using System;

namespace ShopSecondHand.Data.ResponseModels.OrderResponse
{
    public class UpdateOrderResponse
    {
        public Guid? PostId { get; set; }
        public Guid? AccountId { get; set; }
        public double? Total { get; set; }
    }
}
