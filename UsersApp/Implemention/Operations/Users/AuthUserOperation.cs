using AutoMapper;
using SandBoxLib.Implemention.Services;
using SandBoxLib.Models.Exceptions;
using SandBoxLib.Models.RRServices.users;
using UsersApp.Abstractions.Operations.Users;
using UsersApp.Abstractions.Repositories;
using UsersApp.Data.EntityModels;

namespace UsersApp.Implemention.Operations.Users
{
    public class AuthUserOperation : IAuthUserOperation
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        public AuthUserOperation(IUsersRepository usersRepository, 
            IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<UsersGetByIdResponse> ExecuteAsync(AuthUserRequest request)
        {
            var user = await _usersRepository.GetByLoginOrEmailAsync(request.LoginOrEmail);
            string password = Utils.HashSHA512(request.Password);

            if (user.LastAttempt != null)
            {
                var dateDiff = DateTime.Now.ToUniversalTime().Subtract(user.LastAttempt.Value);
                if (dateDiff.Seconds > 3600)
                    user.Attempts = 0;

                if (user.Attempts > 2 && (dateDiff.Seconds < 3600))
                    await ProvideError(user, "Превышено кол-во попыток входа, запись заблокирована на час");
            }

            if (user.Password != password)
                await ProvideError(user, "Не верный пароль");

            if (!user.IsActive)
                await ProvideError(user, "Пользователь заблокирован");

            user.Attempts = 0;
            user.LastLogin = DateTime.Now.ToUniversalTime();
            await _usersRepository.SaveAsync();

            var response = _mapper.Map<UsersGetByIdResponse>(user);
            response.IsSuccess = true;
            return response;
        }

        private async Task ProvideError(eUser user, string message)
        {
            user.Attempts++;
            user.LastAttempt = DateTime.Now.ToUniversalTime();
            await _usersRepository.SaveAsync();
            throw new SandBoxException(message);
        }
    }
}
