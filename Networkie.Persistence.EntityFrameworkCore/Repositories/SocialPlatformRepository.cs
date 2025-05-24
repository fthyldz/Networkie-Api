using Microsoft.EntityFrameworkCore;
using Networkie.Entities;
using Networkie.Persistence.Abstractions.Repositories;
using Networkie.Persistence.EntityFrameworkCore.Contexts;

namespace Networkie.Persistence.EntityFrameworkCore.Repositories;

public class SocialPlatformRepository(IEfCoreDbContext context) : Repository<SocialPlatform>(context), ISocialPlatformRepository
{
}