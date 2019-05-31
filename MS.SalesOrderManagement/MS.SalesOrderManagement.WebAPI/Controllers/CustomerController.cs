using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using MS.Common.Models;
using MS.SalesOrderManagement.Data.Transformations;
using MS.SalesOrderManagement.Service;
using MS.SalesOrderManagement.WebAPI.ActionFilters;
using MS.SalesOrderManagement.WebAPI.SignalRHub;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MS.SalesOrderManagement.WebAPI.Controllers
{
    [ServiceFilter(typeof(SecurityFilter))]
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("SiteCorsPolicy")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ISalesOrderManagementService _service;

        private IHubContext<MessageQueueHub> _messageQueueContext;

        public IConfiguration configuration { get; }

        /// <summary>
        /// Customer Controller
        /// </summary>
        public CustomerController(ISalesOrderManagementService service, IHubContext<MessageQueueHub> messageQueueContext)
        {
            _service = service;
            _messageQueueContext = messageQueueContext;
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="customerDataTransformation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDataTransformation customerDataTransformation)
        {
            SecurityModel securityModel = (SecurityModel)(HttpContext.Items["SecurityModel"]);

            int accountId = securityModel.AccountId;

            customerDataTransformation.AccountId = accountId;

            ResponseModel<CustomerDataTransformation> returnResponse = new ResponseModel<CustomerDataTransformation>();
            try
            {
                returnResponse = await _service.CreateCustomer(customerDataTransformation);
                returnResponse.Token = securityModel.Token;
                if (returnResponse.ReturnStatus == false)
                {
                    return BadRequest(returnResponse);
                }

                return Ok(returnResponse);
            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
                return BadRequest(returnResponse);
            }
        }

        /// <summary>
        /// Update Customer
        /// </summary>
        /// <param name="customerDataTransformation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDataTransformation customerDataTransformation)
        {
            SecurityModel securityModel = (SecurityModel)(HttpContext.Items["SecurityModel"]);

            int accountId = securityModel.AccountId;

            customerDataTransformation.AccountId = accountId;

            ResponseModel<CustomerDataTransformation> returnResponse = new ResponseModel<CustomerDataTransformation>();
            try
            {
                returnResponse = await _service.UpdateCustomer(customerDataTransformation);
                returnResponse.Token = securityModel.Token;
                if (returnResponse.ReturnStatus == false)
                {
                    return BadRequest(returnResponse);
                }

                return Ok(returnResponse);
            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
                return BadRequest(returnResponse);
            }
        }

        /// <summary>
        /// Customer Inquiry
        /// </summary>
        /// <param name="customerInquiryDataTransformation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CustomerInquiry")]
        public async Task<IActionResult> CustomerInquiry([FromBody] CustomerInquiryDataTransformation customerInquiryDataTransformation)
        {
            SecurityModel securityModel = (SecurityModel)(HttpContext.Items["SecurityModel"]);

            int accountId = securityModel.AccountId;
            string customerName = customerInquiryDataTransformation.CustomerName;
            int pageSize = customerInquiryDataTransformation.PageSize;
            int currentPageNumber = customerInquiryDataTransformation.CurrentPageNumber;
            string sortDirection = customerInquiryDataTransformation.SortDirection;
            string sortExpression = customerInquiryDataTransformation.SortExpression;

            ResponseModel<List<CustomerDataTransformation>> returnResponse = new ResponseModel<List<CustomerDataTransformation>>();

            try
            {
                returnResponse = await _service.CustomerInquiry(accountId, customerName, currentPageNumber, pageSize, sortExpression, sortDirection);
                returnResponse.Token = securityModel.Token;
                if (returnResponse.ReturnStatus == false)
                {
                    return BadRequest(returnResponse);
                }

                return Ok(returnResponse);
            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
                return BadRequest(returnResponse);
            }
        }

        /// <summary>
        /// Get Customer
        /// </summary>
        /// <param name="customerDataTransformation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCustomer")]
        public async Task<IActionResult> GetCustomer([FromBody] CustomerDataTransformation customerDataTransformation)
        {
            SecurityModel securityModel = (SecurityModel)(HttpContext.Items["SecurityModel"]);

            int accountId = securityModel.AccountId;
            int customerId = customerDataTransformation.CustomerId;

            ResponseModel<CustomerDataTransformation> returnResponse = new ResponseModel<CustomerDataTransformation>();

            try
            {
                returnResponse = await _service.GetCustomerInformation(accountId, customerId);
                returnResponse.Token = securityModel.Token;
                if (returnResponse.ReturnStatus == false)
                {
                    return BadRequest(returnResponse);
                }

                return Ok(returnResponse);
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