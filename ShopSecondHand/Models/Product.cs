using System;
using System.Collections.Generic;

#nullable disable

namespace ShopSecondHand.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }
        public Guid? CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Post Post { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
