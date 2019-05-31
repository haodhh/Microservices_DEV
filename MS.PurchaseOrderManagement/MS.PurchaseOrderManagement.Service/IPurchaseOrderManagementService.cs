
using MS.Common.Models;
using MS.PurchaseOrderManagement.Data.Transformations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MS.PurchaseOrderManagement.Service
{
    public interface IPurchaseOrderManagementService
    {
        Task<ResponseModel<SupplierDataTransformation>> CreateSupplier(SupplierDataTransformation supplierDataTransformation);

        Task<ResponseModel<SupplierDataTransformation>> UpdateSupplier(SupplierDataTransformation supplierDataTransformation);

        Task<ResponseModel<SupplierDataTransformation>> GetSupplierInformation(int accountId, int supplierId);

        Task<ResponseModel<PurchaseOrderDataTransformation>> CreatePurchaseOrder(PurchaseOrderDataTransformation purchaseOrderDataTransformation);

        Task<ResponseModel<List<SupplierDataTransformation>>> SupplierInquiry(int accountId, string supplierName, int currentPageNumber, int pageSize, string sortExpression, string sortDirection);

        Task<ResponseModel<PurchaseOrderDataTransformation>> GetPurchaseOrder(int accountId, int purchaseOrderId);

        Task<ResponseModel<ProductDataTransformation>> GetProduct(int accountId, string productNumber);

        Task<ResponseModel<PurchaseOrderDetailDataTransformation>> CreatePurchaseOrderDetail(PurchaseOrderDetailDataTransformation purchaseOrderDetailDataTransformation);

        Task<ResponseModel<List<PurchaseOrderDataTransformation>>> PurchaseOrderInquiry(int accountId, string supplierName, int currentPageNumber, int pageSize, string sortExpression, string sortDirection);

        Task<ResponseModel<PurchaseOrderDetailDataTransformation>> UpdatePurchaseOrderDetail(PurchaseOrderDetailDataTransformation purchaseOrderDetailDataTransformation);

        Task<ResponseModel<PurchaseOrderDetailDataTransformation>> DeletePurchaseOrderDetail(PurchaseOrderDetailDataTransformation purchaseOrderDetailDataTransformation);

        Task<ResponseModel<PurchaseOrderDataTransformation>> SubmitPurchaseOrder(PurchaseOrderDataTransformation purchaseOrderDataTransformation);
    }
}