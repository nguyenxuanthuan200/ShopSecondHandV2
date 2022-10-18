using ShopSecondHand.Data.RequestModels.SearchRequest;
using ShopSecondHand.Data.ResponseModels.PostResponse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopSecondHand.Repository.SearchRepository
{
    public interface ISearchRepository
    {
        Task<IEnumerable<GetPostResponse>> SearchFilter(SearchRequest request);
    }
}
