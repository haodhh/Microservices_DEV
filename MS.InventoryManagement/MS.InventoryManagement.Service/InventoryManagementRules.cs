using MS.Common.Models;
using MS.Common.Utilities;
using MS.InventoryManagement.Data;
using MS.InventoryManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.InventoryManagement.Service
{
    /// <summary>
    /// InventoryManagementRules
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InventoryManagementRules<T> : ValidationRules<T>
    {
        public T _entity;

        private IInventoryManagementRepository _repository;

        /// <summary>
        /// InventoryManagementRules
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="repository"></param>
        public InventoryManagementRules(T entity, IInventoryManagementRepository repository) : base(entity)
        {
            _repository = repository;
            _entity = entity;
        }

        public async Task<ValidationResult> Validate()
        {
            ValidateRequired("ProductNumber", "Product Number");
            ValidateRequired("Description", "Description");
            ValidateRequired("UnitPrice", "Unit Price");

            await ValidateUniqueProductNumber("ProductId", "ProductNumber", "AccountId");

            return ValidationResult;
        }

        /// <summary>
        /// Validate Unique Email Address
        /// </summary>
        /// <param name="emailAddress"></param>
        private async Task ValidateUniqueProductNumber(string productId, string productNumber, string accountId)
        {
            object valueOfProductNumber = GetPropertyValue(productNumber);
            object valueOfAccountId = GetPropertyValue(accountId);
            object valueOfProductId = GetPropertyValue(productId);

            Product product = await _repository.GetProductInformationByProductNumber(valueOfProductNumber.ToString(), (int)valueOfAccountId);

            if (product != null && (int)valueOfProductId == 0)
            {
                AddValidationError(productNumber, "Product Number already exists.");
                return;
            }

            if (product != null && product.ProductId != (int)valueOfProductId)
            {
                AddValidationError(productNumber, "Product Number already exists.");
            }
        }
    }
}