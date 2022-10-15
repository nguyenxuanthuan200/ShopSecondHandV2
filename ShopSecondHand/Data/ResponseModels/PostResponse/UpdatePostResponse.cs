using ShopSecondHand.Data.ResponseModels.ProductResponse;
using System;

namespace ShopSecondHand.Data.ResponseModels.PostResponse
{
    public class UpdatePostResponse
    {
        public string Title { get; set; }
        public string PostDescription { get; set; }
        public string ImageUrl { get; set; }
        public int? Price { get; set; }
        public Guid? BuildingId { get; set; }
        public UpdateProductResponse product { get; set; }
    }
}
