using Microsoft.EntityFrameworkCore;
using Survey.Microservices.Architecture.Domain.Entities.v1;
using Survey.Microservices.Architecture.Domain.Interfaces.Repositories.v1;

namespace  Survey.Microservices.Architecture.Infrastructure.Data.Repositories.v1
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<User> GetByEmailAsync(string email) =>
            await dbSet.FirstOrDefaultAsync(user => user.Email == email);
    }
}
