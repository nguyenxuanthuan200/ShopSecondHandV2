using System;

namespace ShopSecondHand.Data.RequestModels.CheckOutRequest
{
    public class CartListProduct
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
