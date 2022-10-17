using System;

namespace ShopSecondHand.Data.RequestModels.SearchRequest
{
    public class SearchRequest
    {
        public string Keyword { get; set; }

        public int? Page { get; set; }

        public string? Order { get; set; } // theo gia

        public string? SortBy { get; set; } // theo ngay moi nhat
        public Guid? CateId { get; set; } // theo category
    }
}
