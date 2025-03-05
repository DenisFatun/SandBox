using Microsoft.EntityFrameworkCore;
using SandBoxLib.Models.Exceptions;
using UsersApp.Abstractions.Repositories;
using UsersApp.Data;
using UsersApp.Data.EntityModels;

namespace UsersApp.Implemention.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly SandBoxDbContext _db;
        private readonly IConfiguration _configuration;
        public UsersRepository(SandBoxDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task<eUser> GetAsync(int Id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if (user == null)
                throw new SandBoxException("Пользователь не найден");
            
            return user;
        }

        public async Task<int> CreateAsync(eUser user)
        {
            var _user = await _db.Users.FirstOrDefaultAsync(x => x.Login == user.Login || x.Email == user.Email);
            if (_user != null)
            {
                if (_user.Login == user.Login)
                    throw new SandBoxException($"Пользователь с логином {user.Login} уже существет");

                if (_user.Email == user.Email)
                    throw new SandBoxException($"Пользователь с эл.почтой {user.Email} уже существет");
            }

            user.Password = _configuration["HashSHA512"] + user.Password;

            user.CreatedDate = DateTime.Now.ToUniversalTime();
            user.Attempts = 0;
            user.IsActive = true;

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user.Id;
        }

        public async Task<eUser> GetByLoginOrEmailAsync(string loginOrEmail)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Login == loginOrEmail || x.Email == loginOrEmail);
            if (user == null)
                throw new SandBoxException("Пользователь не найден");

            return user;
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
