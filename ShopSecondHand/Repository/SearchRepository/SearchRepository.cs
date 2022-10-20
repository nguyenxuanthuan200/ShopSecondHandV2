using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IProductRepository productRepository;
        private readonly IPostRepository postRepository;

        public SearchRepository(ShopSecondHandContext dbContext, IMapper mapper, IProductRepository productRepository
            , IPostRepository postRepository)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
            this.productRepository = productRepository;
            this.postRepository = postRepository;
        }
        public async Task<IEnumerable<GetPostResponse>> SearchFilter(SearchRequest request)
        {
            //var listPostByProductName=  productRepository.GetProductByName(request.Keyword);
            // var listPostByPostName= postRepository.SortPostByName(request.Keyword);
            var listPost = dbContext.Posts.AsQueryable();
            //var listProduct = dbContext.Products.AsQueryable();
            listPost = listPost.Where(p => p.Status == 1);
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                listPost = listPost.Where(p => p.Title.Contains(request.Keyword));
            }
            if (request.Page.HasValue)
            {

            }
            if (!string.IsNullOrEmpty(request.SortBy))
            {

            }
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
            if (request.CateId.HasValue)
            {

            }
            //IEnumerable<GetPostWithProductResponse>  result = listPost.Select(
            //       hh =>
            //       {
            //           new GetPostWithProductResponse
            //           {
            //               //return _mapper.Map<GetPostWithProductResponse>(x);
            //               Id = hh.Id,
            //               Title = hh.Title,
            //               Description = hh.Description,
            //               ImageUrl = hh.ImageUrl,
            //               AccountId = hh.AccountId,
            //               Price = hh.Price,
            //               CreateAt = hh.CreateAt,
            //               LastUpdateAt = hh.LastUpdateAt,
            //               BuildingId = hh.BuildingId,
            //           };
            //       }
            //     );
            //IEnumerable<GetPostResponse> result = listPost.Select(hh =>
            //{
            //    return _mapper.Map<GetPostResponse>(hh);
            //        //Id = hh.Id,
            //        //Title = hh.Title,
            //        //Description = hh.Description,
            //        //ImageUrl = hh.ImageUrl,
            //        //AccountId = hh.AccountId,
            //        //Price = hh.Price,
            //        //CreateAt = hh.CreateAt,
            //        //LastUpdateAt = hh.LastUpdateAt,
            //        //BuildingId = hh.BuildingId,
            //    //Product = new GetProductResponse()
            //    //{
            //    //    Id = hh.Id,
            //    //    Name=listProduct.Where(p=>p.Name),
            //    //}

            //});
            return null;
        }
    }
}
