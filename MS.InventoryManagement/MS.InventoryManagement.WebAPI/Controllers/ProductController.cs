using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using MS.Common.Models;
using MS.InventoryManagement.Data.Transformations;
using MS.InventoryManagement.Service;
using MS.InventoryManagement.WebAPI.ActionFilters;
using MS.InventoryManagement.WebAPI.SignalRHub;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MS.InventoryManagement.WebAPI.Controllers
{
    [ServiceFilter(typeof(SecurityFilter))]
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("SiteCorsPolicy")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IInventoryManagementService _service;

        private IHubContext<MessageQueueHub> _messageQueueContext;

        public IConfiguration configuration { get; }

        /// <summary>
        /// Product Controller
        /// </summary>
        public ProductController(IInventoryManagementService service, IHubContext<MessageQueueHub> messageQueueContext)
        {
            _service = service;
            _messageQueueContext = messageQueueContext;
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="productDataTransformation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDataTransformation productDataTransformation)
        {
            SecurityModel securityModel = (SecurityModel)(HttpContext.Items["SecurityModel"]);

            int accountId = securityModel.AccountId;

            productDataTransformation.AccountId = accountId;

            ResponseModel<ProductDataTransformation> returnResponse = new ResponseModel<ProductDataTransformation>();
            try
            {
                returnResponse = await _service.CreateProduct(productDataTransformation);
                returnResponse.Token = securityModel.Token;
                if (returnResponse.ReturnStatus == false)
                {
                    return BadRequest(returnResponse);
                }

                await _messageQueueContext.Clients.All.SendAsync(MessageQueueEndpoints.InventoryQueue, string.Empty);

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
        /// Update Product
        /// </summary>
        /// <param name="productDataTransformation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDataTransformation productDataTransformation)
        {
            SecurityModel securityModel = (SecurityModel)(HttpContext.Items["SecurityModel"]);

            int accountId = securityModel.AccountId;

            productDataTransformation.AccountId = accountId;

            ResponseModel<ProductDataTransformation> returnResponse = new ResponseModel<ProductDataTransformation>();
            try
            {
                returnResponse = await _service.UpdateProduct(productDataTransformation);
                returnResponse.Token = securityModel.Token;
                if (returnResponse.ReturnStatus == false)
                {
                    return BadRequest(returnResponse);
                }

                await _messageQueueContext.Clients.All.SendAsync(MessageQueueEndpoints.InventoryQueue, string.Empty);

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
        /// <param name="productInquiryDataTransformation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductInquiry")]
        public async Task<IActionResult> ProductInquiry([FromBody] ProductInquiryDataTransformation productInquiryDataTransformation)
        {
            SecurityModel securityModel = (SecurityModel)(HttpContext.Items["SecurityModel"]);

            int accountId = securityModel.AccountId;
            string productNumber = productInquiryDataTransformation.ProductNumber;
            int pageSize = productInquiryDataTransformation.PageSize;
            int currentPageNumber = productInquiryDataTransformation.CurrentPageNumber;
            string sortDirection = productInquiryDataTransformation.SortDirection;
            string sortExpression = productInquiryDataTransformation.SortExpression;

            ResponseModel<List<ProductDataTransformation>> returnResponse = new ResponseModel<List<ProductDataTransformation>>();

            try
            {
                returnResponse = await _service.ProductInquiry(accountId, productNumber, currentPageNumber, pageSize, sortExpression, sortDirection);
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
            int productId = productDataTransformation.ProductId;

            ResponseModel<ProductDataTransformation> returnResponse = new ResponseModel<ProductDataTransformation>();

            try
            {
                returnResponse = await _service.GetProduct(accountId, productId);
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
        /// Upload Product Master
        /// </summary>
        /// <returns></returns>
        [HttpPost, DisableRequestSizeLimit]
        [Route("UploadProductMasterFile")]
        public async Task<IActionResult> UploadProductMasterFile()
        {
            ResponseModel<List<ProductDataTransformation>> returnResponse = new ResponseModel<List<ProductDataTransformation>>();

            try
            {
                SecurityModel securityModel = (SecurityModel)(HttpContext.Items["SecurityModel"]);

                int accountId = securityModel.AccountId;

                var file = Request.Form.Files[0];

                List<ProductDataTransformation> products = new List<ProductDataTransformation>();

                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    var fileContent = reader.ReadToEnd();
                    string[] rows = fileContent.Split(Environment.NewLine.ToCharArray());
                    int counter = 0;
                    foreach (string row in rows)
                    {
                        if (counter > 0)
                        {
                            string[] columns = row.Split('\t');
                            if (columns.Length == 4)
                            {
                                ProductDataTransformation product = new ProductDataTransformation();
                                product.ProductNumber = Convert.ToString(columns[0]);
                                product.Description = Convert.ToString(columns[1]);
                                product.BinLocation = Convert.ToString(columns[2]);
                                product.UnitPrice = Convert.ToDouble(columns[3]);
                                products.Add(product);
                            }
                        }
                        counter++;
                    }
                }

                returnResponse = await _service.UploadProducts(accountId, products);
                returnResponse.Token = securityModel.Token;
                if (returnResponse.ReturnStatus == false)
                {
                    return BadRequest(returnResponse);
                }

                await _messageQueueContext.Clients.All.SendAsync(MessageQueueEndpoints.InventoryQueue, string.Empty);

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