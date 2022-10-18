using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using ShopSecondHand.Data.RequestModels.PostRequest;
using ShopSecondHand.Repository.PostRepository;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ShopSecondHand.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : BaseController
    {
        private readonly IPostRepository postRepository;
        public PostController(IPostRepository postRepository)
        {
            {
                this.postRepository = postRepository;
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            try
            {
                var result = await postRepository.GetPostById(id);
                if (result == null)
                {
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                }
                return CustomResult("Success", result, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);

            }
        }
        [HttpGet("catecorys")]
        public async Task<IActionResult> GetPostByCategoryId(Guid id)
        {
            try
            {
                var result = await postRepository.GetPostByCategoryId(id);
                if (result == null)
                {
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                }
                return CustomResult("Success", result, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);

            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPost()
        {
            try
            {
                var result = await postRepository.GetPost();
                if (result == null)
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                return CustomResult("Success", result, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdatePostRequest request)
        {
            try
            {
                if (request == null)
                {
                    return CustomResult("Cu Phap Sai", HttpStatusCode.BadRequest);
                }

                var update = await postRepository.UpdatePost(id, request);

                if (update == null)
                {
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                }
                //var result = _mapper.Map<CreateAccountResponse>(update);
                return CustomResult("Success", update, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var delete = postRepository.GetPostById(id);
                if (delete == null)
                {
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                }
                postRepository.Delete(id);
                return CustomResult("Success", HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("accounts")]
        public async Task<IActionResult> GetPostByAccountId(Guid id)
        {
            try
            {
                var result = await postRepository.GetPostByAccountId(id);
                if (result == null)
                {
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                }

                return CustomResult("Success", result, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);

            }

        }
        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostRequest request)
        {
            try
            {
                if (request == null)
                {
                    return CustomResult("Cu Phap Sai", HttpStatusCode.BadRequest);
                }
                var create = await postRepository.CreatePost(request);
                if (create == null)
                {
                    return CustomResult("Post da ton tai", HttpStatusCode.Accepted);
                }
                //var result = _mapper.Map<CreateAccountResponse>(create);
                return CustomResult("Success", create, HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);


            }
        }

    }
}
