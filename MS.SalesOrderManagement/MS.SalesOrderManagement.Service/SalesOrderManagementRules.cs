using MS.Common.Models;
using MS.Common.Utilities;
using MS.SalesOrderManagement.Data;
using MS.SalesOrderManagement.Data.Models;
using System.Threading.Tasks;

namespace MS.SalesOrderManagement.Service
{
    public class SalesOrderDetailRules<T> : ValidationRules<T>
    {
        public T _entity;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userDataService"></param>
        public SalesOrderDetailRules(T entity) : base(entity)
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

            return ValidationResult;
        }
    }

    public class CustomerRules<T> : ValidationRules<T>
    {
        public T _entity;

        private ISalesOrderManagementRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userDataService"></param>
        public CustomerRules(T entity, ISalesOrderManagementRepository repository) : base(entity)
        {
            _repository = repository;
            _entity = entity;
        }

        public async Task<ValidationResult> Validate()
        {
            ValidateRequired("CustomerName", "Customer Name");
            ValidateRequired("AddressLine1", "Address Line 1");
            ValidateRequired("City", "City");
            ValidateRequired("Region", "State/Region");
            ValidateRequired("PostalCode", "Postal Code");

            await ValidateUniqueCustomerName("CustomerId", "CustomerName", "AccountId");

            return ValidationResult;
        }

        /// <summary>
        /// Validate Unique Customer Name
        /// </summary>
        /// <param name="emailAddress"></param>
        private async Task ValidateUniqueCustomerName(string customerId, string customerName, string accountId)
        {
            object valueOfCustomerName = GetPropertyValue(customerName);
            object valueOfAccountId = GetPropertyValue(accountId);
            object valueOfCustomerId = GetPropertyValue(customerId);

            Customer customer = await _repository.GetCustomerInformationByCustomerName(valueOfCustomerName.ToString(), (int)valueOfAccountId);

            if (customer != null && (int)valueOfCustomerId == 0)
            {
                AddValidationError(customerName, "Customer Name already exists.");
                return;
            }

            if (customer != null && customer.CustomerId != (int)valueOfCustomerId)
            {
                AddValidationError(customerName, "Customer Name already exists.");
            }
        }
    }
}