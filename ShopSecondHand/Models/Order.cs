using System;
using System.Collections.Generic;

#nullable disable

namespace ShopSecondHand.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public Guid Id { get; set; }
        public Guid? PostId { get; set; }
        public Guid? AccountId { get; set; }
        public double? Total { get; set; }

        public virtual Account Account { get; set; }
        public virtual Post Post { get; set; }
        public virtual Transaction Transaction { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
