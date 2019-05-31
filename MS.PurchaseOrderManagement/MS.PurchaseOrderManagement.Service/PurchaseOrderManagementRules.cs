using MS.Common.Models;
using MS.Common.Utilities;
using MS.PurchaseOrderManagement.Data;
using MS.PurchaseOrderManagement.Data.Models;
using System.Threading.Tasks;

namespace MS.PurchaseOrderManagement.Service
{
    public class PurchaseOrderDetailRules<T> : ValidationRules<T>
    {
        public T _entity;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userDataService"></param>
        public PurchaseOrderDetailRules(T entity) : base(entity)
        {
            _entity = entity;
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <returns></returns>
        public ValidationResult Validate()
        {
            ValidateGreaterThanZero("OrderQuantity", "Order Quantity");
            ValidateDecimalGreaterThanZero("UnitPrice", "Unit Price");

            return ValidationResult;
        }
    }

    public class SupplierRules<T> : ValidationRules<T>
    {
        public T _entity;

        private IPurchaseOrderManagementRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userDataService"></param>
        public SupplierRules(T entity, IPurchaseOrderManagementRepository repository) : base(entity)
        {
            _repository = repository;
            _entity = entity;
        }

        public async Task<ValidationResult> Validate()
        {
            ValidateRequired("SupplierName", "Supplier Name");
            ValidateRequired("AddressLine1", "Address Line 1");
            ValidateRequired("City", "City");
            ValidateRequired("Region", "State/Region");
            ValidateRequired("PostalCode", "Postal Code");

            await ValidateUniqueSupplierName("SupplierId", "SupplierName", "AccountId");

            return ValidationResult;
        }

        /// <summary>
        /// Validate Unique Email Address
        /// </summary>
        /// <param name="emailAddress"></param>
        private async Task ValidateUniqueSupplierName(string supplierId, string supplierName, string accountId)
        {
            object valueOfSupplierName = GetPropertyValue(supplierName);
            object valueOfAccountId = GetPropertyValue(accountId);
            object valueOfSupplierId = GetPropertyValue(supplierId);

            Supplier supplier = await _repository.GetSupplierInformationBySupplierName(valueOfSupplierName.ToString(), (int)valueOfAccountId);

            if (supplier != null && (int)valueOfSupplierId == 0)
            {
                AddValidationError(supplierName, "Supplier Name already exists.");
                return;
            }

            if (supplier != null && supplier.SupplierId != (int)valueOfSupplierId)
            {
                AddValidationError(supplierName, "Supplier Name already exists.");
            }
        }
    }
}