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

namespace ShopSecondHand.Repository.SearchRepository
{
    public class SearchRepository : ISearchRepository
    {
        private readonly ShopSecondHandContext dbContext;

        public SearchRepository(ShopSecondHandContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<GetPostResponse>> SearchFilter(SearchRequest request)
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
            if (request.Page.HasValue)
            {

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

                       new GetPostResponse
                       {
                           Id = hh.Id,
                           Title = hh.Title,
                           Description = hh.Description,
                           ImageUrl = hh.ImageUrl,
                           AccountId = hh.AccountId,
                           Price = hh.Price,
                           CreateAt = hh.CreateAt,
                           LastUpdateAt = hh.LastUpdateAt,
                           BuildingId = hh.BuildingId,

                       }

                 );

            return result;
        }


    }
}
