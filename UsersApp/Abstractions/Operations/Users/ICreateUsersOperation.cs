using SandBoxLib.Models;
using SandBoxLib.Models.RRServices.users;
using UsersApp.Abstractions.Services;

namespace UsersApp.Abstractions.Operations.Users
{
    public interface ICreateUsersOperation: IOperation<UsersCreateRequest, BaseResponse>, IService
    {
    }
}
