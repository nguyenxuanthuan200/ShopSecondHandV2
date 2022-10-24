using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopSecondHand.Data.RequestModels.SearchRequest;
using ShopSecondHand.Data.ResponseModels.PostResponse;
using ShopSecondHand.Data.ResponseModels.ProductResponse;
using ShopSecondHand.Models;
using ShopSecondHand.Repository.PostRepository;
using ShopSecondHand.Repository.ProductRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PagedList;
using ShopSecondHand.Repository.BuildingRepository;

namespace ShopSecondHand.Repository.SearchRepository
{
    public class SearchRepository : ISearchRepository
    {
        private readonly ShopSecondHandContext dbContext;
        private readonly IMapper _mapper;
        private readonly IProductRepository productRepository;
        private readonly IBuildingRepository buildingRepository;

        public SearchRepository(ShopSecondHandContext dbContext, IMapper mapper, IProductRepository productRepository,
            IBuildingRepository buildingRepository)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
            this.productRepository = productRepository;
            this.buildingRepository = buildingRepository;
        }
        public async Task<IEnumerable<GetPostWithProductResponse>> SearchFilter(SearchRequest request)
        {
            var listPost = dbContext.Posts.AsQueryable();
            listPost = listPost.Where(p => p.Status == 1);
            //sort by categoryId
            if (request.CateId.HasValue)
            {
                var targetCandidate = await dbContext.Posts.Join(dbContext.Products,
                   account => account.Id,
                   candidate => candidate.Id,
                   (account, candidate) => new { account, candidate })
               .Where(result => result.candidate.CategoryId == request.CateId && result.account.Status == 1)
               .Select(result => result.account)
               .ToListAsync();

                listPost = targetCandidate.AsQueryable();
            }
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                listPost = listPost.Where(p => p.Title.Contains(request.Keyword));
            }
           
            //sort by ngay moi nhat
            if (!string.IsNullOrEmpty(request.SortBy))
            {
                switch (request.SortBy)
                {
                    case "newdate":
                        listPost = listPost.OrderByDescending(hh => hh.CreateAt);
                        break;
                    case "olddate":
                        listPost = listPost.OrderBy(hh => hh.CreateAt);
                        break;
                }


            }
            //sort by price
            if (!string.IsNullOrEmpty(request.Order))
            {
                switch (request.Order)
                {
                    case "price_decs":
                        listPost = listPost.OrderByDescending(hh => hh.Price);
                        break;
                    case "price_asc":
                        listPost = listPost.OrderBy(hh => hh.Price);
                        break;
                }
            }
            //sort by buildingId
            if (request.BuildingId.HasValue)
            {
                listPost = listPost.Where(p => p.BuildingId == request.BuildingId);
            }



            var result = listPost.Select(
                   hh =>

                       new GetPostWithProductResponse
                       {
                           Id = hh.Id,
                           Title = hh.Title,
                           Description = hh.Description,
                           ImageUrl = hh.ImageUrl,
                           AccountId = hh.AccountId,
                           Price = hh.Price,
                           CreateAt = hh.CreateAt,
                           LastUpdateAt = hh.LastUpdateAt,
                           BuildingId = (Guid)hh.BuildingId,

                       }

                 ).ToList();
            foreach(var temp in result)
            {
                var product = await productRepository.GetProductById(temp.Id);
                var mapProduct = _mapper.Map<GetProductResponse>(product);
                temp.Product = mapProduct;
                //var category = await categoryRepository.GetCategoryById((Guid)product.CategoryId);
                //temp.ca
                var building = await buildingRepository.GetBuildingById(temp.BuildingId);
                temp.Building = building;
            }
            return result.ToPagedList((int)request.Page,(int)request.PageSize);
        }


    }
}
