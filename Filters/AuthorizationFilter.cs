using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace StoreAppAPI.Filters
{
    public class JwtAuthorizationFilterAttribute : ActionFilterAttribute
    {
        private readonly string[] _roles;

        public JwtAuthorizationFilterAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string authHeader = context.HttpContext.Request.Headers["Authorization"];
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();
            try
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                var securityToken = jwtHandler.ReadToken(token) as JwtSecurityToken;
               // var userId = securityToken.Claims.First(claim => claim.Type == "sub").Value;
                var userRoles = securityToken.Claims.Where(claim => claim.Type == "role").Select(claim => claim.Value);
                //var role = userRoles.;
                if (_roles.Any() && !_roles.Intersect(userRoles).Any())
                {
                    context.Result = new ForbidResult();
                    return;
                }

                // add userId to the context for use in the action
                //context.HttpContext.Items["UserId"] = userId;
            }
            catch
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }

}
