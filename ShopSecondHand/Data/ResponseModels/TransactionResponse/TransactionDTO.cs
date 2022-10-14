using System;

namespace ShopSecondHand.Data.ResponseModels.TransactionResponse
{
    public class TransactionDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string TransactionType { get; set; }
        public DateTime? TransactionTime { get; set; }
    }
}
