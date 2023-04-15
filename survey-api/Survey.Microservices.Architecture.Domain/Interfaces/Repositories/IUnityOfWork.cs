using Survey.Microservices.Architecture.Domain.Interfaces.Repositories.v1;

namespace  Survey.Microservices.Architecture.Domain.Interfaces.Repositories
{
    public interface IUnityOfWork
    {
        IUserRepository UserRepository { get; }
        void Commit();
        Task CommitAsync();
    }
}
