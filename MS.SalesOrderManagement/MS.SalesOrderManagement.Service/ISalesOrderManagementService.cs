using MS.Common.Models;
using MS.SalesOrderManagement.Data.Transformations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MS.SalesOrderManagement.Service
{
    public interface ISalesOrderManagementService
    {
        Task<ResponseModel<ProductDataTransformation>> CreateProduct(ProductDataTransformation productDataTransformation);

        Task<ResponseModel<List<CustomerDataTransformation>>> CustomerInquiry(int accountId, string customerName, int currentPageNumber, int pageSize, string sortExpression, string sortDirection);

        Task<ResponseModel<CustomerDataTransformation>> CreateCustomer(CustomerDataTransformation customerDataTransformation);

        Task<ResponseModel<CustomerDataTransformation>> UpdateCustomer(CustomerDataTransformation customerDataTransformation);

        Task<ResponseModel<CustomerDataTransformation>> GetCustomerInformation(int accountId, int customerId);

        Task<ResponseModel<SalesOrderDataTransformation>> CreateSalesOrder(SalesOrderDataTransformation salesOrderDataTransformation);

        Task<ResponseModel<SalesOrderDataTransformation>> GetSalesOrder(int accountId, int salesOrderId);

        Task<ResponseModel<ProductDataTransformation>> GetProduct(int accountId, string productNumber);

        Task<ResponseModel<SalesOrderDetailDataTransformation>> CreateSalesOrderDetail(SalesOrderDetailDataTransformation salesOrderDetailDataTransformation);

        Task<ResponseModel<List<SalesOrderDataTransformation>>> SalesOrderInquiry(int accountId, string customerName, int currentPageNumber, int pageSize, string sortExpression, string sortDirection);

        Task<ResponseModel<SalesOrderDetailDataTransformation>> UpdateSalesOrderDetail(SalesOrderDetailDataTransformation salesOrderDetailDataTransformation);

        Task<ResponseModel<SalesOrderDetailDataTransformation>> DeleteSalesOrderDetail(SalesOrderDetailDataTransformation salesOrderDetailDataTransformation);

        Task<ResponseModel<SalesOrderDataTransformation>> SubmitSalesOrder(SalesOrderDataTransformation salesOrderDataTransformation);
    }
}