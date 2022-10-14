using System;
using System.Collections.Generic;

#nullable disable

namespace ShopSecondHand.Models
{
    public partial class Building
    {
        public Building()
        {
            Accounts = new HashSet<Account>();
            Posts = new HashSet<Post>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
