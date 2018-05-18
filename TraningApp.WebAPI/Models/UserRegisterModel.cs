using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TraningApp.WebAPI.Models
{
    public class UserRegisterModel
    {
        //[Required]
        public string UserName { get; set; }

       // [Required]
        public string FullName { get; set; }

      //  [Required]
        public string Password { get; set; }

       // [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class UserRegisterModelValidator : AbstractValidator<UserRegisterModel>
    {
        public UserRegisterModelValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(user => user.ConfirmPassword).Equal(user => user.Password).WithMessage("Re Password must be equal to Password");
        }
    }
}
