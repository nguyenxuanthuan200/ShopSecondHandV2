using ShopSecondHand.Data.ResponseModels.BuildingResponse;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using ShopSecondHand.Data.RequestModels.BuildingRequest;

namespace ShopSecondHand.Repository.BuildingRepository
{
    public interface IBuildingRepository
    {
        Task<IEnumerable<GetBuildingResponse>> GetBuilding();
        Task<GetBuildingResponse> GetBuildingByName(string name);
        Task<GetBuildingResponse> GetBuildingById(Guid id);
        Task<CreateBuildingResponse> CreateBuilding(CreateBuildingRequest buildingRequest);
        Task<UpdateBuildingResponse> UpdateBuilding(Guid id, UpdateBuildingRequest buildingRequest);
        Task<bool> DeleteBuilding(Guid id);
    }
}
