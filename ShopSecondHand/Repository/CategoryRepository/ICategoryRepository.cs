using ShopSecondHand.Data.RequestModels.CategoryRequest;
using ShopSecondHand.Data.ResponseModels.CategoryResponse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopSecondHand.Repository.CategoryRepository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<GetCategoryResponse>> GetCategory();
        Task<GetCategoryResponse> GetCategoryByName(string name);
        Task<GetCategoryResponse> GetCategoryById(Guid id);
        Task<CreateCategoryResponse> CreateCategory(CreateCategoryRequest categoryRequest);
        Task<UpdateCategoryResponse> UpdateCategory(Guid id, UpdateCategoryRequest categoryRequest);
        Task<bool> DeleteCategory(Guid id);
    }
}
