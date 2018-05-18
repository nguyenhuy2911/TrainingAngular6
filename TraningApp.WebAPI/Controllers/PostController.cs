using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TraningApp.WebAPI.Data.Infrastructure;
using TraningApp.WebAPI.Data.Models;
using TraningApp.WebAPI.Data.Repository;
using TraningApp.WebAPI.Models;
using TraningApp.WebAPI.Extention;
using Newtonsoft.Json;
namespace TraningApp.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IPostRepository _postRepository;
        public PostController(IUnitOfWork unitOfWork, IPostRepository postRepository)
        {
            _unitOfWork = unitOfWork;
            _postRepository = postRepository;
        }

        [HttpGet]
        public IActionResult GetPosts()
        {
            var datas = _postRepository.GetPosts(1, 100);
            var response = datas.MapToViewModel();
            return Ok(JsonConvert.SerializeObject(response));

        }

        [HttpGet("{id}")]
        public IActionResult GetPost(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPost([FromBody]PostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ModelState.Values.SelectMany(x => x.Errors);
                string errmess = string.Join("<br/>", errorList.Select(x => x.ErrorMessage));
                return BadRequest(new Response<object> { IsSuccess = false, Message = errmess });
            }
            var userId = User.Claims.FirstOrDefault(p => p.Type == JwtRegisteredClaimNames.Sid)?.Value;
            var saveData = new Post
            {
                Title = model.Title,
                Description = model.Description,
                CreateDate = DateTime.Now,
                UserId = userId

            };
            _postRepository.Add(saveData);
            if (_unitOfWork.Commit() <= 0)
                return BadRequest(new Response<object> { IsSuccess = false, Message = "Cannot create a post" });
            else
                return Ok(new Response<int> { IsSuccess = true, Message = "Create post success", Result = saveData.Id });

        }

        [HttpPut("{id}")]
        public void EditPost(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}