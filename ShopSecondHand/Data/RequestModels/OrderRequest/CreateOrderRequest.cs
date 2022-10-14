using System;

namespace ShopSecondHand.Data.RequestModels.OrderRequest
{
    public class CreateOrderRequest
    {
        public Guid? PostId { get; set; }
        public Guid? AccountId { get; set; }
        public double? Total { get; set; }
        public Guid? WalletId { get; set; }
        public string Description { get; set; }
        public string TransactionType { get; set; }
       // public DateTime? TransactionTime { get; set; }
    }
}
