using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraningApp.WebAPI.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime CreateDate { get; set; }
        public ICollection<Post> Tasks { get; set; }
        public ICollection<Comment> TaskComment { get; set; }
    }
}
