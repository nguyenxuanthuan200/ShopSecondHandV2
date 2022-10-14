using System;

namespace ShopSecondHand.Data.ResponseModels.BuildingResponse
{
    public class GetBuildingResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
