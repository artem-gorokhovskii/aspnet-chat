using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace chat.Policy
{
    public class RolePolicyHandler : AuthorizationHandler<RolePolicy>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RolePolicy requirement)
        {
            // For User we need to check, that we only have a token
            if (context.User.HasClaim(c => c.Type == "userId"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
