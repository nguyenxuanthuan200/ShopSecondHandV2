using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopSecondHand.Data.RequestModels.ProductRequest;
using ShopSecondHand.Data.ResponseModels.ProductResponse;
using ShopSecondHand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopSecondHand.Repository.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopSecondHandContext dbContext;
        private readonly IMapper _mapper;

        public ProductRepository(ShopSecondHandContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<CreateProductResponse> CreateProduct(CreateProductRequest request)
        {
            //var create = await dbContext.Products
            //      .SingleOrDefaultAsync(p => p.Name.Equals(request.Name));
            //if (create != null)
            //    return null;
            Product a = new Product();
            {
                a.Id = Guid.NewGuid();
                a.Name = request.Name;
                a.Description = request.Description;
                a.Price = request.Price;
                a.Quantity = request.Quantity;
                a.CategoryId = request.CategoryId;
            };
            dbContext.Products.AddAsync(a);
            dbContext.SaveChangesAsync();
            var re = _mapper.Map<CreateProductResponse>(a);
            return re;
        }

        public void DeleteProduct(Guid id)
        {
            var deBuilding = dbContext.Products
                .SingleOrDefault(p => p.Id == id);
            //if (deBuilding == null)
            //{
            //    // throw new Exception("This Building is unavailable!");
            //}
            dbContext.Products.Remove(deBuilding);
            dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetProductResponse>> GetProduct()
        {
            var get = await dbContext.Products.ToListAsync();
            IEnumerable<GetProductResponse> result = get.Select(
                x =>
                {
                    return new GetProductResponse()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        Quantity = x.Quantity,
                        CategoryId = x.CategoryId,
                    };
                }
                ).ToList();
            return result;
        }

        public async Task<GetProductResponse> GetProductById(Guid id)
        {
            var getById = await dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == id);
            if (getById != null)
            {
                var re = new GetProductResponse()
                {
                    Id = getById.Id,
                    Name = getById.Name,
                    Description = getById.Description,
                    Price = getById.Price,
                    Quantity = getById.Quantity,
                    CategoryId = getById.CategoryId,
                };
                return re;
            }
            return null;
        }

        public async Task<IEnumerable<GetProductResponse>> GetProductByName(string name)
        {
            var get = await dbContext.Products.ToListAsync();
            IEnumerable<GetProductResponse> result = get.Select(
                x =>
                {
                    return new GetProductResponse()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        Quantity = x.Quantity,
                        CategoryId = x.CategoryId,
                    };
                }
                ).ToList();
            return result;
        }
        public async Task<IEnumerable<GetProductResponse>> GetProductByCategoryId(Guid id)
        {
            var userByBuilding = await dbContext.Products
                .Where(p => p.CategoryId == id).ToListAsync();

            IEnumerable<GetProductResponse> result = userByBuilding.Select(
                x =>
                {
                    return new GetProductResponse()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        Quantity = x.Quantity,
                        CategoryId = x.CategoryId,
                    };
                }
                ).ToList();
            return result;
        }

        public async Task<UpdateProductResponse> UpdateProduct(Guid id, UpdateProductRequest request)
        {
            var up = await dbContext.Products.SingleOrDefaultAsync(c => c.Id == id);
            if (id != request.Id) return null;
            if (up == null) return null;

            up.Name = request.Name;
            up.Description = request.Description;
            up.Price = request.Price;
            up.Quantity = request.Quantity;
            up.CategoryId = request.CategoryId;
            dbContext.Products.Update(up);
            await dbContext.SaveChangesAsync();

            var upResult = _mapper.Map<UpdateProductResponse>(up);
            return upResult;
        }
    }
}
