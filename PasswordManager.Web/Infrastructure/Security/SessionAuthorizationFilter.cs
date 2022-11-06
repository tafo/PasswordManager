using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PasswordManager.Web.Infrastructure.Security;

public class SessionAuthorizationFilterAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var email = context.HttpContext.Session.GetString("email");
        if (string.IsNullOrEmpty(email))
        {
            context.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "controller", "Account" },
                    { "action", "login" },
                    { "ReturnUrl", Microsoft.AspNetCore.Http.Extensions.UriHelper.GetEncodedUrl(context.HttpContext.Request) }
                }
            );
        }
    }
}