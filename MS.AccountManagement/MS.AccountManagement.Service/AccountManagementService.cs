using Microsoft.Extensions.Configuration;
using MS.AccountManagement.Data;
using MS.AccountManagement.Data.Models;
using MS.AccountManagement.Data.Transformations;
using MS.Common.Models;
using MS.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MS.AccountManagement.Service
{
    public class AccountManagementService : IAccountManagementService
    {
        private IAccountManagementRepository _repository;

        public IConfiguration configuration { get; }

        private readonly ConnectionStrings _connectionStrings;

        /// <summary>
        /// Acount Business Service
        /// </summary>
        /// <param name="accountManagementDbService"></param>
        public AccountManagementService(IAccountManagementRepository repository, ConnectionStrings connectionStrings)
        {
            _repository = repository;
            _connectionStrings = connectionStrings;
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="accountDataTransformation"></param>
        /// <returns></returns>
        public async Task<ResponseModel<AccountDataTransformation>> Register(AccountDataTransformation accountDataTransformation)
        {
            ResponseModel<AccountDataTransformation> returnResponse = new ResponseModel<AccountDataTransformation>();

            User user = new User();
            Account account = new Account();

            accountDataTransformation.EmailAddress = accountDataTransformation.EmailAddress.ToLower();

            try
            {
                _repository.OpenConnection(_connectionStrings.DefaultConnection);
                _repository.BeginTransaction((int)IsolationLevel.Serializable);

                AccountManagementRules<AccountDataTransformation> userBusinessRules = new AccountManagementRules<AccountDataTransformation>(accountDataTransformation, _repository);
                ValidationResult validationResult = await userBusinessRules.Validate();
                if (validationResult.ValidationStatus == false)
                {
                    _repository.RollbackTransaction();

                    returnResponse.ReturnMessage = validationResult.ValidationMessages;
                    returnResponse.ReturnStatus = validationResult.ValidationStatus;

                    return returnResponse;
                }

                user.FirstName = accountDataTransformation.FirstName;
                user.LastName = accountDataTransformation.LastName;
                user.EmailAddress = accountDataTransformation.EmailAddress;
                user.UserTypeId = UserTypes.Administrator;

                account.Name = accountDataTransformation.CompanyName;

                string salt = Hasher.GetSalt();
                string hashedPassword = Hasher.GenerateHash(accountDataTransformation.Password + salt);

                user.Password = hashedPassword;
                user.PasswordSalt = salt;

                await _repository.CreateAccount(account);

                user.AccountId = account.AccountId;

                await _repository.CreateUser(user);

                await _repository.UpdateDatabase();

                _repository.CommitTransaction();
                returnResponse.ReturnStatus = true;
            }
            catch (Exception ex)
            {
                _repository.RollbackTransaction();
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
            }
            finally
            {
                _repository.CloseConnection();
            }

            accountDataTransformation.UserId = user.UserId;
            accountDataTransformation.AccountId = user.AccountId;
            accountDataTransformation.Password = string.Empty;
            accountDataTransformation.PasswordConfirmation = string.Empty;

            returnResponse.Entity = accountDataTransformation;

            return returnResponse;
        }

        /// <summary>
        /// Login to System
        /// </summary>
        /// <param name="AccountDataTransformation"></param>
        /// <returns></returns>
        public async Task<ResponseModel<AccountDataTransformation>> Login(AccountDataTransformation accountDataTransformation)
        {
            ResponseModel<AccountDataTransformation> returnResponse = new ResponseModel<AccountDataTransformation>();

            User user = new User();
            Account account = new Account();

            try
            {
                _repository.OpenConnection(_connectionStrings.DefaultConnection);

                user = await _repository.GetUserByEmailAddress(accountDataTransformation.EmailAddress.ToLower());
                if (user == null)
                {
                    returnResponse.ReturnStatus = false;
                    returnResponse.ReturnMessage.Add("Login incorrect.");
                    return returnResponse;
                }

                string hashedPassword = Hasher.GenerateHash(accountDataTransformation.Password + user.PasswordSalt);

                if (user.Password != hashedPassword)
                {
                    returnResponse.ReturnStatus = false;
                    returnResponse.ReturnMessage.Add("Login incorrect.");
                    return returnResponse;
                }

                account = await _repository.GetAccountInformation(user.AccountId);
                if (account == null)
                {
                    returnResponse.ReturnStatus = false;
                    returnResponse.ReturnMessage.Add("Could not find an account for user.");
                    return returnResponse;
                }

                returnResponse.ReturnStatus = true;
            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
            }
            finally
            {
                _repository.CloseConnection();
            }

            accountDataTransformation.UserId = user.UserId;
            accountDataTransformation.AccountId = user.AccountId;
            accountDataTransformation.FirstName = user.FirstName;
            accountDataTransformation.LastName = user.LastName;
            accountDataTransformation.EmailAddress = user.EmailAddress;
            accountDataTransformation.CompanyName =
            accountDataTransformation.Password = string.Empty;

            returnResponse.Entity = accountDataTransformation;

            return returnResponse;
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="accountDataTransformation"></param>
        /// <returns></returns>
        public async Task<ResponseModel<AccountDataTransformation>> UpdateUser(AccountDataTransformation accountDataTransformation)
        {
            ResponseModel<AccountDataTransformation> returnResponse = new ResponseModel<AccountDataTransformation>();

            try
            {
                _repository.OpenConnection(_connectionStrings.DefaultConnection);
                _repository.BeginTransaction();

                User user = await _repository.GetUserByUserId(accountDataTransformation.UserId);
                if (user == null)
                {
                    _repository.RollbackTransaction();
                    returnResponse.ReturnStatus = false;
                    returnResponse.ReturnMessage.Add("User not found.");
                    return returnResponse;
                }

                await _repository.UpdateUser(user);

                await _repository.UpdateDatabase();
                _repository.CommitTransaction();

                returnResponse.ReturnStatus = true;
            }
            catch (Exception ex)
            {
                _repository.RollbackTransaction();
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
            }
            finally
            {
                _repository.CloseConnection();
            }

            returnResponse.Entity = accountDataTransformation;

            return returnResponse;
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ResponseModel<User>> UpdateUser(int userId)
        {
            ResponseModel<User> returnResponse = new ResponseModel<User>();

            User user = new User();

            try
            {
                user = await _repository.GetUserByUserId(userId);
                if (user == null)
                {
                    returnResponse.ReturnStatus = false;
                    returnResponse.ReturnMessage.Add("User not found.");
                    return returnResponse;
                }

                await _repository.UpdateUser(user);

                returnResponse.ReturnStatus = true;
            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
            }

            returnResponse.Entity = user;

            return returnResponse;
        }
    }
}
