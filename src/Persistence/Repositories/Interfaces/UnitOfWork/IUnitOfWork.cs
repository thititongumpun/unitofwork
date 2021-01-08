using System;
using System.Threading.Tasks;

namespace src.Persistence.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
    }
}