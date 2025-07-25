using System.Data;

namespace UserC.Application.Services;

public interface IUnitOfWork
{
    Task SaveChangeAsync();

    IDbTransaction Begin(IsolationLevel level = IsolationLevel.ReadCommitted);
}