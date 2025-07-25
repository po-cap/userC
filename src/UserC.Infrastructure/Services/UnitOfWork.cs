using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using UserC.Application.Services;
using UserC.Infrastructure.Persistence;

namespace UserC.Infrastructure.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _appDbContext;

    public UnitOfWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task SaveChangeAsync()
    {
        await _appDbContext.SaveChangesAsync();
    }

    public IDbTransaction Begin(IsolationLevel level = IsolationLevel.ReadCommitted)
    {
        var tx = _appDbContext.Database.BeginTransaction(level);
        return tx.GetDbTransaction();
    }
}