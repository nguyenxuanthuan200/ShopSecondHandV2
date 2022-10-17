using ShopSecondHand.Data.RequestModels.PostRequest;
using ShopSecondHand.Data.ResponseModels.PostResponse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopSecondHand.Repository.PostRepository
{
    public interface IPostRepository
    {
        Task<IEnumerable<GetPostWithProductResponse>> GetPost();
        Task<GetPostWithProductResponse> GetPostById(Guid id);
        Task<IEnumerable<GetPostWithProductResponse>> GetPostByAccountId(Guid id);
        Task<CreatePostResponse> CreatePost(CreatePostRequest request);
        Task<UpdatePostResponse> UpdatePost(Guid id, UpdatePostRequest request);
        void Delete(Guid id);

        Task<IEnumerable<GetPostWithProductResponse>> SortPostByName(string name);

    }
}
