using System;

namespace ShopSecondHand.Data.RequestModels.PostRequest
{
    public class UpdatePostRequest
    {
        public string Title { get; set; }
        public string PostDescription { get; set; }
        public string ImageUrl { get; set; }
        public int? Price { get; set; }
        public Guid? BuildingId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int? Quantity { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
