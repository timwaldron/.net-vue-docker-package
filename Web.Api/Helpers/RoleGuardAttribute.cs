using Web.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RoleGuardAttribute : Attribute, IAuthorizationFilter
{
    public Role Role { get; }

    public RoleGuardAttribute(Role role)
    {
        Role = role;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        try
        {
            var role = Convert.ToInt32((context.HttpContext.Items["Account"] as AccountDto)?.Role);
            var guardedRole = Convert.ToInt32(Role.ToString("d"));

            if (role < guardedRole)
            {
                throw new UnauthorizedAccessException();
            }
        }
        catch (Exception)
        {
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            return;
        }
    }
}