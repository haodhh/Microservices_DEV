using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MS.AccountManagement.Data.Transformations;
using MS.AccountManagement.Service;
using MS.Common.Models;
using MS.Common.Utilities;
using System;
using System.Threading.Tasks;

namespace MS.AccountManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("SiteCorsPolicy")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAccountManagementService _amService;

        public IConfiguration configuration { get; }

        /// <summary>
        /// Movies Controller
        /// </summary>
        public AuthorizationController(IAccountManagementService amService)
        {
            _amService = amService;
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="accountDataTransformation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] AccountDataTransformation accountDataTransformation)
        {
            ResponseModel<AccountDataTransformation> returnResponse = new ResponseModel<AccountDataTransformation>();
            try
            {
                returnResponse = await _amService.Register(accountDataTransformation);
                if (returnResponse.ReturnStatus == true)
                {
                    var userId = returnResponse.Entity.UserId;
                    var accountId = returnResponse.Entity.AccountId;
                    var firstName = returnResponse.Entity.FirstName;
                    var lastName = returnResponse.Entity.LastName;
                    var emailAddress = returnResponse.Entity.EmailAddress;
                    var companyName = returnResponse.Entity.CompanyName;

                    var tokenString = TokenManagement.CreateToken(userId, firstName, lastName, emailAddress, accountId, companyName);
                    returnResponse.Entity.IsAuthenicated = true;
                    returnResponse.Entity.Token = tokenString;
                    return Ok(returnResponse);
                }
                else
                {
                    return BadRequest(returnResponse);
                }
            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
                return BadRequest(returnResponse);
            }
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="accountDataTransformation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] AccountDataTransformation accountDataTransformation)
        {
            ResponseModel<AccountDataTransformation> returnResponse = new ResponseModel<AccountDataTransformation>();
            try
            {
                returnResponse = await _amService.Login(accountDataTransformation);
                if (returnResponse.ReturnStatus == true)
                {
                    var userId = returnResponse.Entity.UserId;
                    var accountId = returnResponse.Entity.AccountId;
                    var firstName = returnResponse.Entity.FirstName;
                    var lastName = returnResponse.Entity.LastName;
                    var emailAddress = returnResponse.Entity.EmailAddress;
                    var companyName = returnResponse.Entity.CompanyName;

                    var tokenString = TokenManagement.CreateToken(userId, firstName, lastName, emailAddress, accountId, companyName);
                    returnResponse.Entity.IsAuthenicated = true;
                    returnResponse.Entity.Token = tokenString;
                    return Ok(returnResponse);
                }
                else
                {
                    return BadRequest(returnResponse);
                }
            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
                return BadRequest(returnResponse);
            }
        }
    }
}