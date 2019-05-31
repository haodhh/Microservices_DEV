
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
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IPurchaseOrderManagementService _service;

        private IHubContext<MessageQueueHub> _messageQueueContext;

        public IConfiguration configuration { get; }

        /// <summary>
        /// Purchase Controller
        /// </summary>
        public PurchaseOrderController(IPurchaseOrderManagementService service, IHubContext<MessageQueueHub> messageQueueContext)
        {
            _service = service;
            _messageQueueContext = messageQueueContext;
        }

        /// <summary>
        /// Create Purchase Order
        /// </summary>
        /// <param name="purchaseOrderDataTransformation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreatePurchaseOrder")]
        public async Task<IActionResult> CreatePurchaseOrder([FromBody] PurchaseOrderDataTransformation purchaseOrderDataTransformation)
        {
            SecurityModel securityModel = (SecurityModel)(HttpContext.Items["SecurityModel"]);

            int accountId = securityModel.AccountId;

            purchaseOrderDataTransformation.AccountId = accountId;

            ResponseModel<PurchaseOrderDataTransformation> returnResponse = new ResponseModel<PurchaseOrderDataTransformation>();

            try
            {
                returnResponse = await _service.CreatePurchaseOrder(purchaseOrderDataTransformation);
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
        /// Create Purchase Order Detail
        /// </summary>
        /// <param name="purchaseOrderDetailDataTransformation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreatePurchaseOrderDetail")]
        public async Task<IActionResult> CreatePurchaseOrderDetail([FromBody] PurchaseOrderDetailDataTransformation purchaseOrderDetailDataTransformation)
        {
            SecurityModel securityModel = (SecurityModel)(HttpContext.Items["SecurityModel"]);

            int accountId = securityModel.AccountId;
            int purchaseOrderId = purchaseOrderDetailDataTransformation.PurchaseOrderId;

            purchaseOrderDetailDataTransformation.AccountId = accountId;

            ResponseModel<PurchaseOrderDetailDataTransformation> returnResponse = new ResponseModel<PurchaseOrderDetailDataTransformation>();

            try
            {
                returnResponse = await _service.CreatePurchaseOrderDetail(purchaseOrderDetailDataTransformation);
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
        /// Update Purchase Order Detail
        /// </summary>
        /// <param name="purchaseOrderDetailDataTransformation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdatePurchaseOrderDetail")]
        public async Task<IActionResult> UpdatePurchaseOrderDetail([FromBody] PurchaseOrderDetailDataTransformation purchaseOrderDetailDataTransformation)
        {
            SecurityModel securityModel = (SecurityModel)(HttpContext.Items["SecurityModel"]);

            int accountId = securityModel.AccountId;
            purchaseOrderDetailDataTransformation.AccountId = accountId;

            ResponseModel<PurchaseOrderDetailDataTransformation> returnResponse = new ResponseModel<PurchaseOrderDetailDataTransformation>();

            try
            {
                returnResponse = await _service.UpdatePurchaseOrderDetail(purchaseOrderDetailDataTransformation);
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
        /// Delete Purchase Order Detail
        /// </summary>
        /// <param name="purchaseOrderDetailDataTransformation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeletePurchaseOrderDetail")]
        public async Task<IActionResult> DeletePurchaseOrderDetail([FromBody] PurchaseOrderDetailDataTransformation purchaseOrderDetailDataTransformation)
        {
            SecurityModel securityModel = (SecurityModel)(HttpContext.Items["SecurityModel"]);

            int accountId = securityModel.AccountId;
            purchaseOrderDetailDataTransformation.AccountId = accountId;

            ResponseModel<PurchaseOrderDetailDataTransformation> returnResponse = new ResponseModel<PurchaseOrderDetailDataTransformation>();

            try
            {
                returnResponse = await _service.DeletePurchaseOrderDetail(purchaseOrderDetailDataTransformation);
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
        /// Get Purchase Order
        /// </summary>
        /// <param name="purchaseOrderDataTransformation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPurchaseOrder")]
        public async Task<IActionResult> GetPurchaseOrder([FromBody] PurchaseOrderDataTransformation purchaseOrderDataTransformation)
        {
            SecurityModel securityModel = (SecurityModel)(HttpContext.Items["SecurityModel"]);

            int accountId = securityModel.AccountId;
            int purchaseOrderId = purchaseOrderDataTransformation.PurchaseOrderId;

            ResponseModel<PurchaseOrderDataTransformation> returnResponse = new ResponseModel<PurchaseOrderDataTransformation>();

            try
            {
                returnResponse = await _service.GetPurchaseOrder(accountId, purchaseOrderId);
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
        /// Get Product
        /// </summary>
        /// <param name="productDataTransformation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetProduct")]
        public async Task<IActionResult> GetProduct([FromBody] ProductDataTransformation productDataTransformation)
        {
            SecurityModel securityModel = (SecurityModel)(HttpContext.Items["SecurityModel"]);

            int accountId = securityModel.AccountId;
            string productNumber = productDataTransformation.ProductNumber;

            ResponseModel<ProductDataTransformation> returnResponse = new ResponseModel<ProductDataTransformation>();

            try
            {
                returnResponse = await _service.GetProduct(accountId, productNumber);
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
        /// Purchase Order Inquiry
        /// </summary>
        /// <param name="purchaseOrderInquiryDataTransformation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PurchaseOrderInquiry")]
        public async Task<IActionResult> PurchaseOrderInquiry([FromBody] PurchaseOrderInquiryDataTransformation purchaseOrderInquiryDataTransformation)
        {
            SecurityModel securityModel = (SecurityModel)(HttpContext.Items["SecurityModel"]);

            int accountId = securityModel.AccountId;
            string supplierName = purchaseOrderInquiryDataTransformation.SupplierName;
            int pageSize = purchaseOrderInquiryDataTransformation.PageSize;
            int currentPageNumber = purchaseOrderInquiryDataTransformation.CurrentPageNumber;
            string sortDirection = purchaseOrderInquiryDataTransformation.SortDirection;
            string sortExpression = purchaseOrderInquiryDataTransformation.SortExpression;

            ResponseModel<List<PurchaseOrderDataTransformation>> returnResponse = new ResponseModel<List<PurchaseOrderDataTransformation>>();

            try
            {
                returnResponse = await _service.PurchaseOrderInquiry(accountId, supplierName, currentPageNumber, pageSize, sortExpression, sortDirection);
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
        /// Submit Purchase Order
        /// </summary>
        /// <param name="purchaseOrderDataTransformation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SubmitPurchaseOrder")]
        public async Task<IActionResult> SubmitPurchaseOrder([FromBody] PurchaseOrderDataTransformation purchaseOrderDataTransformation)
        {
            SecurityModel securityModel = (SecurityModel)(HttpContext.Items["SecurityModel"]);

            int accountId = securityModel.AccountId;

            purchaseOrderDataTransformation.AccountId = accountId;

            ResponseModel<PurchaseOrderDataTransformation> returnResponse = new ResponseModel<PurchaseOrderDataTransformation>();

            try
            {
                returnResponse = await _service.SubmitPurchaseOrder(purchaseOrderDataTransformation);
                returnResponse.Token = securityModel.Token;
                if (returnResponse.ReturnStatus == false)
                {
                    return BadRequest(returnResponse);
                }

                await _messageQueueContext.Clients.All.SendAsync(MessageQueueEndpoints.PurchaseOrderQueue, string.Empty);

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