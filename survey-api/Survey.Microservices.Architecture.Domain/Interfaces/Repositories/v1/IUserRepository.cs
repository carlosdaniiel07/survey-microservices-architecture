using Survey.Microservices.Architecture.Domain.Entities.v1;

namespace Survey.Microservices.Architecture.Domain.Interfaces.Repositories.v1
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<bool> AnyByEmailAsync(string email);
    }
}
