using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraningApp.WebAPI.Fillter
{
    public class AdminOnly : IAuthorizationRequirement
    {
    }
    public class AdminAuthorizationHandler : AuthorizationHandler<AdminOnly>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminOnly requirement)
        {
            var roles = context.User.Claims.Where(p => p.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").ToList();
            if (roles != null && roles.Any(p => p.Value == "Admin")){
                context.Succeed(requirement);
            }
            return Task.FromResult(0);
        }
    }
}
