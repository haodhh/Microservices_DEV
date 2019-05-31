using Microsoft.AspNetCore.Mvc.Filters;
using MS.Common.Models;
using MS.Common.Utilities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MS.InventoryManagement.WebAPI.ActionFilters
{
    public class SecurityFilter : IAsyncActionFilter
    {
        /// <summary>
        /// Action Filter
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var firstName = context.HttpContext.User.FindFirst(ClaimTypes.GivenName).Value;
            var lastName = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var emailAddress = context.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var companyName = context.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            var userId = int.Parse(context.HttpContext.User.FindFirst(ClaimTypes.PrimarySid).Value);
            var accountId = int.Parse(context.HttpContext.User.FindFirst(ClaimTypes.PrimaryGroupSid).Value);

            var token = TokenManagement.CreateToken(userId, firstName, lastName, emailAddress, accountId, companyName);

            var securityModel = new SecurityModel();
            securityModel.EmailAddress = emailAddress;
            securityModel.FirstName = firstName;
            securityModel.LastName = lastName;
            securityModel.UserId = userId;
            securityModel.AccountId = accountId;
            securityModel.Token = token;

            context.HttpContext.Items["SecurityModel"] = securityModel;

            var resultContext = await next();
        }
    }
}