using MS.AccountManagement.Data.Models;
using MS.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace MS.AccountManagement.Data
{
    public interface IAccountManagementRepository : IDataRepository, IDisposable
    {
        Task CreateUser(User user);

        Task CreateAccount(Account account);

        Task<User> GetUserByEmailAddress(string emailAddress);

        Task UpdateUser(User user);

        Task UpdateAccount(Account account);

        Task<User> GetUserByUserId(int userId);

        Task<Account> GetAccountInformation(int accountId);
    }
}