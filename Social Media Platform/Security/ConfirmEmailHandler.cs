using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Social_Media_Platform.Models;

namespace Social_Media_Platform.Security
{
    public class ConfirmEmailHandler: AuthorizationHandler<ConfirmEmailRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;

        public ConfirmEmailHandler(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ConfirmEmailRequirement requirement)
        {
            var user = await userManager.GetUserAsync(context.User);
            if(user != null && user.EmailConfirmed)
            {
                context.Succeed(requirement);
            }
            else
            {
                var httpContext = httpContextAccessor.HttpContext;
                context.Succeed(requirement);
                httpContext.Response.Redirect("/Account/EmailNotConfirmed");
            }
        }
    }
}
