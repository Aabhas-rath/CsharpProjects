using System;

namespace Repository.Core
{
    public interface IRepositoryWorker : IDisposable
    {
        int Complete();
    }
}
