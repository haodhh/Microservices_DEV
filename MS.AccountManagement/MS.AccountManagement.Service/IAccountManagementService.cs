using MS.AccountManagement.Data.Models;
using MS.AccountManagement.Data.Transformations;
using MS.Common.Models;
using System.Threading.Tasks;

namespace MS.AccountManagement.Service
{
    public interface IAccountManagementService
    {
        Task<ResponseModel<AccountDataTransformation>> Register(AccountDataTransformation accountDataTransformation);

        Task<ResponseModel<AccountDataTransformation>> Login(AccountDataTransformation accountDataTransformation);

        Task<ResponseModel<AccountDataTransformation>> UpdateUser(AccountDataTransformation accountDataTransformation);

        Task<ResponseModel<User>> UpdateUser(int userId);
    }
}