using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PagedList;
using ShopSecondHand.Data.RequestModels.PostRequest;
using ShopSecondHand.Data.ResponseModels.BuildingResponse;
using ShopSecondHand.Data.ResponseModels.PostResponse;
using ShopSecondHand.Data.ResponseModels.ProductResponse;
using ShopSecondHand.Models;
using ShopSecondHand.Repository.BuildingRepository;
using ShopSecondHand.Repository.CategoryRepository;
using ShopSecondHand.Repository.ProductRepository;
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
        private readonly IProductRepository productRepository;
        private readonly IBuildingRepository buildingRepository;
        private readonly ICategoryRepository categoryRepository;

        public PostRepository(ShopSecondHandContext dbContext, IMapper mapper, IProductRepository productRepository,
            IBuildingRepository buildingRepository, ICategoryRepository categoryRepository)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
            this.productRepository = productRepository;
            this.buildingRepository = buildingRepository;
            this.categoryRepository = categoryRepository;
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
            await dbContext.Posts.AddAsync(post);
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            var re = _mapper.Map<CreatePostResponse>(post);
            var mapProduct = _mapper.Map<GetProductResponse>(product);
            re.Product = mapProduct;

            return re;
        }

        public async void Delete(Guid id)
        {
            var delete = await dbContext.Posts
              .SingleOrDefaultAsync(p => p.Id == id);
            delete.Status = 0;
            dbContext.Posts.Update(delete);
            await dbContext.SaveChangesAsync();
        }

        public async Task<PostTotalResponse> GetPost(int? page, int? pageSize)
        {
            var post = await dbContext.Posts.Where(p => p.Status == 1).ToListAsync();
            if (post == null) return null;
            IEnumerable<GetPostWithProductResponse> result = post.Select(
                   x =>
               {
                   return _mapper.Map<GetPostWithProductResponse>(x);

               }
                ).ToList();
            foreach (var temp in result)
            {
                var product = await productRepository.GetProductById(temp.Id);
                var mapProduct = _mapper.Map<GetProductResponse>(product);
                temp.Product = mapProduct;
                //var category = await categoryRepository.GetCategoryById((Guid)product.CategoryId);
                //temp.ca
                var building = await buildingRepository.GetBuildingById(temp.BuildingId);
                temp.Building = building;
            }
            PostTotalResponse total = new PostTotalResponse();
            if (page == null) page = 1;
            if (pageSize == null) pageSize = 10;
            total.Total = result.Count();
            result = result.ToPagedList((int)page, (int)pageSize);
            
            total.Post = result;
            return total;
        }

        public async Task<IEnumerable<GetPostWithProductResponse>> GetPostByAccountId(Guid id)
        {
            var getByPostId = await dbContext.Posts
               .Where(p => p.AccountId == id && p.Status == 1).ToListAsync();
            if (getByPostId == null)
                return null;

            IEnumerable<GetPostWithProductResponse> result = getByPostId.Select(
                  x =>
                  {
                      return _mapper.Map<GetPostWithProductResponse>(x);

                  }
                ).ToList();
            foreach (var temp in result)
            {
                var product = await productRepository.GetProductById(temp.Id);
                var mapProduct = _mapper.Map<GetProductResponse>(product);
                temp.Product = mapProduct;
                var building = await buildingRepository.GetBuildingById(temp.BuildingId);
                temp.Building = building;
            }

            return result;
        }

        public async Task<IEnumerable<GetPostWithProductResponse>> GetPostByCategoryId(Guid id)
        {
            var listProduct = await dbContext.Products
                 .Where(p => p.CategoryId == id).ToListAsync();

            //IEnumerable<GetPostWithProductResponse> result = listProduct.Select(
            //    x =>
            //    {
            //        var a= GetPostById(x.Id);
            //    }
            //    ).ToList();
            List<GetPostWithProductResponse> list = new List<GetPostWithProductResponse>();
            foreach (var x in listProduct)
            {
                var post = await GetPostById(x.Id);
                if (post != null)
                {
                    list.Add(post);
                }
            }
            return list;
        }

        public async Task<GetPostWithProductResponse> GetPostById(Guid id)
        {
            var getById = await dbContext.Posts.Where(p => p.Id == id && p.Status == 1)
                .SingleOrDefaultAsync();
            var product = await dbContext.Products
                .SingleOrDefaultAsync(p => p.Id == id);
            if (getById != null)
            {
                var map = _mapper.Map<GetPostWithProductResponse>(getById);
                var mapProduct = _mapper.Map<GetProductResponse>(product);
                map.Product = mapProduct;
                //var category = await ca
                var building = await buildingRepository.GetBuildingById((Guid)getById.BuildingId);
                map.Building = building;
                return map;
            }
            return null;
        }

        public async Task<IEnumerable<GetPostWithProductResponse>> SortPostByName(string name)
        {
            var get = new List<Post>();
            if (!string.IsNullOrEmpty(name))
            {
                get = await dbContext.Posts.Where(p => p.Title.Contains(name))
                    .ToListAsync();
            }
            else get = await dbContext.Posts.ToListAsync();
            if (get == null)
                return null;


            IEnumerable<GetPostWithProductResponse> result = get.Select(
                 x =>
                 {
                     return _mapper.Map<GetPostWithProductResponse>(x);

                 }
               ).ToList();

            foreach (var temp in result.ToList())
            {
                var product = await productRepository.GetProductById(temp.Id);
                if (product.Name.Contains(name))
                {
                    temp.Product = null;
                }
                else
                {
                    var mapProduct = _mapper.Map<GetProductResponse>(product);
                    temp.Product = mapProduct;
                }
            }
            return result;
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
            up.LastUpdateAt = DateTime.Now;
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
