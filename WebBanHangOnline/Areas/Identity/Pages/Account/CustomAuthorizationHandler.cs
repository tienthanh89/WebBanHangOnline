using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebBanHangOnline.Models.Entity;

namespace WebBanHangOnline.Areas.Identity.Pages.Account
{
    //public class IsActiveRoleHandler : AuthorizationHandler<ApplicationRole>
    //{
    //    private readonly RoleManager<ApplicationRole> _roleManager;

    //    public IsActiveRoleHandler(RoleManager<ApplicationRole> roleManager)
    //    {
    //        _roleManager = roleManager;
    //    }

    //    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ApplicationRole requirement)
    //    {
    //        if (requirement.Name != null && !context.User.IsInRole(requirement.Name))
    //        {
    //            return;
    //        }

    //        if (requirement.Name != null)
    //        {
    //            var role = await _roleManager.FindByNameAsync(requirement.Name);
    //            if (role is { IsActive: true })
    //            {
    //                context.Succeed(requirement);
    //            }
    //        }
    //    }
    //}




}
