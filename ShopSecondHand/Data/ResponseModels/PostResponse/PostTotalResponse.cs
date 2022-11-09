using System.Collections.Generic;

namespace ShopSecondHand.Data.ResponseModels.PostResponse
{
    public class PostTotalResponse
    {
        public int? Total { get; set; }
        public IEnumerable<GetPostWithProductResponse> Post { get; set; }       
    }
}
