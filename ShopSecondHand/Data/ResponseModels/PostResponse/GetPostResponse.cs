using System;

namespace ShopSecondHand.Data.ResponseModels.PostResponse
{
    public class GetPostResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float? Price { get; set; }
        public string ImageUrl { get; set; }
        public Guid? AccountId { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? LastUpdateAt { get; set; }
        public Guid? BuildingId { get; set; }
    }
}
