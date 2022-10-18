using ShopSecondHand.Data.RequestModels.ProductRequest;
using ShopSecondHand.Data.ResponseModels.PostResponse;
using ShopSecondHand.Data.ResponseModels.ProductResponse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopSecondHand.Repository.ProductRepository
{
    public interface IProductRepository
    {
        Task<IEnumerable<GetProductResponse>> GetProduct();
        Task<IEnumerable<GetProductResponse>> GetProductByName(string name);
        Task<GetProductResponse> GetProductById(Guid id);
        Task<IEnumerable<GetProductResponse>> GetProductByCategoryId(Guid id);
        Task<CreateProductResponse> CreateProduct(CreateProductRequest request);
        Task<UpdateProductResponse> UpdateProduct(Guid id, UpdateProductRequest request);
        void DeleteProduct(Guid id);

        Task<IEnumerable<GetPostWithProductResponse>> SortProductByName(string name);
    }
}
