using System;

namespace ShopSecondHand.Data.RequestModels.CategoryRequest
{
    public class UpdateCategoryRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}
