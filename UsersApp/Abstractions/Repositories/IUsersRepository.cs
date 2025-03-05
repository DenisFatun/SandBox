using UsersApp.Data.EntityModels;

namespace UsersApp.Abstractions.Repositories
{
    public interface IUsersRepository : IRepository
    {
        Task<eUser> GetAsync(int Id);
        Task<int> CreateAsync(eUser user);
        Task<eUser> GetByLoginOrEmailAsync(string loginOrEmail);
        Task SaveAsync();
    }
}
