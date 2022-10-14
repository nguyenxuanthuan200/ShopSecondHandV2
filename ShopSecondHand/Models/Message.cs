using System;
using System.Collections.Generic;

#nullable disable

namespace ShopSecondHand.Models
{
    public partial class Message
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid? SenderId { get; set; }
        public Guid? ReceivereId { get; set; }
        public Guid? UserId { get; set; }
    }
}
