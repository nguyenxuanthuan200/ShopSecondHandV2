using System;

namespace ShopSecondHand.Data.ResponseModels.CategoryResponse
{
    public class GetCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}
