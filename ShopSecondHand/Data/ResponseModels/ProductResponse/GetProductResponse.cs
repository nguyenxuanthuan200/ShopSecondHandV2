using System;

namespace ShopSecondHand.Data.ResponseModels.ProductResponse
{
    public class GetProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float? Price { get; set; }
        public int? Quantity { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
