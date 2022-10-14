using System;
using System.Collections.Generic;

#nullable disable

namespace ShopSecondHand.Models
{
    public partial class Transaction
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public Guid? WalletId { get; set; }
        public string TransactionType { get; set; }
        public DateTime? TransactionTime { get; set; }

        public virtual Order IdNavigation { get; set; }
        public virtual Wallet Wallet { get; set; }
    }
}
