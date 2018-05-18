using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraningApp.WebAPI.Data.Infrastructure;
using TraningApp.WebAPI.Data.Models;

namespace TraningApp.WebAPI.Data.Repository
{
    public interface IPostRepository : IRepository<Post>
    {
        IPaginatedList<Post> GetPosts(int page, int pageSize);
    }
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(ApplicationDbContext context) : base(context) { }
        public IPaginatedList<Post> GetPosts(int page, int pageSize)
        {
            var query = dbset.Include(post => post.User).OrderByDescending(p => p.CreateDate);
            var taskResult = PaginatedList<Post>.CreateAsync(query, page, pageSize);
            taskResult.Wait();
            return taskResult.Result;
        }
    }
}
