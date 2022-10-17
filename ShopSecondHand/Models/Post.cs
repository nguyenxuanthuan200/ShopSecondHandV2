using System;
using System.Collections.Generic;

#nullable disable

namespace ShopSecondHand.Models
{
    public partial class Post
    {
        public Post()
        {
            Orders = new HashSet<Order>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int? Status { get; set; }
        public Guid? AccountId { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? LastUpdateAt { get; set; }
        public float? Price { get; set; }
        public Guid? BuildingId { get; set; }

        public virtual Building Building { get; set; }
        public virtual Product IdNavigation { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
