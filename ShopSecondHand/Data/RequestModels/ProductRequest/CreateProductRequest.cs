﻿using System;

namespace ShopSecondHand.Data.RequestModels.ProductRequest
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
