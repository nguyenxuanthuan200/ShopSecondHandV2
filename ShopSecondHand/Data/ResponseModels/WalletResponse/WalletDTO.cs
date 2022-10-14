using System;

namespace ShopSecondHand.Data.ResponseModels.WalletResponse
{
    public class WalletDTO
    {
        public Guid Id { get; set; }
        public double? Balance { get; set; }
    }
}
