using SandBoxLib.Models.RRServices.users;
using UsersApp.Abstractions.Services;

namespace UsersApp.Abstractions.Operations.Users
{
    public interface IGetUserOperation : IOperation<int, UsersGetByIdResponse>, IService
    {
    }
}
