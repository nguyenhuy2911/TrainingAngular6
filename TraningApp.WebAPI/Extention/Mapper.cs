using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraningApp.WebAPI.Data.Infrastructure;
using TraningApp.WebAPI.Data.Models;
using TraningApp.WebAPI.Models;

namespace TraningApp.WebAPI.Extention
{
    public static class Mapper
    {
        public static IPaginatedList<PostViewModel> MapToViewModel(this IPaginatedList<Post> source)
        {
            var destination = new List<PostViewModel>();
            if (source != null && source.Count > 0)
            {
                foreach (var item in source)
                {
                    var post = new PostViewModel
                    {
                        Id = item.Id,
                        Title = item.Title,
                        CreateDate = item.CreateDate,
                        Description = item.Description,
                        Status = item.Status,
                        UserId = item.UserId
                    };
                    if (item.User != null)
                    {
                        post.User = new UserInfo
                        {
                            FullName = item.User.FullName,
                            UserName = item.User.UserName
                        };
                    }
                    destination.Add(post);
                }
            }
            return new PaginatedList<PostViewModel>(destination, source?.Count ?? 0, source?.PageIndex ?? 0, source?.TotalPages ?? 0);
        }
    }
}
