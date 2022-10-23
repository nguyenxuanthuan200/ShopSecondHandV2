using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopSecondHand.Data.RequestModels.BuildingRequest;
using ShopSecondHand.Data.ResponseModels.BuildingResponse;
using ShopSecondHand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopSecondHand.Repository.BuildingRepository
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly ShopSecondHandContext dbContext;
        private readonly IMapper _mapper;

        public BuildingRepository(ShopSecondHandContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CreateBuildingResponse> CreateBuilding(CreateBuildingRequest buildingRequest)
        {
            var building = await dbContext.Buildings
                 .SingleOrDefaultAsync(p => p.Name.Equals(buildingRequest.Name));
            if (building != null)
                return null;
            Building buildingg = new Building();
            {
                buildingg.Id = Guid.NewGuid();
                buildingg.Name = buildingRequest.Name;
                buildingg.Address = buildingRequest.Address;
            };
            await dbContext.Buildings.AddAsync(buildingg);
            await dbContext.SaveChangesAsync();
            var re = _mapper.Map<CreateBuildingResponse>(buildingg);
            return re;
        }

        public async void DeleteBuilding(Guid id)
        {
            var deBuilding = await dbContext.Buildings
                .SingleOrDefaultAsync(p => p.Id == id);
            dbContext.Buildings.Remove(deBuilding);
            await dbContext.SaveChangesAsync();

        }
        public async Task<IEnumerable<GetBuildingResponse>> GetBuilding()

        {
            var building = await dbContext.Buildings.ToListAsync();
            IEnumerable<GetBuildingResponse> result = building.Select(
                x =>
                {
                    return new GetBuildingResponse()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Address = x.Address
                    };
                }
                ).ToList();
            return result;
        }

        public async Task<GetBuildingResponse> GetBuildingByName(string name)
        {
            var getByName = await dbContext.Buildings

                .FirstOrDefaultAsync(p => p.Name.Equals(name));
            if (getByName != null)
            {
                var re = new GetBuildingResponse()
                {
                    Id = getByName.Id,
                    Name = getByName.Name,
                    Address = getByName.Address
                };
                return re;
            }
            return null;
        }
        public async Task<GetBuildingResponse> GetBuildingById(Guid id)
        {
            var getById = await dbContext.Buildings

                .FirstOrDefaultAsync(p => p.Id == id);
            if (getById != null)
            {
                var re = new GetBuildingResponse()
                {
                    Id = getById.Id,
                    Name = getById.Name,
                    Address = getById.Address
                };
                return re;
            }
            return null;
        }
        public async Task<UpdateBuildingResponse> UpdateBuilding(Guid id, UpdateBuildingRequest buildingRequest)
        {
            var upBuilding = await dbContext.Buildings.SingleOrDefaultAsync(c => c.Id == id);
            if (id != buildingRequest.Id) return null;
            if (upBuilding == null) return null;

            upBuilding.Name = buildingRequest.Name;
            upBuilding.Address = buildingRequest.Address;
            dbContext.Buildings.Update(upBuilding);
            await dbContext.SaveChangesAsync();

            var up = _mapper.Map<UpdateBuildingResponse>(upBuilding);
            return up;
        }


    }
}
