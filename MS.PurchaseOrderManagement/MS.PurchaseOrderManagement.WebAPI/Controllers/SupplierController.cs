
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using MS.Common.Models;
using MS.PurchaseOrderManagement.Data.Transformations;
using MS.PurchaseOrderManagement.Service;
using MS.PurchaseOrderManagement.WebAPI.ActionFilters;
using MS.PurchaseOrderManagement.WebAPI.SignalRHub;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MS.PurchaseOrderManagement.WebAPI.Controllers
{
    [ServiceFilter(typeof(SecurityFilter))]
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("SiteCorsPolicy")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IPurchaseOrderManagementService _service;

        private IHubContext<MessageQueueHub> _messageQueueContext;

        public IConfiguration configuration { get; }

        /// <summary>
        /// Supplier Controller
        /// </summary>
        public SupplierController(IPurchaseOrderManagementService service, IHubContext<MessageQueueHub> messageQueueContext)
        {
            _service = service;
            _messageQueueContext = messageQueueContext;
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="supplierDataTransformation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateSupplier")]
        public async Task<IActionResult> CreateSupplier([FromBody] SupplierDataTransformation supplierDataTransformation)
        {
            SecurityModel securityModel = (SecurityModel)(HttpContext.Items["SecurityModel"]);

            int accountId = securityModel.AccountId;

            supplierDataTransformation.AccountId = accountId;

            ResponseModel<SupplierDataTransformation> returnResponse = new ResponseModel<SupplierDataTransformation>();
            try
            {
                returnResponse = await _service.CreateSupplier(supplierDataTransformation);
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
        /// Update Supplier
        /// </summary>
        /// <param name="supplierDataTransformation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateSupplier")]
        public async Task<IActionResult> UpdateSupplier([FromBody] SupplierDataTransformation supplierDataTransformation)
        {
            SecurityModel securityModel = (SecurityModel)(HttpContext.Items["SecurityModel"]);

            int accountId = securityModel.AccountId;

            supplierDataTransformation.AccountId = accountId;

            ResponseModel<SupplierDataTransformation> returnResponse = new ResponseModel<SupplierDataTransformation>();
            try
            {
                returnResponse = await _service.UpdateSupplier(supplierDataTransformation);
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
        /// Supplier Inquiry
        /// </summary>
        /// <param name="supplierInquiryDataTransformation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SupplierInquiry")]
        public async Task<IActionResult> SupplierInquiry([FromBody] SupplierInquiryDataTransformation supplierInquiryDataTransformation)
        {
            SecurityModel securityModel = (SecurityModel)(HttpContext.Items["SecurityModel"]);

            int accountId = securityModel.AccountId;
            string supplierName = supplierInquiryDataTransformation.SupplierName;
            int pageSize = supplierInquiryDataTransformation.PageSize;
            int currentPageNumber = supplierInquiryDataTransformation.CurrentPageNumber;
            string sortDirection = supplierInquiryDataTransformation.SortDirection;
            string sortExpression = supplierInquiryDataTransformation.SortExpression;

            ResponseModel<List<SupplierDataTransformation>> returnResponse = new ResponseModel<List<SupplierDataTransformation>>();

            try
            {
                returnResponse = await _service.SupplierInquiry(accountId, supplierName, currentPageNumber, pageSize, sortExpression, sortDirection);
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
        /// Get Supplier
        /// </summary>
        /// <param name="supplierDataTransformation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSupplier")]
        public async Task<IActionResult> GetSupplier([FromBody] SupplierDataTransformation supplierDataTransformation)
        {
            SecurityModel securityModel = (SecurityModel)(HttpContext.Items["SecurityModel"]);

            int accountId = securityModel.AccountId;
            int supplierId = supplierDataTransformation.SupplierId;

            ResponseModel<SupplierDataTransformation> returnResponse = new ResponseModel<SupplierDataTransformation>();

            try
            {
                returnResponse = await _service.GetSupplierInformation(accountId, supplierId);
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