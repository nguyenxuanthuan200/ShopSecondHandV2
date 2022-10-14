using System;
using System.Collections.Generic;

#nullable disable

namespace ShopSecondHand.Models
{
    public partial class Wallet
    {
        public Wallet()
        {
            Transactions = new HashSet<Transaction>();
        }

        public Guid Id { get; set; }
        public double? Balance { get; set; }

        public virtual Account IdNavigation { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
