using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraningApp.WebAPI.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public DateTime? CreateDate { get; set; }
        public int Status { get; set; }
        public UserInfo User { get; set; }
    }
    public class PostViewModelValidator :  AbstractValidator<PostViewModel>
    {
        public PostViewModelValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
