using SandBoxLib.Models.RRServices.users;
using UsersApp.Abstractions.Operations.Users;
using UsersApp.Abstractions.Repositories;
using AutoMapper;

namespace UsersApp.Implemention.Operations.Users
{
    public class GetUserOperation : IGetUserOperation
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        public GetUserOperation(IUsersRepository usersRepository, 
            IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<UsersGetByIdResponse> ExecuteAsync(int id)
        {
            var user = await  _usersRepository.GetAsync(id);
            var response = _mapper.Map<UsersGetByIdResponse>(user);
            response.IsSuccess = true;
            return response;
        }
    }
}
