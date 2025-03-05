using SandBoxLib.Filters;
using SandBoxLib.Models.RRServices.users;
using UsersApp.Abstractions.Operations.Users;

namespace UsersApp.Config
{
    public static class InitEndpoints
    {
        public static void AddUserEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("api/users").WithTags("users");

            group.MapPost("/", async (ICreateUsersOperation op, UsersCreateRequest request) =>
            {
                return await op.ExecuteAsync(request);
            }).Validate<UsersCreateRequest>();

            group.MapGet("/{id}", async (IGetUserOperation op, int id) =>
            {
                return await op.ExecuteAsync(id);
            });

            group.MapPost("/login", async (IAuthUserOperation op, AuthUserRequest request) =>
            {
                return await op.ExecuteAsync(request);
            }).Validate<AuthUserRequest>();

        }
    }
}
