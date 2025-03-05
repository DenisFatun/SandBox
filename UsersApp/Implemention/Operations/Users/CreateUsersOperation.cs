using AutoMapper;
using SandBoxLib.Models;
using SandBoxLib.Models.RRServices.users;
using UsersApp.Abstractions.Operations.Users;
using UsersApp.Abstractions.Repositories;
using UsersApp.Data.EntityModels;

namespace UsersApp.Implemention.Operations.Users
{
    public class CreateUsersOperation : ICreateUsersOperation
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        public CreateUsersOperation(IUsersRepository usersRepository, 
            IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> ExecuteAsync(UsersCreateRequest request)
        {
            var user = _mapper.Map<eUser>(request);
            await _usersRepository.CreateAsync(user);
            return new BaseResponse { IsSuccess = true, Message = "Пользователь успешно создан" };
        }
    }
}
