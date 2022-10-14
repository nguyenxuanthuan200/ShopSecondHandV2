using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopSecondHand.Data.RequestModels.PostRequest;
using ShopSecondHand.Data.ResponseModels.PostResponse;
using ShopSecondHand.Data.ResponseModels.ProductResponse;
using ShopSecondHand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopSecondHand.Repository.PostRepository
{
    public class PostRepository : IPostRepository
    {
        private readonly ShopSecondHandContext dbContext;
        private readonly IMapper _mapper;

        public PostRepository(ShopSecondHandContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<CreatePostResponse> CreatePost(CreatePostRequest request)
        {
            var id = Guid.NewGuid();
            Post post = new Post();
            {
                post.Id = id;
                post.Title = request.Title;
                post.Description = request.PostDescription;
                post.ImageUrl = request.ImageUrl;
                post.Status = 1;
                post.AccountId = request.AccountId;
                post.CreateAt = DateTime.Now;
                post.LastUpdateAt = null;
                post.Price = request.Price;
                post.BuildingId = request.BuildingId;
            };
            Product product = new Product();
            {
                product.Id = id;
                product.Name = request.ProductName;
                product.Description = request.ProductDescription;
                product.Price = request.Price;
                product.Quantity = request.Quantity;
                product.CategoryId = request.CategoryId;
            }
            dbContext.Posts.AddAsync(post);
            dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            var re = _mapper.Map<CreatePostResponse>(post);
            var mapProduct=_mapper.Map<GetProductResponse>(product);
            re.Product = mapProduct;
            //re.Product.Name = product.Name;
            //re.Product.Description = product.Description;
            //re.Product.Price = product.Price;
            //re.Product.Quantity = product.Quantity;
            //re.Product.CategoryId = product.CategoryId;

            return re;
        }

        public void Delete(Guid id)
        {
            var delete = dbContext.Posts
              .SingleOrDefault(p => p.Id == id);
            delete.Status = 0;
            dbContext.Posts.Update(delete);
            dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetPostWithProductResponse>> GetPost()
        {
            var post = await dbContext.Posts.Where(p=>p.Status==1).ToListAsync();
            if (post == null) return null;
            IEnumerable<GetPostWithProductResponse> result = (IEnumerable<GetPostWithProductResponse>)post.Select(
                 async x =>
               {
                   Product product = await dbContext.Products.SingleOrDefaultAsync(p => p.Id == x.Id);
                   var map = _mapper.Map<GetPostWithProductResponse>(x);
                   var mapProduct = _mapper.Map<GetProductResponse>(product);
                   map.Product = mapProduct;
                   //return new GetPostResponse()
                   //{
                   //    Id = x.Id,
                   //    Title = x.Title,
                   //    Description = x.Description,
                   //    ImageUrl = x.ImageUrl,
                   //    AccountId=x.AccountId,
                   //    CreateAt=x.CreateAt,
                   //    LastUpdateAt=x.LastUpdateAt,
                   //    BuildingId=x.BuildingId
                   //};

               }
                ).ToList();
            return result;
        }

        public async Task<IEnumerable<GetPostWithProductResponse>> GetPostByAccountId(Guid id)
        {
            var getByPostId = await dbContext.Posts
               .Where(p => p.AccountId == id).Where(p => p.Status == 1).ToListAsync();
            if (getByPostId == null)
                return null;

            IEnumerable<GetPostWithProductResponse> result = (IEnumerable<GetPostWithProductResponse>)getByPostId.Select(
                  x =>
                 {
                     Product product =  dbContext.Products.SingleOrDefault(p => p.Id == x.Id);
                     var map = _mapper.Map<GetPostWithProductResponse>(x);
                     var mapProduct = _mapper.Map<GetProductResponse>(product);
                     map.Product = mapProduct;
                 }
                ).ToList() as IEnumerable<GetPostWithProductResponse>;
            return result;
        }

        public async Task<GetPostWithProductResponse> GetPostById(Guid id)
        {
            var getById = await dbContext.Posts.Where(p => p.Status == 1)
                .SingleOrDefaultAsync(p => p.Id == id);

            var product = await dbContext.Products
                .SingleOrDefaultAsync(p => p.Id == id);
            if (getById != null)
            {
                // var Transaction = _mapper.Map<TransactionDTO>(transaction);
                //var re = new GetPostWithProductResponse()
                //{
                //    Id = getById.Id,
                //    PostId = getById.PostId,
                //    AccountId = getById.AccountId,
                //    Total = getById.Total,
                //    Product = product
                //};
                var map = _mapper.Map<GetPostWithProductResponse>(getById);
                var mapProduct = _mapper.Map<GetProductResponse>(product);
                map.Product = mapProduct;
                return map;
            }
            return null;
        }

        public async Task<UpdatePostResponse> UpdatePost(Guid id, UpdatePostRequest request)
        {
            var up = await dbContext.Posts.Where(p => p.Status == 1).SingleOrDefaultAsync(c => c.Id == id);
            var upProduct = await dbContext.Products.SingleOrDefaultAsync(c => c.Id == id);
            // if (id != request.Id) return null;
            if (up == null) return null;

            up.Title = request.Title;
            up.Description = request.PostDescription;
            up.ImageUrl = request.ImageUrl;
            up.Price = request.Price;
            up.BuildingId = request.BuildingId;
            dbContext.Posts.Update(up);

            upProduct.Name = request.ProductName;
            upProduct.Description = request.ProductDescription;
            upProduct.Price = request.Price;
            upProduct.Quantity = request.Quantity;
            upProduct.CategoryId = request.CategoryId;
            dbContext.Products.Update(upProduct);
            await dbContext.SaveChangesAsync();
            var mapProduct = _mapper.Map<UpdateProductResponse>(upProduct);
            var update = _mapper.Map<UpdatePostResponse>(up);
            update.product = mapProduct;
            return update;
        }
    }
}
