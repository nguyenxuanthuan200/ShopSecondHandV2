using ShopSecondHand.Data.ResponseModels.OrderDetailResponse;
using ShopSecondHand.Data.ResponseModels.PostResponse;
using ShopSecondHand.Data.ResponseModels.TransactionResponse;
using System;

namespace ShopSecondHand.Data.ResponseModels.OrderResponse
{
    public class GetOrderTransactionPostResponse
    {
        public Guid Id { get; set; }
        public GetPostWithProductResponse Post { get; set; }
        public double? Total { get; set; }

        public int? Quantity { get; set; }

        public TransactionDTO Transaction { get; set; }
    }
}
