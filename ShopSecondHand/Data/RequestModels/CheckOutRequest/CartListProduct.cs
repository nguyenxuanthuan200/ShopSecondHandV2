using System;

namespace ShopSecondHand.Data.RequestModels.CheckOutRequest
{
    public class CartListProduct
    {
        public Guid Id { get; set; }
        public double Total { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
