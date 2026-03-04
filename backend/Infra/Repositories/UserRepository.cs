using App.Interfaces;
using Core.Models;
using Infra.Context;
using Infra.Repositories.Common;

namespace Infra.Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(BackendContext context) : base(context)
    {
    }
}
