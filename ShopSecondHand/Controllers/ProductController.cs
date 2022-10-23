using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSecondHand.Data.RequestModels.ProductRequest;
using ShopSecondHand.Data.ResponseModels.ProductResponse;
using ShopSecondHand.Repository.ProductRepository;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ShopSecondHand.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductRepository productRepository;
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            try
            {
                var result = await productRepository.GetProduct();
                if (result == null)
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                return CustomResult("Success", result, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                var result = await productRepository.GetProductById(id);
                if (result == null)
                {
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                }
                return CustomResult("Success", result, System.Net.HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);

            }
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            try
            {
                var result = await productRepository.GetProductByName(name);
                if (result == null)
                {
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                }
                return CustomResult("Success", result, System.Net.HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);

            }
        }
        [HttpGet("categories")]
        public async Task<IActionResult> GetProductByCategoryId(Guid id)
        {
            try
            {
                var result = await productRepository.GetProductByCategoryId(id);
                if (result == null)
                {
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                }
                return CustomResult("Success", result, System.Net.HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);

            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductRequest request)
        {
            try
            {
                if (request == null)
                {
                    return CustomResult("Cu Phap Sai", HttpStatusCode.BadRequest);
                }
                var create = await productRepository.CreateProduct(request);
                if (create == null)
                {
                    return CustomResult("Order da ton tai", HttpStatusCode.Accepted);
                }
                return CustomResult("Success", create, HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductRequest request)
        {
            try
            {
                if (request == null)
                {
                    return CustomResult("Cu Phap Sai", HttpStatusCode.BadRequest);
                }
                var update = await productRepository.UpdateProduct(id, request);


                if (update == null)
                {
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                }

                return CustomResult("Success", update, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);
            }

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            try
            {
                var delete = productRepository.GetProductById(id);


                if (delete == null)
                {
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                }
                productRepository.DeleteProduct(id);
                return CustomResult("Success", HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);
            }
        }
    }
}
