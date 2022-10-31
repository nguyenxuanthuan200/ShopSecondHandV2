using System;
using System.Collections.Generic;

namespace ShopSecondHand.Data.RequestModels.CheckOutRequest
{
    public class CheckOutRequest
    {
        
        public Guid AccountId { get; set; }
        //public double Total { get; set; }
        public Guid? WalletId { get; set; }
        public string? Description { get; set; }
        public string TransactionType { get; set; }
        public List<CartListProduct> CartList { get; set; }
    }
}
