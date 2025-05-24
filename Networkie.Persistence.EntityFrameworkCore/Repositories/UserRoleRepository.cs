using Networkie.Entities;
using Networkie.Persistence.Abstractions.Repositories;
using Networkie.Persistence.EntityFrameworkCore.Contexts;

namespace Networkie.Persistence.EntityFrameworkCore.Repositories;

public class UserRoleRepository(IEfCoreDbContext context) : Repository<UserRole>(context), IUserRoleRepository
{
}