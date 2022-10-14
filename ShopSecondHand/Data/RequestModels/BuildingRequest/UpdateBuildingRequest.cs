using System;

namespace ShopSecondHand.Data.RequestModels.BuildingRequest
{
    public class UpdateBuildingRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
